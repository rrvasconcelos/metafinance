using MetaFinance.Domain.Financial.Interfaces.Repositories;
using MetaFinance.Domain.SharedKernel.UnitOfWork;

namespace MetaFinance.Domain.Financial.Interfaces.UnitOfWork;

public interface IFinancialUnitOfWork : IUnitOfWork
{
    ITransactionRepository Transactions { get; } 
}