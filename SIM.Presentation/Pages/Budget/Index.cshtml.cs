using Microsoft.AspNetCore.Mvc;
using SIM.Core.Interfaces.Services;
using SIM.Presentation.Pages.Base;
using SIM.Presentation.ViewModels;

namespace SIM.Presentation.Pages.Budget
{
    public class BudgetModel : BasePageModel
    {
        public ICollection<BudgetViewModel> Budgets { get; set; }
        private readonly IBudgetService _budgetService;
        public BudgetModel(IBudgetService budgetService)
        {
            _budgetService = budgetService;
        }
        public async Task<IActionResult> OnGetAsync()
        {
            if (!IsAuthenticated) return RedirectToPage("/Login");

            Budgets = (await _budgetService.GetActiveBudgets(CurrentUserId)).Select(x => new BudgetViewModel 
            { 
                Id = x.Id,
                Category = x.Category,
                CategoryId = x.CategoryId,
                EndDate = x.EndDate,
                StartDate = x.StartDate,
                TotalAmount = x.TotalAmount,
                TotalExpense = x.TotalExpense,
                Percentage = Math.Round((x.TotalExpense / x.TotalAmount) * 100, 1)
            }).ToList();

            return Page();
        }
    }
}
