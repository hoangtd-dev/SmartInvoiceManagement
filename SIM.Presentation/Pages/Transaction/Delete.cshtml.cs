using Microsoft.AspNetCore.Mvc;
using SIM.Core.Enums;
using SIM.Core.Exceptions;
using SIM.Core.Interfaces.Services;
using SIM.Presentation.Pages.Base;

namespace SIM.Presentation.Pages.Transaction
{
    public class DeleteModel : BasePageModel
    {
        [BindProperty(SupportsGet = true)]
        public int? Id { get; set; }
        private readonly ITransactionService _TransactionService;
        public DeleteModel(ITransactionService TransactionService)
        {
            _TransactionService = TransactionService;
        }
        public async Task<IActionResult> OnGetAsync()
        {
            if (!IsAuthenticated) return RedirectToPage("/Login");

            try
            {
                await _TransactionService.DeleteTransaction (Id.Value);
                TempData["ToastStatus"] = ToastStatusEnum.Success;
                TempData["ToastMessage"] = "Transaction deleted successfully!";

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

            return RedirectToPage("/Transaction/Index");
        }
    }
}
