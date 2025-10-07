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
        public Task<Product> AddAsync(Product entity)
        {
            throw new NotImplementedException();
        }

        public Task DeleteAsync(int id)
        {
            throw new NotImplementedException();
        }

        public Task<List<Product>> GetAllAsync()
        {
            throw new NotImplementedException();
        }

        public Task<Product?> GetByIdAsync(int id)
        {
            throw new NotImplementedException();
        }

        public Task UpdateAsync(Product entity)
        {
            throw new NotImplementedException();
        }
    }
}
