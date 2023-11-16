using Xivotec.CleanArchitecture.Domain.ToDoListAggregate.Entities;

namespace Xivotec.CleanArchitecture.Application.ToDoListFeature.Dtos;

/// <summary>
/// DTO representing a <see cref="ToDoItem"/>.
/// </summary>
public record ToDoItemDto
{
    public Guid Id { get; set; }

    public Guid ListId { get; set; }

    public string? Title { get; set; }

    public string? Note { get; set; }

    public DateTime? Reminder { get; set; }

    public bool Done { get; set; }
}