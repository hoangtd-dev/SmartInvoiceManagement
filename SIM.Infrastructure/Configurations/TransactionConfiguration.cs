using Microsoft.EntityFrameworkCore;
using SIM.Core.Entities;

namespace SIM.Infrastructure.Configurations
{
    partial class TableConfigurations
    {
        static void TransactionConfigureTable(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Transaction>(entity =>
            {
                entity.Property(e => e.TotalAmount).HasColumnType("decimal(18,2)").IsRequired();
                entity.Property(e => e.TransactionType).IsRequired();

                entity.HasOne(i => i.User)
                      .WithMany(c => c.Transactions)
                      .HasForeignKey(i => i.UserId)
                      .OnDelete(DeleteBehavior.Restrict);
            });
        }
    }
}
