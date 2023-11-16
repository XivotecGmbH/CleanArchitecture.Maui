using CommunityToolkit.Mvvm.Input;
using Xivotec.CleanArchitecture.Presentation.Services.Navigation;
using Xivotec.CleanArchitecture.Presentation.ViewModels.ToDoList;

namespace Xivotec.CleanArchitecture.Presentation.ViewModels.Controls;

public partial class MainMenuViewModel : ViewModelBase
{
    public MainMenuViewModel(INavigationService navigation) : base(navigation)
    {
    }

    [RelayCommand]
    public async Task NavigateToToDoListAsync()
        => await Navigation.NavigateToAsync(nameof(ToDoListViewModel));
}
