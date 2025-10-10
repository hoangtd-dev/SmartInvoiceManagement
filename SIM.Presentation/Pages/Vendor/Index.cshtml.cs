using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using SIM.Core.Interfaces.Services;

namespace SIM.Presentation.Pages.Vendor
{
    public class VendorModel : PageModel
    {
        public ICollection<Core.DTOs.Responses.VendorModel> Vendors { get; set; }
        private readonly IVendorService _vendorService;
        public VendorModel(IVendorService vendorService)
        {
            _vendorService = vendorService;
        }
        public async Task OnGetAsync()
        {
            Vendors = await _vendorService.GetVendors();
        }
    }
}
