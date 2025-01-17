using MetaFinance.Domain.Financial.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace MetaFinance.Data.Financial.Mappings;

public class InstallmentMapping: IEntityTypeConfiguration<Installment>
{
    public void Configure(EntityTypeBuilder<Installment> builder)
    {
        builder.ToTable("Installments");

        builder.HasKey(x => x.Id);

        builder.Property(x => x.Id)
            .ValueGeneratedOnAdd();
        
        builder.Property(x => x.TransactionId)
            .IsRequired();
        
        builder.Property(x => x.InstallmentNumber)
            .IsRequired();
        
        builder.Property(x => x.TotalInstallments)
            .IsRequired();
        
        builder.Property(x => x.DueDate)
            .IsRequired();
        
        builder.OwnsOne(x => x.Amount, navigationBuilder =>
        {
            navigationBuilder.Property(p => p.Amount)
                .HasColumnName("Amount")
                .HasColumnType("decimal(18,2)")
                .IsRequired();
        });
        
        builder.Property(x => x.Status)
            .IsRequired()
            .HasConversion<int>();
        
        builder.Property(x => x.PaymentDate)
            .IsRequired(false);
        
        builder.Property(x => x.UserId)
            .IsRequired()
            .HasMaxLength(200);
        
        builder.HasOne(x => x.Transaction)
            .WithMany(x => x.Installments)
            .HasForeignKey(x => x.TransactionId)
            .OnDelete(DeleteBehavior.Cascade);
        
        builder.Property(x=> x.CreatedBy)
            .IsRequired()
            .HasMaxLength(200);
        
        builder.Property(e => e.LastModifiedBy)
            .HasMaxLength(250);
        
        builder.HasIndex(x => x.UserId)
            .HasDatabaseName("IX_Installments_UserId");
        
        builder.HasIndex(x => new { x.TransactionId, x.InstallmentNumber })
            .IsUnique()
            .HasDatabaseName("IX_Installments_TransactionId_InstallmentNumber");
    }
}