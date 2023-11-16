using MediatR;
using Microsoft.Extensions.Logging;

namespace Xivotec.CleanArchitecture.Application.Common.Behaviors;

/// <summary>
/// Defines behavior for unhandled exceptions in pipeline requests.
/// </summary>
/// <typeparam name="TRequest">The request.</typeparam>
/// <typeparam name="TResponse">The response.</typeparam>
public class UnhandledExceptionBehaviour<TRequest, TResponse> : IPipelineBehavior<TRequest, TResponse> where TRequest : notnull
{
    private readonly ILogger<TRequest> _logger;

    public UnhandledExceptionBehaviour(ILogger<TRequest> logger)
    {
        _logger = logger;
    }

    public async Task<TResponse> Handle(TRequest request, RequestHandlerDelegate<TResponse> next, CancellationToken cancellationToken)
    {
        try
        {
            // check next operation for potential unhandled exceptions
            return await next();
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "Unhandled Exception for Request {@Request}", request);
            throw;
        }
    }
}