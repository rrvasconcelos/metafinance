using System.Reflection;
using MetaFinance.Domain.Financial.Entities;
using Microsoft.EntityFrameworkCore;

namespace MetaFinance.Data.Context;

public class MetaFinanceContext(DbContextOptions<MetaFinanceContext> options) : DbContext(options)
{
    public DbSet<Transaction> Transactions { get; set; }
    public DbSet<Installment> Installments { get; set; }
    public DbSet<Category> Categories { get; set; }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        base.OnModelCreating(modelBuilder);
        modelBuilder.ApplyConfigurationsFromAssembly(Assembly.GetExecutingAssembly());
    }
}