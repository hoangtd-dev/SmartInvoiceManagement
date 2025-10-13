using Microsoft.AspNetCore.Mvc.RazorPages;
using SIM.Core.Interfaces.Services;

namespace SIM.Presentation.Pages.TransactionCategory
{
    public class TransactionCategoryModel : PageModel
    {
        public ICollection<Core.DTOs.Responses.TransactionCategoryModel> TransactionCategories { get; set; }
        private readonly ITransactionCategoryService _transactionCategoryService;
        public TransactionCategoryModel(ITransactionCategoryService transactionCategoryService)
        {
            _transactionCategoryService = transactionCategoryService;
        }
        public async Task OnGetAsync()
        {
            TransactionCategories = await _transactionCategoryService.GetTransactionCategories();
        }
    }
}
