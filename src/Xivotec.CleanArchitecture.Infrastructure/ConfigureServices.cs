using Xivotec.CleanArchitecture.Application.Common.Persistence.Interfaces;
using Xivotec.CleanArchitecture.Application.Services.PersistenceConfiguration;
using Xivotec.CleanArchitecture.Application.Services.Time;
using Xivotec.CleanArchitecture.Infrastructure.Persistence;
using Xivotec.CleanArchitecture.Infrastructure.Services.PersistenceConfiguration;
using Xivotec.CleanArchitecture.Infrastructure.Services.SystemClock;

namespace Microsoft.Extensions.DependencyInjection;

public static class ConfigureServices
{
    public static IServiceCollection RegisterInfrastructureServices(this IServiceCollection services)
    {
        services.AddSingleton<IUnitOfWork, UnitOfWork>();
        services.AddSingleton<IPersistenceConfigurationService, PersistenceConfigurationService>();
        services.AddSingleton<ISystemClockService, SystemClockService>();

        return services;
    }
}