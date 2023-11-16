using FluentValidation;
using MediatR;
using System.Text;
using ValidationException = FluentValidation.ValidationException;

namespace Xivotec.CleanArchitecture.Application.Common.Behaviors;

/// <summary>
/// Defines validation behavior for mediator requests and responses.
/// </summary>
/// <typeparam name="TRequest">Request type.</typeparam>
/// <typeparam name="TResponse">Response type.</typeparam>
public class ValidationBehavior<TRequest, TResponse> : IPipelineBehavior<TRequest, TResponse> where TRequest : notnull
{
    private readonly IEnumerable<IValidator<TRequest>> _validators;

    public ValidationBehavior(IEnumerable<IValidator<TRequest>> validators)
    {
        _validators = validators;
    }

    public async Task<TResponse> Handle(TRequest request,
        RequestHandlerDelegate<TResponse> next,
        CancellationToken cancellationToken)
    {
        if (!_validators.Any())
        {
            return await next();
        }

        var context = new ValidationContext<TRequest>(request);
        var validationResults = await Task.WhenAll(
            _validators.Select(v =>
                v.ValidateAsync(context, cancellationToken)));

        var failures = validationResults.
            Where(r => r.Errors.Any())
            .SelectMany(r => r.Errors)
            .ToList();

        if (failures.Any())
        {
            var message = new StringBuilder();
            failures.ForEach(item => message
                .Append(item.ErrorMessage)
                .AppendLine());

            throw new ValidationException(failures);
        }

        return await next();
    }
}