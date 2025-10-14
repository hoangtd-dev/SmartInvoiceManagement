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
        public BudgetService(IBudgetRepository budgetRepository)
        {
            _budgetRepository = budgetRepository;
        }
        public async Task AddBudget(CreateBudgetRequest budget)
        {
            var newBudget = new Budget
            { 
                CategoryId = budget.CategoryId,
                StartDate = budget.StartDate,
                EndDate = budget.EndDate,
                TotalAmount = budget.TotalAmount,
                Status = BudgetStatusEnum.Active
            };

            await _budgetRepository.AddAsync(newBudget);
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
                StartDate = budget.StartDate,
                EndDate = budget.EndDate,
                CategoryId = budget.Category.Id,
                Category = new TransactionCategoryModel
                {
                    Id = budget.Category.Id,
                    Name = budget.Category.Name,
                }
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
                CategoryId = budget.Category.Id,
                Category = new TransactionCategoryModel 
                { 
                    Id = budget.Category.Id,
                    Name = budget.Category.Name,
                }
            };
        }

        public async Task<ICollection<BudgetModel>> GetExpiredBudgets(int userId)
        {
            var budgets = await _budgetRepository.GetExpiredBudgets(userId);

            return budgets.Select(budget => new BudgetModel {
                Id = budget.Id,
                TotalAmount = budget.TotalAmount,
                StartDate = budget.StartDate,
                EndDate = budget.EndDate,
                CategoryId = budget.Category.Id,
                Category = new TransactionCategoryModel
                {
                    Id = budget.Category.Id,
                    Name = budget.Category.Name,
                }
            }).ToList();
        }

        public async Task UpdateBudget(UpdateBudgetRequest updatedBudget)
        {
            var budget = await _budgetRepository.GetByIdAsync(updatedBudget.Id);

            if (budget is null) throw new NotFoundException($"Budget with id:{updatedBudget!.Id} is not found !!!");

            budget.CategoryId = updatedBudget.CategoryId;
            budget.TotalAmount = updatedBudget.TotalAmount;
            budget.StartDate = updatedBudget.StartDate;
            budget.EndDate = updatedBudget.EndDate;

            await _budgetRepository.UpdateAsync(budget);
        }
    }
}
