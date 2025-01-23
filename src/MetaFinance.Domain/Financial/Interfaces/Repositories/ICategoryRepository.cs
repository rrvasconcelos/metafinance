using MetaFinance.Domain.Financial.Entities;

namespace MetaFinance.Domain.Financial.Interfaces.Repositories;

public interface ICategoryRepository
{
    Task<Category> CreateAsync(Category category);
    Task<Category> UpdateAsync(Category category);
    Task DeleteAsync(Category category);
    
    Task<IEnumerable<Category>> GetAllAsync(CancellationToken cancellationToken);
    Task<Category?> GetByIdAsync(int id);
    Task<bool> ExistsByNameAsync(string name, CancellationToken cancellationToken);
    Task<bool> ExistsByNameAndDifferentIdAsync(string name, int currentId, CancellationToken cancellationToken);
}