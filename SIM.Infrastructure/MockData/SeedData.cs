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

            var invoice1Total = (1 * product1) + (2 * product2);
            var invoice2Total = 3 * product3;
            var invoice3Total = product4 + product5;
            var invoice4Total = product6 + (3 * product7);
            var invoice5Total = product8;

            modelBuilder.Entity<Invoice>().HasData(
                new Invoice
                {
                    Id = 1,
                    CreatedDate = new DateTime(2024, 2, 15),
                    TotalAmount = invoice1Total,
                    Status = InvoiceStatusEnum.Paid,
                    UserId = 1
                },
                new Invoice
                {
                    Id = 2,
                    CreatedDate = new DateTime(2024, 1, 20),
                    TotalAmount = invoice2Total,
                    Status = InvoiceStatusEnum.Paid,
                    UserId = 2
                },
                new Invoice
                {
                    Id = 3,
                    CreatedDate = new DateTime(2024, 2, 1),
                    TotalAmount = invoice3Total,
                    Status = InvoiceStatusEnum.Pending,
                    UserId = 1
                },
                new Invoice
                {
                    Id = 4,
                    CreatedDate = new DateTime(2024, 2, 5),
                    TotalAmount = invoice4Total,
                    Status = InvoiceStatusEnum.Pending,
                    UserId = 3
                },
                new Invoice
                {
                    Id = 5,
                    CreatedDate = new DateTime(2024, 2, 8),
                    TotalAmount = invoice5Total,
                    Status = InvoiceStatusEnum.Cancelled,
                    UserId = 2
                }
            );

            modelBuilder.Entity<InvoiceItem>().HasData(
                new InvoiceItem { Id = 1, InvoiceId = 1, ProductId = 1, Quantity = 1, Price = product1, Total = product1 * 1, CreatedDate = new DateTime(2024, 1, 1) },
                new InvoiceItem { Id = 2, InvoiceId = 1, ProductId = 2, Quantity = 2, Price = product2, Total = product2 * 2, CreatedDate = new DateTime(2024, 1, 1) },

                new InvoiceItem { Id = 3, InvoiceId = 2, ProductId = 3, Quantity = 3, Price = product3, Total = product3 * 3, CreatedDate = new DateTime(2024, 1, 1) },

                new InvoiceItem { Id = 4, InvoiceId = 3, ProductId = 5, Quantity = 1, Price = product4, Total = product4 * 1, CreatedDate = new DateTime(2024, 1, 1) },
                new InvoiceItem { Id = 5, InvoiceId = 3, ProductId = 6, Quantity = 1, Price = product5, Total = product5 * 1, CreatedDate = new DateTime(2024, 1, 1) },

                new InvoiceItem { Id = 6, InvoiceId = 4, ProductId = 4, Quantity = 1, Price = product6, Total = product6 * 1, CreatedDate = new DateTime(2024, 1, 1) },
                new InvoiceItem { Id = 7, InvoiceId = 4, ProductId = 3, Quantity = 3, Price = product7, Total = product7 * 3, CreatedDate = new DateTime(2024, 1, 1) },

                new InvoiceItem { Id = 8, InvoiceId = 5, ProductId = 2, Quantity = 1, Price = product8, Total = product8 * 1, CreatedDate = new DateTime(2024, 1, 1) }
            );
        }
    }

}
