using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using MediatR;
using Xivotec.CleanArchitecture.Application.ToDoListFeature.Commands;
using Xivotec.CleanArchitecture.Application.ToDoListFeature.Dtos;
using Xivotec.CleanArchitecture.Presentation.Services.Navigation;

namespace Xivotec.CleanArchitecture.Presentation.ViewModels.ToDoList;

[QueryProperty(nameof(SelectedItem), nameof(SelectedItem))]
public sealed partial class ToDoDetailViewModel : ViewModelBase
{
    [ObservableProperty]
    private ToDoItemDto _selectedItem;

    // Properties for ToDoItem UI fields
    [ObservableProperty]
    private string _note;

    [ObservableProperty]
    private string _title;

    [ObservableProperty]
    private bool _isNewItem;

    private readonly IMediator _mediator;

    public ToDoDetailViewModel(
        INavigationService navigation,
        IMediator mediator)
        : base(navigation)
    {
        _mediator = mediator;
    }

    [RelayCommand]
    public async Task SaveItem()
    {
        SelectedItem.Id = Guid.NewGuid();
        SelectedItem.Title = Title;
        SelectedItem.Note = Note;

        await _mediator.Send(new AddToDoItemCommand(SelectedItem));
        await Navigation.NavigateBackAsync();
    }

    [RelayCommand]
    public async Task UpdateItem()
    {
        SelectedItem.Title = Title;
        SelectedItem.Note = Note;

        await _mediator.Send(new UpdateToDoItemCommand(SelectedItem));
        await Navigation.NavigateBackAsync();
    }

    [RelayCommand]
    public async Task DeleteItem()
    {
        await _mediator.Send(new DeleteToDoItemCommand(SelectedItem));
        await Navigation.NavigateBackAsync();
    }

    [RelayCommand]
    public async Task CancelItem()
    {
        await Navigation.NavigateBackAsync();
    }

    public override void OnNavigatedTo()
    {
        base.OnNavigatedTo();

        Title = SelectedItem.Title ?? string.Empty;
        Note = SelectedItem.Note ?? string.Empty;

        IsNewItem = false;

        if (SelectedItem.Id.Equals(Guid.Empty))
        {
            IsNewItem = true;
        }
    }
}
