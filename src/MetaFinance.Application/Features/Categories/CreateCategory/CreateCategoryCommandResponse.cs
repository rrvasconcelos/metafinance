using MetaFinance.Domain.Financial.Enums;

namespace MetaFinance.Application.Features.Categories.CreateCategory;

public class CreateCategoryCommandResponse
{
    public int Id { get; set; }
    public string Name { get; set; } = null!;
    public string? Description { get; private set; }
    public CategoryType Type { get; private set; }
    public bool IsActive { get; private set; }
}