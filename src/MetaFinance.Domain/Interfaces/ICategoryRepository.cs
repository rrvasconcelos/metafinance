using MetaFinance.Domain.Financial.Entities;

namespace MetaFinance.Domain.Interfaces;

public interface ICategoryRepository
{
    Task<Category> CreateAsync(Category category);
    Task<Category> UpdateAsync(Category category);
    Task<Category> DeleteAsync(Guid id);
    
    Task<IEnumerable<Category>> GetAllAsync();
    Task<Category> GetByIdAsync(Guid id);
    Task<Category> GetByNameAsync(string name);
}