using MetaFinance.Domain.Financial.Enums;

namespace MetaFinance.Application.Features.Transactions;

public interface ITransactionCommand
{
    public string Description { get; }
    public decimal Value { get; }
    public TransactionType Type { get; }
    public TransactionMethod Method { get; }
    public int CategoryId { get; }
    public int? TotalInstallments { get; }
    public string UserId { get; }
}