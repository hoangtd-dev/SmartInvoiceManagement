using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using SIM.Core.Interfaces.Repositories;
using SIM.Infrastructure.Repositories;

namespace SIM.Infrastructure
{
    public static class DependencyInjection
    {
        public static IServiceCollection AddInfrastructure(this IServiceCollection services, string? connectionString)
        {
            if (connectionString is null)
            {
                connectionString = "Server=localhost\\SQLEXPRESS;Database=imdb;Trusted_Connection=true;TrustServerCertificate=true;";
            }

            services.AddDbContext<AppDbContext>(options =>
                options.UseSqlServer(connectionString));

            services.AddScoped<IUserRepository, UserRepository>();
            services.AddScoped<IVendorRepository, VendorRepository>();
            services.AddScoped<ITransactionRepository, TransactionRepository>();
            services.AddScoped<ITransactionItemRepository, TransactionItemRepository>();
            services.AddScoped<ITransactionCategoryRepository, TransactionCategoryRepository>();
            services.AddScoped<IBudgetRepository, BudgetRepository>();

            return services;
        }
    }
}
