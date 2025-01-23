using FluentValidation;

namespace MetaFinance.Application.Features.Categories;

public abstract class CategoryValidatorBase<T> : AbstractValidator<T> where T : ICategoryCommand
{
    protected CategoryValidatorBase()
    {
        RuleFor(x => x.Name)
            .NotEmpty().WithMessage("Name is required.")
            .MaximumLength(100).WithMessage("Name must not exceed 100 characters.");

        RuleFor(x => x.Description)
            .MaximumLength(300).WithMessage("Description must not exceed 300 characters.");

        RuleFor(x => x.Type)
            .IsInEnum().WithMessage("Invalid category type.");
    }
}