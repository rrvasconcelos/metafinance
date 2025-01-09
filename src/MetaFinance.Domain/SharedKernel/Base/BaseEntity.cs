namespace MetaFinance.Domain.SharedKernel.Base;

public abstract class BaseEntity<TId>
{
    public required TId Id { get; init; }

    protected BaseEntity(TId id)
    {
        Id = id;
    }

    protected BaseEntity()
    {
    }

    public override bool Equals(object? obj)
    {
        if (obj is not BaseEntity<TId> other) return false;
        return Id?.Equals(other.Id) ?? false;
    }

    public override int GetHashCode() => Id?.GetHashCode() ?? 0;
}
