using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using SIM.Core.Enums;
using SIM.Core.Exceptions;
using SIM.Core.Interfaces.Services;

namespace SIM.Presentation.Pages.Product
{
    public class DeleteModel : PageModel
    {
        [BindProperty(SupportsGet = true)]
        public int? Id { get; set; }
        private readonly IProductService _productService;
        public DeleteModel(IProductService productService)
        {
            _productService = productService;
        }
        public async Task<IActionResult> OnGetAsync()
        {
            try
            {
                await _productService.DeleteProduct(Id.Value);
                TempData["ToastStatus"] = ToastStatusEnum.Success;
                TempData["ToastMessage"] = "Product deleted successfully!";
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
            return RedirectToPage("/Product/Index");
        }
    }
}
