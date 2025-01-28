using MetaFinance.Domain.Financial.Enums;
using MetaFinance.Domain.SharedKernel.ValueObjects;

namespace MetaFinance.Application.Features.Transactions.CreateTransaction;

public record CreateTransactionCommandResponse(
    long Id,
    string Description,
    Money Amount,
    int Month,
    int Year,
    TransactionType Type,
    TransactionMethod Method,
    TransactionStatus Status,
    int? TotalInstallments,
    bool IsInstallment,
    int CategoryId,
    string UserId
);