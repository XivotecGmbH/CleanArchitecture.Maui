using MediatR;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;
using System.Diagnostics;

namespace Xivotec.CleanArchitecture.Application.Common.Behaviors;

/// <summary>
/// Analyzes pipeline requests and logs long-running ones.
/// </summary>
/// <typeparam name="TRequest">The request.</typeparam>
/// <typeparam name="TResponse">The response.</typeparam>
public class PerformanceBehaviour<TRequest, TResponse> : IPipelineBehavior<TRequest, TResponse> where TRequest : notnull
{
    private const string DefaultPerformanceThreshold = "500";
    private const string LongRunningTaskMessage = "Long Running Request: {Name} ({ElapsedMilliseconds} milliseconds) {@Request}";

    private readonly Stopwatch _timer = new();
    private readonly ILogger<TRequest> _logger;
    private readonly uint _performanceThreshold;

    public PerformanceBehaviour(
        ILogger<TRequest> logger,
        IConfiguration configuration)
    {
        _logger = logger;
        _performanceThreshold = uint.Parse(configuration["PerformanceThreshold"] ?? DefaultPerformanceThreshold);
    }

    public async Task<TResponse> Handle(TRequest request,
        RequestHandlerDelegate<TResponse> next,
        CancellationToken cancellationToken)
    {
        // Track execution time of next operation
        _timer.Start();
        var response = await next();

        _timer.Stop();
        var elapsedMilliseconds = _timer.ElapsedMilliseconds;

        // Log Warning if it took too much time
        if (elapsedMilliseconds > _performanceThreshold)
        {
            var requestName = typeof(TRequest).Name;

            _logger.LogWarning(LongRunningTaskMessage,
                requestName, elapsedMilliseconds, request);
        }

        return response;
    }
}