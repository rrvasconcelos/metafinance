using MetaFinance.Domain.Financial.Enums;
using MetaFinance.Domain.SharedKernel.Aggregates;
using MetaFinance.Domain.SharedKernel.Base;
using MetaFinance.Domain.SharedKernel.Exceptions;

namespace MetaFinance.Domain.Financial.Entities;

public class Category : AuditableEntity<int>, IAggregateRoot
{
    public string Name { get; private set; } = string.Empty;
    public string? Description { get; private set; }
    public CategoryType Type { get; private set; }
    public bool IsActive { get; private set; }

    protected Category() : base("default_user")
    {
    }

    public Category(string name, CategoryType type, string? description, string createdBy)
        : base(createdBy)
    {
        ValidateCategory(name, type);

        Name = name.Trim();
        Type = type;
        Description = description?.Trim();
        IsActive = true;
    }

    public void Update(string? name, CategoryType? type, string? description, string modifiedBy)
    {
        ValidateName(name);
        
        if (name is not null)
        {
            Name = name.Trim();
        }

        if (type.HasValue)
        {
            ValidateType(type.Value);
            Type = type.Value;
        }

        Description = description?.Trim();
        UpdateAudit(modifiedBy);
    }

    public void Deactivate(string modifiedBy)
    {
        IsActive = false;
        UpdateAudit(modifiedBy);
    }

    public void Activate(string modifiedBy)
    {
        IsActive = true;
        UpdateAudit(modifiedBy);
    }

    private static void ValidateCategory(string name, CategoryType type)
    {
        ValidateName(name);
        ValidateType(type);
    }

    private static void ValidateName(string? name)
    {
        if (string.IsNullOrWhiteSpace(name))
            throw new DomainException("Category name cannot be empty.");

        if (name.Length > 100)
            throw new DomainException("Category name cannot exceed 100 characters.");
    }

    private static void ValidateType(CategoryType type)
    {
        if (!Enum.IsDefined(type))
            throw new DomainException("Invalid category type.");
    }
}