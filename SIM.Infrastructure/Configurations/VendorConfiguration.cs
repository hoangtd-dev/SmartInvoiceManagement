using Microsoft.EntityFrameworkCore;
using SIM.Core.Entities;

namespace SIM.Infrastructure.Configurations
{
    partial class TableConfigurations
    {
        static void VendorConfigureTable(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Vendor>(entity =>
            {
                entity.Property(e => e.VendorName).IsRequired().HasMaxLength(200);
                entity.Property(e => e.ContactEmail).HasMaxLength(255);
                entity.Property(e => e.ContactPhone).HasMaxLength(20);
                entity.Property(e => e.Address).HasMaxLength(500);
            });
        }
    }
}
