using MetaFinance.Domain.Financial.Enums;
using MetaFinance.Domain.SharedKernel.Base;
using MetaFinance.Domain.SharedKernel.Exceptions;
using MetaFinance.Domain.SharedKernel.ValueObjects;

namespace MetaFinance.Domain.Financial.Entities;

public class Transaction : AuditableEntity<long>
{
    public string Description { get; private set; }
    public Money Amount { get; private set; }
    public DateTime PaymentDate { get; private set; } = DateTime.UtcNow;
    public int Month => PaymentDate.Month;
    public int Year => PaymentDate.Year;
    public TransactionType Type { get; private set; }
    public TransactionMethod TransactionMethod { get; private set; }
    public TransactionStatus TransactionStatus { get; private set; }
    public int CategoryId { get; private set; }
    public long UserId { get; private set; }

    public Category Category { get; private set; }

    public Transaction(
        string description,
        Money amount,
        TransactionType transactionType,
        TransactionMethod transactionMethod,
        TransactionStatus transactionStatus,
        int categoryId,
        long userId) : base(userId)
    {
        ValidateTransaction(description, amount, categoryId, userId);

        Description = description;
        Amount = amount;
        Type = transactionType;
        TransactionMethod = transactionMethod;
        TransactionStatus = transactionStatus;
        CategoryId = categoryId;
        UserId = userId;
    }

    private static void ValidateTransaction(string description, Money amount, int categoryId, long userId)
    {
        if (string.IsNullOrWhiteSpace(description))
            throw new DomainException("Description is required");

        if (amount.Amount <= 0)
            throw new DomainException("Amount must be greater than zero");

        if (categoryId <= 0)
            throw new DomainException("Invalid CategoryId");

        if (userId <= 0)
            throw new DomainException("Invalid UserId");
    }

    public void UpdateStatus(TransactionStatus newStatus, long modifiedBy)
    {
        TransactionStatus = newStatus;
        UpdateAudit(modifiedBy);
    }
}