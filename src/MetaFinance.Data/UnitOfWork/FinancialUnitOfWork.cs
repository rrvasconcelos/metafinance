using MetaFinance.Data.Context;
using MetaFinance.Domain.Financial.Interfaces.Repositories;
using MetaFinance.Domain.Financial.Interfaces.UnitOfWork;

namespace MetaFinance.Data.UnitOfWork;

public class FinancialUnitOfWork(MetaFinanceContext context, ITransactionRepository repository)
    : IFinancialUnitOfWork
{
    public ITransactionRepository Transactions { get; } = repository;

    public async Task<int> SaveChangesAsync(CancellationToken cancellationToken = default)
    {
        return await context.SaveChangesAsync(cancellationToken);
    }
    
    public void Dispose()
    {
        GC.SuppressFinalize(this);
        context.Dispose();
    }
}