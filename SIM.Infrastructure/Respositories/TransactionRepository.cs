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

        public Task DeleteAsync(int id)
        {
            throw new NotImplementedException();
        }

        public async Task<ICollection<Transaction>> GetAllAsync()
        {
            // TODO: Get transactions of current user Id
            return await _appDbContext.Transactions.ToListAsync();
        }

        public async Task<ICollection<Transaction>> GetLatestTransactionsAsync(int take)
        {
            // TODO: Get transactions of current user Id
            return await _appDbContext.Transactions
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

        public async Task<ICollection<Transaction>> GetIncomeExpenseInMonthAsync(int month, int year)
        {
            var (startDate, endDate) = DateHelpers.GetStartAndEndDateOfMonth(month, year);

            // TODO: Get transactions of current user Id
            return await _appDbContext.Transactions
                .Where(x => x.CreatedDate >= startDate && x.CreatedDate <= endDate)
                .ToListAsync();
        }
    }
}
