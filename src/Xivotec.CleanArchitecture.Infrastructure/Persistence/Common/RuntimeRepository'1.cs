using Xivotec.CleanArchitecture.Domain.Common;

namespace Xivotec.CleanArchitecture.Infrastructure.Persistence.Common;

/// <summary>
/// Default Entity Repository Implementation. Uses Guid for Id Type.
/// </summary>
/// <typeparam name="TEntity">Entity type the Repository takes.</typeparam>
public abstract class RuntimeRepository<TEntity> : RuntimeRepository<TEntity, Guid> where TEntity : Entity
{
}
