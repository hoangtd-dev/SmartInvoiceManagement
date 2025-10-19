using Microsoft.AspNetCore.Mvc;
using SIM.Core.Interfaces.Services;
using SIM.Presentation.Pages.Base;

namespace SIM.Presentation.Pages.Transactions
{
    public class IndexModel : BasePageModel
    {
        private readonly ITransactionService _transactionService;
        private readonly IBudgetService _budgetService;
        public ICollection<Core.DTOs.Responses.TransactionModel> Transactions { get; set; }
        public int OverBudgetCount { get; set; }

        public IndexModel(ITransactionService transactionService, IBudgetService budgetService)
        {
            _transactionService = transactionService;
            _budgetService = budgetService;
        }

        public async Task<IActionResult> OnGetAsync()
        {
            if (!IsAuthenticated) return RedirectToPage("/Login");

            Transactions = await _transactionService.GetTransactions();

            OverBudgetCount = await _budgetService.OverBudgetCount(CurrentUserId);

            return Page();
        }
    }
}
