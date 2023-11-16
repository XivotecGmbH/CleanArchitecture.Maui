using AutoMapper;
using MediatR;
using Microsoft.Extensions.Logging;
using Xivotec.CleanArchitecture.Application.Services.DomainDispatcher;
using Xivotec.CleanArchitecture.Application.ToDoListFeature.Commands;
using Xivotec.CleanArchitecture.Application.ToDoListFeature.Dtos;
using Xivotec.CleanArchitecture.Domain.ToDoListAggregate.Entities;
using Xivotec.CleanArchitecture.Domain.ToDoListAggregate.Events;

namespace Xivotec.CleanArchitecture.Application.ToDoListFeature.EventHandlers;

/// <summary>
/// DomainEvent Handler for <see cref="ToDoListDeletedEvent"/>
/// </summary>
public class TodoListDeletedEventHandler
    : INotificationHandler<DomainEventNotification<ToDoListDeletedEvent>>
{
    private readonly ILogger<TodoListDeletedEventHandler> _logger;
    private readonly IMediator _mediator;
    private readonly IMapper _mapper;


    /// <inheritdoc cref="TodoListDeletedEventHandler"/>
    /// <param name="logger">Logger Service</param>
    /// <param name="mediatR">MediatR Service</param>
    /// <param name="mapper">Mapper Service</param>
    public TodoListDeletedEventHandler(
        ILogger<TodoListDeletedEventHandler> logger,
        IMediator mediatR,
        IMapper mapper)
    {
        _logger = logger;
        _mediator = mediatR;
        _mapper = mapper;
    }

    /// <summary>
    /// Log deleted List and initiate cascading delete of items
    /// </summary>
    /// <param name="notification">MediatR Notification containing DomainEvent</param>
    /// <param name="cancellationToken">Optional cancellationToken</param>
    public async Task Handle(DomainEventNotification<ToDoListDeletedEvent> notification,
        CancellationToken cancellationToken)
    {
        var domainEvent = notification.Value;
        ToDoList toDoList = domainEvent.Value;

        // Request to delete every item in list
        foreach (var todoItem in toDoList.ToDoItems)
        {
            var itemToDelete = _mapper.Map<ToDoItemDto>(todoItem);
            await _mediator.Send(new DeleteToDoItemCommand(itemToDelete), cancellationToken);
        }

        _logger.LogInformation("ToDo List deleted: {itemId}", toDoList.Id);
    }
}
