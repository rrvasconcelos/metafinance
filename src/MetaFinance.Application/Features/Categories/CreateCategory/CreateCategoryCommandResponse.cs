using MetaFinance.Domain.Financial.Enums;

namespace MetaFinance.Application.Features.Categories.CreateCategory;

public class CreateCategoryCommandResponse
{
    public int Id { get; set; }
    public string Name { get; set; } = null!;
    public string? Description { get;  set; }
    public CategoryType Type { get;  set; }
    public bool IsActive { get;  set; }
}