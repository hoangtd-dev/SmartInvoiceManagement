
using Microsoft.EntityFrameworkCore;
using SIM.Core.Entities;
using SIM.Core.Interfaces.Repositories;

namespace SIM.Infrastructure.Respositories
{
    public class TransactionCategoryRepository : ITransactionCategoryRepository
    {
        private readonly AppDbContext _appDbContext;
        public TransactionCategoryRepository(AppDbContext appDbContext)
        {
            _appDbContext = appDbContext;
        }
        public Task<TransactionCategory> AddAsync(TransactionCategory entity)
        {
            throw new NotImplementedException();
        }

        public Task DeleteAsync(int id)
        {
            throw new NotImplementedException();
        }

        public async Task<ICollection<TransactionCategory>> GetAllAsync()
        {
            return await _appDbContext.TransactionCategories.ToListAsync();
        }

        public Task<TransactionCategory?> GetByIdAsync(int id)
        {
            throw new NotImplementedException();
        }

        public Task UpdateAsync(TransactionCategory entity)
        {
            throw new NotImplementedException();
        }
    }
}
