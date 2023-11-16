using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using Xivotec.CleanArchitecture.Presentation.Services.Navigation;

namespace Xivotec.CleanArchitecture.Presentation.ViewModels;

public abstract partial class ViewModelBase : ObservableObject
{
    protected INavigationService Navigation { get; private set; }

    protected ViewModelBase(INavigationService navigation)
    {
        Navigation = navigation;
    }

    /// <summary>
    /// Relay Commands using the MAUI intern ShellContent Navigation Stack for Base Pages.
    /// </summary>
    [RelayCommand]
    public async Task NavigateToHomeAsync()
        => await Navigation.NavigateToAsync("///Home");

    [RelayCommand]
    public async Task NavigateToSettingsAsync()
        => await Navigation.NavigateToAsync("///Settings");

    /// <summary>
    /// Relay Command stub for OnNavigatedTo Event.
    /// </summary>
    [RelayCommand]
    public virtual void OnNavigatedTo() { }

    /// <summary>
    /// Relay Command stub for Appearing Event.
    /// </summary>
    [RelayCommand]
    public virtual async Task PageAppearing()
    {
        await Task.CompletedTask;
    }

    /// <summary>
    /// Relay Command stub for Loaded Event.
    /// </summary>
    [RelayCommand]
    public virtual async Task PageLoaded()
    {
        await Task.CompletedTask;
    }
}
