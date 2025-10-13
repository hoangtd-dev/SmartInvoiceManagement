using Microsoft.AspNetCore.Mvc;
using SIM.Core.Interfaces.Services;
using SIM.Presentation.Pages.Base;

namespace SIM.Presentation.Pages.Product
{
    public class ProductModel : BasePageModel
    {
        public ICollection<Core.DTOs.Responses.ProductModel> Products { get; set; }
        private readonly IProductService _productService;
        public ProductModel(IProductService productService)
        {
            _productService = productService;
        }
        public async Task<IActionResult> OnGetAsync()
        {
            if (!IsAuthenticated) return RedirectToPage("/Login");

            Products = await _productService.GetProducts();
            return Page();
        }
    }
}
