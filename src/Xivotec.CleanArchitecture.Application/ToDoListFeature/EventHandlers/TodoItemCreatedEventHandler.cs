using MediatR;
using Microsoft.Extensions.Logging;
using Xivotec.CleanArchitecture.Application.Services.DomainDispatcher;
using Xivotec.CleanArchitecture.Domain.ToDoListAggregate.Events;

namespace Xivotec.CleanArchitecture.Application.ToDoListFeature.EventHandlers;

/// <summary>
/// DomainEvent Handler for <see cref="ToDoItemCreatedEvent"/>
/// </summary>
public class TodoItemCreatedEventHandler
    : INotificationHandler<DomainEventNotification<ToDoItemCreatedEvent>>
{
    private readonly ILogger<TodoItemCreatedEventHandler> _logger;

    /// <inheritdoc cref="TodoItemCreatedEventHandler"/>
    /// <param name="logger">Logger Service</param>
    public TodoItemCreatedEventHandler(ILogger<TodoItemCreatedEventHandler> logger)
    {
        _logger = logger;
    }

    /// <summary>
    /// Handler method. For now only logs which ToDoItem was added.
    /// </summary>
    /// <param name="notification">MediatR Notification containing DomainEvent</param>
    /// <param name="cancellationToken">Optional cancellationToken</param>
    public Task Handle(DomainEventNotification<ToDoItemCreatedEvent> notification,
        CancellationToken cancellationToken)
    {
        var domainEvent = notification.Value;

        _logger.LogInformation("Item Created: {itemId}", domainEvent.Value.Id);

        return Task.CompletedTask;
    }
}
