using FluentValidation;

namespace MetaFinance.Application.Features.Categories.UpdateCategory;

public class UpdateCategoryCommandValidator : CategoryValidatorBase<UpdateCategoryCommand>
{
    public UpdateCategoryCommandValidator()
    {
        RuleFor(x => x.Id)
            .NotEmpty().WithMessage("Id is required.");
    }
}