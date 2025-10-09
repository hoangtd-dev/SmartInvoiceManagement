
using SIM.Core.DTOs.Requests;
using SIM.Core.DTOs.Responses;

namespace SIM.Core.Interfaces.Services
{
    public interface ITransactionService
    {
        Task CreateTransaction(CreateTransactionRequest Transaction);
        Task UpdateTransaction(UpdateTransactionRequest Transaction);
        Task DeleteTransaction(int id);
        Task<TransactionModel> GetTransactionById(int id);
        Task<ICollection<TransactionModel>> GetTransactions();
        Task<ICollection<TransactionModel>> GetLastestTransactions(int take);
        Task<IncomeExpenseModel> GetIncomeExpensesInMonth(int month, int year);
    }
}
