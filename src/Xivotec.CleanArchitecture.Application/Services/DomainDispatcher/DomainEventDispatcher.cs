using MediatR;
using Xivotec.CleanArchitecture.Domain.Common.Interfaces;

namespace Xivotec.CleanArchitecture.Application.Services.DomainDispatcher;

/// <inheritdoc cref = "IDomainEventDispatcher" />
public class DomainEventDispatcher : IDomainEventDispatcher
{
    private readonly IMediator _mediator;

    /// <inheritdoc cref="DomainEventDispatcher"/>
    public DomainEventDispatcher(IMediator mediator)
    {
        _mediator = mediator;
    }

    /// <inheritdoc cref="IDomainEventDispatcher.RaiseDomainEvent(IDomainEvent)" />
    public async Task RaiseDomainEvent(IDomainEvent domainEvent)
    {
        var eventType = domainEvent.GetType();
        var notification = Activator.CreateInstance(typeof(DomainEventNotification<>)
            .MakeGenericType(eventType), domainEvent) as INotification;

        await _mediator.Publish(notification!);
    }
}
