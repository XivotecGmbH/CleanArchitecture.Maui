namespace Xivotec.CleanArchitecture.Presentation.Services.Navigation;

public interface INavigationService
{
    /// <summary>
    /// Navigates to page given by viewmodel name as route.
    /// </summary>
    /// <param name="route">Viewmodel route to be navigated to.</param>
    /// <param name="parameters">Optional params.</param>
    public Task NavigateToAsync(string route, IDictionary<string, object> parameters = null);

    /// <summary>
    /// Navigates back to last visited page.
    /// </summary>
    public Task NavigateBackAsync();

    /// <summary>
    /// Navigates back to current Appshell Basepage
    /// </summary>
    public Task ReturnToRootAsync();
}
