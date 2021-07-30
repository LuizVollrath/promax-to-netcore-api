using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.ChangeTracking;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;

namespace Promax.NetCore.Infra.Database.Context
{
    public interface ICommonContext
    {
        IEnumerable<EntityEntry> CheckEntities();

        DbSet<T> GetDbSet<T>() where T : class;

        bool HasChanges();

        int SaveChanges();

        Task<int> SaveChangesAsync(CancellationToken cancellationToken = default);

        void SetCascadeTiming(CascadeTiming cascadeTiming);
    }
}
