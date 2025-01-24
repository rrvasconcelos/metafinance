namespace MetaFinance.Domain.SharedKernel.Base;

public abstract class AuditableEntity<TId> : BaseEntity<TId>
{
    public DateTime CreatedAt { get; private set; } = DateTime.UtcNow;
    public string CreatedBy { get; private set; }
    public DateTime? LastModifiedAt { get; private set; }
    public string? LastModifiedBy { get; private set; }
        
    protected AuditableEntity(string createdBy)
    {
        if (string.IsNullOrWhiteSpace(createdBy))
            throw new ArgumentException("CreatedBy cannot be null or empty.", nameof(createdBy));
                
        CreatedBy = createdBy;
    }

    protected void UpdateAudit(string modifiedBy)
    {
        if (string.IsNullOrWhiteSpace(modifiedBy))
            throw new ArgumentException("modifiedBy cannot be null or empty.", nameof(modifiedBy));
        
        LastModifiedAt = DateTime.Now;
        LastModifiedBy = modifiedBy;
    }
}