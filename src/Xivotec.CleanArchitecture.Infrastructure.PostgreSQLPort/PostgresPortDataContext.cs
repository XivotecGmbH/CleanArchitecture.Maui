using CommunityToolkit.Mvvm.Messaging;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using Npgsql;
using Xivotec.CleanArchitecture.Application.Messages;
using Xivotec.CleanArchitecture.Domain.ToDoListAggregate.Entities;
using Xivotec.CleanArchitecture.Infrastructure.Persistence.Common.Interfaces;

namespace Xivotec.CleanArchitecture.Infrastructure.PostgreSQLPort;

/// <summary>
/// Technology-specific implementation of <see cref="IDataContext"/>.
/// </summary>
public class PostgresPortDataContext : DbContext, IDataContext
{
    // Associated Database entries
    public DbSet<ToDoList>? TodoLists { get; set; }
    public DbSet<ToDoItem>? TodoItems { get; set; }

    private const string DbErrorString = "Database operation failed. \nCheck configuration " +
        "and server status and restart the application.";

    private readonly ILogger<PostgresPortDataContext>? _logger;

    // Needed for generating Migrations independent from main application
    public PostgresPortDataContext()
    {
    }

    public PostgresPortDataContext(DbContextOptions<PostgresPortDataContext> options, ILogger<PostgresPortDataContext> logger)
        : base(options)
    {
        MigrateDatabase();
        _logger = logger;
    }

    /// <inheritdoc cref="IDataContext.SaveChangesAsync(CancellationToken)"/>
    public override async Task<int> SaveChangesAsync(CancellationToken cancellationToken = new())
        => await base.SaveChangesAsync(cancellationToken);

    /// <inheritdoc cref="IDataContext.CommitAsync(CancellationToken)"/>
    public Task<int> CommitAsync(CancellationToken cancellationToken)
    {
        throw new NotImplementedException();
    }

    /// <inheritdoc cref="IDataContext.RollbackAsync(CancellationToken)"/>
    public Task<int> RollbackAsync(CancellationToken cancellationToken)
    {
        throw new NotImplementedException();
    }

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
    {
        optionsBuilder.UseNpgsql()
            .UseLazyLoadingProxies();
    }

    private void MigrateDatabase()
    {
        try
        {
            Database.Migrate();
        }
        catch (NpgsqlException ex)
        {
            WeakReferenceMessenger.Default.Send(new ErrorMessage(DbErrorString));
            _logger!.LogError(message: DbErrorString, exception: ex);
        }
    }
}