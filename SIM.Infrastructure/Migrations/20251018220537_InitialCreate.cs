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
                name: "TransactionCategories",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Description = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    IsDeleted = table.Column<bool>(type: "bit", nullable: false),
                    CreatedDate = table.Column<DateTime>(type: "datetime2", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_TransactionCategories", x => x.Id);
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
                    IsDeleted = table.Column<bool>(type: "bit", nullable: false),
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
                    IsDeleted = table.Column<bool>(type: "bit", nullable: false),
                    CreatedDate = table.Column<DateTime>(type: "datetime2", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Vendors", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Budgets",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    TotalAmount = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    TotalExpense = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    StartDate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    EndDate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    Status = table.Column<int>(type: "int", nullable: false),
                    CategoryId = table.Column<int>(type: "int", nullable: true),
                    UserId = table.Column<int>(type: "int", nullable: false),
                    IsDeleted = table.Column<bool>(type: "bit", nullable: false),
                    CreatedDate = table.Column<DateTime>(type: "datetime2", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Budgets", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Budgets_TransactionCategories_CategoryId",
                        column: x => x.CategoryId,
                        principalTable: "TransactionCategories",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_Budgets_Users_UserId",
                        column: x => x.UserId,
                        principalTable: "Users",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Transactions",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    TotalAmount = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    TransactionType = table.Column<int>(type: "int", nullable: false),
                    CategoryId = table.Column<int>(type: "int", nullable: false),
                    UserId = table.Column<int>(type: "int", nullable: false),
                    VendorId = table.Column<int>(type: "int", nullable: false),
                    IsDeleted = table.Column<bool>(type: "bit", nullable: false),
                    CreatedDate = table.Column<DateTime>(type: "datetime2", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Transactions", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Transactions_TransactionCategories_CategoryId",
                        column: x => x.CategoryId,
                        principalTable: "TransactionCategories",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_Transactions_Users_UserId",
                        column: x => x.UserId,
                        principalTable: "Users",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_Transactions_Vendors_VendorId",
                        column: x => x.VendorId,
                        principalTable: "Vendors",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "TransactionItems",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    ItemName = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Quantity = table.Column<int>(type: "int", nullable: false),
                    Price = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    Total = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    TransactionId = table.Column<int>(type: "int", nullable: false),
                    IsDeleted = table.Column<bool>(type: "bit", nullable: false),
                    CreatedDate = table.Column<DateTime>(type: "datetime2", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_TransactionItems", x => x.Id);
                    table.ForeignKey(
                        name: "FK_TransactionItems_Transactions_TransactionId",
                        column: x => x.TransactionId,
                        principalTable: "Transactions",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.InsertData(
                table: "TransactionCategories",
                columns: new[] { "Id", "CreatedDate", "Description", "IsDeleted", "Name" },
                values: new object[,]
                {
                    { 1, new DateTime(2024, 2, 15, 0, 0, 0, 0, DateTimeKind.Unspecified), null, false, "Food" },
                    { 2, new DateTime(2024, 2, 15, 0, 0, 0, 0, DateTimeKind.Unspecified), null, false, "Transportation" },
                    { 3, new DateTime(2024, 2, 15, 0, 0, 0, 0, DateTimeKind.Unspecified), null, false, "Housing" },
                    { 4, new DateTime(2024, 2, 15, 0, 0, 0, 0, DateTimeKind.Unspecified), null, false, "Salary" },
                    { 5, new DateTime(2024, 2, 15, 0, 0, 0, 0, DateTimeKind.Unspecified), null, false, "Electronics" },
                    { 6, new DateTime(2024, 2, 15, 0, 0, 0, 0, DateTimeKind.Unspecified), null, false, "Office Supplies" }
                });

            migrationBuilder.InsertData(
                table: "Users",
                columns: new[] { "Id", "Address", "CreatedDate", "Email", "FirstName", "IsDeleted", "LastName", "PasswordHash", "Phone" },
                values: new object[] { 1, "123 Main St", new DateTime(2024, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), "admin@gmail.com", "Edward", false, "Tran", "100000.xoOaNrEukE+6bc1KUCkD+Q==.CP+sKGB54pKQ+qDs5yh1XMjrEOc2kJKniMlVGh17W7o=", "123-456-7890" });

            migrationBuilder.InsertData(
                table: "Vendors",
                columns: new[] { "Id", "Address", "ContactEmail", "ContactPhone", "CreatedDate", "IsDeleted", "VendorName" },
                values: new object[,]
                {
                    { 1, "789 Tech Park", "info@techsupplies.com", "555-100-2000", new DateTime(2024, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), false, "Tech Supplies Inc." },
                    { 2, "321 Business Blvd", "sales@officeessentials.com", "555-100-3000", new DateTime(2024, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), false, "Office Essentials" },
                    { 3, "555 Digital Ave", "support@premiumelectronics.com", "555-100-4000", new DateTime(2024, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), false, "Premium Electronics" }
                });

            migrationBuilder.InsertData(
                table: "Budgets",
                columns: new[] { "Id", "CategoryId", "CreatedDate", "EndDate", "IsDeleted", "StartDate", "Status", "TotalAmount", "TotalExpense", "UserId" },
                values: new object[,]
                {
                    { 1, 1, new DateTime(2024, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), new DateTime(2024, 1, 31, 0, 0, 0, 0, DateTimeKind.Unspecified), false, new DateTime(2024, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), 1, 500.00m, 450.00m, 1 },
                    { 2, 2, new DateTime(2024, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), new DateTime(2024, 1, 31, 0, 0, 0, 0, DateTimeKind.Unspecified), false, new DateTime(2024, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), 1, 300.00m, 0m, 1 },
                    { 3, 3, new DateTime(2024, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), new DateTime(2024, 1, 31, 0, 0, 0, 0, DateTimeKind.Unspecified), false, new DateTime(2024, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), 1, 1200.00m, 100m, 1 },
                    { 4, 1, new DateTime(2024, 2, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), new DateTime(2024, 2, 29, 0, 0, 0, 0, DateTimeKind.Unspecified), false, new DateTime(2024, 2, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), 1, 550.00m, 0m, 1 },
                    { 5, 2, new DateTime(2024, 2, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), new DateTime(2024, 2, 29, 0, 0, 0, 0, DateTimeKind.Unspecified), false, new DateTime(2024, 2, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), 1, 350.00m, 0m, 1 },
                    { 6, 3, new DateTime(2024, 2, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), new DateTime(2024, 2, 29, 0, 0, 0, 0, DateTimeKind.Unspecified), false, new DateTime(2024, 2, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), 1, 1200.00m, 0m, 1 },
                    { 7, 1, new DateTime(2024, 3, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), new DateTime(2024, 3, 31, 0, 0, 0, 0, DateTimeKind.Unspecified), false, new DateTime(2024, 3, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), 1, 600.00m, 0m, 1 },
                    { 8, 2, new DateTime(2024, 3, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), new DateTime(2024, 3, 31, 0, 0, 0, 0, DateTimeKind.Unspecified), false, new DateTime(2024, 3, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), 1, 400.00m, 0m, 1 },
                    { 9, 3, new DateTime(2024, 3, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), new DateTime(2024, 3, 31, 0, 0, 0, 0, DateTimeKind.Unspecified), false, new DateTime(2024, 3, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), 1, 1200.00m, 0m, 1 },
                    { 10, 1, new DateTime(2024, 4, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), new DateTime(2025, 9, 30, 0, 0, 0, 0, DateTimeKind.Unspecified), false, new DateTime(2025, 7, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), 0, 650.00m, 700.00m, 1 },
                    { 11, 2, new DateTime(2024, 4, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), new DateTime(2025, 9, 30, 0, 0, 0, 0, DateTimeKind.Unspecified), false, new DateTime(2025, 7, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), 0, 450.00m, 100.00m, 1 },
                    { 12, 3, new DateTime(2024, 4, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), new DateTime(2025, 9, 30, 0, 0, 0, 0, DateTimeKind.Unspecified), false, new DateTime(2025, 7, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), 0, 1250.00m, 1100.00m, 1 }
                });

            migrationBuilder.InsertData(
                table: "Transactions",
                columns: new[] { "Id", "CategoryId", "CreatedDate", "IsDeleted", "TotalAmount", "TransactionType", "UserId", "VendorId" },
                values: new object[,]
                {
                    { 1, 4, new DateTime(2025, 1, 5, 0, 0, 0, 0, DateTimeKind.Unspecified), false, 12000m, 0, 1, 3 },
                    { 2, 5, new DateTime(2025, 1, 10, 0, 0, 0, 0, DateTimeKind.Unspecified), false, 1029.98m, 1, 1, 1 },
                    { 3, 4, new DateTime(2025, 2, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), false, 12500m, 0, 1, 3 },
                    { 4, 6, new DateTime(2025, 2, 15, 0, 0, 0, 0, DateTimeKind.Unspecified), false, 299.98m, 1, 1, 2 },
                    { 5, 4, new DateTime(2025, 3, 5, 0, 0, 0, 0, DateTimeKind.Unspecified), false, 11800m, 0, 1, 3 },
                    { 6, 5, new DateTime(2025, 3, 20, 0, 0, 0, 0, DateTimeKind.Unspecified), false, 499.98m, 1, 1, 1 },
                    { 7, 4, new DateTime(2025, 4, 3, 0, 0, 0, 0, DateTimeKind.Unspecified), false, 13000m, 0, 1, 3 },
                    { 8, 6, new DateTime(2025, 4, 18, 0, 0, 0, 0, DateTimeKind.Unspecified), false, 229.98m, 1, 1, 2 },
                    { 9, 4, new DateTime(2025, 5, 7, 0, 0, 0, 0, DateTimeKind.Unspecified), false, 12200m, 0, 1, 3 },
                    { 10, 5, new DateTime(2025, 5, 22, 0, 0, 0, 0, DateTimeKind.Unspecified), false, 399.98m, 1, 1, 3 },
                    { 11, 4, new DateTime(2025, 6, 4, 0, 0, 0, 0, DateTimeKind.Unspecified), false, 12800m, 0, 1, 3 },
                    { 12, 5, new DateTime(2025, 6, 19, 0, 0, 0, 0, DateTimeKind.Unspecified), false, 999.99m, 1, 1, 1 },
                    { 13, 1, new DateTime(2025, 7, 6, 0, 0, 0, 0, DateTimeKind.Unspecified), false, 13500m, 1, 1, 3 },
                    { 14, 6, new DateTime(2025, 7, 25, 0, 0, 0, 0, DateTimeKind.Unspecified), false, 389.96m, 1, 1, 1 },
                    { 15, 3, new DateTime(2025, 8, 2, 0, 0, 0, 0, DateTimeKind.Unspecified), false, 12600m, 1, 1, 3 },
                    { 16, 6, new DateTime(2025, 8, 16, 0, 0, 0, 0, DateTimeKind.Unspecified), false, 229.98m, 1, 1, 2 },
                    { 17, 3, new DateTime(2025, 9, 8, 0, 0, 0, 0, DateTimeKind.Unspecified), false, 13200m, 1, 1, 3 },
                    { 18, 5, new DateTime(2025, 9, 21, 0, 0, 0, 0, DateTimeKind.Unspecified), false, 399.98m, 1, 1, 1 },
                    { 19, 4, new DateTime(2025, 10, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), false, 12400m, 0, 1, 3 },
                    { 20, 5, new DateTime(2025, 10, 28, 0, 0, 0, 0, DateTimeKind.Unspecified), false, 349.98m, 1, 1, 3 }
                });

            migrationBuilder.InsertData(
                table: "TransactionItems",
                columns: new[] { "Id", "CreatedDate", "IsDeleted", "ItemName", "Price", "Quantity", "Total", "TransactionId" },
                values: new object[,]
                {
                    { 1, new DateTime(2025, 1, 10, 0, 0, 0, 0, DateTimeKind.Unspecified), false, "Laptop", 999.99m, 1, 999.99m, 2 },
                    { 2, new DateTime(2025, 2, 10, 0, 0, 0, 0, DateTimeKind.Unspecified), false, "Charger", 29.99m, 2, 29.99m, 2 },
                    { 3, new DateTime(2025, 2, 15, 0, 0, 0, 0, DateTimeKind.Unspecified), false, "Printer", 149.99m, 2, 299.98m, 4 },
                    { 4, new DateTime(2025, 3, 20, 0, 0, 0, 0, DateTimeKind.Unspecified), false, "Grinder Machine", 199.99m, 1, 199.99m, 6 },
                    { 5, new DateTime(2025, 3, 20, 0, 0, 0, 0, DateTimeKind.Unspecified), false, "Vacuum Machine", 299.99m, 1, 299.99m, 6 },
                    { 6, new DateTime(2025, 4, 18, 0, 0, 0, 0, DateTimeKind.Unspecified), false, "Notebook Pack", 79.99m, 1, 79.99m, 8 },
                    { 7, new DateTime(2025, 4, 18, 0, 0, 0, 0, DateTimeKind.Unspecified), false, "Desk Organizer", 149.99m, 1, 149.99m, 8 },
                    { 8, new DateTime(2025, 5, 22, 0, 0, 0, 0, DateTimeKind.Unspecified), false, "Wireless Headset", 199.99m, 2, 399.98m, 10 },
                    { 9, new DateTime(2025, 6, 19, 0, 0, 0, 0, DateTimeKind.Unspecified), false, "Laptop Pro", 999.99m, 1, 999.99m, 12 },
                    { 10, new DateTime(2025, 7, 25, 0, 0, 0, 0, DateTimeKind.Unspecified), false, "Office Chair", 29.99m, 3, 89.97m, 14 },
                    { 11, new DateTime(2025, 7, 25, 0, 0, 0, 0, DateTimeKind.Unspecified), false, "Filing Cabinet", 299.99m, 1, 299.99m, 14 },
                    { 12, new DateTime(2025, 8, 16, 0, 0, 0, 0, DateTimeKind.Unspecified), false, "Paper Cutter", 149.99m, 1, 149.99m, 16 },
                    { 13, new DateTime(2025, 8, 16, 0, 0, 0, 0, DateTimeKind.Unspecified), false, "Marker Set", 79.99m, 1, 79.99m, 16 },
                    { 14, new DateTime(2025, 9, 21, 0, 0, 0, 0, DateTimeKind.Unspecified), false, "Smartphone", 199.99m, 2, 399.98m, 18 },
                    { 15, new DateTime(2025, 10, 28, 0, 0, 0, 0, DateTimeKind.Unspecified), false, "Wireless Keyboard", 149.99m, 1, 149.99m, 20 },
                    { 16, new DateTime(2025, 10, 28, 0, 0, 0, 0, DateTimeKind.Unspecified), false, "Bluetooth Mouse", 199.99m, 1, 199.99m, 20 }
                });

            migrationBuilder.CreateIndex(
                name: "IX_Budgets_CategoryId",
                table: "Budgets",
                column: "CategoryId");

            migrationBuilder.CreateIndex(
                name: "IX_Budgets_UserId",
                table: "Budgets",
                column: "UserId");

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
                name: "IX_Transactions_VendorId",
                table: "Transactions",
                column: "VendorId");

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
                name: "Budgets");

            migrationBuilder.DropTable(
                name: "TransactionItems");

            migrationBuilder.DropTable(
                name: "Transactions");

            migrationBuilder.DropTable(
                name: "TransactionCategories");

            migrationBuilder.DropTable(
                name: "Users");

            migrationBuilder.DropTable(
                name: "Vendors");
        }
    }
}
