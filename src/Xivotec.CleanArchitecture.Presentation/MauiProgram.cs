using CommunityToolkit.Maui;
using Xivotec.CleanArchitecture.Presentation.Setup;

namespace Xivotec.CleanArchitecture.Presentation;

public static class MauiProgram
{
    public static MauiApp CreateMauiApp()
    {
        var builder = MauiApp.CreateBuilder();
        builder
            .UseMauiApp<App>()
            .ConfigureFonts(fonts =>
            {
                fonts.AddFont("OpenSans-Regular.ttf", "OpenSansRegular");
            })
            .UseMauiCommunityToolkit();

        builder.Services
            .RegisterConfiguration()
            .RegisterSerilog()
            .AddCoreServices()
            .RegisterPresentationModels()
            .RegisterPresentationServices()
            .RegisterInfrastructureServices()
            .RegisterPostgreSqlPortServices()
            .RegisterRoutes();

        return builder.Build();
    }
}