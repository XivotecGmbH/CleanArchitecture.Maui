using FluentAssertions;
using Xivotec.CleanArchitecture.Application.Services.DomainDispatcher;
using Xivotec.CleanArchitecture.Application.UnitTests.ToDoListFeature.Common;
using Xivotec.CleanArchitecture.Domain.ToDoListAggregate.Entities;
using Xivotec.CleanArchitecture.Domain.ToDoListAggregate.Events;
using Xunit;

namespace Xivotec.CleanArchitecture.Application.UnitTests.DomainDispatcher;

public class DomainEventNotificationTest : BaseObjects
{
    private DomainEventNotification<ToDoItemCreatedEvent>? _sut;

    [Fact]
    public void DomainEventDispatcher_ShouldCallMethod()
    {
        //Arrange
        var domainItem = new ToDoItem();
        var domainEvent = new ToDoItemCreatedEvent()
        {
            Value = domainItem
        };

        //Act
        _sut = new DomainEventNotification<ToDoItemCreatedEvent>(domainEvent);

        //Assert
        _sut.Value.Should().Be(domainEvent);
    }
}
