using MetaFinance.Domain.Financial.Enums;

namespace MetaFinance.Application.Features.Categories.GetCategories;

public record GetCategoriesQueryResponse(
    int Id,
    string Name,
    string? Description,
    CategoryType Type,
    bool IsActive);