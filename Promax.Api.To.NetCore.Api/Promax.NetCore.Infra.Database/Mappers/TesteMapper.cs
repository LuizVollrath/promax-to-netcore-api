using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Promax.NetCore.Domain.Entities;
using Promax.NetCore.Infra.Database.Mappers.Abstraction;

namespace Promax.NetCore.Infra.Database.Mappers
{
    internal class TesteMapper : EntityMapper<Teste>
    {
        public override void MapEntity(EntityTypeBuilder entityTypeBuilder)
        {
            base.MapEntity(entityTypeBuilder);

            entityTypeBuilder.Property(nameof(Teste.Nome))
                .IsRequired();
        }
    }
}
