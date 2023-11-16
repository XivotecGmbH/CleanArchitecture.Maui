using Xivotec.CleanArchitecture.Domain.Common.Interfaces;

namespace Xivotec.CleanArchitecture.Domain.Common;

/// <inheritdoc cref="IAuditableEntity"/>
public abstract class AuditableEntity<TId> : Entity<TId>, IAuditableEntity where TId : notnull
{
    /// <inheritdoc cref="IAuditableEntity.CreatedBy"/>
    public string? CreatedBy { get; set; }

    /// <inheritdoc cref="IAuditableEntity.CreatedOn"/>
    public DateTime CreatedOn { get; set; }

    /// <inheritdoc cref="IAuditableEntity.LastModifiedBy"/>
    public string? LastModifiedBy { get; set; }

    /// <inheritdoc cref="IAuditableEntity.LastModifiedOn"/>
    public DateTime LastModifiedOn { get; set; }
}
