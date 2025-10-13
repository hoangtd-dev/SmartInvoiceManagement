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

            modelBuilder.Entity<Product>().HasData(
                new Product { Id = 1, ProductName = "Laptop", Description = "High-performance laptop", Price = product1, StockQuantity = 50, VendorId = 1, CreatedDate = new DateTime(2024, 1, 1) },
                new Product { Id = 2, ProductName = "Monitor", Description = "24-inch LED monitor", Price = product2, StockQuantity = 100, VendorId = 1, CreatedDate = new DateTime(2024, 1, 1) },
                new Product { Id = 3, ProductName = "Office Chair", Description = "Ergonomic office chair", Price = product3, StockQuantity = 75, VendorId = 2, CreatedDate = new DateTime(2024, 1, 1) },
                new Product { Id = 4, ProductName = "Desk", Description = "Standing desk", Price = product4, StockQuantity = 30, VendorId = 2, CreatedDate = new DateTime(2024, 1, 1) },
                new Product { Id = 5, ProductName = "Keyboard", Description = "Mechanical keyboard", Price = product5, StockQuantity = 200, VendorId = 1, CreatedDate = new DateTime(2024, 1, 1) },
                new Product { Id = 6, ProductName = "Mouse", Description = "Wireless mouse", Price = product6, StockQuantity = 150, VendorId = 1, CreatedDate = new DateTime(2024, 1, 1) },
                new Product { Id = 7, ProductName = "Webcam", Description = "HD webcam", Price = product7, StockQuantity = 80, VendorId = 3, CreatedDate = new DateTime(2024, 1, 1) },
                new Product { Id = 8, ProductName = "Headset", Description = "Noise-cancelling headset", Price = product8, StockQuantity = 60, VendorId = 3, CreatedDate = new DateTime(2024, 1, 1) }
            );

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
                new Transaction { Id = 13, CreatedDate = new DateTime(2025, 7, 6), TotalAmount = 13500, TransactionType = TransactionTypeEnum.Income, UserId = 1, CategoryId = 4, VendorId = 3 },
                new Transaction { Id = 14, CreatedDate = new DateTime(2025, 7, 25), TotalAmount = product5 * 3 + product6, TransactionType = TransactionTypeEnum.Expense, UserId = 1, CategoryId = 6, VendorId = 1 },

                // August 2025
                new Transaction { Id = 15, CreatedDate = new DateTime(2025, 8, 2), TotalAmount = 12600, TransactionType = TransactionTypeEnum.Income, UserId = 1, CategoryId = 4, VendorId = 3 },
                new Transaction { Id = 16, CreatedDate = new DateTime(2025, 8, 16), TotalAmount = product3 + product4, TransactionType = TransactionTypeEnum.Expense, UserId = 1, CategoryId = 6, VendorId = 2 },

                // September 2025
                new Transaction { Id = 17, CreatedDate = new DateTime(2025, 9, 8), TotalAmount = 13200, TransactionType = TransactionTypeEnum.Income, UserId = 1, CategoryId = 4, VendorId = 3 },
                new Transaction { Id = 18, CreatedDate = new DateTime(2025, 9, 21), TotalAmount = product2 * 2, TransactionType = TransactionTypeEnum.Expense, UserId = 1, CategoryId = 5, VendorId = 1 },

                // October 2025
                new Transaction { Id = 19, CreatedDate = new DateTime(2025, 10, 1), TotalAmount = 12400, TransactionType = TransactionTypeEnum.Income, UserId = 1, CategoryId = 4, VendorId = 3 },
                new Transaction { Id = 20, CreatedDate = new DateTime(2025, 10, 28), TotalAmount = product7 + product8, TransactionType = TransactionTypeEnum.Expense, UserId = 1, CategoryId = 5, VendorId = 3 }
            );

            modelBuilder.Entity<TransactionItem>().HasData(
                // January
                new TransactionItem { Id = 1, TransactionId = 2, ProductId = 1, Quantity = 1, Price = product1, Total = product1, CreatedDate = new DateTime(2025, 1, 10) },
                new TransactionItem { Id = 2, TransactionId = 2, ProductId = 5, Quantity = 1, Price = product5, Total = product5, CreatedDate = new DateTime(2025, 1, 10) },

                // February
                new TransactionItem { Id = 3, TransactionId = 4, ProductId = 3, Quantity = 2, Price = product3, Total = product3 * 2, CreatedDate = new DateTime(2025, 2, 15) },

                // March
                new TransactionItem { Id = 4, TransactionId = 6, ProductId = 2, Quantity = 1, Price = product2, Total = product2, CreatedDate = new DateTime(2025, 3, 20) },
                new TransactionItem { Id = 5, TransactionId = 6, ProductId = 6, Quantity = 1, Price = product6, Total = product6, CreatedDate = new DateTime(2025, 3, 20) },

                // April
                new TransactionItem { Id = 6, TransactionId = 8, ProductId = 4, Quantity = 1, Price = product4, Total = product4, CreatedDate = new DateTime(2025, 4, 18) },
                new TransactionItem { Id = 7, TransactionId = 8, ProductId = 7, Quantity = 1, Price = product7, Total = product7, CreatedDate = new DateTime(2025, 4, 18) },

                // May
                new TransactionItem { Id = 8, TransactionId = 10, ProductId = 8, Quantity = 2, Price = product8, Total = product8 * 2, CreatedDate = new DateTime(2025, 5, 22) },

                // June
                new TransactionItem { Id = 9, TransactionId = 12, ProductId = 1, Quantity = 1, Price = product1, Total = product1, CreatedDate = new DateTime(2025, 6, 19) },

                // July
                new TransactionItem { Id = 10, TransactionId = 14, ProductId = 5, Quantity = 3, Price = product5, Total = product5 * 3, CreatedDate = new DateTime(2025, 7, 25) },
                new TransactionItem { Id = 11, TransactionId = 14, ProductId = 6, Quantity = 1, Price = product6, Total = product6, CreatedDate = new DateTime(2025, 7, 25) },

                // August
                new TransactionItem { Id = 12, TransactionId = 16, ProductId = 3, Quantity = 1, Price = product3, Total = product3, CreatedDate = new DateTime(2025, 8, 16) },
                new TransactionItem { Id = 13, TransactionId = 16, ProductId = 4, Quantity = 1, Price = product4, Total = product4, CreatedDate = new DateTime(2025, 8, 16) },

                // September
                new TransactionItem { Id = 14, TransactionId = 18, ProductId = 2, Quantity = 2, Price = product2, Total = product2 * 2, CreatedDate = new DateTime(2025, 9, 21) },

                // October
                new TransactionItem { Id = 15, TransactionId = 20, ProductId = 7, Quantity = 1, Price = product7, Total = product7, CreatedDate = new DateTime(2025, 10, 28) },
                new TransactionItem { Id = 16, TransactionId = 20, ProductId = 8, Quantity = 1, Price = product8, Total = product8, CreatedDate = new DateTime(2025, 10, 28) }
            );
        }
    }

}
