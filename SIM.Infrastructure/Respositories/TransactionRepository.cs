using Microsoft.EntityFrameworkCore;
using SIM.Core.Entities;
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

        public async Task<List<Transaction>> GetAllAsync()
        {
            return await _appDbContext.Transactions.ToListAsync();
        }

        public Task<Transaction?> GetByIdAsync(int id)
        {
            throw new NotImplementedException();
        }

        public Task UpdateAsync(Transaction entity)
        {
            throw new NotImplementedException();
        }
    }
}
