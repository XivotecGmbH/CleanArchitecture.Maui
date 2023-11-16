namespace Xivotec.CleanArchitecture.Domain.Common.Interfaces;

/// <summary>
/// Entity Extension for auditable properties.
/// </summary>
public interface IAuditableEntity
{
    /// <summary>
    /// The User created the entity.
    /// </summary>
    string? CreatedBy { get; set; }

    /// <summary>
    /// DateTime the Entity was created
    /// </summary>
    DateTime CreatedOn { get; set; }

    /// <summary>
    /// The User who last modified the entity.
    /// </summary>
    string? LastModifiedBy { get; set; }

    /// <summary>
    /// DateTime the Entity was last modified
    /// </summary>
    DateTime LastModifiedOn { get; set; }
}
