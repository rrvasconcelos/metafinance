using MetaFinance.Domain.Financial.Enums;

namespace MetaFinance.Application.Features.Categories.UpdateCategory;

public record UpdateCategoryCommandResponse(int Id, string Name, string? Description, CategoryType Type);