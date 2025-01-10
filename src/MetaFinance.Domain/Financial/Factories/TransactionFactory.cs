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
            totalInstallments,
            userId);
    }

    public static Transaction CreateIncome(
        string description,
        Money amount,
        TransactionMethod method,
        int categoryId,
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
            totalInstallments,
            userId);
    }
    
    public static Transaction CreateExpenseWithInstallments(
        string description,
        Money amount,
        TransactionMethod method,
        int categoryId,
        int totalInstallments,
        DateTime firstDueDate,
        string userId)
    {
        var transaction = CreateExpense(
            description,
            amount,
            method,
            categoryId,
            totalInstallments,
            userId);

        transaction.AddInstallments(totalInstallments, firstDueDate);

        return transaction;
    }
    
    private static Transaction Create(
        string description,
        Money amount,
        TransactionType type,
        TransactionMethod method,
        TransactionStatus status,
        int categoryId,
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
            totalInstallments,
            userId);
    }
}