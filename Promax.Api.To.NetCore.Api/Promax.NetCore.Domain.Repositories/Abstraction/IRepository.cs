using Promax.NetCore.Domain.Entities.Abstraction;
using System.Threading.Tasks;

namespace Promax.NetCore.Domain.Repositories.Abstraction
{
    public interface IRepository<TEntity> where TEntity : class, IEntity
    {
        Task<TEntity> GetByIdAsync(int id);
        Task<TEntity> CreateAsync(TEntity entity);
        Task UpdateAsync(TEntity entity);
        Task DeleteAsync(TEntity entity);
    }
}
