using Microsoft.AspNetCore.Mvc;
using SIM.Core.Enums;
using SIM.Core.Exceptions;
using SIM.Core.Interfaces.Services;
using SIM.Presentation.Pages.Base;

namespace SIM.Presentation.Pages.TransactionCategory
{
    public class DeleteModel : BasePageModel
    {
        [BindProperty(SupportsGet = true)]
        public int? Id { get; set; }
        private readonly ITransactionCategoryService _transactionCategoryService;
        public DeleteModel(ITransactionCategoryService transactionCategoryService)
        {
            _transactionCategoryService = transactionCategoryService;
        }
        public async Task<IActionResult> OnGetAsync()
        {
            if (!IsAuthenticated) return RedirectToPage("/Login");

            try
            {
                await _transactionCategoryService.DeleteCategory(Id.Value);
                TempData["ToastStatus"] = ToastStatusEnum.Success;
                TempData["ToastMessage"] = "Category deleted successfully!";
            }
            catch (NotFoundException ex)
            {
                TempData["ToastStatus"] = ToastStatusEnum.Fail;
                TempData["ToastMessage"] = ex.Message;
            }
            catch (Exception)
            {
                TempData["ToastStatus"] = ToastStatusEnum.Fail;
                TempData["ToastMessage"] = "System Error !!!";
            }
            return RedirectToPage("/TransactionCategory/Index");
        }
    }
}
