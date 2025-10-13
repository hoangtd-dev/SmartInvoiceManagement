using SIM.Core.DTOs.Requests;
using SIM.Core.DTOs.Responses;

namespace SIM.Core.Interfaces.Services
{
    public interface IBudgetService
    {
        Task AddBudget(CreateBudgetRequest budget);
        Task UpdateBudget(UpdateBudgetRequest budget);
        Task<ICollection<BudgetModel>> GetActiveBudgets(int userId);
        Task<ICollection<BudgetModel>> GetExpiredBudgets(int userId);
        Task<BudgetModel> GetBudgetById(int id);
        Task DeleteBudget(int id);
    }
}
