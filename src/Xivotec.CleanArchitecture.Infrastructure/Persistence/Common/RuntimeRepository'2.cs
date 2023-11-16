using Xivotec.CleanArchitecture.Application.Common.Persistence.Interfaces;
using Xivotec.CleanArchitecture.Application.Exceptions;
using Xivotec.CleanArchitecture.Domain.Common;
using Xivotec.CleanArchitecture.Infrastructure.Persistence.Common.Interfaces;

namespace Xivotec.CleanArchitecture.Infrastructure.Persistence.Common;

/// <summary>
/// Generic repository implementation, normally not used directly.
/// </summary>
/// <typeparam name="TEntity">Entity type of the repository.</typeparam>
/// <typeparam name="TId">Id Type of the repository, can be anything</typeparam>
public abstract class RuntimeRepository<TEntity, TId>
    : IRepository<TEntity, TId>, IRuntimeRepository where TEntity : Entity<TId> where TId : notnull
{
    private readonly List<TEntity> _list = new();

    /// <inheritdoc cref="IRepository{TEntity, TId}.AddAsync(TEntity)"/>
    public async Task<TEntity> AddAsync(TEntity entity)
    {
        _list.Add(entity);
        return await Task.FromResult(entity);
    }

    /// <inheritdoc cref="IRepository{TEntity, TId}.DeleteAsync(TEntity)"/>
    public async Task DeleteAsync(TEntity entity)
    {
        var toRemove = await GetByIdAsync(entity.Id);
        _list.Remove(toRemove);
        await Task.CompletedTask;
    }

    /// <inheritdoc cref="IRepository{TEntity, TId}.GetAllAsync"/>
    public async Task<List<TEntity>> GetAllAsync()
    {
        return await Task.FromResult(_list);
    }

    /// <inheritdoc cref="IRepository{TEntity, TId}.GetByIdAsync(TId)"/>
    public async Task<TEntity> GetByIdAsync(TId id)
    {
        try
        {
            var foundEntities = _list.Where(x => x.Id.Equals(id));
            var entity = foundEntities.Single();

            // Id should be unique --> only one item
            return await Task.FromResult(entity);
        }
        catch (Exception ex)
        {
            throw new ItemNotFoundException(id.ToString(), ex);
        }
    }

    /// <inheritdoc cref="IRepository{TEntity, TId}.UpdateAsync(TEntity)"/>
    public async Task UpdateAsync(TEntity entity)
    {
        var entityToUpdate = await GetByIdAsync(entity.Id);
        var entityIndex = _list.IndexOf(entityToUpdate);
        _list[entityIndex] = entity;
    }
}
