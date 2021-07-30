using Promax.NetCore.Application.Dto;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Promax.NetCore.Application.Services.Contracts
{
    public interface ITesteApplicationService
    {
        Task<IEnumerable<TesteListagemDto>> GetAtivosAsync();
    }
}
