using Microsoft.EntityFrameworkCore;
using SIM.Core.Entities;

namespace SIM.Infrastructure.Configurations
{
    partial class TableConfigurations
    {
        static void BudgetConfigureTable(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Budget>(entity =>
            {
                entity.Property(e => e.TotalAmount).HasColumnType("decimal(18,2)").IsRequired();
                entity.Property(e => e.StartDate).IsRequired();
                entity.Property(e => e.EndDate).IsRequired();
                entity.Property(e => e.Status).IsRequired();

                entity.HasOne(t => t.Category)
                    .WithMany(c => c.Budgets)
                    .HasForeignKey(t => t.CategoryId)
                    .OnDelete(DeleteBehavior.Cascade);

                entity.HasOne(t => t.User)
                    .WithMany(c => c.Budgets)
                    .HasForeignKey(t => t.UserId)
                    .OnDelete(DeleteBehavior.Cascade);
            });
        }
    }
}
