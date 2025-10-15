using Microsoft.EntityFrameworkCore;
using SIM.Core.Entities;
using SIM.Core.Enums;
using SIM.Core.Interfaces.Repositories;

namespace SIM.Infrastructure.Respositories
{
    internal class BudgetRepository : IBudgetRepository
    {
        private readonly AppDbContext _appDbContext;
        public BudgetRepository(AppDbContext appDbContext)
        {
            _appDbContext = appDbContext;
        }
        public async Task<Budget> AddAsync(Budget entity)
        {
            var budget = await _appDbContext.Budgets.AddAsync(entity);
            await _appDbContext.SaveChangesAsync();
            return budget.Entity;
        }

        public async Task DeleteAsync(Budget entity)
        {
            entity.IsDeleted = true;
            await _appDbContext.SaveChangesAsync();
        }

        public async Task<ICollection<Budget>> GetAllAsync()
        {
            return await _appDbContext.Budgets
                .Include(x => x.Category)
                .Where(x => !x.IsDeleted)
                .OrderBy(x => x.Category == null ? 0 : 1)
                .ToListAsync();
        }

        public async Task<Budget?> GetByIdAsync(int id)
        {
            return await _appDbContext.Budgets
                .Include(x => x.Category)
                .Where(x => !x.IsDeleted)
                .FirstOrDefaultAsync(x => x.Id == id);
        }

        public async Task<ICollection<Budget>> GetExpiredBudgets(int userId)
        {
            return await _appDbContext.Budgets
                .Include(x => x.Category)
                .Where(x => x.UserId == userId && x.Status == BudgetStatusEnum.Expired && !x.IsDeleted)
                .OrderBy(x => x.Category == null ? 0 : 1)
                .ToListAsync();
        }

        public async Task<ICollection<Budget>> GetActiveBudgets(int userId)
        {
            return await _appDbContext.Budgets
                .Include(x => x.Category)
                .Where(x => x.UserId == userId && x.Status == BudgetStatusEnum.Active && !x.IsDeleted)
                .OrderBy(x => x.Category == null ? 0 : 1)
                .ToListAsync();
        }

        public async Task UpdateAsync(Budget entity)
        {
            _appDbContext.Budgets.Update(entity);
            await _appDbContext.SaveChangesAsync();
        }

        public async Task<Budget?> GetExistingBudgetByCreatedDateAsync(int userId, DateTime createdDate, int? categoryId)
        {
            var query = _appDbContext.Budgets.AsQueryable();

            if (categoryId is not null)
            {
                query = query.Where(x => x.CategoryId == categoryId);
            }

            return await query.FirstOrDefaultAsync(x => x.UserId == userId && x.StartDate <= createdDate && createdDate <= x.EndDate && x.Status == BudgetStatusEnum.Active);
        }

        public async Task<int> OverBudgetCount(int userId)
        {
            return await _appDbContext.Budgets
                .Where(x => x.UserId == userId && x.Status == BudgetStatusEnum.Active && x.TotalExpense > x.TotalAmount)
                .CountAsync();
        }

        public async Task<bool> HasActiveBudgetAsync(int userId, int? categoryId)
        {
            return await _appDbContext.Budgets.AnyAsync(x => x.UserId == userId && x.CategoryId == categoryId && x.Status == BudgetStatusEnum.Active);
        }
    }
}
