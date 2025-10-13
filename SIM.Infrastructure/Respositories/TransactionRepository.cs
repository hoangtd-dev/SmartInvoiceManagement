using Microsoft.EntityFrameworkCore;
using SIM.Core.Entities;
using SIM.Core.Helpers;
using SIM.Core.Interfaces.Repositories;

namespace SIM.Infrastructure.Respositories
{
    public class TransactionRepository : ITransactionRepository
    {
        private readonly AppDbContext _appDbContext;
        public TransactionRepository(AppDbContext appDbContext)
        {
            _appDbContext = appDbContext;
        }
        public Task<Transaction> AddAsync(Transaction entity)
        {
            throw new NotImplementedException();
        }

        public async Task DeleteAsync(Transaction entity)
        {
            entity.IsDeleted = true;
            await _appDbContext.SaveChangesAsync();
        }

        public async Task<ICollection<Transaction>> GetAllAsync()
        {
            return await _appDbContext.Transactions.ToListAsync();
        }

        public async Task<ICollection<Transaction>> GetLatestTransactionsOfCurrentUserAsync(int userId, int take = 5)
        {
            return await _appDbContext.Transactions
                .Where(x => x.UserId == userId)
                .Include(x => x.Category)
                .Include(x => x.Vendor)
                .OrderByDescending(x => x.CreatedDate)
                .Take(take)
                .ToListAsync();
        }

        public Task<Transaction?> GetByIdAsync(int id)
        {
            throw new NotImplementedException();
        }

        public Task UpdateAsync(Transaction entity)
        {
            throw new NotImplementedException();
        }

        public async Task<ICollection<Transaction>> GetIncomeExpenseOfCurrentUserInMonthAsync(int userId, int month, int year)
        {
            var (startDate, endDate) = DateHelpers.GetStartAndEndDateOfMonth(month, year);

            return await _appDbContext.Transactions
                .Where(x => x.UserId == userId && x.CreatedDate >= startDate && x.CreatedDate <= endDate)
                .ToListAsync();
        }
    }
}
