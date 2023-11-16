using MediatR.Pipeline;
using Microsoft.Extensions.Logging;

namespace Xivotec.CleanArchitecture.Application.Common.Behaviors;

/// <summary>
/// Logs pipeline requests.
/// </summary>
/// <typeparam name="TRequest">The request.</typeparam>
public class LoggingBehaviour<TRequest> : IRequestPreProcessor<TRequest> where TRequest : notnull
{
    private readonly ILogger<TRequest> _logger;

    public LoggingBehaviour(ILogger<TRequest> logger)
    {
        _logger = logger;
    }

    public async Task Process(TRequest request, CancellationToken cancellationToken)
    {
        var requestName = typeof(TRequest).Name;

        _logger.LogInformation("Request: {Name} {@Request}", requestName, request);
        await Task.CompletedTask;
    }
}