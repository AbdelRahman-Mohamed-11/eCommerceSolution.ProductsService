using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using ProductsMicroService.BusinessLogic.Entities;

namespace ProductsMicroService.DataAccess.Configurations;

public class ProductConfiguration : IEntityTypeConfiguration<Product>
{
    public void Configure(EntityTypeBuilder<Product> builder)
    {
        builder.ToTable("Products");

        builder.Property(p => p.Name).HasMaxLength(100).IsRequired();
        builder.Property(p => p.UnitPrice).HasColumnType("decimal(10,2)");

        builder.HasIndex(c => c.Name);
    }
}