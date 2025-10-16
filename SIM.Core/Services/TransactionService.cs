
using SIM.Core.DTOs.Requests;
using SIM.Core.DTOs.Responses;
using SIM.Core.Entities;
using SIM.Core.Enums;
using SIM.Core.Exceptions;
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

            await _transactionRepository.AddAsync(newTransaction);

            // fetch related names using repositories (if provided ids exist)
            string? vendorName = null;
            string? categoryName = null;
            if (newTransaction.VendorId > 0)
            {
                var v = await _vendorRepository.GetByIdAsync(newTransaction.VendorId);
                vendorName = v?.VendorName;
            }
            if (newTransaction.CategoryId > 0)
            {
                var c = await _transactionCategoryRepository.GetByIdAsync(newTransaction.CategoryId);
                categoryName = c?.Name;
            }

            return new TransactionModel
            {
                Id = newTransaction.Id,
                Type = newTransaction.TransactionType,
                TotalAmount = newTransaction.TotalAmount,
                CreateDate = newTransaction.CreatedDate,
                CategoryName = categoryName,
                VendorName = vendorName
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

            if (transaction is null) throw new NotFoundException($"Transaction with id:{id} is not found !!!");

            // fetch related names via repositories
            string? vendorName = null;
            string? categoryName = null;
            if (transaction.VendorId > 0)
            {
                var v = await _vendorRepository.GetByIdAsync(transaction.VendorId);
                vendorName = v?.VendorName;
            }
            if (transaction.CategoryId > 0)
            {
                var c = await _transactionCategoryRepository.GetByIdAsync(transaction.CategoryId);
                categoryName = c?.Name;
            }

            return new TransactionModel
            {
                Id = transaction.Id,
                Type = transaction.TransactionType,
                TotalAmount = transaction.TotalAmount,
                CreateDate = transaction.CreatedDate,
                CategoryName = categoryName,
                VendorName = vendorName
            };
        }

        public async Task<ICollection<TransactionModel>> GetLastestTransactionsOfCurrentUser(int userId, int take)
        {
            var transactions = await _transactionRepository.GetLatestTransactionsOfCurrentUserAsync(userId, take);

            var result = new List<TransactionModel>();
            foreach (var transaction in transactions)
            {
                string? vendorName = null;
                string? categoryName = null;
                if (transaction.VendorId > 0)
                {
                    var v = await _vendorRepository.GetByIdAsync(transaction.VendorId);
                    vendorName = v?.VendorName;
                }
                if (transaction.CategoryId > 0)
                {
                    var c = await _transactionCategoryRepository.GetByIdAsync(transaction.CategoryId);
                    categoryName = c?.Name;
                }

                result.Add(new TransactionModel
                {
                    Id = transaction.Id,
                    Type = transaction.TransactionType,
                    TotalAmount = transaction.TotalAmount,
                    CreateDate = transaction.CreatedDate,
                    CategoryName = categoryName,
                    VendorName = vendorName
                });
            }

            return result;
        }

        public async Task<ICollection<TransactionModel>> GetTransactions()
        {
            var transactions = await _transactionRepository.GetAllAsync();

            var result = new List<TransactionModel>();
            foreach (var transaction in transactions)
            {
                string? vendorName = null;
                string? categoryName = null;
                if (transaction.VendorId > 0)
                {
                    var v = await _vendorRepository.GetByIdAsync(transaction.VendorId);
                    vendorName = v?.VendorName;
                }
                if (transaction.CategoryId > 0)
                {
                    var c = await _transactionCategoryRepository.GetByIdAsync(transaction.CategoryId);
                    categoryName = c?.Name;
                }

                result.Add(new TransactionModel
                {
                    Id = transaction.Id,
                    Type = transaction.TransactionType,
                    TotalAmount = transaction.TotalAmount,
                    CreateDate = transaction.CreatedDate,
                    CategoryName = categoryName,
                    VendorName = vendorName
                });
            }

            return result;
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

        public async Task<IncomeExpenseModel> GetIncomeExpensesOfCurrentUserInMonth(int userId, int month, int year)
        {
            var transactions = await _transactionRepository.GetIncomeExpenseOfCurrentUserInMonthAsync(userId, month, year);

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
