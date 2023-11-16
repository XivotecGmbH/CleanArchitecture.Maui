using MediatR;
using NSubstitute;
using Xivotec.CleanArchitecture.Application.Services.DomainDispatcher;
using Xivotec.CleanArchitecture.Application.UnitTests.ToDoListFeature.Common;
using Xivotec.CleanArchitecture.Domain.Common.Interfaces;
using Xivotec.CleanArchitecture.Domain.ToDoListAggregate.Events;
using Xunit;

namespace Xivotec.CleanArchitecture.Application.UnitTests.DomainDispatcher;

public class DomainEventDispatcherTest : BaseObjects
{
    private readonly IDomainEventDispatcher _sut;
    private readonly IMediator _mediator = Substitute.For<IMediator>();
    private IDomainEvent? _domainEvent;

    public DomainEventDispatcherTest()
    {
        _sut = new DomainEventDispatcher(_mediator);
    }


    [Fact]
    public async Task DomainEventDispatcher_ShouldCallMethod()
    {
        //Arrange
        _domainEvent = Substitute.For<ToDoItemCreatedEvent>();

        //Act
        await _sut.RaiseDomainEvent(_domainEvent);

        //Assert
        _ = _domainEvent.Received(1).GetType();
        await _mediator.Received(1).Publish(Arg.Any<INotification>());
    }
}
