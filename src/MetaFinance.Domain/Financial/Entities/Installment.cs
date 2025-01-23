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

    private Installment() : base("userId")
    {
        Amount = Money.Zero; // Assumindo que você tem um valor padrão
        UserId = "default";
        Transaction = null!; // Use isso apenas se tiver certeza que será setado pelo EF
    }

    public Installment(
        long transactionId,
        int installmentNumber,
        int totalInstallments,
        DateTime dueDate,
        Money amount,
        string userId) : base(userId)
    {
        if (string.IsNullOrWhiteSpace(userId))
            throw new DomainException("Invalid UserId for audit.");
        
        ValidateInstallment(transactionId, installmentNumber, totalInstallments, dueDate, amount, userId);

        TransactionId = transactionId;
        InstallmentNumber = installmentNumber;
        TotalInstallments = totalInstallments;
        DueDate = dueDate;
        Amount = amount;
        Status = InstallmentStatus.Pending;
        UserId = userId;
        Transaction = null!;
    }

    private static void ValidateInstallment(
        long transactionId,
        int installmentNumber,
        int totalInstallments,
        DateTime dueDate,
        Money amount,
        string userId)
    {
        var errors = new List<string>();

        if (totalInstallments <= 0)
            errors.Add("TotalInstallments must be greater than zero.");

        if (installmentNumber <= 0 || installmentNumber > totalInstallments)
            errors.Add("Invalid InstallmentNumber.");

        if (dueDate < DateTime.UtcNow.Date)
            errors.Add("DueDate cannot be in the past.");

        if (amount.Amount <= 0)
            errors.Add("Amount must be greater than zero.");

        if (string.IsNullOrWhiteSpace(userId))
            errors.Add("Invalid UserId.");

        if (errors.Count != 0)
            throw new DomainException(string.Join(" ", errors));
    }

    public void MarkAsPaid(DateTime paymentDate, string modifiedBy)
    {
        ValidateMarkAsPaid(paymentDate);

        Status = InstallmentStatus.Paid;
        PaymentDate = paymentDate;
        UpdateAudit(modifiedBy);
    }
    
    private void ValidateMarkAsPaid(DateTime paymentDate)
    {
        if (Status == InstallmentStatus.Paid)
            throw new DomainException("Installment is already paid.");

        if (paymentDate.Date > DateTime.UtcNow.Date)
            throw new DomainException("Payment date cannot be in the future.");
    }

    public bool IsOverdue => Status == InstallmentStatus.Pending && DueDate.Date < DateTime.UtcNow.Date;
}