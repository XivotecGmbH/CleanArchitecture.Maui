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

[QueryProperty(nameof(SelectedList), nameof(SelectedList))]
public sealed partial class ToDoItemViewModel : ViewModelBase
{
    [ObservableProperty]
    private ToDoListDto _selectedList;

    [ObservableProperty]
    private ToDoListDto _selectedItem;

    [ObservableProperty]
    private ObservableCollection<ToDoItemDto> _toDoItemsCollection = new();

    [ObservableProperty]
    private string _title;

    private readonly IMediator _mediator;

    public ToDoItemViewModel(
        INavigationService navigation,
        IMediator mediator)
        : base(navigation)
    {
        _mediator = mediator;
    }

    [RelayCommand]
    public async Task AddNewItem()
    {
        var newItem = new ToDoItemDto
        {
            Id = Guid.Empty,
            ListId = SelectedList.Id
        };

        var navigateDictionary = new Dictionary<string, object>
        {
            ["SelectedItem"] = newItem
        };

        await Navigation.NavigateToAsync(nameof(ToDoDetailViewModel), navigateDictionary);
    }

    [RelayCommand]
    public async Task ItemTapped(ToDoItemDto selectedItem)
    {
        var navigateDictionary = new Dictionary<string, object>
        {
            ["SelectedItem"] = selectedItem
        };

        await Navigation.NavigateToAsync(nameof(ToDoDetailViewModel), navigateDictionary);
    }

    [RelayCommand]
    public async Task DeleteItem(ToDoItemDto selectedItem)
    {
        await _mediator.Send(new DeleteToDoItemCommand(selectedItem));
        ToDoItemsCollection.Remove(selectedItem);
    }

    [RelayCommand]
    public async Task ItemDoneTapped(ToDoItemDto selectedItem)
    {
        selectedItem.Done = !selectedItem.Done;
        await _mediator.Send(new UpdateToDoItemCommand(selectedItem));
        SortItems();
    }

    public override void OnNavigatedTo()
    {
        base.OnNavigatedTo();

        Title = SelectedList.Title ?? string.Empty;
    }

    public override async Task PageAppearing()
    {
        await base.PageAppearing();

        await RefreshItems();
    }

    private async Task RefreshItems()
    {
        var foundItemList = await _mediator.Send(new GetToDoListByIdQuery(SelectedList.Id));
        ToDoItemsCollection = foundItemList.ToDoItems.ToObservableCollection();
        SortItems();
    }

    private void SortItems()
    {
        // separate List in ticked and un-ticked, sorted by title
        var tickedItems = ToDoItemsCollection
            .Where(x => x.Done)
            .OrderBy(item => item.Title);

        var unTickedItems = ToDoItemsCollection
            .Where(x => !x.Done)
            .OrderBy(item => item.Title);

        // recombine resulting lists
        ToDoItemsCollection = unTickedItems.Concat(tickedItems)
            .ToObservableCollection();
    }
}
