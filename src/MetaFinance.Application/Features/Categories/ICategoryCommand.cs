using MetaFinance.Domain.Financial.Enums;

namespace MetaFinance.Application.Features.Categories;

public interface ICategoryCommand
{
    string Name { get; }
    string? Description { get; }
    CategoryType Type { get; }
}