using MetaFinance.Domain.Financial.Enums;

namespace MetaFinance.Application.Features.Categories.CreateCategory;

public record CreateCategoryCommandResponse(
    int Id,
    string Name,
    string? Description,
    CategoryType Type,
    bool IsActive);