using MetaFinance.Data.Context;
using MetaFinance.Domain.Financial.Entities;
using MetaFinance.Domain.Financial.Interfaces.Repositories;
using MetaFinance.Domain.SharedKernel.Exceptions;
using Microsoft.EntityFrameworkCore;

namespace MetaFinance.Data.Financial.Repositories;

public class TransactionRepository(MetaFinanceContext context)
    : ITransactionRepository
{
    public async Task<Transaction> AddAsync(Transaction transaction)
    {
        try
        {
            await context.Transactions.AddAsync(transaction);
            return transaction;
        }
        catch (DbUpdateException ex) 
        {
            throw new TransactionPersistenceException("Error adding transaction.", ex);
        }
    }

    public async Task<Transaction> UpdateAsync(Transaction transaction)
    {
        try
        {
            context.Transactions.Update(transaction);
            await Task.CompletedTask;
            
            return transaction;
        }
        catch (DbUpdateException ex)
        {
            throw new TransactionPersistenceException("Error updating transaction.", ex);
        }
    }

    public async Task DeleteAsync(Transaction transaction)
    {
        try
        {
            context.Transactions.Remove(transaction);
            await Task.CompletedTask;
        }
        catch (DbUpdateException ex)
        {
            throw new TransactionPersistenceException("Error deleting transaction.", ex);
        }
    }

    public async Task<Transaction?> GetByIdAsync(long id)
    {
        try
        {
            return await context.Transactions.FindAsync(id);
        }
        catch (Exception ex)
        {
            throw new TransactionPersistenceException("Error when obtaining transaction by id.", ex);
        }
    }

    public async Task<List<Transaction>> GetAllAsync()
    {
        try
        {
            return await context
                .Transactions
                .AsNoTracking()
                .ToListAsync();
        }
        catch (Exception ex)
        {
            throw new TransactionPersistenceException("Error getting all transactions.", ex);
        }
    }

    public async Task<IEnumerable<Transaction>> GetByUserIdAsync(string userId)
    {
        try
        {
            return await context
                .Transactions
                .AsNoTracking()
                .Where(x => x.UserId == userId)
                .ToListAsync();
        }
        catch (Exception ex)
        {
            throw new TransactionPersistenceException("Error when getting all transactions per user.", ex);
        }
    }
}