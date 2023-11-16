namespace Xivotec.CleanArchitecture.Application.Exceptions;

public sealed class RepositoryException : Exception
{
    public RepositoryException()
    {
    }

    public RepositoryException(string operation)
        : base($"Repository operation {operation} failed")
    {
    }

    public RepositoryException(string operation, Exception exception)
        : base($"Repository operation {operation} failed", exception)
    {
    }
}