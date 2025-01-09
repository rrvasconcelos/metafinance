namespace MetaFinance.Domain.SharedKernel.Base;

public abstract class AuditableEntity<TId>(long createdBy) : BaseEntity<TId>
{
    public DateTime CreatedAt { get; private set; } = DateTime.UtcNow;
    public long CreatedBy { get; private set; } = createdBy;
    public DateTime? LastModifiedAt { get; private set; }
    public long? LastModifiedBy { get; private set; }

    protected void UpdateAudit(long modifiedBy)
    {
        LastModifiedAt = DateTime.UtcNow;
        LastModifiedBy = modifiedBy;
    }
}