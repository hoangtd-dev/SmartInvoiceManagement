using Microsoft.EntityFrameworkCore;
using SIM.Core.Entities;

namespace SIM.Infrastructure.Configurations
{
    partial class TableConfigurations
    {
        static void ProductConfigureTable(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Product>(entity =>
            {
                entity.Property(e => e.ProductName).IsRequired().HasMaxLength(200);
                entity.Property(e => e.Description).HasMaxLength(1000);
                entity.Property(e => e.Price).HasColumnType("decimal(18,2)").IsRequired();
                entity.Property(e => e.StockQuantity).IsRequired();
                
                entity.HasOne(p => p.Vendor)
                      .WithMany(v => v.Products)
                      .HasForeignKey(p => p.VendorId)
                      .OnDelete(DeleteBehavior.Restrict);
            });
        }
    }
}
