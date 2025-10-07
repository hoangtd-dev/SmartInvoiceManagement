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
                new User { Id = 1, FirstName = "John", PasswordHash = "abc@123", LastName = "Doe", Email = "john.doe@email.com", Phone = "123-456-7890", Address = "123 Main St", CreatedDate = new DateTime(2024, 1, 1) },
                new User { Id = 2, FirstName = "Jane", PasswordHash = "abc@123", LastName = "Smith", Email = "jane.smith@email.com", Phone = "123-456-7891", Address = "456 Oak Ave", CreatedDate = new DateTime(2024, 1, 1) },
                new User { Id = 3, FirstName = "Bob", PasswordHash = "abc@123", LastName = "Johnson", Email = "bob.johnson@email.com", Phone = "123-456-7892", Address = "789 Pine Rd", CreatedDate = new DateTime(2024, 1, 1) },
                new User { Id = 4, FirstName = "Alice", PasswordHash = "abc@123", LastName = "Brown", Email = "alice.brown@email.com", Phone = "123-456-7893", Address = "321 Elm St", CreatedDate = new DateTime(2024, 1, 1) },
                new User { Id = 5, FirstName = "Charlie", PasswordHash = "abc@123", LastName = "Wilson", Email = "charlie.wilson@email.com", Phone = "123-456-7894", Address = "654 Maple Dr", CreatedDate = new DateTime(2024, 1, 1) }
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
                new TransactionCategory
                { 
                    Id = 1,
                    Name = "Food",
                    CreatedDate = new DateTime(2024, 2, 15)
                },
                new TransactionCategory
                {
                    Id = 2,
                    Name = "Transportation",
                    CreatedDate = new DateTime(2024, 2, 15)
                },
                new TransactionCategory
                {
                    Id = 3,
                    Name = "Housing",
                    CreatedDate = new DateTime(2024, 2, 15)
                }
            );

            var transaction1Total = (1 * product1) + (2 * product2);
            var transaction2Total = 3 * product3;
            var transaction3Total = product4 + product5;
            var transaction4Total = product6 + (3 * product7);
            var transaction5Total = product8;

            modelBuilder.Entity<Transaction>().HasData(
                new Transaction
                {
                    Id = 1,
                    CreatedDate = new DateTime(2024, 2, 15),
                    TotalAmount = transaction1Total,
                    TransactionType = TransactionTypeEnum.Expense,
                    UserId = 1,
                    CategoryId = 1
                },
                new Transaction
                {
                    Id = 2,
                    CreatedDate = new DateTime(2024, 1, 20),
                    TotalAmount = transaction2Total,
                    TransactionType = TransactionTypeEnum.Expense,
                    UserId = 2,
                    CategoryId = 2
                },
                new Transaction
                {
                    Id = 3,
                    CreatedDate = new DateTime(2024, 2, 1),
                    TotalAmount = transaction3Total,
                    TransactionType = TransactionTypeEnum.Expense,
                    UserId = 1,
                    CategoryId = 3
                },
                new Transaction
                {
                    Id = 4,
                    CreatedDate = new DateTime(2024, 2, 5),
                    TotalAmount = transaction4Total,
                    TransactionType = TransactionTypeEnum.Expense,
                    UserId = 3,
                    CategoryId = 1
                },
                new Transaction
                {
                    Id = 5,
                    CreatedDate = new DateTime(2024, 2, 8),
                    TotalAmount = transaction5Total,
                    TransactionType = TransactionTypeEnum.Expense,
                    UserId = 2,
                    CategoryId = 1
                },
                new Transaction
                {
                    Id = 6,
                    CreatedDate = new DateTime(2024, 2, 8),
                    TotalAmount = 10000,
                    TransactionType = TransactionTypeEnum.Income,
                    UserId = 1
                },
                new Transaction
                {
                    Id = 7,
                    CreatedDate = new DateTime(2024, 2, 8),
                    TotalAmount = 20000,
                    TransactionType = TransactionTypeEnum.Income,
                    UserId = 2
                }
            );

            modelBuilder.Entity<TransactionItem>().HasData(
                new TransactionItem { Id = 1, TransactionId = 1, ProductId = 1, Quantity = 1, Price = product1, Total = product1 * 1, CreatedDate = new DateTime(2024, 1, 1) },
                new TransactionItem { Id = 2, TransactionId = 1, ProductId = 2, Quantity = 2, Price = product2, Total = product2 * 2, CreatedDate = new DateTime(2024, 1, 1) },

                new TransactionItem { Id = 3, TransactionId = 2, ProductId = 3, Quantity = 3, Price = product3, Total = product3 * 3, CreatedDate = new DateTime(2024, 1, 1) },

                new TransactionItem { Id = 4, TransactionId = 3, ProductId = 5, Quantity = 1, Price = product4, Total = product4 * 1, CreatedDate = new DateTime(2024, 1, 1) },
                new TransactionItem { Id = 5, TransactionId = 3, ProductId = 6, Quantity = 1, Price = product5, Total = product5 * 1, CreatedDate = new DateTime(2024, 1, 1) },

                new TransactionItem { Id = 6, TransactionId = 4, ProductId = 4, Quantity = 1, Price = product6, Total = product6 * 1, CreatedDate = new DateTime(2024, 1, 1) },
                new TransactionItem { Id = 7, TransactionId = 4, ProductId = 3, Quantity = 3, Price = product7, Total = product7 * 3, CreatedDate = new DateTime(2024, 1, 1) },

                new TransactionItem { Id = 8, TransactionId = 5, ProductId = 2, Quantity = 1, Price = product8, Total = product8 * 1, CreatedDate = new DateTime(2024, 1, 1) }
            );
        }
    }

}
