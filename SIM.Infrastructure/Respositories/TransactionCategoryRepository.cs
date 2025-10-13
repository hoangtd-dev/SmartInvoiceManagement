
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
        public async Task<TransactionCategory> AddAsync(TransactionCategory entity)
        {
            var transactionCategory = await _appDbContext.TransactionCategories.AddAsync(entity);
            await _appDbContext.SaveChangesAsync();
            return transactionCategory.Entity;
        }

        public async Task DeleteAsync(TransactionCategory entity)
        {
            entity.IsDeleted = true;
            await _appDbContext.SaveChangesAsync();
        }

        public async Task<ICollection<TransactionCategory>> GetAllAsync()
        {
            return await _appDbContext.TransactionCategories
                .Where(x => !x.IsDeleted)
                .ToListAsync();
        }

        public async Task<TransactionCategory?> GetByIdAsync(int id)
        {
            return await _appDbContext.TransactionCategories
                .Where(x => !x.IsDeleted)
                .FirstOrDefaultAsync(x => x.Id == id);
        }

        public async Task UpdateAsync(TransactionCategory entity)
        {
            _appDbContext.TransactionCategories.Update(entity);
            await _appDbContext.SaveChangesAsync();
        }
    }
}
