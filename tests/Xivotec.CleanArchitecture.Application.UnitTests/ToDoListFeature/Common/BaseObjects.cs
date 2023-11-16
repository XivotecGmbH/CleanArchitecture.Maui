using Xivotec.CleanArchitecture.Application.ToDoListFeature.Dtos;
using Xivotec.CleanArchitecture.Domain.ToDoListAggregate.Entities;

namespace Xivotec.CleanArchitecture.Application.UnitTests.ToDoListFeature.Common;

public class BaseObjects
{
    protected readonly List<ToDoItem> ToDoItems = new()
    {
        new ToDoItem()
        {
            Id = Guid.NewGuid(),
            Title = "Title 1",
            Note = "Note 1",
            Reminder = DateTime.MinValue,
            Done = false
        },
        new ToDoItem()
        {
            Id = Guid.NewGuid(),
            Title = "Title 2",
            Note = "Note 2",
            Reminder = DateTime.MaxValue,
            Done = true
        }
    };

    protected readonly List<ToDoItemDto> ToDoItemsDto = new()
    {
        new ToDoItemDto()
        {
            Id = Guid.NewGuid(),
            Title = "Title 1",
            Note = "Note 1",
            Reminder = DateTime.MinValue,
            Done = false
        },
        new ToDoItemDto()
        {
            Id = Guid.NewGuid(),
            Title = "Title 2",
            Note = "Note 2",
            Reminder = DateTime.MaxValue,
            Done = true
        }
    };

    protected readonly ToDoList ToDoList;
    protected readonly ToDoListDto ToDoListDto;

    protected BaseObjects()
    {
        ToDoList = new ToDoList { Id = Guid.NewGuid(), Title = "List 1", ToDoItems = ToDoItems };
        ToDoListDto = new ToDoListDto
        {
            Id = ToDoList.Id,
            Title = ToDoList.Title,
            ToDoItems = ToDoItemsDto,
        };
    }
}