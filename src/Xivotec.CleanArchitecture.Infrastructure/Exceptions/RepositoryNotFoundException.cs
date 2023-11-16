using Xivotec.CleanArchitecture.Infrastructure.Persistence;

namespace Xivotec.CleanArchitecture.Infrastructure.Exceptions;

/// <summary>
/// Exception used when a repository is not found within the <see cref="UnitOfWork"/>
/// </summary>
public sealed class RepositoryNotFoundException : Exception
{
    public RepositoryNotFoundException()
    {
    }

    public RepositoryNotFoundException(string repositoryType)
        : base($"Repository {repositoryType} for the requested data type was not found.")
    {
    }

    public RepositoryNotFoundException(string repositoryType, Exception exception)
        : base($"Repository {repositoryType} for the requested data type was not found.", exception)
    {
    }
}
