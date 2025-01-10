using MetaFinance.Domain.Financial.Enums;
using MetaFinance.Domain.SharedKernel.Base;

namespace MetaFinance.Domain.Financial.Entities;

public class Category : AuditableEntity<int>
{
    public string Name { get; private set; }
    public string? Description { get; private set; }
    public CategoryType Type { get; private set; }
    public bool IsActive { get; private set; }

    protected Category() { }

    public Category(string name, CategoryType type, string? description, string createdBy) : base(createdBy)
    {
        if (string.IsNullOrWhiteSpace(name))
            throw new ArgumentException("Category name cannot be empty.", nameof(name));

        Name = name.Trim();
        Type = type;
        Description = description?.Trim();
        IsActive = true;
    }

    public void Deactivate() => IsActive = false;

    public void Activate() => IsActive = true;

    public void Update(string? name, CategoryType? type, string? description, string modifiedBy)
    {
        if (!string.IsNullOrWhiteSpace(name))
            Name = name.Trim();

        if (type.HasValue)
            Type = type.Value;

        Description = description?.Trim();

        UpdateAudit(modifiedBy);
    }
}


