using FluentValidation;
using MetaFinance.Application.Features.Categories.UpdateCategory;

namespace MetaFinance.Application.Features.Categories.CreateCategory;

public class CreateCategoryCommandValidator : CategoryValidatorBase<CreateCategoryCommand>
{
    public CreateCategoryCommandValidator()
    {
    }
}