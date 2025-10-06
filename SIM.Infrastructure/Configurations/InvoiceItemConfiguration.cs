using Microsoft.EntityFrameworkCore;
using SIM.Core.Entities;

namespace SIM.Infrastructure.Configurations
{
    partial class TableConfigurations
    {
        static void InvoiceItemConfigureTable(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<InvoiceItem>(entity =>
            {
                entity.Property(e => e.Quantity).IsRequired();
                entity.Property(e => e.Price).HasColumnType("decimal(18,2)").IsRequired();
                entity.Property(e => e.Total).HasColumnType("decimal(18,2)").IsRequired();

                entity.HasOne(ii => ii.Invoice)
                      .WithMany(i => i.InvoiceItems)
                      .HasForeignKey(ii => ii.InvoiceId)
                      .OnDelete(DeleteBehavior.Cascade);

                entity.HasOne(ii => ii.Product)
                      .WithMany(p => p.InvoiceItems)
                      .HasForeignKey(ii => ii.ProductId)
                      .OnDelete(DeleteBehavior.Restrict);
            });
        }
    }
}
