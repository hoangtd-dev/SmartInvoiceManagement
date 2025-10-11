using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc.Rendering;
using SIM.Core.DTOs.Requests;
using SIM.Core.Enums;
using SIM.Core.Exceptions;
using SIM.Core.Interfaces.Services;

namespace SIM.Presentation.Pages.Product
{
    public class SaveModel : PageModel
    {
        [BindProperty(SupportsGet = true)]
        public int? Id { get; set; }
        public List<SelectListItem> Vendors { get; set; } = new List<SelectListItem>();
        [BindProperty]
        public Core.DTOs.Responses.ProductModel Product { get; set; } = new Core.DTOs.Responses.ProductModel();

        private readonly IProductService _productService;
        private readonly IVendorService _vendorService;

        public SaveModel(IProductService productService, IVendorService vendorService)
        {
            _productService = productService;
            _vendorService = vendorService;
        }
        public async Task<IActionResult> OnGetAsync()
        {
            await GetVendors();

            if (Id.HasValue)
            {
                try
                {
                    Product = await _productService.GetProductById(Id.Value);
                }
                catch (NotFoundException ex)
                {
                    TempData["ToastStatus"] = ToastStatusEnum.Fail;
                    TempData["ToastMessage"] = ex.Message;
                    return RedirectToPage("/Product/Index");
                }
                catch (Exception)
                {
                    TempData["ToastStatus"] = ToastStatusEnum.Fail;
                    TempData["ToastMessage"] = "System Error !!!";
                }
            }
            return Page();
        }

        private async Task GetVendors()
        {
            try
            {
                var vendors = await _vendorService.GetVendors();
                foreach (var vendor in vendors)
                {
                    Vendors.Add(new SelectListItem { 
                        Value = vendor.Id.ToString(),
                        Text = vendor.Name
                    });
                }
            }
            catch (Exception)
            {
                TempData["ToastStatus"] = ToastStatusEnum.Fail;
                TempData["ToastMessage"] = "System Error !!!";
            }
        }

        public IActionResult OnPostBack()
        {
            return RedirectToPage("/Product/Index");
        }

        public async Task<IActionResult> OnPostSaveAsync()
        {
            if (!ModelState.IsValid) return Page();
            try
            {
                if (Id.HasValue)
                {
                    var product = new UpdateProductRequest { 
                        Id = Id.Value,
                        Description = Product.Description,
                        Price = Product.Price,
                        ProductName = Product.Name,
                        StockQuantity = Product.StockQuantity,
                        VendorId = Product.VendorId
                    };
                    await _productService.UpdateProduct(product);
                    TempData["ToastStatus"] = ToastStatusEnum.Success;
                    TempData["ToastMessage"] = "Product updated successfully!";
                }
                else
                {
                    var product = new CreateProductRequest
                    {
                        Description = Product.Description,
                        Price = Product.Price,
                        ProductName = Product.Name,
                        StockQuantity = Product.StockQuantity,
                        VendorId = Product.VendorId
                    };
                    await _productService.AddProduct(product);
                    TempData["ToastStatus"] = ToastStatusEnum.Success;
                    TempData["ToastMessage"] = "Product created successfully!";
                }
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
            return RedirectToPage("./Index");
        }
    }
}
