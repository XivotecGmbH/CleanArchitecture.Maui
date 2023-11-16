namespace Xivotec.CleanArchitecture.Infrastructure.Persistence.Common.Interfaces;

public interface IDataContext : IDisposable
{
    /// <summary>
    /// Saves last transactions to the DataContext
    /// </summary>
    /// <param name="cancellationToken"></param>
    Task<int> SaveChangesAsync(CancellationToken cancellationToken);

    /// <summary>
    /// Commits and saves last transactions to the DataContext
    /// </summary>
    /// <param name="cancellationToken"></param>
    Task<int> CommitAsync(CancellationToken cancellationToken);

    /// <summary>
    /// Reverts the last commit from the DataContext
    /// </summary>
    /// <param name="cancellationToken"></param>
    Task<int> RollbackAsync(CancellationToken cancellationToken);
}
