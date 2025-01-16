using MetaFinance.Domain.Financial.Entities;

namespace MetaFinance.Domain.Financial.Interfaces;

public interface ITransactionRepository
{
    Task<Transaction> AddAsync(Transaction transaction);
    Task UpdateAsync(Transaction transaction);
    Task DeleteAsync(Transaction transaction);
    
    Task<Transaction> GetByIdAsync(long id);
    Task<IEnumerable<Transaction>> GetAllAsync();
    Task<IEnumerable<Transaction>> GetByUserIdAsync(string userId);
}