using Microsoft.EntityFrameworkCore;
using SIM.Core.Entities;

namespace SIM.Infrastructure.Configurations
{
    partial class TableConfigurations
    {
        static void TransactionItemConfigureTable(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<TransactionItem>(entity =>
            {
                entity.Property(e => e.Quantity).IsRequired();
                entity.Property(e => e.Price).HasColumnType("decimal(18,2)").IsRequired();
                entity.Property(e => e.Total).HasColumnType("decimal(18,2)").IsRequired();

                entity.HasOne(ti => ti.Transtraction)
                      .WithMany(i => i.TransactionItems)
                      .HasForeignKey(ti => ti.TransactionId)
                      .OnDelete(DeleteBehavior.Cascade);
            });
        }
    }
}
