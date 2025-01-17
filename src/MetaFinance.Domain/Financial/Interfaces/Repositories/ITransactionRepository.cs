using MetaFinance.Domain.Financial.Entities;

namespace MetaFinance.Domain.Financial.Interfaces.Repositories;

public interface ITransactionRepository
{
    Task<Transaction> AddAsync(Transaction transaction);
    Task<Transaction> UpdateAsync(Transaction transaction);
    Task DeleteAsync(Transaction transaction);
    
    Task<Transaction?> GetByIdAsync(long id);
    Task<List<Transaction>> GetAllAsync();
    Task<IEnumerable<Transaction>> GetByUserIdAsync(string userId);
}