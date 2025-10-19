using Microsoft.EntityFrameworkCore;
using SIM.Core.Entities;
using SIM.Core.Interfaces.Repositories;

namespace SIM.Infrastructure.Repositories
{
    public class TransactionRepository : ITransactionRepository
    {
        private readonly AppDbContext _appDbContext;
        public TransactionRepository(AppDbContext appDbContext)
        {
            _appDbContext = appDbContext;
        }
        public async Task<Transaction> AddAsync(Transaction entity)
        {
            var transaction = await _appDbContext.Transactions.AddAsync(entity);
            await _appDbContext.SaveChangesAsync();
            return transaction.Entity;
        }

        public async Task DeleteAsync(Transaction entity)
        {
            entity.IsDeleted = true;
            await _appDbContext.SaveChangesAsync();
        }

        public async Task<ICollection<Transaction>> GetAllAsync()
        {
            return await _appDbContext.Transactions
                .Include(x => x.Category)
                .Include(x => x.Vendor)
                .Where(x => !x.IsDeleted)
                .OrderByDescending(x => x.CreatedDate)
                .ToListAsync();
        }

        public async Task<ICollection<Transaction>> GetLatestTransactionsOfCurrentUserAsync(int userId, int take = 5)
        {
            return await _appDbContext.Transactions
                .Where(x => x.UserId == userId)
                .Where(x => !x.IsDeleted)
                .Include(x => x.Category)
                .Include(x => x.Vendor)
                .OrderByDescending(x => x.CreatedDate)
                .Take(take)
                .ToListAsync();
        }

        public async Task<Transaction?> GetByIdAsync(int id)
        {
            return await _appDbContext.Transactions
                .Where(x => !x.IsDeleted)
                .Include(x => x.Category)
                .Include(x => x.Vendor)
                .FirstOrDefaultAsync(x => x.Id == id);
        }

        public async Task UpdateAsync(Transaction entity)
        {
            _appDbContext.Transactions.Update(entity);
            await _appDbContext.SaveChangesAsync();
        }

        public async Task<ICollection<Transaction>> GetIncomeExpenseOfCurrentUserAsync(int userId, DateTime startDate, DateTime endDate, int? categoryId)
        {
            var query = _appDbContext.Transactions.AsQueryable();

            if (categoryId is not null)
            {
                query = query.Where(x => x.CategoryId == categoryId);
            }

            return await query
                .Where(x => x.UserId == userId && x.CreatedDate >= startDate && x.CreatedDate <= endDate)
                .Where(x => !x.IsDeleted)
                .ToListAsync();
        }
    }
}
