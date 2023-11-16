using Xivotec.CleanArchitecture.Domain.Common;

namespace Xivotec.CleanArchitecture.Domain.ToDoListAggregate.Entities;

public class ToDoList : Entity
{
    /// <summary>
    /// Title of the list to display
    /// </summary>
    public string? Title { get; set; }

    /// <summary>
    /// Reference list of all included ToDoItem Entities
    /// </summary>
    public virtual List<ToDoItem> ToDoItems { get; set; } = new();
}
