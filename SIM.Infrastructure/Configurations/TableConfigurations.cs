using Microsoft.EntityFrameworkCore;

namespace SIM.Infrastructure.Configurations
{
    public partial class TableConfigurations
    {
        public static void CreateTable(ModelBuilder modelBuilder)
        {
            UserConfigureTable(modelBuilder);
            VendorConfigureTable(modelBuilder);
            TransactionCategoryConfigureTable(modelBuilder);
            TransactionConfigureTable(modelBuilder);
            TransactionItemConfigureTable(modelBuilder);
            BudgetConfigureTable(modelBuilder);
        }
    }
}
