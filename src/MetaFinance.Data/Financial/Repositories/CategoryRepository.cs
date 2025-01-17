using MetaFinance.Data.Context;
using MetaFinance.Domain.Financial.Entities;
using MetaFinance.Domain.Financial.Interfaces.Repositories;
using MetaFinance.Domain.SharedKernel.Exceptions;
using Microsoft.EntityFrameworkCore;

namespace MetaFinance.Data.Financial.Repositories;

public class CategoryRepository(MetaFinanceContext context):ICategoryRepository
{
    public async Task<Category> CreateAsync(Category category)
    {
        try
        {
            await context.Categories.AddAsync(category);

            return category;
        }
        catch (DbUpdateException ex) 
        {
            throw new TransactionPersistenceException("Error adding transaction.", ex);
        }
    }

    public async Task<Category> UpdateAsync(Category category)
    {
        try
        {
            context.Categories.Update(category);

            await Task.CompletedTask;
            
            return category;
        }
        catch (DbUpdateException ex) 
        {
            throw new TransactionPersistenceException("Error adding transaction.", ex);
        }
    }

    public async Task DeleteAsync(Category category)
    {
        try
        {
            context.Categories.Remove(category);

            await Task.CompletedTask;
        }
        catch (DbUpdateException ex) 
        {
            throw new TransactionPersistenceException("Error adding transaction.", ex);
        }
    }

    public async Task<IEnumerable<Category>> GetAllAsync()
    {
        return await context
            .Categories
            .AsNoTracking()
            .ToListAsync();
    }

    public async Task<Category?> GetByIdAsync(Guid id)
    {
        return await context
            .Categories
            .FindAsync(id);
    }

    public async Task<Category?> GetByNameAsync(string name)
    {
        return await context
            .Categories
            .FirstOrDefaultAsync(c => c.Name == name);
    }
}