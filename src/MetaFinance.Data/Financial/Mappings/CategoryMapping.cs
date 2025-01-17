using MetaFinance.Domain.Financial.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace MetaFinance.Data.Financial.Mappings;

public class CategoryMapping : IEntityTypeConfiguration<Category>
{
    public void Configure(EntityTypeBuilder<Category> builder)
    {
        builder.ToTable("Categories");

        builder.HasKey(x => x.Id);

        builder.Property(x => x.Id)
            .ValueGeneratedOnAdd();
        
        builder.Property(x => x.Description)
            .IsRequired()
            .HasMaxLength(300);
        
        builder.Property(x => x.Type)
            .IsRequired()
            .HasConversion<int>();

        builder.Property(x => x.IsActive)
            .IsRequired();
        
        builder.Property(x=> x.CreatedBy)
            .IsRequired()
            .HasMaxLength(200);
        
        builder.Property(e => e.LastModifiedBy)
            .HasMaxLength(250);
        
        builder.HasIndex(x => x.Name)
            .IsUnique()
            .HasDatabaseName("IX_Categories_Name");
    }
}