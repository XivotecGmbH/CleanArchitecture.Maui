using System.ComponentModel;
using Xivotec.CleanArchitecture.Presentation.Services.Navigation;
using Xivotec.CleanArchitecture.Presentation.Views.Controls;
using Xivotec.CleanArchitecture.Presentation.Views.Pages;

namespace Microsoft.Extensions.DependencyInjection;

public static class ConfigureServices
{
    private const int NameRemoveCount = 4;

    /// <summary>
    /// Extension method. Registers every available View and ViewModel using type deduction and naming convention.
    /// </summary>
    public static IServiceCollection RegisterPresentationModels(this IServiceCollection services)
    {
        // Get the assembly where your views and view models are located
        var assembly = typeof(MainPage).Assembly;

        // Register all views and view models using reflection
        foreach (var type in assembly.GetTypes())
        {
            // Check if the type is a view
            if (type.BaseType == typeof(HeaderFooterPage))
            {
                services.AddSingleton(type);
            }

            // Check if the type is a view model
            if (type.Name.EndsWith("ViewModel") && type.GetInterfaces()
                .Contains(typeof(INotifyPropertyChanged)))
            {
                services.AddSingleton(type);
            }
        }

        return services;
    }

    /// <summary>
    /// Extension method. Registers every available navigation route using naming conventions
    /// </summary>
    public static IServiceCollection RegisterRoutes(this IServiceCollection services)
    {
        foreach (var (routeName, pageType) in
        // Routes are naming convention based.
        from cPage in services.Where(type => type.ServiceType.BaseType == typeof(HeaderFooterPage))
        let nameOfPage = cPage.ServiceType.Name
        let routeName = nameOfPage.Remove(nameOfPage.Length - NameRemoveCount, NameRemoveCount) + "ViewModel"
        let pageType = cPage.ServiceType
        select (routeName, pageType))
        {
            Routing.RegisterRoute(routeName, pageType);
        }
        return services;
    }

    public static IServiceCollection RegisterPresentationServices(this IServiceCollection services)
    {
        services.AddSingleton<INavigationService, MauiNavigationService>();

        return services;
    }
}
