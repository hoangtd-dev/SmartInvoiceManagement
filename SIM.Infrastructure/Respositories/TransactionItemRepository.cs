using SIM.Core.Entities;
using SIM.Core.Interfaces.Repositories;

namespace SIM.Infrastructure.Respositories
{
    public class TransactionItemRepository : ITransactionItemRepository
    {
        private readonly AppDbContext _appDbContext;
        public TransactionItemRepository(AppDbContext appDbContext) 
        { 
            _appDbContext = appDbContext;
        }
        public Task<TransactionItem> AddAsync(TransactionItem entity)
        {
            throw new NotImplementedException();
        }

        public Task DeleteAsync(int id)
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
