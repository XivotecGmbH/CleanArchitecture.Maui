using CommunityToolkit.Maui.Core.Extensions;
using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using MediatR;
using System.Collections.ObjectModel;
using Xivotec.CleanArchitecture.Application.ToDoListFeature.Commands;
using Xivotec.CleanArchitecture.Application.ToDoListFeature.Dtos;
using Xivotec.CleanArchitecture.Application.ToDoListFeature.Queries;
using Xivotec.CleanArchitecture.Presentation.Services.Navigation;

namespace Xivotec.CleanArchitecture.Presentation.ViewModels.ToDoList;

public sealed partial class ToDoListViewModel : ViewModelBase
{
    [ObservableProperty]
    private ObservableCollection<ToDoListDto> _toDoListsCollection;

    [ObservableProperty]
    private ToDoListDto _selectedList;

    [ObservableProperty]
    private string _title;

    [ObservableProperty]
    private bool _buttonActive;

    private readonly IMediator _mediator;

    public ToDoListViewModel(
        INavigationService navigation,
        IMediator mediator)
        : base(navigation)
    {
        _mediator = mediator;

        ButtonActive = false;
        ToDoListsCollection = new ObservableCollection<ToDoListDto>();
    }

    [RelayCommand]
    public async Task AddNewList()
    {
        // redundant as button is only visible if title contains at least one Char.
        if (Title == string.Empty)
        {
            return;
        }

        var newToDoList = new ToDoListDto
        {
            Id = Guid.NewGuid(),
            Title = Title
        };
        await _mediator.Send(new AddToDoListCommand(newToDoList));

        // reset & update UI
        await RefreshList();
        Title = string.Empty;
    }

    [RelayCommand]
    public async Task ListTapped(ToDoListDto selectedList)
    {
        var navigateDictionary = new Dictionary<string, object>
        {
            ["SelectedList"] = selectedList
        };

        await Navigation.NavigateToAsync(
            $"{nameof(ToDoItemViewModel)}?NewList={false}",
            navigateDictionary);
    }

    [RelayCommand]
    public async Task DeleteList(ToDoListDto selectedToDoList)
    {
        await _mediator.Send(new DeleteToDoListCommand(selectedToDoList));
        ToDoListsCollection.Remove(selectedToDoList);
    }

    partial void OnTitleChanged(string value)
        => ButtonActive = value.Length > 0;

    public override async Task PageAppearing()
    {
        await base.PageAppearing();

        await RefreshList();
    }

    private async Task RefreshList()
    {
        var foundToDoLists = await _mediator.Send(new GetToDoListListQuery());
        ToDoListsCollection = foundToDoLists.ToObservableCollection();
    }
}
