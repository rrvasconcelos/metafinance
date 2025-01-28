using FluentResults;
using Mapster;
using MediatR;
using MetaFinance.Application.Validation;
using MetaFinance.Domain.Financial.Enums;
using MetaFinance.Domain.Financial.Factories;
using MetaFinance.Domain.Financial.Interfaces.UnitOfWork;
using MetaFinance.Domain.SharedKernel.ValueObjects;

namespace MetaFinance.Application.Features.Transactions.CreateTransaction;

public sealed record CreateTransactionCommand(
    string Description,
    decimal Value,
    TransactionType Type,
    TransactionMethod Method,
    int CategoryId,
    int? TotalInstallments,
    string UserId)
    : IRequest<Result<CreateTransactionCommandResponse>>, ITransactionCommand;

    public class CreateTransactionHandler(
        ITransactionUnitOfWork transactionUnitOfWork,
        ICategoryUnitOfWork categoryUnitOfWork)
        : IRequestHandler<CreateTransactionCommand, Result<CreateTransactionCommandResponse>>
    {
        public async Task<Result<CreateTransactionCommandResponse>> Handle(
            CreateTransactionCommand request, 
            CancellationToken cancellationToken)
        {
            var category = await categoryUnitOfWork.Categories.GetByIdAsync(request.CategoryId);

            if (category is null)
            {
                return Result.Fail(new CategoryNotFoundError(request.CategoryId));
            }

            var transaction = request.Type switch
            {
                TransactionType.Expense => TransactionFactory.CreateExpense(
                    request.Description,
                    new Money(request.Value),
                    request.Method,
                    request.CategoryId,
                    request.TotalInstallments,
                    request.UserId),
                TransactionType.Income => TransactionFactory.CreateIncome(
                    request.Description,
                    new Money(request.Value),
                    request.Method,
                    request.CategoryId,
                    request.TotalInstallments,
                    request.UserId),
                TransactionType.InstallmentExpense => TransactionFactory.CreateExpenseWithInstallments(
                    request.Description,
                    new Money(request.Value),
                    request.Method,
                    request.CategoryId,
                    request.TotalInstallments!.Value,
                    DateTime.UtcNow,
                    request.UserId),
                _ => throw new ArgumentOutOfRangeException(nameof(request), request.Type, "Invalid transaction type.")
            };

            await transactionUnitOfWork.Transactions.AddAsync(transaction);
            await transactionUnitOfWork.SaveChangesAsync(cancellationToken);

            var response = transaction.Adapt<CreateTransactionCommandResponse>();

            return Result.Ok(response);
        }
    }