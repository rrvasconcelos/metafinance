using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using FluentValidation;

namespace MetaFinance.Application.Features.Transactions;

public class TransactionValidatorBase<T> : AbstractValidator<T> where T : ITransactionCommand
{
    public TransactionValidatorBase()
    {
        RuleFor(x => x.Description)
            .NotEmpty().WithMessage("Description is required.")
            .MaximumLength(250).WithMessage("Description must not exceed 250 characters.");

        RuleFor(x => x.Value)
            .GreaterThan(0).WithMessage("Value must be greater than 0.");

        RuleFor(x => x.Type)
            .IsInEnum().WithMessage("Invalid transaction type.");

        RuleFor(x => x.Method)
            .IsInEnum().WithMessage("Invalid transaction method.");

        RuleFor(x => x.CategoryId)
            .GreaterThan(0).WithMessage("Category is required.");

        RuleFor(x => x.UserId)
            .NotEmpty().WithMessage("User is required.");
    }
}

