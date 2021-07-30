using Microsoft.EntityFrameworkCore;
using Promax.NetCore.Domain.Entities;
using Promax.NetCore.Domain.Repositories.Contracts;
using Promax.NetCore.Infra.Database.Context;
using Promax.NetCore.Infra.Repositories.Abstraction;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Promax.NetCore.Infra.Repositories
{
    internal class TesteRepository : Repository<Teste>, ITesteRepository
    {
        public TesteRepository(IPromaxContext context) : base(context)
        {
        }

        public async Task<IEnumerable<Teste>> GetAtivosAsync()
        {
            return await Query()
                .Where(r => r.Ativo)
                .ToListAsync();
        }
    }
}
