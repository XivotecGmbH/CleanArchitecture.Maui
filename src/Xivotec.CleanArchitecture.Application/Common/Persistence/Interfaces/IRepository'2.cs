using Xivotec.CleanArchitecture.Domain.Common;

namespace Xivotec.CleanArchitecture.Application.Common.Persistence.Interfaces;

/// <summary>
/// Generic Repository interface. Normally not used directly.
/// </summary>
/// <typeparam name="TEntity">Entity type the Repository takes.</typeparam>
/// <typeparam name="TId">Id Type the Entities uses</typeparam>
public interface IRepository<TEntity, TId> where TEntity : Entity<TId>
{
    /// <summary>
    /// Returns Entity specified by id. Throws <see cref="Exceptions.ItemNotFoundException"/> if none was found.
    /// </summary>
    /// <param name="id">Id of the entity</param>
    /// <exception cref="Exceptions.ItemNotFoundException"></exception>
    Task<TEntity> GetByIdAsync(TId id);

    /// <summary>
    /// Returns all entities stored inside the repository.
    /// </summary>
    Task<List<TEntity>> GetAllAsync();

    /// <summary>
    /// Adds entity to the repository.
    /// </summary>
    /// <param name="entity">Entity to add.</param>
    Task<TEntity> AddAsync(TEntity entity);

    /// <summary>
    /// Updates entity.
    /// </summary>
    /// <param name="entity">Entity to update.</param>
    Task UpdateAsync(TEntity entity);

    /// <summary>
    /// Removes Entity from the repository.
    /// </summary>
    /// <param name="entity">Entity to be removed.</param>
    Task DeleteAsync(TEntity entity);
}