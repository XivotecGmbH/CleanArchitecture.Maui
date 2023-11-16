using Microsoft.EntityFrameworkCore;
using Xivotec.CleanArchitecture.Application.Common.Persistence.Interfaces;
using Xivotec.CleanArchitecture.Application.Exceptions;
using Xivotec.CleanArchitecture.Domain.Common;
using Xivotec.CleanArchitecture.Infrastructure.Persistence.Common.Interfaces;

namespace Xivotec.CleanArchitecture.Infrastructure.PostgreSQLPort.Common;

public abstract class EfCorePersistentRepository<TEntity, TId>
    : IRepository<TEntity, TId>, IPersistentRepository
    where TEntity : Entity<TId>
    where TId : notnull
{
    private readonly DbContext _context;

    protected EfCorePersistentRepository(DbContext dataContext)
    {
        _context = dataContext;
    }

    public async Task<TEntity> AddAsync(TEntity entity)
    {
        await _context.Set<TEntity>()
            .AddAsync(entity);
        await _context.SaveChangesAsync();
        return entity;
    }

    public async Task DeleteAsync(TEntity entity)
    {
        var exist = await GetByIdAsync(entity.Id);
        _context.Set<TEntity>()
            .Remove(exist);
        await _context.SaveChangesAsync();
    }

    public async Task<List<TEntity>> GetAllAsync()
    {
        var result = _context.Set<TEntity>();
        return await result.ToListAsync();
    }

    public async Task<TEntity> GetByIdAsync(TId id)
    {
        return await _context.Set<TEntity>()
            .FindAsync(id) ?? throw new ItemNotFoundException();
    }

    public async Task UpdateAsync(TEntity entity)
    {
        var exist = await GetByIdAsync(entity.Id);
        _context.Entry(exist)
            .CurrentValues.SetValues(entity);
        await _context.SaveChangesAsync();
    }
}
