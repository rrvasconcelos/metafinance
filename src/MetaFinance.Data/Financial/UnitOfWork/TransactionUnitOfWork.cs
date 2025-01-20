using MetaFinance.Data.Context;
using MetaFinance.Domain.Financial.Interfaces.Repositories;
using MetaFinance.Domain.Financial.Interfaces.UnitOfWork;

namespace MetaFinance.Data.Financial.UnitOfWork;

public class TransactionUnitOfWork(MetaFinanceContext context, ITransactionRepository repository)
    : ITransactionUnitOfWork
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