using Microsoft.AspNetCore.Mvc;
using SIM.Core.Enums;
using SIM.Core.Exceptions;
using SIM.Core.Interfaces.Services;
using SIM.Presentation.Pages.Base;

namespace SIM.Presentation.Pages.Product
{
    public class DeleteModel : BasePageModel
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
            if (!IsAuthenticated) return RedirectToPage("/Login");

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
