using System.Threading.Tasks;

namespace Promax.NetCore.Domain.Repositories.Abstraction
{
    public interface IUnitOfWork
    {
        Task CommitAsync();
    }
}
