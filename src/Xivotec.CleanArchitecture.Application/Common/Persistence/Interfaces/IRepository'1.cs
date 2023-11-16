using Xivotec.CleanArchitecture.Domain.Common;

namespace Xivotec.CleanArchitecture.Application.Common.Persistence.Interfaces;

/// <summary>
/// Default Repository Interface.
/// </summary>
/// <typeparam name="TEntity">Entity Type the repository has.</typeparam>
public interface IRepository<TEntity> : IRepository<TEntity, Guid> where TEntity : Entity
{
}
