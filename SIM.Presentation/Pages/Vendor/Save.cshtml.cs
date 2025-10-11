using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc.Rendering;
using SIM.Core.DTOs.Requests;
using SIM.Core.Enums;
using SIM.Core.Exceptions;
using SIM.Core.Interfaces.Services;

namespace SIM.Presentation.Pages.Vendor
{
    public class SaveModel : PageModel
    {
        [BindProperty(SupportsGet = true)]
        public int? Id { get; set; }
        [BindProperty]
        public Core.DTOs.Responses.VendorModel Vendor { get; set; } = new Core.DTOs.Responses.VendorModel();

        private readonly IVendorService _vendorService;

        public SaveModel(IVendorService vendorService)
        {
            _vendorService = vendorService;
        }
        public async Task OnGetAsync()
        {
            if (Id.HasValue)
            {
                try
                {
                    Vendor = await _vendorService.GetVendorById(Id.Value);
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
            }
        }

        public IActionResult OnPostBack()
        {
            return RedirectToPage("/Vendor/Index");
        }

        public async Task<IActionResult> OnPostSaveAsync()
        {
            if (!ModelState.IsValid) return Page();

            try
            {
                if (Id.HasValue)
                {
                    var vendor = new UpdateVendorRequest { 
                        Id = Id.Value,
                        VendorName = Vendor.Name,
                        ContactPhone = Vendor.ContactPhone,
                        Address = Vendor.Address,
                        ContactEmail = Vendor.ContactEmail,
                    };
                    await _vendorService.UpdateVendor(vendor);
                    TempData["ToastStatus"] = ToastStatusEnum.Success;
                    TempData["ToastMessage"] = "Vendor updated successfully!";
                }
                else
                {
                    var vendor = new CreateVendorRequest
                    {
                        VendorName = Vendor.Name,
                        ContactPhone = Vendor.ContactPhone,
                        Address = Vendor.Address,
                        ContactEmail = Vendor.ContactEmail,
                    };
                    await _vendorService.AddVendor(vendor);
                    TempData["ToastStatus"] = ToastStatusEnum.Success;
                    TempData["ToastMessage"] = "Vendor created successfully!";
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
