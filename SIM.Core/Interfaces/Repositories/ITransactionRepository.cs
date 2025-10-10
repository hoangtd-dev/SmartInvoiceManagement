using SIM.Core.DTOs.Responses;
using SIM.Core.Entities;
using SIM.Core.Interfaces.Repositories.Base;

namespace SIM.Core.Interfaces.Repositories
{
    public interface ITransactionRepository : IRepositoryBase<Transaction> {
        Task<ICollection<Transaction>> GetLatestTransactionsAsync(int take);
        Task<ICollection<Transaction>> GetIncomeExpenseInMonthAsync(int month, int year);
    }
}
