using SIM.Core.Entities;
using SIM.Core.Interfaces.Repositories.Base;

namespace SIM.Core.Interfaces.Repositories
{
    public interface IBudgetRepository : IRepositoryBase<Budget>
    {
        Task UpdateBudgetByCategory(int userId, decimal amount, int categoryId, DateTime createDate);
        Task UpdateBudgetToExpire(int userId);
        Task<ICollection<Budget>> GetActiveBudgets(int userId);
        Task<ICollection<Budget>> GetExpiredBudgets(int userId);
        Task<int> OverBudgetCount(int userId);
        Task<bool> HasActiveBudgetAsync(int userId, int? categoryId);
        Task<Budget?> GetExistingBudgetByCreatedDateAsync(int userId, DateTime createdDate, int? categoryId);
    }
}
