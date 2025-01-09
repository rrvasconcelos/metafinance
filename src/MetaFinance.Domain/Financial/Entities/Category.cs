using MetaFinance.Domain.Financial.Enums;
using MetaFinance.Domain.SharedKernel.Base;

namespace MetaFinance.Domain.Financial.Entities;

public class Category : BaseEntity<int>
{
    public string Name { get; private set; }
    public string? Description { get; private set; }
    public CategoryType Type { get; private set; }
    public bool IsActive { get; private set; }

    private Category() { }

    public Category(string name, CategoryType type, string? description = null)
    {
        Name = name;
        Type = type;
        Description = description;
        IsActive = true;
    }

    public void Deactivate() => IsActive = false;
    public void Activate() => IsActive = true;
}
