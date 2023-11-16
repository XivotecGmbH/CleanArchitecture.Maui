using Xivotec.CleanArchitecture.Domain.ToDoListAggregate.Entities;
using Xivotec.CleanArchitecture.Infrastructure.PostgreSQLPort.Common;

namespace Xivotec.CleanArchitecture.Infrastructure.PostgreSQLPort.Repositories;

public class ToDoItemRepository : EfCorePersistentRepository<ToDoItem>
{
    public ToDoItemRepository(PostgresPortDataContext context) : base(context)
    {
    }
}
