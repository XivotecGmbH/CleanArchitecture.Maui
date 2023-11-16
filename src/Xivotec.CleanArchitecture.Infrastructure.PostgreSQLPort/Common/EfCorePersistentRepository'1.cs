using Microsoft.EntityFrameworkCore;
using Xivotec.CleanArchitecture.Domain.Common;

namespace Xivotec.CleanArchitecture.Infrastructure.PostgreSQLPort.Common;

public abstract class EfCorePersistentRepository<TEntity> : EfCorePersistentRepository<TEntity, Guid> where TEntity : Entity
{
    protected EfCorePersistentRepository(DbContext dataContext) : base(dataContext)
    {
    }
}
