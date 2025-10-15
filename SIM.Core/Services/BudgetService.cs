using SIM.Core.DTOs.Requests;
using SIM.Core.DTOs.Responses;
using SIM.Core.Entities;
using SIM.Core.Enums;
using SIM.Core.Exceptions;
using SIM.Core.Interfaces.Repositories;
using SIM.Core.Interfaces.Services;

namespace SIM.Core.Services
{
    public class BudgetService : IBudgetService
    {
        private readonly IBudgetRepository _budgetRepository;
        private readonly ITransactionRepository _transactionRepository;
        public BudgetService(IBudgetRepository budgetRepository, ITransactionRepository transactionRepository)
        {
            _budgetRepository = budgetRepository;
            _transactionRepository = transactionRepository;
        }
        public async Task AddBudget(CreateBudgetRequest budget)
        {
            var transactions = await _transactionRepository.GetIncomeExpenseOfCurrentUserAsync(budget.UserId, budget.StartDate, budget.EndDate, budget.CategoryId);
            var totalExpense = transactions.Where(t => t.TransactionType == TransactionTypeEnum.Expense).Sum(transaction => transaction.TotalAmount);

            var newBudget = new Budget
            { 
                CategoryId = budget.CategoryId,
                StartDate = budget.StartDate,
                EndDate = budget.EndDate,
                TotalAmount = budget.TotalAmount,
                TotalExpense = totalExpense,
                UserId = budget.UserId,
                Status = BudgetStatusEnum.Active
            };

            await _budgetRepository.AddAsync(newBudget);
        }

        public async Task<bool> CheckOverBudget(int userId, DateTime createdDate, decimal totalAmount, int? categoryId)
        {
            var budget = await _budgetRepository.GetExistingBudgetByCreatedDateAsync(userId, createdDate, categoryId);
            if (budget == null) throw new NotFoundException($"Budget is not found !!!");

            return (totalAmount + budget.TotalExpense) > budget.TotalAmount;
        }

        public async Task DeleteBudget(int id)
        {
            var budget = await _budgetRepository.GetByIdAsync(id);

            if (budget is null) throw new NotFoundException($"Budget with id:{id} is not found !!!");

            await _budgetRepository.DeleteAsync(budget);
        }

        public async Task<ICollection<BudgetModel>> GetActiveBudgets(int userId)
        {
            var budgets = await _budgetRepository.GetActiveBudgets(userId);

            return budgets.Select(budget => new BudgetModel
            {
                Id = budget.Id,
                TotalAmount = budget.TotalAmount,
                TotalExpense = budget.TotalExpense,
                StartDate = budget.StartDate,
                EndDate = budget.EndDate,
                CategoryId = budget.Category is not null ? budget.Category.Id : null,
                Category = budget.Category is not null ? new TransactionCategoryModel
                {
                    Id = budget.Category.Id,
                    Name = budget.Category.Name,
                } : null
            }).ToList();
        }

        public async Task<BudgetModel> GetBudgetById(int id)
        {
            var budget = await _budgetRepository.GetByIdAsync(id);

            if (budget is null) throw new NotFoundException($"Budget with id:{id} is not found !!!");

            return new BudgetModel
            {
                Id = budget.Id,
                TotalAmount = budget.TotalAmount,
                StartDate = budget.StartDate,
                EndDate = budget.EndDate,
                CategoryId = budget.Category is not null ? budget.Category.Id : null,
                Category = budget.Category is not null ? new TransactionCategoryModel 
                { 
                    Id = budget.Category.Id,
                    Name = budget.Category.Name,
                } : null
            };
        }

        public async Task<ICollection<BudgetModel>> GetExpiredBudgets(int userId)
        {
            var budgets = await _budgetRepository.GetExpiredBudgets(userId);

            return budgets.Select(budget => new BudgetModel {
                Id = budget.Id,
                TotalAmount = budget.TotalAmount,
                TotalExpense = budget.TotalExpense,
                StartDate = budget.StartDate,
                EndDate = budget.EndDate,
                CategoryId = budget.Category is not null ? budget.Category.Id : null,
                Category = budget.Category is not null ? new TransactionCategoryModel
                {
                    Id = budget.Category.Id,
                    Name = budget.Category.Name,
                } : null
            }).ToList();
        }

        public async Task<bool> HasActiveBudget(int userId, int? categoryId)
        {
            var hasActiveBudget = await _budgetRepository.HasActiveBudgetAsync(userId, categoryId);

            if (hasActiveBudget) throw new DuplicateException("Please update current budget with this category type or delete it to create new one !!!");

            return hasActiveBudget;
        }

        public Task<int> OverBudgetCount(int userId)
        {
            return _budgetRepository.OverBudgetCount(userId);
        }

        public async Task UpdateBudget(UpdateBudgetRequest updatedBudget)
        {
            var budget = await _budgetRepository.GetByIdAsync(updatedBudget.Id);

            if (budget is null) throw new NotFoundException($"Budget with id:{updatedBudget!.Id} is not found !!!");

            budget.TotalAmount = updatedBudget.TotalAmount;
            budget.StartDate = updatedBudget.StartDate;
            budget.EndDate = updatedBudget.EndDate;

            await _budgetRepository.UpdateAsync(budget);
        }
    }
}
