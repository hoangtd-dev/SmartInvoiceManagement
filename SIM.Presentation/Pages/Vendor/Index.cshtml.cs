using Microsoft.AspNetCore.Mvc;
using SIM.Core.Interfaces.Services;
using SIM.Presentation.Pages.Base;

namespace SIM.Presentation.Pages.Vendor
{
    public class VendorModel : BasePageModel
    {
        public ICollection<Core.DTOs.Responses.VendorModel> Vendors { get; set; }
        private readonly IVendorService _vendorService;
        public VendorModel(IVendorService vendorService)
        {
            _vendorService = vendorService;
        }
        public async Task<IActionResult> OnGetAsync()
        {
            if (!IsAuthenticated) return RedirectToPage("/Login");

            Vendors = await _vendorService.GetVendors();
            return Page();
        }
    }
}
