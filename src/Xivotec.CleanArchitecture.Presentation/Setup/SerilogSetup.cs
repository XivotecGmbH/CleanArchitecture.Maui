using Microsoft.Extensions.Configuration;
using Serilog;
using Serilog.Enrichers.ShortTypeName;
using Serilog.Settings.Configuration;

namespace Xivotec.CleanArchitecture.Presentation.Setup;

public static class SerilogSetup
{
    private const string LogDataPath = "logs\\Xivotec.CleanArchitecture.Log.txt";
    private const string LogDataFormat = "[{Timestamp:yyyy-MM-dd HH:mm:ss}] [{Level}] " +
        "({ShortTypeName}) {Message}{NewLine}{Exception} \n";

    public static IServiceCollection RegisterSerilog(this IServiceCollection services)
    {
        var logFilePath = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, LogDataPath);

        // Get IConfiguration service
        var serviceProvider = services.BuildServiceProvider();
        var configService = serviceProvider.GetService<IConfiguration>();

        var options = new ConfigurationReaderOptions(
            typeof(ConsoleLoggerConfigurationExtensions).Assembly);

        // Set Serilog configuration
        Log.Logger = new LoggerConfiguration()
            .ReadFrom.Configuration(configService, options)
            .Enrich.FromLogContext()
            .Enrich.WithShortTypeName()
            .WriteTo.File(
                logFilePath,
                rollingInterval: RollingInterval.Day,
                outputTemplate: LogDataFormat
                )
            .CreateLogger();

        // Add Serilog as logger
        services.AddLogging(logging =>
        {
            logging.AddSerilog(dispose: true);
        });

        return services;
    }
}
