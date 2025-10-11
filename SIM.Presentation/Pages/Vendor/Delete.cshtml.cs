using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using SIM.Core.Enums;
using SIM.Core.Exceptions;
using SIM.Core.Interfaces.Services;

namespace SIM.Presentation.Pages.Vendor
{
    public class DeleteModel : PageModel
    {
        [BindProperty(SupportsGet = true)]
        public int? Id { get; set; }
        private readonly IVendorService _vendorService;
        public DeleteModel(IVendorService vendorService)
        {
            _vendorService = vendorService;
        }
        public async Task<IActionResult> OnGetAsync()
        {
            try
            {
                await _vendorService.DeleteVendor(Id.Value);
                TempData["ToastStatus"] = ToastStatusEnum.Success;
                TempData["ToastMessage"] = "Vendor deleted successfully!";

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

            return RedirectToPage("/Vendor/Index");
        }
    }
}
