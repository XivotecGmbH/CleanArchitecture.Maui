using FluentValidation;
using Xivotec.CleanArchitecture.Application.ToDoListFeature.Commands;

namespace Xivotec.CleanArchitecture.Application.ToDoListFeature.Validation.Commands;

/// <summary>
/// Validator for <see cref="DeleteToDoListCommand"/>.
/// </summary>
public sealed class DeleteToDoListCommandValidator : AbstractValidator<DeleteToDoListCommand>
{
    public DeleteToDoListCommandValidator()
    {
        RuleFor(x => x.Item.Id).NotEmpty();
    }
}