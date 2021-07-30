using Promax.NetCore.Application.Dto;
using Promax.NetCore.Application.Services.Contracts;
using Promax.NetCore.Domain.Repositories.Abstraction;
using Promax.NetCore.Domain.Repositories.Contracts;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Promax.NetCore.Application.Services
{
    internal class TesteApplicationService : ITesteApplicationService
    {
        private readonly ITesteRepository _testeRepository;
        private readonly IUnitOfWork _unitOfWork;

        public TesteApplicationService(ITesteRepository testeRepository,
            IUnitOfWork unitOfWork)
        {
            _testeRepository = testeRepository;
            _unitOfWork = unitOfWork;
        }

        public async Task<IEnumerable<TesteListagemDto>> GetAtivosAsync()
        {
            var testes = await _testeRepository.GetAtivosAsync();

            return testes.Select(teste => new TesteListagemDto
            {
                Nome = teste.Nome,
                Valor = teste.Valor
            });
        }
    }
}
