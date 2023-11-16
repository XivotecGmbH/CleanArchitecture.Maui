using FluentValidation;
using Xivotec.CleanArchitecture.Application.ToDoListFeature.Commands;

namespace Xivotec.CleanArchitecture.Application.ToDoListFeature.Validation.Commands;

/// <summary>
/// Validator for <see cref="UpdateToDoItemCommand"/>.
/// </summary>
public class UpdateToDoItemCommandValidator : AbstractValidator<UpdateToDoItemCommand>
{
    public UpdateToDoItemCommandValidator()
    {
        RuleFor(item => item.Item.Id).NotEmpty();
        RuleFor(item => item.Item.Title).NotEmpty();
    }
}