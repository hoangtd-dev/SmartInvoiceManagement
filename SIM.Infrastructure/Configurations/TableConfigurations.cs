using Microsoft.EntityFrameworkCore;

namespace SIM.Infrastructure.Configurations
{
    public partial class TableConfigurations
    {
        public static void CreateTable(ModelBuilder modelBuilder)
        {
            UserConfigureTable(modelBuilder);
            VendorConfigureTable(modelBuilder);
            ProductConfigureTable(modelBuilder);
            InvoiceConfigureTable(modelBuilder);
            InvoiceItemConfigureTable(modelBuilder);
        }
    }
}
