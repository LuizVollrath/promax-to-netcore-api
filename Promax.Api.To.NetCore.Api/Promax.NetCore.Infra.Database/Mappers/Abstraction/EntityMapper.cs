using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Promax.NetCore.Domain.Entities.Abstraction;
using System;
using System.Diagnostics.CodeAnalysis;

namespace Promax.NetCore.Infra.Database.Mappers.Abstraction
{
    [ExcludeFromCodeCoverage]
    internal abstract class EntityMapper<T> : IEntityMapper<T> where T : class, IEntity
    {
        public virtual void MapEntity(EntityTypeBuilder entityTypeBuilder)
        {
            var type = typeof(T);
            entityTypeBuilder.ToTable(type.Name);
            entityTypeBuilder.Property(nameof(IEntity.CreatedAt)).HasDefaultValueSql("SYSUTCDATETIME()");
            MapDefaultStringProperties(entityTypeBuilder);
        }

        private static void MapDefaultStringProperties(EntityTypeBuilder entityTypeBuilder)
        {
            if (entityTypeBuilder == null)
                throw new ArgumentNullException(nameof(entityTypeBuilder));

            var type = typeof(T);
            foreach (var property in type.GetProperties())
            {
                if (property.PropertyType == typeof(string)) 
                {
                    var builder = entityTypeBuilder.Property(property.Name);
                    builder.StringDefaultMaxLength();
                    builder.IsUnicode(false);
                }
                if (property.PropertyType == typeof(decimal))
                {
                    var builder = entityTypeBuilder.Property(property.Name);
                    builder.DecimalDefaultType();
                }
            }
        }
    }
}
