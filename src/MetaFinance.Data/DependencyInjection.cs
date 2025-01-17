using MetaFinance.Data.Context;
using MetaFinance.Data.Financial.Repositories;
using MetaFinance.Data.UnitOfWork;
using MetaFinance.Domain.Financial.Interfaces.Repositories;
using MetaFinance.Domain.Financial.Interfaces.UnitOfWork;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace MetaFinance.Data;

public static class DependencyInjection
{
    public static IServiceCollection AddInfrastructure(this IServiceCollection services, IConfiguration configuration)
    {
        services
            .AddDbContext<MetaFinanceContext>(option=>
                option.UseSqlServer(configuration.GetConnectionString("DefaultConnection")));

        services.AddScoped<ITransactionRepository, TransactionRepository>();
        services.AddScoped<ICategoryRepository, CategoryRepository>();
        services.AddScoped<ICategoryUnitOfWork, CategoryUnitOfWork>();

        return services;
    }
}