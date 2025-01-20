using MetaFinance.Domain.Financial.Entities;

namespace MetaFinance.Domain.Financial.Interfaces.Repositories;

public interface ICategoryRepository
{
    Task<Category> CreateAsync(Category category);
    Task<Category> UpdateAsync(Category category);
    Task DeleteAsync(Category category);
    
    Task<IEnumerable<Category>> GetAllAsync(CancellationToken cancellationToken);
    Task<Category?> GetByIdAsync(Guid id);
    Task<Category?> GetByNameAsync(string name);
}