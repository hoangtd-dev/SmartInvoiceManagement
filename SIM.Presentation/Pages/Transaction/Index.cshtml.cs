using Microsoft.AspNetCore.Mvc;
using SIM.Core.Interfaces.Services;
using SIM.Presentation.Pages.Base;

namespace SIM.Presentation.Pages.Transactions
{
    public class IndexModel : BasePageModel
    {
        private readonly ITransactionService _transactionService;
        public ICollection<Core.DTOs.Responses.TransactionModel> Transactions { get; set; }

        public IndexModel(ITransactionService transactionService)
        {
            _transactionService = transactionService;
        }

        public async Task<IActionResult> OnGetAsync()
        {
            if (!IsAuthenticated) return RedirectToPage("/Login");

            Transactions = await _transactionService.GetTransactions();
            return Page();
        }
    }
}
