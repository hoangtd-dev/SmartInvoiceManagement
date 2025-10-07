using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace SIM.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class InitialCreate : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "TransactionCategory",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Description = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    CreatedDate = table.Column<DateTime>(type: "datetime2", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_TransactionCategory", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Users",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    FirstName = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: false),
                    LastName = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: false),
                    PasswordHash = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Email = table.Column<string>(type: "nvarchar(255)", maxLength: 255, nullable: true),
                    Phone = table.Column<string>(type: "nvarchar(20)", maxLength: 20, nullable: true),
                    Address = table.Column<string>(type: "nvarchar(255)", maxLength: 255, nullable: true),
                    CreatedDate = table.Column<DateTime>(type: "datetime2", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Users", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Vendors",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    VendorName = table.Column<string>(type: "nvarchar(200)", maxLength: 200, nullable: false),
                    ContactEmail = table.Column<string>(type: "nvarchar(255)", maxLength: 255, nullable: true),
                    ContactPhone = table.Column<string>(type: "nvarchar(20)", maxLength: 20, nullable: true),
                    Address = table.Column<string>(type: "nvarchar(500)", maxLength: 500, nullable: true),
                    CreatedDate = table.Column<DateTime>(type: "datetime2", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Vendors", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Transactions",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    TotalAmount = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    TransactionType = table.Column<int>(type: "int", nullable: false),
                    CategoryId = table.Column<int>(type: "int", nullable: true),
                    UserId = table.Column<int>(type: "int", nullable: false),
                    CreatedDate = table.Column<DateTime>(type: "datetime2", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Transactions", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Transactions_TransactionCategory_CategoryId",
                        column: x => x.CategoryId,
                        principalTable: "TransactionCategory",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_Transactions_Users_UserId",
                        column: x => x.UserId,
                        principalTable: "Users",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "Products",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    ProductName = table.Column<string>(type: "nvarchar(200)", maxLength: 200, nullable: false),
                    Description = table.Column<string>(type: "nvarchar(1000)", maxLength: 1000, nullable: true),
                    Price = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    StockQuantity = table.Column<int>(type: "int", nullable: false),
                    ImageBase64 = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    VendorId = table.Column<int>(type: "int", nullable: false),
                    CreatedDate = table.Column<DateTime>(type: "datetime2", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Products", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Products_Vendors_VendorId",
                        column: x => x.VendorId,
                        principalTable: "Vendors",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "TransactionItems",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Quantity = table.Column<int>(type: "int", nullable: false),
                    Price = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    Total = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    TransactionId = table.Column<int>(type: "int", nullable: false),
                    ProductId = table.Column<int>(type: "int", nullable: false),
                    CreatedDate = table.Column<DateTime>(type: "datetime2", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_TransactionItems", x => x.Id);
                    table.ForeignKey(
                        name: "FK_TransactionItems_Products_ProductId",
                        column: x => x.ProductId,
                        principalTable: "Products",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_TransactionItems_Transactions_TransactionId",
                        column: x => x.TransactionId,
                        principalTable: "Transactions",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.InsertData(
                table: "TransactionCategory",
                columns: new[] { "Id", "CreatedDate", "Description", "Name" },
                values: new object[,]
                {
                    { 1, new DateTime(2024, 2, 15, 0, 0, 0, 0, DateTimeKind.Unspecified), null, "Food" },
                    { 2, new DateTime(2024, 2, 15, 0, 0, 0, 0, DateTimeKind.Unspecified), null, "Transportation" },
                    { 3, new DateTime(2024, 2, 15, 0, 0, 0, 0, DateTimeKind.Unspecified), null, "Housing" }
                });

            migrationBuilder.InsertData(
                table: "Users",
                columns: new[] { "Id", "Address", "CreatedDate", "Email", "FirstName", "LastName", "PasswordHash", "Phone" },
                values: new object[,]
                {
                    { 1, "123 Main St", new DateTime(2024, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), "john.doe@email.com", "John", "Doe", "abc@123", "123-456-7890" },
                    { 2, "456 Oak Ave", new DateTime(2024, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), "jane.smith@email.com", "Jane", "Smith", "abc@123", "123-456-7891" },
                    { 3, "789 Pine Rd", new DateTime(2024, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), "bob.johnson@email.com", "Bob", "Johnson", "abc@123", "123-456-7892" },
                    { 4, "321 Elm St", new DateTime(2024, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), "alice.brown@email.com", "Alice", "Brown", "abc@123", "123-456-7893" },
                    { 5, "654 Maple Dr", new DateTime(2024, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), "charlie.wilson@email.com", "Charlie", "Wilson", "abc@123", "123-456-7894" }
                });

            migrationBuilder.InsertData(
                table: "Vendors",
                columns: new[] { "Id", "Address", "ContactEmail", "ContactPhone", "CreatedDate", "VendorName" },
                values: new object[,]
                {
                    { 1, "789 Tech Park", "info@techsupplies.com", "555-100-2000", new DateTime(2024, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), "Tech Supplies Inc." },
                    { 2, "321 Business Blvd", "sales@officeessentials.com", "555-100-3000", new DateTime(2024, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), "Office Essentials" },
                    { 3, "555 Digital Ave", "support@premiumelectronics.com", "555-100-4000", new DateTime(2024, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), "Premium Electronics" }
                });

            migrationBuilder.InsertData(
                table: "Products",
                columns: new[] { "Id", "CreatedDate", "Description", "ImageBase64", "Price", "ProductName", "StockQuantity", "VendorId" },
                values: new object[,]
                {
                    { 1, new DateTime(2024, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), "High-performance laptop", "", 999.99m, "Laptop", 50, 1 },
                    { 2, new DateTime(2024, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), "24-inch LED monitor", "", 199.99m, "Monitor", 100, 1 },
                    { 3, new DateTime(2024, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), "Ergonomic office chair", "", 149.99m, "Office Chair", 75, 2 },
                    { 4, new DateTime(2024, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), "Standing desk", "", 79.99m, "Desk", 30, 2 },
                    { 5, new DateTime(2024, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), "Mechanical keyboard", "", 29.99m, "Keyboard", 200, 1 },
                    { 6, new DateTime(2024, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), "Wireless mouse", "", 299.99m, "Mouse", 150, 1 },
                    { 7, new DateTime(2024, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), "HD webcam", "", 149.99m, "Webcam", 80, 3 },
                    { 8, new DateTime(2024, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), "Noise-cancelling headset", "", 199.99m, "Headset", 60, 3 }
                });

            migrationBuilder.InsertData(
                table: "Transactions",
                columns: new[] { "Id", "CategoryId", "CreatedDate", "TotalAmount", "TransactionType", "UserId" },
                values: new object[,]
                {
                    { 1, 1, new DateTime(2024, 2, 15, 0, 0, 0, 0, DateTimeKind.Unspecified), 1399.97m, 1, 1 },
                    { 2, 2, new DateTime(2024, 1, 20, 0, 0, 0, 0, DateTimeKind.Unspecified), 449.97m, 1, 2 },
                    { 3, 3, new DateTime(2024, 2, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), 109.98m, 1, 1 },
                    { 4, 1, new DateTime(2024, 2, 5, 0, 0, 0, 0, DateTimeKind.Unspecified), 749.96m, 1, 3 },
                    { 5, 1, new DateTime(2024, 2, 8, 0, 0, 0, 0, DateTimeKind.Unspecified), 199.99m, 1, 2 },
                    { 6, null, new DateTime(2024, 2, 8, 0, 0, 0, 0, DateTimeKind.Unspecified), 10000m, 0, 1 },
                    { 7, null, new DateTime(2024, 2, 8, 0, 0, 0, 0, DateTimeKind.Unspecified), 20000m, 0, 2 }
                });

            migrationBuilder.InsertData(
                table: "TransactionItems",
                columns: new[] { "Id", "CreatedDate", "Price", "ProductId", "Quantity", "Total", "TransactionId" },
                values: new object[,]
                {
                    { 1, new DateTime(2024, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), 999.99m, 1, 1, 999.99m, 1 },
                    { 2, new DateTime(2024, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), 199.99m, 2, 2, 399.98m, 1 },
                    { 3, new DateTime(2024, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), 149.99m, 3, 3, 449.97m, 2 },
                    { 4, new DateTime(2024, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), 79.99m, 5, 1, 79.99m, 3 },
                    { 5, new DateTime(2024, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), 29.99m, 6, 1, 29.99m, 3 },
                    { 6, new DateTime(2024, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), 299.99m, 4, 1, 299.99m, 4 },
                    { 7, new DateTime(2024, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), 149.99m, 3, 3, 449.97m, 4 },
                    { 8, new DateTime(2024, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), 199.99m, 2, 1, 199.99m, 5 }
                });

            migrationBuilder.CreateIndex(
                name: "IX_Products_VendorId",
                table: "Products",
                column: "VendorId");

            migrationBuilder.CreateIndex(
                name: "IX_TransactionItems_ProductId",
                table: "TransactionItems",
                column: "ProductId");

            migrationBuilder.CreateIndex(
                name: "IX_TransactionItems_TransactionId",
                table: "TransactionItems",
                column: "TransactionId");

            migrationBuilder.CreateIndex(
                name: "IX_Transactions_CategoryId",
                table: "Transactions",
                column: "CategoryId");

            migrationBuilder.CreateIndex(
                name: "IX_Transactions_UserId",
                table: "Transactions",
                column: "UserId");

            migrationBuilder.CreateIndex(
                name: "IX_Users_Email",
                table: "Users",
                column: "Email",
                unique: true,
                filter: "[Email] IS NOT NULL");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "TransactionItems");

            migrationBuilder.DropTable(
                name: "Products");

            migrationBuilder.DropTable(
                name: "Transactions");

            migrationBuilder.DropTable(
                name: "Vendors");

            migrationBuilder.DropTable(
                name: "TransactionCategory");

            migrationBuilder.DropTable(
                name: "Users");
        }
    }
}
