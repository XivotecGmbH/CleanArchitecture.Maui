using Xivotec.CleanArchitecture.Domain.Common;

namespace Xivotec.CleanArchitecture.Application.Common.Persistence.Interfaces;

/// <summary>
/// UnitOfWork. Manages all interactions between Database and Repositories
/// </summary>
public interface IUnitOfWork : IDisposable
{
    /// <summary>
    /// Returns Repository specified by TEntity with ID Type Guid/>
    /// </summary>
    /// <typeparam name="TEntity">Entity Type the repository contains.</typeparam>
    public IRepository<TEntity, Guid> GetRepository<TEntity>() where TEntity : Entity;

    /// <summary>
    /// Returns Repository specified by TEntity with ID Type TId />
    /// </summary>
    /// <typeparam name="TEntity">Entity Type the repository contains.</typeparam>
    /// <typeparam name="TId">ID Type the repository uses.</typeparam>
    public IRepository<TEntity, TId> GetRepository<TEntity, TId>() where TEntity : Entity<TId>;

    /// <summary>
    /// Commits changes in the dbcontext
    /// </summary>
    /// <param name="cancellationToken">optional</param>
    public Task CommitAsync(CancellationToken cancellationToken = default);

    /// <summary>
    /// Rolls back every change since the last <see cref="IUnitOfWork.SaveAsync"/>.
    /// </summary>
    public Task RollbackAsync(CancellationToken cancellationToken = default);

    /// <summary>
    /// Saves all changes since the last save into the database.
    /// </summary>
    public Task SaveAsync(CancellationToken cancellationToken = default);
}