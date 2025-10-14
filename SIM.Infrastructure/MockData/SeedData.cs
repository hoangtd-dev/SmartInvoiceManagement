using Microsoft.EntityFrameworkCore;
using SIM.Core.Entities;
using SIM.Core.Enums;

namespace SIM.Infrastructure.MockData
{
    public static class SeedData
    {
        public static void Create(ModelBuilder modelBuilder)
        {
            // TODO: Update PasswordHash
            modelBuilder.Entity<User>().HasData(
                new User { Id = 1, FirstName = "Edward", PasswordHash = "100000.xoOaNrEukE+6bc1KUCkD+Q==.CP+sKGB54pKQ+qDs5yh1XMjrEOc2kJKniMlVGh17W7o=", LastName = "Tran", Email = "admin@gmail.com", Phone = "123-456-7890", Address = "123 Main St", CreatedDate = new DateTime(2024, 1, 1) }
            );

            modelBuilder.Entity<Vendor>().HasData(
                new Vendor { Id = 1, VendorName = "Tech Supplies Inc.", ContactEmail = "info@techsupplies.com", ContactPhone = "555-100-2000", Address = "789 Tech Park", CreatedDate = new DateTime(2024, 1, 1) },
                new Vendor { Id = 2, VendorName = "Office Essentials", ContactEmail = "sales@officeessentials.com", ContactPhone = "555-100-3000", Address = "321 Business Blvd", CreatedDate = new DateTime(2024, 1, 1) },
                new Vendor { Id = 3, VendorName = "Premium Electronics", ContactEmail = "support@premiumelectronics.com", ContactPhone = "555-100-4000", Address = "555 Digital Ave", CreatedDate = new DateTime(2024, 1, 1) }
            );

            var product1 = 999.99m;
            var product2 = 199.99m;
            var product3 = 149.99m;
            var product4 = 79.99m;
            var product5 = 29.99m;
            var product6 = 299.99m;
            var product7 = 149.99m;
            var product8 = 199.99m;

            modelBuilder.Entity<TransactionCategory>().HasData(
                new TransactionCategory { Id = 1, Name = "Food", CreatedDate = new DateTime(2024, 2, 15) },
                new TransactionCategory { Id = 2, Name = "Transportation", CreatedDate = new DateTime(2024, 2, 15) },
                new TransactionCategory { Id = 3, Name = "Housing", CreatedDate = new DateTime(2024, 2, 15) },
                new TransactionCategory { Id = 4, Name = "Salary", CreatedDate = new DateTime(2024, 2, 15) },
                new TransactionCategory { Id = 5, Name = "Electronics", CreatedDate = new DateTime(2024, 2, 15) },
                new TransactionCategory { Id = 6, Name = "Office Supplies", CreatedDate = new DateTime(2024, 2, 15) }
            );

            modelBuilder.Entity<Transaction>().HasData(
                // January 2025
                new Transaction { Id = 1, CreatedDate = new DateTime(2025, 1, 5), TotalAmount = 12000, TransactionType = TransactionTypeEnum.Income, UserId = 1, CategoryId = 4, VendorId = 3 },
                new Transaction { Id = 2, CreatedDate = new DateTime(2025, 1, 10), TotalAmount = product1 + product5, TransactionType = TransactionTypeEnum.Expense, UserId = 1, CategoryId = 5, VendorId = 1 },

                // February 2025
                new Transaction { Id = 3, CreatedDate = new DateTime(2025, 2, 1), TotalAmount = 12500, TransactionType = TransactionTypeEnum.Income, UserId = 1, CategoryId = 4, VendorId = 3 },
                new Transaction { Id = 4, CreatedDate = new DateTime(2025, 2, 15), TotalAmount = product3 * 2, TransactionType = TransactionTypeEnum.Expense, UserId = 1, CategoryId = 6, VendorId = 2 },

                // March 2025
                new Transaction { Id = 5, CreatedDate = new DateTime(2025, 3, 5), TotalAmount = 11800, TransactionType = TransactionTypeEnum.Income, UserId = 1, CategoryId = 4, VendorId = 3 },
                new Transaction { Id = 6, CreatedDate = new DateTime(2025, 3, 20), TotalAmount = product2 + product6, TransactionType = TransactionTypeEnum.Expense, UserId = 1, CategoryId = 5, VendorId = 1 },

                // April 2025
                new Transaction { Id = 7, CreatedDate = new DateTime(2025, 4, 3), TotalAmount = 13000, TransactionType = TransactionTypeEnum.Income, UserId = 1, CategoryId = 4, VendorId = 3 },
                new Transaction { Id = 8, CreatedDate = new DateTime(2025, 4, 18), TotalAmount = product4 + product7, TransactionType = TransactionTypeEnum.Expense, UserId = 1, CategoryId = 6, VendorId = 2 },

                // May 2025
                new Transaction { Id = 9, CreatedDate = new DateTime(2025, 5, 7), TotalAmount = 12200, TransactionType = TransactionTypeEnum.Income, UserId = 1, CategoryId = 4, VendorId = 3 },
                new Transaction { Id = 10, CreatedDate = new DateTime(2025, 5, 22), TotalAmount = product8 * 2, TransactionType = TransactionTypeEnum.Expense, UserId = 1, CategoryId = 5, VendorId = 3 },

                // June 2025
                new Transaction { Id = 11, CreatedDate = new DateTime(2025, 6, 4), TotalAmount = 12800, TransactionType = TransactionTypeEnum.Income, UserId = 1, CategoryId = 4, VendorId = 3 },
                new Transaction { Id = 12, CreatedDate = new DateTime(2025, 6, 19), TotalAmount = product1, TransactionType = TransactionTypeEnum.Expense, UserId = 1, CategoryId = 5, VendorId = 1 },

                // July 2025
                new Transaction { Id = 13, CreatedDate = new DateTime(2025, 7, 6), TotalAmount = 13500, TransactionType = TransactionTypeEnum.Income, UserId = 1, CategoryId = 1, VendorId = 3 },
                new Transaction { Id = 14, CreatedDate = new DateTime(2025, 7, 25), TotalAmount = product5 * 3 + product6, TransactionType = TransactionTypeEnum.Expense, UserId = 1, CategoryId = 6, VendorId = 1 },

                // August 2025
                new Transaction { Id = 15, CreatedDate = new DateTime(2025, 8, 2), TotalAmount = 12600, TransactionType = TransactionTypeEnum.Income, UserId = 1, CategoryId = 3, VendorId = 3 },
                new Transaction { Id = 16, CreatedDate = new DateTime(2025, 8, 16), TotalAmount = product3 + product4, TransactionType = TransactionTypeEnum.Expense, UserId = 1, CategoryId = 6, VendorId = 2 },

                // September 2025
                new Transaction { Id = 17, CreatedDate = new DateTime(2025, 9, 8), TotalAmount = 13200, TransactionType = TransactionTypeEnum.Income, UserId = 1, CategoryId = 3, VendorId = 3 },
                new Transaction { Id = 18, CreatedDate = new DateTime(2025, 9, 21), TotalAmount = product2 * 2, TransactionType = TransactionTypeEnum.Expense, UserId = 1, CategoryId = 5, VendorId = 1 },

                // October 2025
                new Transaction { Id = 19, CreatedDate = new DateTime(2025, 10, 1), TotalAmount = 12400, TransactionType = TransactionTypeEnum.Income, UserId = 1, CategoryId = 4, VendorId = 3 },
                new Transaction { Id = 20, CreatedDate = new DateTime(2025, 10, 28), TotalAmount = product7 + product8, TransactionType = TransactionTypeEnum.Expense, UserId = 1, CategoryId = 5, VendorId = 3 }
            );

            modelBuilder.Entity<TransactionItem>().HasData(
                // January
                new TransactionItem { Id = 1, TransactionId = 2, Quantity = 1, Price = product1, Total = product1, CreatedDate = new DateTime(2025, 1, 10) },
                new TransactionItem { Id = 2, TransactionId = 2, Quantity = 2, Price = product5, Total = product5, CreatedDate = new DateTime(2025, 2, 10) },

                // February
                new TransactionItem { Id = 3, TransactionId = 4, Quantity = 2, Price = product3, Total = product3 * 2, CreatedDate = new DateTime(2025, 2, 15) },

                // March
                new TransactionItem { Id = 4, TransactionId = 6, Quantity = 1, Price = product2, Total = product2, CreatedDate = new DateTime(2025, 3, 20) },
                new TransactionItem { Id = 5, TransactionId = 6, Quantity = 1, Price = product6, Total = product6, CreatedDate = new DateTime(2025, 3, 20) },

                // April
                new TransactionItem { Id = 6, TransactionId = 8, Quantity = 1, Price = product4, Total = product4, CreatedDate = new DateTime(2025, 4, 18) },
                new TransactionItem { Id = 7, TransactionId = 8, Quantity = 1, Price = product7, Total = product7, CreatedDate = new DateTime(2025, 4, 18) },

                // May
                new TransactionItem { Id = 8, TransactionId = 10, Quantity = 2, Price = product8, Total = product8 * 2, CreatedDate = new DateTime(2025, 5, 22) },

                // June
                new TransactionItem { Id = 9, TransactionId = 12, Quantity = 1, Price = product1, Total = product1, CreatedDate = new DateTime(2025, 6, 19) },

                // July
                new TransactionItem { Id = 10, TransactionId = 14, Quantity = 3, Price = product5, Total = product5 * 3, CreatedDate = new DateTime(2025, 7, 25) },
                new TransactionItem { Id = 11, TransactionId = 14, Quantity = 1, Price = product6, Total = product6, CreatedDate = new DateTime(2025, 7, 25) },

                // August
                new TransactionItem { Id = 12, TransactionId = 16, Quantity = 1, Price = product3, Total = product3, CreatedDate = new DateTime(2025, 8, 16) },
                new TransactionItem { Id = 13, TransactionId = 16, Quantity = 1, Price = product4, Total = product4, CreatedDate = new DateTime(2025, 8, 16) },

                // September
                new TransactionItem { Id = 14, TransactionId = 18, Quantity = 2, Price = product2, Total = product2 * 2, CreatedDate = new DateTime(2025, 9, 21) },

                // October
                new TransactionItem { Id = 15, TransactionId = 20, Quantity = 1, Price = product7, Total = product7, CreatedDate = new DateTime(2025, 10, 28) },
                new TransactionItem { Id = 16, TransactionId = 20, Quantity = 1, Price = product8, Total = product8, CreatedDate = new DateTime(2025, 10, 28) }
            );

            modelBuilder.Entity<Budget>().HasData(
                new Budget
                {
                    Id = 1,
                    IsDeleted = false,
                    CreatedDate = new DateTime(2024, 1, 1),
                    TotalAmount = 500.00m,
                    TotalExpense = 450.00m,
                    StartDate = new DateTime(2024, 1, 1),
                    EndDate = new DateTime(2024, 1, 31),
                    Status = BudgetStatusEnum.Expired,
                    CategoryId = 1,
                    UserId = 1
                },
                new Budget
                {
                    Id = 2,
                    IsDeleted = false,
                    CreatedDate = new DateTime(2024, 1, 1),
                    TotalAmount = 300.00m,
                    TotalExpense = 0m,
                    StartDate = new DateTime(2024, 1, 1),
                    EndDate = new DateTime(2024, 1, 31),
                    Status = BudgetStatusEnum.Expired,
                    CategoryId = 2,
                    UserId = 1
                },
                new Budget
                {
                    Id = 3,
                    IsDeleted = false,
                    CreatedDate = new DateTime(2024, 1, 1),
                    TotalAmount = 1200.00m,
                    TotalExpense = 100m,
                    StartDate = new DateTime(2024, 1, 1),
                    EndDate = new DateTime(2024, 1, 31),
                    Status = BudgetStatusEnum.Expired,
                    CategoryId = 3,
                    UserId = 1
                },
                new Budget
                {
                    Id = 4,
                    IsDeleted = false,
                    CreatedDate = new DateTime(2024, 2, 1),
                    TotalAmount = 550.00m,
                    TotalExpense = 0,
                    StartDate = new DateTime(2024, 2, 1),
                    EndDate = new DateTime(2024, 2, 29),
                    Status = BudgetStatusEnum.Expired,
                    CategoryId = 1,
                    UserId = 1
                },
                new Budget
                {
                    Id = 5,
                    IsDeleted = false,
                    CreatedDate = new DateTime(2024, 2, 1),
                    TotalAmount = 350.00m,
                    TotalExpense = 0,
                    StartDate = new DateTime(2024, 2, 1),
                    EndDate = new DateTime(2024, 2, 29),
                    Status = BudgetStatusEnum.Expired,
                    CategoryId = 2,
                    UserId = 1
                },
                new Budget
                {
                    Id = 6,
                    IsDeleted = false,
                    CreatedDate = new DateTime(2024, 2, 1),
                    TotalAmount = 1200.00m,
                    TotalExpense = 0,
                    StartDate = new DateTime(2024, 2, 1),
                    EndDate = new DateTime(2024, 2, 29),
                    Status = BudgetStatusEnum.Expired,
                    CategoryId = 3,
                    UserId = 1
                },
                new Budget
                {
                    Id = 7,
                    IsDeleted = false,
                    CreatedDate = new DateTime(2024, 3, 1),
                    TotalAmount = 600.00m,
                    TotalExpense = 0,
                    StartDate = new DateTime(2024, 3, 1),
                    EndDate = new DateTime(2024, 3, 31),
                    Status = BudgetStatusEnum.Expired,
                    CategoryId = 1,
                    UserId = 1
                },
                new Budget
                {
                    Id = 8,
                    IsDeleted = false,
                    CreatedDate = new DateTime(2024, 3, 1),
                    TotalAmount = 400.00m,
                    TotalExpense = 0,
                    StartDate = new DateTime(2024, 3, 1),
                    EndDate = new DateTime(2024, 3, 31),
                    Status = BudgetStatusEnum.Expired,
                    CategoryId = 2,
                    UserId = 1
                },
                new Budget
                {
                    Id = 9,
                    IsDeleted = false,
                    CreatedDate = new DateTime(2024, 3, 1),
                    TotalAmount = 1200.00m,
                    TotalExpense = 0,
                    StartDate = new DateTime(2024, 3, 1),
                    EndDate = new DateTime(2024, 3, 31),
                    Status = BudgetStatusEnum.Expired,
                    CategoryId = 3,
                    UserId = 1
                },
                new Budget
                {
                    Id = 10,
                    IsDeleted = false,
                    CreatedDate = new DateTime(2024, 4, 1),
                    TotalAmount = 650.00m,
                    TotalExpense = 700.00m,
                    StartDate = new DateTime(2025, 7, 1),
                    EndDate = new DateTime(2025, 9, 30),
                    Status = BudgetStatusEnum.Active,
                    CategoryId = 1,
                    UserId = 1
                },
                new Budget
                {
                    Id = 11,
                    IsDeleted = false,
                    CreatedDate = new DateTime(2024, 4, 1),
                    TotalAmount = 450.00m,
                    TotalExpense = 100.00m,
                    StartDate = new DateTime(2025, 7, 1),
                    EndDate = new DateTime(2025, 9, 30),
                    Status = BudgetStatusEnum.Active,
                    CategoryId = 2,
                    UserId = 1
                },
                new Budget
                {
                    Id = 12,
                    IsDeleted = false,
                    CreatedDate = new DateTime(2024, 4, 1),
                    TotalAmount = 1250.00m,
                    TotalExpense = 1100.00m,
                    StartDate = new DateTime(2025, 7, 1),
                    EndDate = new DateTime(2025, 9, 30),
                    Status = BudgetStatusEnum.Active,
                    CategoryId = 3,
                    UserId = 1
                }
            );
        }
    }

}
