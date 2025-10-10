using Microsoft.EntityFrameworkCore;
using SIM.Core.Entities;
using SIM.Infrastructure.Configurations;
using SIM.Infrastructure.MockData;

namespace SIM.Infrastructure
{
    public class AppDbContext : DbContext
    {
        public AppDbContext() { }
        public AppDbContext(DbContextOptions<AppDbContext> options) : base(options) { }
        public DbSet<User> Users { get; set; }
        public DbSet<Vendor> Vendors { get; set; }
        public DbSet<Product> Products { get; set; }
        public DbSet<Transaction> Transactions { get; set; }
        public DbSet<TransactionCategory> TransactionCategories { get; set; }
        public DbSet<TransactionItem> TransactionItems { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            if (!optionsBuilder.IsConfigured)
            {
                optionsBuilder.UseSqlServer("Server=localhost\\SQLEXPRESS;Database=imdb;Trusted_Connection=true;TrustServerCertificate=true;");
            }
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);
            TableConfigurations.CreateTable(modelBuilder);
            SeedData.Create(modelBuilder);
        }
    }
}
