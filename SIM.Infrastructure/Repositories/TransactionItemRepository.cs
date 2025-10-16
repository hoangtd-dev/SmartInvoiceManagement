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

        public Task<TransactionItem?> GetByIdAsync(int id)
        {
            throw new NotImplementedException();
        }

        public Task UpdateAsync(TransactionItem entity)
        {
            throw new NotImplementedException();
        }
    }
}
