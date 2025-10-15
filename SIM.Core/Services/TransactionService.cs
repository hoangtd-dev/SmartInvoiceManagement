
using SIM.Core.DTOs.Requests;
using SIM.Core.DTOs.Responses;
using SIM.Core.Entities;
using SIM.Core.Enums;
using SIM.Core.Helpers;
using SIM.Core.Interfaces.Repositories;
using SIM.Core.Interfaces.Services;

namespace SIM.Core.Services
{
    public class TransactionService : ITransactionService
    {
        private readonly ITransactionRepository _transactionRepository;
        public TransactionService(ITransactionRepository transactionRepository) {
            _transactionRepository = transactionRepository;
        }

        public async Task CreateTransaction(CreateTransactionRequest transaction)
        {
            // TODO: Mapping
            await _transactionRepository.AddAsync(new Transaction());
        }

        public async Task DeleteTransaction(int id)
        {
            throw new NotImplementedException();
        }

        public async Task<TransactionModel> GetTransactionById(int id)
        {
            var transaction = await _transactionRepository.GetByIdAsync(id);

            // TODO: Mapping
            return new TransactionModel();
        }

        public async Task<ICollection<TransactionModel>> GetLastestTransactionsOfCurrentUser(int userId, int take)
        {
            var transactions = await _transactionRepository.GetLatestTransactionsOfCurrentUserAsync(userId, take);

            return transactions.Select(transaction => new TransactionModel
            {
                Id = transaction.Id,
                Type = transaction.TransactionType,
                TotalAmount = transaction.TotalAmount,
                CreateDate = transaction.CreatedDate,
                CategoryName = transaction.Category is not null ? transaction.Category.Name : null,
                VendorName = transaction.Vendor is not null ? transaction.Vendor.VendorName : null
            }).ToList();
        }

        public async Task<ICollection<TransactionModel>> GetTransactions()
        {
            var transactions = await _transactionRepository.GetAllAsync();

            return transactions.Select(transaction => new TransactionModel
            {
                Type = transaction.TransactionType,
                TotalAmount = transaction.TotalAmount,
            }).ToList();
        }

        public async Task UpdateTransaction(UpdateTransactionRequest transaction)
        {
            // TODO: Mapping
            await _transactionRepository.UpdateAsync(new Transaction());
        }

        public async Task<IncomeExpenseModel> GetIncomeExpensesOfCurrentUser(int userId, int month, int year)
        {
            var (startDate, endDate) = DateHelpers.GetStartAndEndDateOfMonth(month, year);

            var transactions = await _transactionRepository.GetIncomeExpenseOfCurrentUserAsync(userId, startDate, endDate);

            var totalIncome = transactions.Where(t => t.TransactionType == TransactionTypeEnum.Income).Sum(transaction => transaction.TotalAmount);
            var totalExpense = transactions.Where(t => t.TransactionType == TransactionTypeEnum.Expense).Sum(transaction => transaction.TotalAmount);

            return new IncomeExpenseModel { 
                Expense = totalExpense,
                Income = totalIncome,
                Month = month,
                Year = year
            };
        }
    }
}
