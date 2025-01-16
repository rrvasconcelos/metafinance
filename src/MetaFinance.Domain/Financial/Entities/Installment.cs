using MetaFinance.Domain.Financial.Enums;
using MetaFinance.Domain.SharedKernel.Base;
using MetaFinance.Domain.SharedKernel.Exceptions;
using MetaFinance.Domain.SharedKernel.ValueObjects;

namespace MetaFinance.Domain.Financial.Entities;

public class Installment : AuditableEntity<long>
{
    public long TransactionId { get; private set; }
    public int InstallmentNumber { get; private set; }
    public int TotalInstallments { get; private set; }
    public DateTime DueDate { get; private set; }
    public Money Amount { get; private set; }
    public InstallmentStatus Status { get; private set; }
    public DateTime? PaymentDate { get; private set; }
    public string UserId { get; private set; }

    public Transaction Transaction { get; private set; }

    public Installment(
        long transactionId,
        int installmentNumber,
        int totalInstallments,
        DateTime dueDate,
        Money amount,
        string userId) : base(userId)
    {
        ValidateInstallment(transactionId, installmentNumber, totalInstallments, dueDate, amount, userId);

        TransactionId = transactionId;
        InstallmentNumber = installmentNumber;
        TotalInstallments = totalInstallments;
        DueDate = dueDate;
        Amount = amount;
        Status = InstallmentStatus.Pending;
        UserId = userId;
    }

    private static void ValidateInstallment(
        long transactionId,
        int installmentNumber,
        int totalInstallments,
        DateTime dueDate,
        Money amount,
        string userId)
    {
        if (totalInstallments <= 0)
            throw new DomainException("TotalInstallments must be greater than zero");

        if (installmentNumber <= 0 || installmentNumber > totalInstallments)
            throw new DomainException("Invalid InstallmentNumber");

        if (dueDate < DateTime.UtcNow.Date)
            throw new DomainException("DueDate cannot be in the past");

        if (amount.Amount <= 0)
            throw new DomainException("Amount must be greater than zero");

        if (string.IsNullOrWhiteSpace(userId))
            throw new DomainException("Invalid UserId");
    }

    public void MarkAsPaid(DateTime paymentDate, string modifiedBy)
    {
        if (Status == InstallmentStatus.Paid)
            throw new DomainException("Installment is already paid");

        if (paymentDate.Date > DateTime.UtcNow.Date)
            throw new DomainException("Payment date cannot be in the future");

        Status = InstallmentStatus.Paid;
        PaymentDate = paymentDate;
        UpdateAudit(modifiedBy);
    }

    public bool IsOverdue() => Status == InstallmentStatus.Pending && DueDate.Date < DateTime.UtcNow.Date;
}