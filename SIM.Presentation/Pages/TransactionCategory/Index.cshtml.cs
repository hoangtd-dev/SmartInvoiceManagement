using Microsoft.AspNetCore.Mvc;
using SIM.Core.Interfaces.Services;
using SIM.Presentation.Pages.Base;

namespace SIM.Presentation.Pages.TransactionCategory
{
    public class TransactionCategoryModel : BasePageModel
    {
        public ICollection<Core.DTOs.Responses.TransactionCategoryModel> TransactionCategories { get; set; }
        private readonly ITransactionCategoryService _transactionCategoryService;
        public TransactionCategoryModel(ITransactionCategoryService transactionCategoryService)
        {
            _transactionCategoryService = transactionCategoryService;
        }
        public async Task<IActionResult> OnGetAsync()
        {
            if (!IsAuthenticated) return RedirectToPage("/Login");

            TransactionCategories = await _transactionCategoryService.GetTransactionCategories();
            return Page();
        }
    }
}
