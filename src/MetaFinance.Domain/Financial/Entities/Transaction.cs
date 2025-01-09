using MetaFinance.Domain.Financial.Enums;
using MetaFinance.Domain.SharedKernel.Base;
using MetaFinance.Domain.SharedKernel.ValueObjects;

namespace MetaFinance.Domain.Financial.Entities;

public class Transaction : BaseEntity<long>
{
    public string Description { get; private set; }
    public Money TotalValue { get; private set; }
    public Money? InstallmentValue { get; private set; }
    public DateTime FirstPaymentDate { get; private set; } = DateTime.Now;
    public TransactionType Type { get; private set; }
    public bool IsInstallment { get; private set; }
    public PaymentMethod PaymentMethod { get; private set; }
    public int? TotalInstallments { get; private set; }
    public int CategoryId { get; private set; }
    public long UserId { get; private set; }
    
    public Category? Category { get; init; }

    private Transaction(
        string description, 
        Money totalValue, 
        TransactionType type, 
        bool isInstallment, 
        PaymentMethod paymentMethod, 
        int categoryId, 
        long userId)
    {
        Description = description;
        TotalValue = totalValue;
        Type = type;
        IsInstallment = isInstallment;
        PaymentMethod = paymentMethod;
        CategoryId = categoryId;
        UserId = userId;
    }
}