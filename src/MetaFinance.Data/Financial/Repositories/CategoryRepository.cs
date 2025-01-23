using MetaFinance.Data.Context;
using MetaFinance.Domain.Financial.Entities;
using MetaFinance.Domain.Financial.Interfaces.Repositories;
using MetaFinance.Domain.SharedKernel.Exceptions;
using Microsoft.EntityFrameworkCore;

namespace MetaFinance.Data.Financial.Repositories;

public class CategoryRepository(MetaFinanceContext context) : ICategoryRepository
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

    public async Task<IEnumerable<Category>> GetAllAsync(CancellationToken cancellationToken)
    {
        return await context
            .Categories
            .AsNoTracking()
            .ToListAsync(cancellationToken);
    }

    public async Task<Category?> GetByIdAsync(int id)
    {
        return await context
            .Categories
            .FindAsync(id);
    }

    public async Task<bool> ExistsByNameAsync(string name, CancellationToken cancellationToken)
    {
        return await context
            .Categories
            .AsNoTracking()
            .AnyAsync(c => c.Name == name, cancellationToken);
    }

    public async Task<bool> ExistsByNameAndDifferentIdAsync(string name, int currentId, CancellationToken cancellationToken)
    {
        return await context.Categories.AnyAsync(c => 
                EF.Functions.Like(c.Name, name) &&  
                c.Id != currentId, 
            cancellationToken);
    }
}