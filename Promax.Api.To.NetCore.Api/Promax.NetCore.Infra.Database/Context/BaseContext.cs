using Microsoft.EntityFrameworkCore;
using Promax.NetCore.Infra.Database.Mappers;

namespace Promax.NetCore.Infra.Database.Context
{
    internal abstract class BaseContext : DbContext
    {
        protected string Schema { get; }

        protected BaseContext(DbContextOptions optionsBuilder, string schema) : base(optionsBuilder)
        {
            Schema = schema;
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.HasDefaultSchema(Schema);
            base.OnModelCreating(modelBuilder);
        }
    }

    internal abstract class BaseContext<TEntity> : BaseContext
        where TEntity : class
    {
        private readonly IEntityMapperRegister _entityMapperRegister;

        protected BaseContext(DbContextOptions optionsBuilder, string schema)
            : base(optionsBuilder, schema)
        {
        }

        protected BaseContext(DbContextOptions optionsBuilder, IEntityMapperRegister entityMapperRegister, string schema)
            : base(optionsBuilder, schema)
        {
            _entityMapperRegister = entityMapperRegister;
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            _entityMapperRegister?.ExecuteFor<TEntity>(modelBuilder);
        }
    }
}
