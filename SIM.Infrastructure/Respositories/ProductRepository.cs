using Microsoft.EntityFrameworkCore;
using SIM.Core.Entities;
using SIM.Core.Interfaces.Repositories;

namespace SIM.Infrastructure.Respositories
{
    public class ProductRepository : IProductRepository
    {
        private readonly AppDbContext _appDbContext;
        public ProductRepository(AppDbContext appDbContext)
        {
            _appDbContext = appDbContext;
        }
        public async Task<Product> AddAsync(Product entity)
        {
            var product = await _appDbContext.Products.AddAsync(entity);
            await _appDbContext.SaveChangesAsync();
            return product.Entity;
        }

        public async Task DeleteAsync(Product entity)
        {
            entity.IsDeleted = true;
            await _appDbContext.SaveChangesAsync();
        }

        public async Task<ICollection<Product>> GetAllAsync()
        {
            return await _appDbContext.Products
                .Where(x => !x.IsDeleted)
                .Include(x => x.Vendor)
                .ToListAsync();
        }

        public async Task<Product?> GetByIdAsync(int id)
        {
            return await _appDbContext.Products
                .Where(x => !x.IsDeleted)
                .FirstOrDefaultAsync(x => x.Id == id);
        }

        public async Task UpdateAsync(Product entity)
        {
            _appDbContext.Products.Update(entity);
            await _appDbContext.SaveChangesAsync();
        }
    }
}
