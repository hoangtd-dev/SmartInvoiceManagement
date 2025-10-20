using Microsoft.EntityFrameworkCore;
using SIM.Core.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SIM.Infrastructure.Configurations
{
    partial class TableConfigurations
    {
        static void TransactionCategoryConfigureTable(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<TransactionCategory>(entity =>
            {
                entity.Property(e => e.Name).IsRequired();
            });
        }
    }
}
