using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using SIM.Core.DTOs.Requests;
using SIM.Core.Enums;
using SIM.Core.Interfaces.Services;
using SIM.Presentation.Pages.Base;

namespace SIM.Presentation.Pages.Transactions
{
    public class CreateModel : BasePageModel
    {
        private readonly IVendorService _vendorService;
        private readonly ITransactionCategoryService _transactionCategoryService;
        private readonly ITransactionItemService _transactionItemService;
        private readonly ITransactionService _transactionService;

        public CreateModel(IVendorService vendorService, ITransactionCategoryService transactionCategoryService, ITransactionItemService transactionItemService, ITransactionService transactionService)
        {
            _vendorService = vendorService;
            _transactionCategoryService = transactionCategoryService;
            _transactionItemService = transactionItemService;
            _transactionService = transactionService;

        }
        [BindProperty]
        public TransactionInput Input { get; set; } = new TransactionInput();

        [BindProperty]
        public VendorInput NewVendor { get; set; } = new VendorInput();

        public IEnumerable<SelectListItem> TransactionTypeOptions { get; set; } = Enumerable.Empty<SelectListItem>();
        public IEnumerable<SelectListItem> CategoryOptions { get; set; } = Enumerable.Empty<SelectListItem>();
        public IEnumerable<SelectListItem> VendorOptions { get; set; } = Enumerable.Empty<SelectListItem>();

        public async Task OnGetAsync(int? newVendorId)
        {
            await LoadOptionsAsync();

            if (newVendorId.HasValue)
            {
                Input.VendorId = newVendorId.Value;
            }
        }

        public async Task<IActionResult> OnPostCreateVendorAsync()
        {
            if (string.IsNullOrWhiteSpace(NewVendor.VendorName))
            {
                ModelState.AddModelError("NewVendor.VendorName", "Vendor name is required");
                await LoadOptionsAsync();
                return Page();
            }

            var vendorReq = new CreateVendorRequest
            {
                VendorName = NewVendor.VendorName,
                ContactEmail = NewVendor.ContactEmail,
                ContactPhone = NewVendor.ContactPhone,
                Address = NewVendor.Address
            };

            var created = await _vendorService.AddVendor(vendorReq);

            return RedirectToPage("/Transactions/Create", new { newVendorId = created.Id });
        }

        public async Task<IActionResult> OnPostAsync()
        {
            if (!IsAuthenticated)
            {
                return RedirectToPage("/Login");
            }
            if (Input.VendorId == 0 && !string.IsNullOrWhiteSpace(NewVendor?.VendorName))
            {
                var vendorReq = new CreateVendorRequest
                {
                    VendorName = NewVendor.VendorName,
                    Address = NewVendor.Address,
                    ContactEmail = NewVendor.ContactEmail,
                    ContactPhone = NewVendor.ContactPhone
                };

                var createdVendor = await _vendorService.AddVendor(vendorReq);
                if (createdVendor != null) Input.VendorId = createdVendor.Id;
            }

            try
            {
                var createTxReq = new CreateTransactionRequest
                {
                    UserId = CurrentUserId,
                    VendorId = Input.VendorId,
                    CategoryId = Input.CategoryId,
                    TotalAmount = Input.TotalAmount,
                    TransactionType = Input.TransactionType
                };

                var createdTransaction = await _transactionService.CreateTransaction(createTxReq);
                if (createdTransaction == null)
                {
                    ModelState.AddModelError(string.Empty, "Unable to create transaction.");
                    await LoadOptionsAsync();
                    return Page();
                }

                if (Input.Items?.Any() == true)
                {
                    foreach (var item in Input.Items)
                    {
                        var req = new CreateTransactionItemRequest
                        {
                            TransactionId = createdTransaction.Id,
                            Quantity = item.Quantity,
                            Price = item.Price,
                            Total = item.Total
                        };

                        await _transactionItemService.CreateTransactionItem(req);
                    }
                }
                TempData["ToastMessage"] = "Transaction created successfully.";
                TempData["ToastStatus"] = ToastStatusEnum.Success;
                return RedirectToPage("/Transaction/Index");
            }
            catch (Exception e)
            {
                TempData["ToastMessage"] = $"Failed to create transaction: {e.Message}";
                TempData["ToastStatus"] = ToastStatusEnum.Fail;
                await LoadOptionsAsync();
                return Page();
            }
        }

        private async Task LoadOptionsAsync()
        {
            TransactionTypeOptions = Enum.GetValues(typeof(TransactionTypeEnum))
                .Cast<TransactionTypeEnum>()
                .Select(e => new SelectListItem { Value = ((int)e).ToString(), Text = e.ToString() })
                .ToList();

            var cats = await _transactionCategoryService.GetTransactionCategories();
            CategoryOptions = cats.Select(c => new SelectListItem { Value = c.Id.ToString(), Text = c.Name }).ToList();

            var vendors = await _vendorService.GetVendors();
            VendorOptions = vendors.Select(v => new SelectListItem { Value = v.Id.ToString(), Text = v.Name }).ToList();
        }
    }
}
