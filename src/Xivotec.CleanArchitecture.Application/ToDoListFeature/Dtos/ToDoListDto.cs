using Xivotec.CleanArchitecture.Domain.ToDoListAggregate.Entities;

namespace Xivotec.CleanArchitecture.Application.ToDoListFeature.Dtos;

/// <summary>
/// DTO representing a <see cref="ToDoList"/>.
/// </summary>
public record ToDoListDto
{
    public Guid Id { get; set; }
    public string? Title { get; set; }
    public List<ToDoItemDto> ToDoItems { get; set; } = new();
}