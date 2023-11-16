using Microsoft.EntityFrameworkCore;
using Xivotec.CleanArchitecture.Application.Common.DependencyInjection;
using Xivotec.CleanArchitecture.Application.Services.PersistenceConfiguration;
using Xivotec.CleanArchitecture.Infrastructure.Persistence.Common.Interfaces;
using Xivotec.CleanArchitecture.Infrastructure.PostgreSQLPort;

namespace Microsoft.Extensions.DependencyInjection;

public static class ConfigureServices
{
    public static IServiceCollection RegisterPostgreSqlPortServices(this IServiceCollection services)
    {
        services
            .RegisterInterfaceImplementationAsSingletons<IPersistentRepository>();

        var serviceProvider = services.BuildServiceProvider();
        var persistenceConfigurationService = serviceProvider.GetRequiredService<IPersistenceConfigurationService>();

        services
            .AddSingleton<IDataContext, PostgresPortDataContext>()
            .AddDbContext<PostgresPortDataContext>(
                options => options.UseNpgsql(persistenceConfigurationService.GetPersistenceConfigurationString()));

        return services;
    }
}
