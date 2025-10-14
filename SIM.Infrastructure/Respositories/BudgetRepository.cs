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
                .ToListAsync();
        }

        public async Task<ICollection<Budget>> GetActiveBudgets(int userId)
        {
            return await _appDbContext.Budgets
                .Include(x => x.Category)
                .Where(x => x.UserId == userId && x.Status == BudgetStatusEnum.Active && !x.IsDeleted)
                .ToListAsync();
        }

        public async Task UpdateAsync(Budget entity)
        {
            _appDbContext.Budgets.Update(entity);
            await _appDbContext.SaveChangesAsync();
        }
    }
}
