using Microsoft.Extensions.Configuration;
using System.Reflection;
using Xivotec.CleanArchitecture.Application.Exceptions;

namespace Xivotec.CleanArchitecture.Presentation.Setup;

public static class ConfigurationSetup
{
    private const string AppSettingsFilePath = "Xivotec.CleanArchitecture.Presentation.appsettings.json";

    public static IServiceCollection RegisterConfiguration(this IServiceCollection services)
    {
        var generalConfigStream = Assembly.GetExecutingAssembly()
            .GetManifestResourceStream(AppSettingsFilePath);

        var configBuilder = new ConfigurationBuilder()
            .AddJsonStream(generalConfigStream ??
            throw new InvalidConfigurationException("Unable to load configuration file"));

        IConfiguration configuration = configBuilder.Build();

        services.AddSingleton(configuration);

        return services;
    }
}
