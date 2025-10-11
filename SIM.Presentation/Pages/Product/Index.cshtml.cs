using Microsoft.AspNetCore.Mvc.RazorPages;
using SIM.Core.Interfaces.Services;

namespace SIM.Presentation.Pages.Product
{
    public class ProductModel : PageModel
    {
        public ICollection<Core.DTOs.Responses.ProductModel> Products { get; set; }
        private readonly IProductService _productService;
        public ProductModel(IProductService productService)
        {
            _productService = productService;
        }
        public async Task OnGetAsync()
        {
            Products = await _productService.GetProducts();
        }
    }
}
