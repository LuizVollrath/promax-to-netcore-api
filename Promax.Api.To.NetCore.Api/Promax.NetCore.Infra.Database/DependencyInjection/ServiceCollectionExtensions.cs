using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Diagnostics;
using Microsoft.EntityFrameworkCore.Storage;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Promax.NetCore.Domain.Entities.Abstraction;
using Promax.NetCore.Infra.Database.Configuration;
using Promax.NetCore.Infra.Database.Context;
using Promax.NetCore.Infra.Database.Mappers;
using Promax.NetCore.Infra.Database.Mappers.Abstraction;

namespace Promax.NetCore.Infra.Database.DependencyInjection
{
    public static class ServiceCollectionExtensions
    {
        private const string DefaultAppSettingsFile = "appsettings.json";

        public static IServiceCollection AddPromaxContextSqlServer(this IServiceCollection serviceCollection)
        {
            serviceCollection.AddDatabaseSettings();
            serviceCollection.AddSqlServerDbContext<IPromaxContext, PromaxContext>();
            serviceCollection.AddSqlServerEntityMapperRegister<EntityMapper<IEntity>>();

            return serviceCollection;
        }

        public static IServiceCollection AddPromaxContextSqlServerInMemory(this IServiceCollection serviceCollection)
        {
            serviceCollection.AddSqlServerDbContextInMemory<IPromaxContext, PromaxContext>();
            serviceCollection.AddSqlServerEntityMapperRegister<EntityMapper<IEntity>>();

            return serviceCollection;
        }

        private static IServiceCollection AddSqlServerDbContext<TContextService, TContextImplementation>(
            this IServiceCollection serviceCollection) where TContextImplementation : DbContext, TContextService
        {
            var connectionsStrings = serviceCollection.BuildServiceProvider().GetService<IConnectionStrings>();

            serviceCollection.AddEntityFrameworkProxies();
            serviceCollection.AddDbContext<TContextService, TContextImplementation>(options =>
                SetOptionsBuilderForSqlServer(options, connectionsStrings));

            return serviceCollection;
        }

        private static IServiceCollection AddSqlServerDbContextInMemory<TContextService, TContextImplementation>(
            this IServiceCollection serviceCollection) where TContextImplementation : DbContext, TContextService
        {
            serviceCollection.AddEntityFrameworkProxies();
            serviceCollection.AddDbContext<TContextService, TContextImplementation>(SetOptionsBuilderForUseInMemory);

            return serviceCollection;
        }

        private static IServiceCollection AddSqlServerEntityMapperRegister<TEntityBaseMapper>(
            this IServiceCollection serviceCollection)
            where TEntityBaseMapper : class, IEntityMapper
        {
            serviceCollection.AddSingleton<IEntityMapperRegister, EntityMapperRegister<TEntityBaseMapper>>();

            return serviceCollection;
        }

        private static void SetOptionsBuilderForSqlServer(DbContextOptionsBuilder optionsBuilder,
            IConnectionStrings connectionStrings)
        {
            optionsBuilder
                .UseLazyLoadingProxies()
                .ConfigureWarnings(warningBuilder => warningBuilder.Ignore(CoreEventId.DetachedLazyLoadingWarning))
                .UseSqlServer(connectionStrings.DbConnection);
        }

        private static void SetOptionsBuilderForUseInMemory(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder
                .ConfigureWarnings(warningBuilder => warningBuilder.Ignore(CoreEventId.DetachedLazyLoadingWarning, CoreEventId.ManyServiceProvidersCreatedWarning))
                .UseLazyLoadingProxies()
                .UseInMemoryDatabase("DefaultDatabase", new InMemoryDatabaseRoot());
        }

        private static IServiceCollection AddDatabaseSettings(this IServiceCollection serviceCollection,
            string appSettingsFile = DefaultAppSettingsFile)
        {
            serviceCollection.AddSingleton<IConnectionStrings>(GetDatabaseSettings(appSettingsFile).ConnectionStrings);
            return serviceCollection;
        }

        private static DatabaseSettings GetDatabaseSettings(string fileName)
        {
            return new ConfigurationBuilder()
                .AddJsonFile(fileName)
                .AddEnvironmentVariables()
                .Build()
                .Get<DatabaseSettings>();
        }
    }
}
