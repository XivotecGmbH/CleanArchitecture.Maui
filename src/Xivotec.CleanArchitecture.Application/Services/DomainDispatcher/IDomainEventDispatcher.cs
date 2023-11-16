using Xivotec.CleanArchitecture.Domain.Common.Interfaces;

namespace Xivotec.CleanArchitecture.Application.Services.DomainDispatcher;

/// <summary>
/// Service for raising Domain Events
/// </summary>
public interface IDomainEventDispatcher
{
    /// <summary>
    /// Raises a new DomainEventNotification using MediatR
    /// </summary>
    /// <param name="domainEvent"><see cref="IDomainEvent"/> to publish</param>
    Task RaiseDomainEvent(IDomainEvent domainEvent);
}