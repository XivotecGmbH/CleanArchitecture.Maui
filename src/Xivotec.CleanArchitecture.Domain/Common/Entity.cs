namespace Xivotec.CleanArchitecture.Domain.Common;

/// <summary>
/// Default Entity base class. Uses Guid as Id Type.
/// </summary>
public abstract class Entity : Entity<Guid>
{
}