using Xivotec.CleanArchitecture.Domain.Common;
using Xivotec.CleanArchitecture.Domain.ToDoListAggregate.Entities;

namespace Xivotec.CleanArchitecture.Domain.ToDoListAggregate.Events;

public sealed class ToDoListDeletedEvent : DomainEvent<ToDoList>
{
}
