using MetaFinance.Domain.SharedKernel.Base;
using MetaFinance.Domain.SharedKernel.Exceptions;
using MetaFinance.Domain.SharedKernel.ValueObjects;

namespace MetaFinance.Domain.Financial.Entities;

public class InstallmentControl : BaseEntity<long>
{
    public Transaction Transaction { get; private set; }
    public int InstallmentNumber { get; private set; }
    public DateTime DueDate { get; private set; }
    public bool IsPaid { get; private set; }
    public DateTime? PaymentDate { get; private set; }
    public Money Value { get; private set; }

    private InstallmentControl() : base(0) { }

    public InstallmentControl(int installmentNumber, DateTime dueDate, Money value) 
        : base(0)
    {
        InstallmentNumber = installmentNumber;
        DueDate = dueDate;
        Value = value;
        IsPaid = false;
    }

    public void MarkAsPaid(DateTime paymentDate)
    {
        if (IsPaid)
            throw new DomainException("Installment is already paid");

        IsPaid = true;
        PaymentDate = paymentDate;
    }
}