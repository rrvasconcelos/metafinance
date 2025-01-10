using MetaFinance.Domain.Financial.Entities;
using MetaFinance.Domain.Financial.Enums;
using MetaFinance.Domain.SharedKernel.ValueObjects;

namespace MetaFinance.Domain.Financial.Factories;

public class TransactionFactory
{
    public static Transaction CreateExpense(
        string description,
        Money amount,
        TransactionMethod method,
        int categoryId,
        bool isInstallment,
        int? totalInstallments,
        string userId)
    {
        return Create(
            description,
            amount,
            TransactionType.Expense,
            method,
            TransactionStatus.Pending,
            categoryId,
            isInstallment,
            totalInstallments,
            userId);
    }

    public static Transaction CreateIncome(
        string description,
        Money amount,
        TransactionMethod method,
        int categoryId,
        bool isInstallment,
        int? totalInstallments,
        string userId)
    {
        return Create(
            description,
            amount,
            TransactionType.Income,
            method,
            TransactionStatus.Pending,
            categoryId,
            isInstallment,
            totalInstallments,
            userId);
    }
    
    private static Transaction Create(
        string description,
        Money amount,
        TransactionType type,
        TransactionMethod method,
        TransactionStatus status,
        int categoryId,
        bool isInstallment,
        int? totalInstallments,
        string userId)
    {
        return new Transaction(
            description,
            amount,
            type,
            method,
            status,
            categoryId,
            isInstallment,
            totalInstallments,
            userId);
    }
}