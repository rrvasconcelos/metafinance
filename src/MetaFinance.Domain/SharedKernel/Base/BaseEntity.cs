﻿namespace MetaFinance.Domain.SharedKernel.Base;

public abstract class BaseEntity<TId>
{
    public TId Id { get; protected set; }
    
    protected BaseEntity() { }
    
    protected BaseEntity(TId id)
    {
        Id = id;
    }

    public override bool Equals(object? obj)
    {
        if (obj is not BaseEntity<TId> other) return false;
        return Id?.Equals(other.Id) ?? false;
    }

    public override int GetHashCode() => Id?.GetHashCode() ?? 0;
}