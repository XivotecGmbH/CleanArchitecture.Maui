using Xivotec.CleanArchitecture.Application.Common.Persistence.Interfaces;
using Xivotec.CleanArchitecture.Domain.Common;
using Xivotec.CleanArchitecture.Infrastructure.Exceptions;
using Xivotec.CleanArchitecture.Infrastructure.Persistence.Common.Interfaces;

namespace Xivotec.CleanArchitecture.Infrastructure.Persistence;

/// <inheritdoc cref="IUnitOfWork"/>
public sealed class UnitOfWork : IUnitOfWork
{
    private readonly IDataContext _dataContext;

    private readonly Dictionary<Type, object> _repositories = new();

    public UnitOfWork(
        IDataContext dataContext,
        IEnumerable<IPersistentRepository> persistentRepositories,
        IEnumerable<IRuntimeRepository> runTimeRepositories
        )
    {
        _dataContext = dataContext;

        foreach (var repo in persistentRepositories)
        {
            AddRepositoryToDictionary(repo);
        }

        foreach (var repo in runTimeRepositories)
        {
            AddRepositoryToDictionary(repo);
        }
    }

    public void Dispose()
    {
        _dataContext.Dispose();
    }

    /// <inheritdoc cref="IUnitOfWork.GetRepository{TEntity, TId}()"/>
    public IRepository<TEntity, TId> GetRepository<TEntity, TId>() where TEntity : Entity<TId>
    {
        var type = typeof(TEntity);

        if (_repositories.TryGetValue(type, out var repository))
        {
            return (IRepository<TEntity, TId>)repository;
        }

        throw new RepositoryNotFoundException();
    }

    /// <inheritdoc cref="IUnitOfWork.GetRepository{TEntity}()"/>
    public IRepository<TEntity, Guid> GetRepository<TEntity>() where TEntity : Entity
        => GetRepository<TEntity, Guid>();

    /// <inheritdoc cref="IUnitOfWork.CommitAsync(CancellationToken)"/>
    public async Task CommitAsync(CancellationToken cancellationToken = default)
        => await _dataContext.CommitAsync(cancellationToken);

    /// <inheritdoc cref="IUnitOfWork.RollbackAsync"/>
    public async Task RollbackAsync(CancellationToken cancellationToken = default)
        => await _dataContext.RollbackAsync(cancellationToken);

    /// <inheritdoc cref="IUnitOfWork.SaveAsync"/>
    public async Task SaveAsync(CancellationToken cancellationToken = default)
        => await _dataContext.SaveChangesAsync(cancellationToken);

    /// <summary>
    /// Adds IRepository Type to the internal references.
    /// </summary>
    /// <param name="repo">Repository to add</param>
    private void AddRepositoryToDictionary(object repo)
    {
        var genArg = GetRepositoryGenericType(repo);

        if (genArg is not null)
        {
            _repositories.Add(genArg, repo);
        }
    }

    /// <summary>
    /// Returns the generictype TEntity of an <see cref="IRepository{TEntity, TId}"/>
    /// if the repos implements it, null otherwise.
    /// </summary>
    /// <param name="repo">Repo to check.</param>
    private Type? GetRepositoryGenericType(object repo)
    {
        var typ = repo.GetType();

        foreach (var intType in typ.GetInterfaces())
        {
            if (!intType.IsGenericType)
            {
                continue;
            }

            var genType = intType.GetGenericTypeDefinition();

            if (genType == typeof(IRepository<>) || genType == typeof(IRepository<,>))
            {
                return intType.GetGenericArguments()[0];
            }
        }
        return null;
    }
}
