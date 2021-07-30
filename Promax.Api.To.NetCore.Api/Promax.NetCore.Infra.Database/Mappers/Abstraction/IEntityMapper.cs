using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Promax.NetCore.Infra.Database.Mappers
{
    public interface IEntityMapper
    {
        void MapEntity(EntityTypeBuilder entityTypeBuilder);
    }

    public interface IEntityMapper<TEntity> : IEntityMapper where TEntity : class
    {
    }
}
