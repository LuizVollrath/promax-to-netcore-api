using Promax.NetCore.Domain.Entities;
using Promax.NetCore.Domain.Repositories.Abstraction;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Promax.NetCore.Domain.Repositories.Contracts
{
    public interface ITesteRepository : IRepository<Teste>
    {
        Task<IEnumerable<Teste>> GetAtivosAsync();
    }
}
