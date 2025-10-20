using Microsoft.EntityFrameworkCore;
using SIM.Core.Entities;
using SIM.Core.Interfaces.Repositories;

namespace SIM.Infrastructure.Repositories
{
    public class TransactionItemRepository : ITransactionItemRepository
    {
        private readonly AppDbContext _appDbContext;
        public TransactionItemRepository(AppDbContext appDbContext)
        {
            _appDbContext = appDbContext;
        }
        public async Task<TransactionItem> AddAsync(TransactionItem entity)
        {
            var transactionItem = await _appDbContext.TransactionItems.AddAsync(entity);
            await _appDbContext.SaveChangesAsync();
            return transactionItem.Entity;
        }

        public Task DeleteAsync(TransactionItem entity)
        {
            throw new NotImplementedException();
        }

        public Task<ICollection<TransactionItem>> GetAllAsync()
        {
            throw new NotImplementedException();
        }

        public async Task<TransactionItem?> GetByIdAsync(int id)
        {
            return await _appDbContext.TransactionItems
                .Where(x => !x.IsDeleted)
                .FirstOrDefaultAsync(x => x.Id == id);
        }

        public async Task<ICollection<TransactionItem>> GetByTransactionIdAsync(int transactionId)
        {
            return await _appDbContext.TransactionItems
                .Where(x => !x.IsDeleted)
                .Where(x => x.TransactionId == transactionId)
                .ToListAsync();
        }

        public async Task UpdateAsync(TransactionItem entity)
        {
            _appDbContext.TransactionItems.Update(entity);
            await _appDbContext.SaveChangesAsync();
        }
    }
}
