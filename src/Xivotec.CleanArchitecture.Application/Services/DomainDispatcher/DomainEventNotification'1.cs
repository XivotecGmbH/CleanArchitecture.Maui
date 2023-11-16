using MediatR;
using Xivotec.CleanArchitecture.Domain.Common.Interfaces;

namespace Xivotec.CleanArchitecture.Application.Services.DomainDispatcher;

/// <summary>
/// Wrapper for <see cref="IDomainEvent"/>, defined inside Domain, to work with 
/// <see cref="INotification"/> from MediatR.
/// </summary>
public sealed class DomainEventNotification<TDomainEvent> : INotification
    where TDomainEvent : IDomainEvent
{
    /// <summary>
    /// <see cref="IDomainEvent"/> inside the Wrapper.
    /// </summary>
    public TDomainEvent Value { get; }

    /// <inheritdoc cref="DomainEventNotification{TDomainEvent}"/>
    /// <param name="value"><see cref="IDomainEvent"/> to be wrapped.</param>
    public DomainEventNotification(TDomainEvent value)
    {
        Value = value;
    }
}