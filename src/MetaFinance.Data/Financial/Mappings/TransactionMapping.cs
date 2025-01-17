using MetaFinance.Domain.Financial.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace MetaFinance.Data.Financial.Mappings;

public class TransactionMapping: IEntityTypeConfiguration<Transaction>
{
    public void Configure(EntityTypeBuilder<Transaction> builder)
    {
        builder.ToTable("Transactions");

        builder.HasKey(x => x.Id);

        builder.Property(x => x.Id)
            .ValueGeneratedOnAdd();
        
        builder.Property(x => x.Description)
            .IsRequired()
            .HasMaxLength(250);
        
        builder.OwnsOne(x => x.Amount, navigationBuilder =>
        {
            navigationBuilder.Property(p => p.Amount)
                .HasColumnName("Amount")
                .HasColumnType("decimal(18,2)")
                .IsRequired();
        });
        
        builder.Property(x => x.CreatedDate)
            .IsRequired();
        
        builder.Property(x => x.Type)
            .IsRequired()
            .HasConversion<int>();
        
        builder.Property(x => x.Status)
            .IsRequired()
            .HasConversion<int>();

        builder.Property(x => x.TotalInstallments)
            .IsRequired(false);

        builder.Property(x => x.CategoryId)
            .IsRequired();

        builder.Property(x => x.UserId)
            .IsRequired()
            .HasMaxLength(200);
        
        builder.HasOne(x => x.Category)
            .WithMany()
            .HasForeignKey(x => x.CategoryId)
            .OnDelete(DeleteBehavior.Restrict);
        
        builder.Property(x=> x.CreatedBy)
            .IsRequired()
            .HasMaxLength(200);
        
        builder.Property(e => e.LastModifiedBy)
            .HasMaxLength(250);
        
        builder.HasIndex(x => x.UserId)
            .HasDatabaseName("IX_Transactions_UserId");
        
        builder.HasIndex(x => new { x.CreatedDate, x.CategoryId })
            .HasDatabaseName("IX_Transactions_CreatedDate_CategoryId");
    }
}