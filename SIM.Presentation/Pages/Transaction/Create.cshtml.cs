using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using SIM.Core.Entities;
using SIM.Core.Enums;
using SIM.Infrastructure;

namespace SIM.Presentation.Pages.Transactions
{
    public class CreateModel : PageModel
    {
        private readonly AppDbContext _db;

        public CreateModel(AppDbContext db)
        {
            _db = db;
        }

        [BindProperty]
        public TransactionInput Input { get; set; } = new TransactionInput();

        [BindProperty]
        public VendorInput NewVendor { get; set; } = new VendorInput();

        public IEnumerable<SelectListItem> TransactionTypeOptions { get; set; } = Enumerable.Empty<SelectListItem>();
        public IEnumerable<SelectListItem> CategoryOptions { get; set; } = Enumerable.Empty<SelectListItem>();
        public IEnumerable<SelectListItem> VendorOptions { get; set; } = Enumerable.Empty<SelectListItem>();
        public IEnumerable<SelectListItem> ProductOptions { get; set; } = Enumerable.Empty<SelectListItem>();

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

            var vendor = new Vendor
            {
                VendorName = NewVendor.VendorName,
                ContactEmail = NewVendor.ContactEmail,
                ContactPhone = NewVendor.ContactPhone,
                Address = NewVendor.Address,
                CreatedDate = DateTime.UtcNow
            };

            _db.Vendors.Add(vendor);
            await _db.SaveChangesAsync();

            // Redirect and preselect the newly created vendor
            return RedirectToPage("/Transactions/Create", new { newVendorId = vendor.Id });
        }

        public async Task<IActionResult> OnPostAsync()
        {
            if (!ModelState.IsValid)
            {
                await LoadOptionsAsync();
                return Page();
            }

            // Temporary: get user id from authentication. Replace with real auth later.
            var userId = 1;

            var transaction = new Core.Entities.Transaction
            {
                TransactionType = Input.TransactionType,
                CategoryId = Input.CategoryId,
                VendorId = Input.VendorId,
                TotalAmount = Input.TotalAmount,
                UserId = userId,
                CreatedDate = DateTime.UtcNow
            };

            _db.Transactions.Add(transaction);
            await _db.SaveChangesAsync(); // get transaction.Id

            if (Input.Items?.Any() == true)
            {
                foreach (var item in Input.Items)
                {
                    var txItem = new TransactionItem
                    {
                        TransactionId = transaction.Id,
                        ProductId = item.ProductId,
                        Quantity = item.Quantity,
                        Price = item.Price,
                        Total = item.Total
                    };

                    _db.TransactionItems.Add(txItem);
                }

                await _db.SaveChangesAsync();
            }

            // After create go back to dashboard or transactions list
            return RedirectToPage("/Dashboard/Index");
        }

        private async Task LoadOptionsAsync()
        {
            TransactionTypeOptions = Enum.GetValues(typeof(TransactionTypeEnum))
                .Cast<TransactionTypeEnum>()
                .Select(e => new SelectListItem { Value = ((int)e).ToString(), Text = e.ToString() })
                .ToList();

            var cats = await _db.Set<TransactionCategory>().AsNoTracking().ToListAsync();
            CategoryOptions = cats.Select(c => new SelectListItem { Value = c.Id.ToString(), Text = c.Name }).ToList();

            var vendors = await _db.Vendors.AsNoTracking().ToListAsync();
            VendorOptions = vendors.Select(v => new SelectListItem { Value = v.Id.ToString(), Text = v.VendorName }).ToList();

            var products = await _db.Products.AsNoTracking().ToListAsync();
            ProductOptions = products.Select(p => new SelectListItem { Value = p.Id.ToString(), Text = p.ProductName }).ToList();
        }

        public class TransactionInput
        {
            public TransactionTypeEnum TransactionType { get; set; }
            public decimal TotalAmount { get; set; }
            public int CategoryId { get; set; }
            public int VendorId { get; set; }
            public List<TransactionItemInput> Items { get; set; } = new List<TransactionItemInput>();
        }

        public class TransactionItemInput
        {
            public int ProductId { get; set; }
            public int Quantity { get; set; }
            public decimal Price { get; set; }
            public decimal Total { get; set; }
        }

        public class VendorInput
        {
            public string VendorName { get; set; }
            public string ContactEmail { get; set; }
            public string ContactPhone { get; set; }
            public string Address { get; set; }
        }
    }
}
