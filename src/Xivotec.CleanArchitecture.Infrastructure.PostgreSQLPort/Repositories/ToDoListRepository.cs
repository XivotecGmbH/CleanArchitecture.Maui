using Xivotec.CleanArchitecture.Domain.ToDoListAggregate.Entities;
using Xivotec.CleanArchitecture.Infrastructure.PostgreSQLPort.Common;

namespace Xivotec.CleanArchitecture.Infrastructure.PostgreSQLPort.Repositories;

public class ToDoListRepository : EfCorePersistentRepository<ToDoList>
{
    public ToDoListRepository(PostgresPortDataContext context) : base(context)
    {
    }
}
