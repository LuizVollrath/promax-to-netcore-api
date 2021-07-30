using Microsoft.EntityFrameworkCore;

namespace Promax.NetCore.Infra.Database.Mappers
{
    public interface IEntityMapperRegister
    {
        void ExecuteFor<TEntity>(ModelBuilder modelBuilder);
    }
}
