
using SIM.Core.DTOs.Requests;
using SIM.Core.DTOs.Responses;
using SIM.Core.Entities;
using SIM.Core.Enums;
using SIM.Core.Exceptions;
using SIM.Core.Helpers;
using SIM.Core.Interfaces.Repositories;
using SIM.Core.Interfaces.Services;

namespace SIM.Core.Services
{
    public class TransactionService : ITransactionService
    {
        private readonly ITransactionRepository _transactionRepository;
        private readonly IVendorRepository _vendorRepository;
        private readonly ITransactionCategoryRepository _transactionCategoryRepository;
        public TransactionService(ITransactionRepository transactionRepository,
            IVendorRepository vendorRepository,
            ITransactionCategoryRepository transactionCategoryRepository)
        {
            _transactionRepository = transactionRepository;
            _vendorRepository = vendorRepository;
            _transactionCategoryRepository = transactionCategoryRepository;
        }

        public async Task<TransactionModel> CreateTransaction(CreateTransactionRequest transaction)
        {
            var newTransaction = new Transaction
            {
                UserId = transaction.UserId,
                VendorId = transaction.VendorId,
                CategoryId = transaction.CategoryId,
                TotalAmount = transaction.TotalAmount,
                TransactionType = transaction.TransactionType,
                CreatedDate = DateTime.UtcNow
            };

            var createdTransaction = await _transactionRepository.AddAsync(newTransaction);
            return new TransactionModel
            {
                Id = createdTransaction.Id,
                Type = createdTransaction.TransactionType,
                TotalAmount = createdTransaction.TotalAmount,
                CreateDate = createdTransaction.CreatedDate,
                CategoryName = createdTransaction.Category is not null ? createdTransaction.Category.Name : null,
                VendorName = createdTransaction.Vendor is not null ? createdTransaction.Vendor.VendorName : null
            };
        }

        public async Task DeleteTransaction(int id)
        {
            var transaction = await _transactionRepository.GetByIdAsync(id);
            if (transaction is null) throw new NotFoundException($"Transaction with id:{id} is not found !!!");

            await _transactionRepository.DeleteAsync(transaction);
        }

        public async Task<TransactionModel> GetTransactionById(int id)
        {
            var transaction = await _transactionRepository.GetByIdAsync(id);

            return new TransactionModel
            {
                Id = transaction.Id,
                Type = transaction.TransactionType,
                TotalAmount = transaction.TotalAmount,
                CreateDate = transaction.CreatedDate,
                CategoryName = transaction.Category is not null ? transaction.Category.Name : null,
                VendorName = transaction.Vendor is not null ? transaction.Vendor.VendorName : null
            };
        }

        public async Task<ICollection<TransactionModel>> GetLatestTransactionsOfCurrentUser(int userId, int take)
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
                Id = transaction.Id,
                Type = transaction.TransactionType,
                TotalAmount = transaction.TotalAmount,
                CreateDate = transaction.CreatedDate,
                CategoryName = transaction.Category is not null ? transaction.Category.Name : null,
                VendorName = transaction.Vendor is not null ? transaction.Vendor.VendorName : null
            }).ToList();
        }

        public async Task UpdateTransaction(UpdateTransactionRequest transaction)
        {
            var existing = await _transactionRepository.GetByIdAsync(transaction.Id);
            if (existing is null) throw new NotFoundException($"Transaction with id:{transaction.Id} is not found !!!");

            existing.VendorId = transaction.VendorId;
            existing.CategoryId = transaction.CategoryId;
            existing.TotalAmount = transaction.TotalAmount;
            existing.TransactionType = transaction.TransactionType;

            await _transactionRepository.UpdateAsync(existing);
        }

        public async Task<IncomeExpenseModel> GetIncomeExpensesOfCurrentUser(int userId, int month, int year)
        {
            var (startDate, endDate) = DateHelpers.GetStartAndEndDateOfMonth(month, year);

            var transactions = await _transactionRepository.GetIncomeExpenseOfCurrentUserAsync(userId, startDate, endDate);

            var totalIncome = transactions.Where(t => t.TransactionType == TransactionTypeEnum.Income).Sum(transaction => transaction.TotalAmount);
            var totalExpense = transactions.Where(t => t.TransactionType == TransactionTypeEnum.Expense).Sum(transaction => transaction.TotalAmount);

            return new IncomeExpenseModel
            {
                Expense = totalExpense,
                Income = totalIncome,
                Month = month,
                Year = year
            };
        }
    }
}
