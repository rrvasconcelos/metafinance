using MetaFinance.Domain.Financial.Enums;

namespace MetaFinance.Application.Features.Categories.GetCategories;

public class GetCategoriesQueryResponse
{
    public int Id { get; set; }
    public string Name { get;  set; } = string.Empty;
    public string? Description { get;  set; }
    public CategoryType Type { get;  set; }
    public bool IsActive { get;  set; }
}