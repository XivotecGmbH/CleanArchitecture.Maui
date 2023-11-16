namespace Xivotec.CleanArchitecture.Presentation.Services.Navigation;

public sealed class MauiNavigationService : INavigationService
{

    private const int MinimalPagesForBackNavigation = 2;

    /// <inheritdoc cref="INavigationService.NavigateToAsync(string, IDictionary{string, object})"/>
    public async Task NavigateToAsync(string route, IDictionary<string, object> parameters = null)
    {
        // return if navigated to self -> no duplicated pages in page stack
        if (route == Shell.Current.CurrentPage.BindingContext.ToString())
        {
            return;
        }

        // navigate back if new page is previous page
        var navigationStack = Shell.Current.Navigation.NavigationStack;
        if (navigationStack.Count > MinimalPagesForBackNavigation)
        {
            var previousPage = navigationStack[navigationStack.Count - MinimalPagesForBackNavigation];
            var previousRoute = previousPage.BindingContext.GetType().Name;

            if (route == previousRoute)
            {
                await NavigateBackAsync();
                return;
            }
        }

        // navigate to new page
        if (parameters != null)
        {
            await Shell.Current.GoToAsync(route, parameters);
            return;
        }
        await Shell.Current.GoToAsync(route);
    }

    /// <inheritdoc cref="INavigationService.NavigateBackAsync"/>
    public async Task NavigateBackAsync()
        => await Shell.Current.GoToAsync("..");

    /// <inheritdoc cref="INavigationService.ReturnToRootAsync"/>
    public async Task ReturnToRootAsync()
    {
        var stack = Shell.Current.Navigation.NavigationStack;
        while (stack.Count > 1)
        {
            Shell.Current.Navigation.RemovePage(stack[1]);
        }
        await Shell.Current.GoToAsync("..");
    }
}
