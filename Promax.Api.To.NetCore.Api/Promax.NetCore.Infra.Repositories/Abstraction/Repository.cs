using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.ChangeTracking;
using Promax.NetCore.Domain.Entities.Abstraction;
using Promax.NetCore.Infra.Database.Context;
using System.Diagnostics.CodeAnalysis;
using System.Linq;
using System.Threading.Tasks;

namespace Promax.NetCore.Infra.Repositories.Abstraction
{
    [ExcludeFromCodeCoverage]
    internal abstract class Repository<TEntity> where TEntity : class, IEntity
    {
        private readonly IPromaxContext _context;

        protected Repository(IPromaxContext context)
        {
            _context = context;
            _context.SetCascadeTiming(CascadeTiming.OnSaveChanges);
        }

        public virtual async Task<TEntity> CreateAsync(TEntity entity)
        {
            return (await _context.GetDbSet<TEntity>().AddAsync(entity)).Entity;
        }

        public virtual Task DeleteAsync(TEntity entity)
        {
            _context.GetDbSet<TEntity>().Remove(entity);
            return Task.CompletedTask;
        }

        public virtual async Task<TEntity> GetByIdAsync(int id)
        {
            return await _context.GetDbSet<TEntity>().FindAsync(id);
        }

        public virtual Task UpdateAsync(TEntity entity)
        {
            _context.GetDbSet<TEntity>().Update(entity);
            return Task.CompletedTask;
        }

        protected virtual IQueryable<TEntity> Query()
        {
            return _context.GetDbSet<TEntity>().AsNoTracking().AsQueryable();
        }

        protected virtual IQueryable<TEntity> QueryWithTracking()
        {
            return _context.GetDbSet<TEntity>().AsQueryable();
        }
    }
}
