using SIM.Core.Entities;
using SIM.Core.Interfaces.Repositories.Base;

namespace SIM.Core.Interfaces.Repositories
{
    public interface ITransactionItemRepository : IRepositoryBase<TransactionItem>
    {
        Task<ICollection<TransactionItem>> GetByTransactionIdAsync(int transactionId);
    }
}
