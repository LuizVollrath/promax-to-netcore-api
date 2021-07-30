using Microsoft.Extensions.DependencyInjection;
using Promax.NetCore.Application.Services;
using Promax.NetCore.Application.Services.Contracts;
using System.Diagnostics.CodeAnalysis;

namespace Promax.NetCore.Application.DependencyInjection
{
    [ExcludeFromCodeCoverage]
    public static class ServiceCollectionExtensions
    {
        public static IServiceCollection AddPromaxApplicationServices(this IServiceCollection serviceCollection)
        {
            serviceCollection.AddScoped<ITesteApplicationService, TesteApplicationService>();

            return serviceCollection;
        }
    }
}
