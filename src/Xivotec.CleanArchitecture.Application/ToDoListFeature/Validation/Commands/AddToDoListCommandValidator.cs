using FluentValidation;
using Xivotec.CleanArchitecture.Application.ToDoListFeature.Commands;

namespace Xivotec.CleanArchitecture.Application.ToDoListFeature.Validation.Commands;

/// <summary>
/// Validator for <see cref="AddToDoListCommand"/>.
/// </summary>
public sealed class AddToDoListCommandValidator : AbstractValidator<AddToDoListCommand>
{
    public AddToDoListCommandValidator()
    {
        RuleFor(x => x.Item.Id).NotEmpty();
        RuleFor(x => x.Item.Title).NotEmpty();
        RuleFor(x => x.Item.Title).MaximumLength(100);
    }
}