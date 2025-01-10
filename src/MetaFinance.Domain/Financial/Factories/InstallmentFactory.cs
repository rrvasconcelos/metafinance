using MetaFinance.Domain.Financial.Entities;
using MetaFinance.Domain.SharedKernel.ValueObjects;

namespace MetaFinance.Domain.Financial.Factories;

public class InstallmentFactory
{
    public static IEnumerable<Installment> CreateInstallments(
        long transactionId,
        int totalInstallments,
        DateTime firstDueDate,
        Money totalAmount,
        string userId)
    {
        var installments = new List<Installment>();
        var installmentAmount = new Money(totalAmount.Amount / totalInstallments);
        
        for (var i = 1; i <= totalInstallments; i++)
        {
            var dueDate = firstDueDate.AddMonths(i - 1);
            var installment = new Installment(
                transactionId,
                i,
                totalInstallments,
                dueDate,
                installmentAmount,
                userId);
                
            installments.Add(installment);
        }

        return installments;
    }
}