
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
        private readonly ITransactionItemRepository _transactionItemRepository;
        private readonly IBudgetRepository _budgetRepository;
        public TransactionService(
            ITransactionRepository transactionRepository,
            IVendorRepository vendorRepository,
            ITransactionCategoryRepository transactionCategoryRepository,
            ITransactionItemRepository transactionItemRepository,
            IBudgetRepository budgetRepository)
        {
            _transactionRepository = transactionRepository;
            _vendorRepository = vendorRepository;
            _transactionCategoryRepository = transactionCategoryRepository;
            _transactionItemRepository = transactionItemRepository;
            _budgetRepository = budgetRepository;
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

            if (transaction.TransactionType == TransactionTypeEnum.Expense)
            { 
                await _budgetRepository.UpdateBudgetByCategory(transaction.UserId, transaction.TotalAmount, transaction.CategoryId);
            }

            return new TransactionModel
            {
                Id = createdTransaction.Id,
                TransactionType = createdTransaction.TransactionType,
                TotalAmount = createdTransaction.TotalAmount,
                CreateDate = createdTransaction.CreatedDate,
                Category = new TransactionCategoryModel
                {
                    Id = createdTransaction.CategoryId,
                },
                Vendor = new VendorModel
                {
                    Id = createdTransaction.VendorId,
                }
            };
        }

        public async Task DeleteTransaction(int id)
        {
            var transaction = await _transactionRepository.GetByIdAsync(id);
            if (transaction is null) throw new NotFoundException($"Transaction with id:{id} is not found !!!");

            if (transaction.TransactionType == TransactionTypeEnum.Expense)
            {
                await _budgetRepository.UpdateBudgetByCategory(transaction.UserId, -(transaction.TotalAmount), transaction.CategoryId);
            }

            await _transactionRepository.DeleteAsync(transaction);
        }

        public async Task<TransactionModel> GetTransactionById(int id)
        {
            var transaction = await _transactionRepository.GetByIdAsync(id);
            var listItems = await _transactionItemRepository.GetByTransactionIdAsync(id);

            return new TransactionModel
            {
                Id = transaction.Id,
                TransactionType = transaction.TransactionType,
                TotalAmount = transaction.TotalAmount,
                CreateDate = transaction.CreatedDate,
                CategoryId = transaction.CategoryId,
                VendorId = transaction.VendorId,
                Category = new TransactionCategoryModel
                {
                    Id = transaction.Category.Id,
                    Name = transaction.Category.Name
                },
                Vendor = new VendorModel
                {
                    Id = transaction.Vendor.Id,
                    Name = transaction.Vendor.VendorName
                },
                Items = listItems.Select(item => new TransactionItemModel
                {
                    Id = item.Id,
                    ItemName = item.ItemName,
                    Total = item.Total,
                    Quantity = item.Quantity,
                    Price = item.Price
                }).ToList()
            };
        }

        public async Task<ICollection<TransactionModel>> GetLatestTransactionsOfCurrentUser(int userId, int take)
        {
            var transactions = await _transactionRepository.GetLatestTransactionsOfCurrentUserAsync(userId, take);

            return transactions.Select(transaction => new TransactionModel
            {
                Id = transaction.Id,
                TransactionType = transaction.TransactionType,
                TotalAmount = transaction.TotalAmount,
                CreateDate = transaction.CreatedDate,
                Category = new TransactionCategoryModel
                {
                    Id = transaction.Category.Id,
                    Name = transaction.Category.Name
                },
                Vendor = new VendorModel
                {
                    Id = transaction.Vendor.Id,
                    Name = transaction.Vendor.VendorName
                }
            }).ToList();
        }

        public async Task<ICollection<TransactionModel>> GetTransactions()
        {
            var transactions = await _transactionRepository.GetAllAsync();

            return transactions.Select(transaction => new TransactionModel
            {
                Id = transaction.Id,
                TransactionType = transaction.TransactionType,
                TotalAmount = transaction.TotalAmount,
                CreateDate = transaction.CreatedDate,
                CategoryId = transaction.CategoryId,
                VendorId = transaction.VendorId,
                Category = new TransactionCategoryModel
                {
                    Id = transaction.Category.Id,
                    Name = transaction.Category.Name
                },
                Vendor = new VendorModel
                {
                    Id = transaction.Vendor.Id,
                    Name = transaction.Vendor.VendorName
                },
                Items = transaction.TransactionItems.Select(item => new TransactionItemModel
                {
                    Id = item.Id,
                    ItemName = item.ItemName,
                    Total = item.Total,
                    Quantity = item.Quantity,
                    Price = item.Price
                }).ToList(),
            }).ToList();
        }

        public async Task UpdateTransaction(UpdateTransactionRequest transaction)
        {
            var existing = await _transactionRepository.GetByIdAsync(transaction.Id);
            if (existing is null) throw new NotFoundException($"Transaction with id:{transaction.Id} is not found !!!");

            var amount = 0m;
            if (existing.TransactionType == TransactionTypeEnum.Expense && transaction.TransactionType == TransactionTypeEnum.Expense)
            {
                amount = transaction.TotalAmount - existing.TotalAmount;
            }
            else if (existing.TransactionType == TransactionTypeEnum.Income && transaction.TransactionType == TransactionTypeEnum.Expense)
            {
                amount = transaction.TotalAmount;
            }
            else if (existing.TransactionType == TransactionTypeEnum.Expense && transaction.TransactionType == TransactionTypeEnum.Income)
            { 
                amount = -existing.TotalAmount;
            }

            await _budgetRepository.UpdateBudgetByCategory(transaction.UserId, amount, transaction.CategoryId);

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
