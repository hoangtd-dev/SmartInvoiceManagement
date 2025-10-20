using SIM.Core.DTOs.Responses;
using SIM.Core.Entities;
using SIM.Core.Interfaces.Repositories.Base;

namespace SIM.Core.Interfaces.Repositories
{
    public interface ITransactionRepository : IRepositoryBase<Transaction> {
        Task<ICollection<Transaction>> GetLatestTransactionsOfCurrentUserAsync(int userId, int take);
        Task<ICollection<Transaction>> GetIncomeExpenseOfCurrentUserAsync(int userId, DateTime startDate, DateTime endDate, int? categoryId = null);
    }
}
