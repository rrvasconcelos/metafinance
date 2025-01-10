namespace MetaFinance.Domain.SharedKernel.Base;

public abstract class AuditableEntity<TId>(string createdBy) : BaseEntity<TId>
{
    public DateTime CreatedAt { get; private set; } = DateTime.UtcNow;
    public string CreatedBy { get; private set; } = createdBy;
    public DateTime? LastModifiedAt { get; private set; }
    public string? LastModifiedBy { get; private set; }

    protected void UpdateAudit(string modifiedBy)
    {
        LastModifiedAt = DateTime.UtcNow;
        LastModifiedBy = modifiedBy;
    }
}