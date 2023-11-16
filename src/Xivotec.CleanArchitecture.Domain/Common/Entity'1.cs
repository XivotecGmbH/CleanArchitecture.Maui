namespace Xivotec.CleanArchitecture.Domain.Common;

/// <summary>
/// Generic Entity base class. Id Type can be anything
/// </summary>
/// <typeparam name="TId">Id Type of the Entity</typeparam>
public abstract class Entity<TId>
{
    /// <summary>
    /// Generic Unique Entity Id
    /// </summary>
    public TId Id { get; init; }

    protected virtual bool Equals(Entity<TId> other)
    {
        return EqualityComparer<TId>.Default.Equals(Id, other.Id);
    }

    public override bool Equals(object? obj)
    {
        if (obj is null || GetType() != obj.GetType())
        {
            return false;
        }

        return Equals((Entity<TId>)obj);
    }

    public override int GetHashCode()
    {
        return EqualityComparer<TId>.Default.GetHashCode(Id!);
    }
}
