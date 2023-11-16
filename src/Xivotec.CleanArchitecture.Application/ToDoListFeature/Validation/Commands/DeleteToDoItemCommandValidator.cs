using FluentValidation;
using Xivotec.CleanArchitecture.Application.ToDoListFeature.Commands;

namespace Xivotec.CleanArchitecture.Application.ToDoListFeature.Validation.Commands;

/// <summary>
/// Validator for <see cref="DeleteToDoItemCommand"/>.
/// </summary>
public class DeleteToDoItemCommandValidator : AbstractValidator<DeleteToDoItemCommand>
{
    public DeleteToDoItemCommandValidator()
    {
        RuleFor(item => item.Item.Id).NotEmpty();
    }
}