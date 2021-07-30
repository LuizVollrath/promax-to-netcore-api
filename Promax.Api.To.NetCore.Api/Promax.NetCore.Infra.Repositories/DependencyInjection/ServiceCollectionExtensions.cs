using Microsoft.Extensions.DependencyInjection;
using Promax.NetCore.Domain.Repositories.Abstraction;
using Promax.NetCore.Domain.Repositories.Contracts;
using Promax.NetCore.Infra.Database.DependencyInjection;
using System.Diagnostics.CodeAnalysis;

namespace Promax.NetCore.Infra.Repositories.DependencyInjection
{
    [ExcludeFromCodeCoverage]
    public static class ServiceCollectionExtensions
    {
        public static IServiceCollection AddSqlServerPersistence(this IServiceCollection serviceCollection)
        {
            serviceCollection.AddPromaxContextSqlServer();
            serviceCollection.AddRepositoriesImplementation();

            return serviceCollection;
        }

        public static IServiceCollection AddSqlServerPersistenceInMemory(this IServiceCollection serviceCollection)
        {
            serviceCollection.AddPromaxContextSqlServerInMemory();
            serviceCollection.AddRepositoriesImplementation();

            return serviceCollection;
        }

        private static void AddRepositoriesImplementation(this IServiceCollection serviceCollection)
        {
            serviceCollection.AddScoped<IUnitOfWork, UnitOfWork>();

            serviceCollection.AddScoped<ITesteRepository, TesteRepository>();
        }
    }
}
