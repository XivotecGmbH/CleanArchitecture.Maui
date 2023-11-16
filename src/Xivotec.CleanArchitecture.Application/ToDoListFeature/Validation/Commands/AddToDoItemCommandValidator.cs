using FluentValidation;
using Xivotec.CleanArchitecture.Application.ToDoListFeature.Commands;

namespace Xivotec.CleanArchitecture.Application.ToDoListFeature.Validation.Commands;

/// <summary>
/// Validator for <see cref="AddToDoItemCommand"/>.
/// </summary>
public class AddToDoItemCommandValidator : AbstractValidator<AddToDoItemCommand>
{
    public AddToDoItemCommandValidator()
    {
        RuleFor(item => item.Item.Id).NotEmpty();
        RuleFor(item => item.Item.Title).NotEmpty();
    }
}