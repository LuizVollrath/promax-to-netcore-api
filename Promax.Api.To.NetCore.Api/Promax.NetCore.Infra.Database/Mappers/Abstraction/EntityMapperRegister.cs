using Microsoft.EntityFrameworkCore;
using System;
using System.Linq;

namespace Promax.NetCore.Infra.Database.Mappers
{
    internal class EntityMapperRegister<TEntityBaseMapper> : IEntityMapperRegister
        where TEntityBaseMapper : class, IEntityMapper
    {
        public void ExecuteFor<TEntity>(ModelBuilder modelBuilder)
        {
            var mapperInterfaceType = typeof(IEntityMapper);
            var mapperBaseType = typeof(TEntityBaseMapper);

            var mapperTypes = mapperBaseType.Assembly.GetTypes()
                .Where(type => !type.IsAbstract && !type.IsInterface && mapperInterfaceType.IsAssignableFrom(type))
                .ToArray();

            var entityInterface = typeof(TEntity);
            var entityTypes = entityInterface.Assembly.GetTypes()
                .Where(type => !type.IsInterface && entityInterface.IsAssignableFrom(type) && !type.IsAbstract);

            var entityMethod = typeof(ModelBuilder).GetMethods().Single(x =>
                x.Name == "Entity" && x.IsGenericMethod && x.ReturnType.Name == "EntityTypeBuilder`1");

            foreach (var entityType in entityTypes)
            {
                var entityMapperType = mapperTypes.FirstOrDefault(m => m.Name == $"{entityType.Name}Mapper");
                if (entityMapperType == default)
                {
                    throw new EntityNotMappedException(entityType.FullName);
                }

                // Get type of entity to be mapped
                var genericTypeArg = entityMapperType.GetInterfaces().Single(i => i.IsGenericType).GenericTypeArguments.Single();

                // Get method builder.Entity<TEntity>
                var genericEntityMethod = entityMethod.MakeGenericMethod(genericTypeArg);

                // Invoke builder.Entity<TEntity> to get a builder for the entity to be mapped
                var entityBuilder = genericEntityMethod.Invoke(modelBuilder, null);

                var mapper = Activator.CreateInstance(entityMapperType);
                mapper.GetType().GetMethod("MapEntity")?.Invoke(mapper, new[] { entityBuilder });
            }
        }
    }
}
