using Promax.NetCore.Domain.Repositories.Abstraction;
using Promax.NetCore.Infra.Database.Context;
using System.Diagnostics.CodeAnalysis;
using System.Threading.Tasks;

namespace Promax.NetCore.Infra.Repositories
{
    [ExcludeFromCodeCoverage]
    internal class UnitOfWork : IUnitOfWork
    {
        private readonly IPromaxContext _context;

        public UnitOfWork(IPromaxContext context)
        {
            _context = context;
        }

        public async Task CommitAsync()
        {
            if (!_context.HasChanges())
                return;

            await _context.SaveChangesAsync();
        }
    }
}
