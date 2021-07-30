using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.ChangeTracking;
using Promax.NetCore.Infra.Database.Mappers;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace Promax.NetCore.Infra.Database.Context
{
    internal abstract class CommonContext<TEntity> : BaseContext<TEntity>, ICommonContext
        where TEntity : class
    {
        private const string DefaultSchema = "promax";

        protected CommonContext(DbContextOptions optionsBuilder, string schema = DefaultSchema)
            : base(optionsBuilder, schema)
        {
        }

        protected CommonContext(DbContextOptions optionsBuilder, IEntityMapperRegister entityMapperRegister,
            string schema = DefaultSchema)
            : base(optionsBuilder, entityMapperRegister, schema)
        {
        }

        public IEnumerable<EntityEntry> CheckEntities() => ChangeTracker.Entries();

        public DbSet<T> GetDbSet<T>() where T : class => Set<T>();

        public bool HasChanges() => ChangeTracker.HasChanges();

        public override int SaveChanges()
        {
            OnBeforeSaveChanges();
            return base.SaveChanges(true);
        }

        public override async Task<int> SaveChangesAsync(CancellationToken cancellationToken = default)
        {
            OnBeforeSaveChanges();
            return await base.SaveChangesAsync(true, cancellationToken);
        }

        public void SetCascadeTiming(CascadeTiming cascadeTiming)
        {
            ChangeTracker.CascadeDeleteTiming = cascadeTiming;
            ChangeTracker.DeleteOrphansTiming = cascadeTiming;
        }

        protected abstract void OnBeforeAddEntry(EntityEntry entityEntry);

        protected abstract void OnBeforeEditEntry(EntityEntry entityEntry);

        private void OnBeforeSaveChanges()
        {
            CheckAddedEntries();
            CheckModifiedEntries();
        }

        private void CheckAddedEntries()
        {
            foreach (var entityEntry in ChangeTracker.Entries()
                .Where(v => v.State == EntityState.Added))
            {
                OnBeforeAddEntry(entityEntry);
            }
        }

        private void CheckModifiedEntries()
        {
            foreach (var entry in ChangeTracker.Entries()
                .Where(v => v.State == EntityState.Modified))
            {
                OnBeforeEditEntry(entry);
            }
        }
    }
}
