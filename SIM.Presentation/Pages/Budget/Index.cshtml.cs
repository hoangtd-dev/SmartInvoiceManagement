using Microsoft.AspNetCore.Mvc;
using SIM.Core.Interfaces.Services;
using SIM.Presentation.Pages.Base;

namespace SIM.Presentation.Pages.Budget
{
    public class BudgetModel : BasePageModel
    {
        public ICollection<Core.DTOs.Responses.BudgetModel> Budgets { get; set; }
        private readonly IBudgetService _budgetService;
        public BudgetModel(IBudgetService budgetService)
        {
            _budgetService = budgetService;
        }
        public async Task<IActionResult> OnGetAsync()
        {
            if (!IsAuthenticated) return RedirectToPage("/Login");

            Budgets = await _budgetService.GetActiveBudgets(CurrentUserId);
            return Page();
        }
    }
}
