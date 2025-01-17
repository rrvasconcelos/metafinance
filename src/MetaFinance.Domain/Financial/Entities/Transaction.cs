using MetaFinance.Domain.Financial.Enums;
using MetaFinance.Domain.SharedKernel.Aggregates;
using MetaFinance.Domain.SharedKernel.Base;
using MetaFinance.Domain.SharedKernel.Exceptions;
using MetaFinance.Domain.SharedKernel.ValueObjects;

namespace MetaFinance.Domain.Financial.Entities;

public class Transaction : AuditableEntity<long>, IAggregateRoot
{
    public  string Description { get; private set; } = string.Empty;
    public Money Amount { get; private set; }
    public DateTime CreatedDate { get; private set; } = DateTime.UtcNow;
    public int Month => CreatedDate.Month;
    public int Year => CreatedDate.Year;
    public TransactionType Type { get; private set; }
    public TransactionMethod Method { get; private set; }
    public TransactionStatus Status { get; private set; }
    public int? TotalInstallments { get; private set; }
    public int CategoryId { get; private set; }
    public string UserId { get; private set; }
    public bool IsInstallment => TotalInstallments is > 1;
    public IReadOnlyCollection<Installment> Installments { get; private set; } = [];
    public Category Category { get; private set; }

    protected Transaction():base("default_user") 
    {
    }

    public Transaction(
        string description,
        Money amount,
        TransactionType transactionType,
        TransactionMethod method,
        TransactionStatus status,
        int categoryId,
        int? totalInstallments,
        string userId) : base(userId)
    {
        ValidateTransaction(description, amount, categoryId, totalInstallments, userId);

        Description = description;
        Amount = amount;
        Type = transactionType;
        Method = method;
        Status = status;
        CategoryId = categoryId;
        UserId = userId;
        TotalInstallments = totalInstallments;
    }

    private static void ValidateTransaction(
        string description,
        Money amount,
        int categoryId,
        int? totalInstallments,
        string userId)
    {
        if (string.IsNullOrWhiteSpace(description))
            throw new DomainException("Description is required.");

        if (amount.Amount <= 0)
            throw new DomainException("Amount must be greater than zero.");

        if (categoryId <= 0)
            throw new DomainException("Invalid CategoryId.");

        if (string.IsNullOrWhiteSpace(userId))
            throw new DomainException("Invalid UserId.");

        if (totalInstallments is <= 1)
            throw new DomainException("Invalid number of installments.");
    }

    public void AddInstallments(int totalInstallments, DateTime firstDueDate)
    {
        if (!IsInstallment || totalInstallments != TotalInstallments)
            throw new DomainException("Invalid installment configuration.");

        var installments = new List<Installment>();
        var installmentAmountValue = Math.Round(Amount.Amount / totalInstallments, 2);
        var remainingAmount = Amount.Amount - (installmentAmountValue * (totalInstallments - 1));

        for (var i = 1; i <= totalInstallments; i++)
        {
            var dueDate = firstDueDate.AddMonths(i - 1);
            var amount = i == totalInstallments ? remainingAmount : installmentAmountValue;

            installments.Add(new Installment(this.Id,
                i,
                totalInstallments,
                dueDate,
                new Money(amount),
                UserId));
        }

        Installments = installments;
    }
}