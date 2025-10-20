using System.Text.Json;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using SIM.Core.DTOs.Requests;
using SIM.Core.DTOs.Responses;
using SIM.Core.Enums;
using SIM.Core.Exceptions;
using SIM.Core.Interfaces.Services;
using SIM.Presentation.Pages.Base;

namespace SIM.Presentation.Pages.Transactions
{
    public class UpsertModel : BasePageModel
    {
        [BindProperty(SupportsGet = true)]
        public int? Id { get; set; }

        private readonly IVendorService _vendorService;
        private readonly ITransactionCategoryService _transactionCategoryService;
        private readonly ITransactionItemService _transactionItemService;
        private readonly ITransactionService _transactionService;

        public UpsertModel(
            IVendorService vendorService,
            ITransactionCategoryService transactionCategoryService,
            ITransactionItemService transactionItemService,
            ITransactionService transactionService)
        {
            _vendorService = vendorService;
            _transactionCategoryService = transactionCategoryService;
            _transactionItemService = transactionItemService;
            _transactionService = transactionService;
        }

        [BindProperty]
        public TransactionInputModel Transaction { get; set; } = new TransactionInputModel();

        [BindProperty]
        public VendorInputModel NewVendor { get; set; } = new VendorInputModel();

        public IEnumerable<SelectListItem> TransactionTypeOptions { get; set; } = Enumerable.Empty<SelectListItem>();
        public IEnumerable<SelectListItem> CategoryOptions { get; set; } = Enumerable.Empty<SelectListItem>();
        public IEnumerable<SelectListItem> VendorOptions { get; set; } = Enumerable.Empty<SelectListItem>();

        public bool IsEditMode => Id.HasValue;

        public async Task<IActionResult> OnGetAsync(int? id)
        {
            if (!IsAuthenticated) return RedirectToPage("/Login");
            try
            {
                if (IsEditMode)
                {
                    var transaction = await _transactionService.GetTransactionById(Id.Value);
                    Transaction = new TransactionInputModel
                    {
                        Id = transaction.Id,
                        TransactionType = transaction.TransactionType,
                        TotalAmount = transaction.TotalAmount,
                        VendorId = transaction.VendorId.Value,
                        Vendor = transaction.Vendor,
                        CategoryId = transaction.CategoryId,
                        Category = transaction.Category,
                        CreateDate = transaction.CreateDate,
                        Items = transaction.Items.Select(item => new TransactionItemInputModel
                        {
                            Id = item.Id,
                            TransactionId = transaction.Id,
                            ItemName = item.ItemName,
                            Price = item.Price,
                            Quantity = item.Quantity,
                            Total = item.Total
                        }).ToList()
                    };
                }
                else
                {
                    Transaction = new TransactionInputModel
                    {
                        CreateDate = DateTime.Today // ✅ Default to today for new transaction
                    };
                }
                await LoadOptionsAsync();

            }

            catch (NotFoundException ex)
            {
                TempData["ToastStatus"] = ToastStatusEnum.Fail;
                TempData["ToastMessage"] = ex.Message;
                return RedirectToPage("/Transaction/Index");
            }
            catch (Exception ex)
            {
                TempData["ToastStatus"] = ToastStatusEnum.Fail;
                TempData["ToastMessage"] = "System Error !!!";
            }

            return Page();
        }

        public IActionResult OnGetVendorPartial()
        {
            return Partial("VendorInlinePartial", new VendorInputModel());
        }

        // Returns a transaction item row partial
        public IActionResult OnGetItemPartial(int index)
        {
            return Partial("TransactionItemPartial", new TransactionItemInputModel { Index = index });
        }

        public async Task<IActionResult> OnPostAsync()
        {

            try
            {
                if (!ModelState.IsValid)
                {
                    var errorMessages = new List<string>();
                    Console.WriteLine("ModelState is invalid. Errors:" + JsonSerializer.Serialize(ModelState, new JsonSerializerOptions { WriteIndented = true }));

                    var vendorValue = ModelState.ContainsKey("Transaction.VendorId") ? ModelState["Transaction.VendorId"]?.RawValue?.ToString()?.Trim() : null;
                    var categoryValue = ModelState.ContainsKey("Transaction.CategoryId") ? ModelState["Transaction.CategoryId"]?.RawValue?.ToString()?.Trim() : null;
                    var typeValue = ModelState.ContainsKey("Transaction.TransactionType") ? ModelState["Transaction.TransactionType"]?.RawValue?.ToString()?.Trim() : null;

                    if (string.IsNullOrEmpty(categoryValue) || categoryValue == "0")
                        errorMessages.Add("Error: Category is required");
                    if (string.IsNullOrEmpty(typeValue))
                        errorMessages.Add("Error: Transaction Type is required");
                    if ((string.IsNullOrEmpty(vendorValue) || vendorValue == "0") && NewVendor.VendorName is null)
                        errorMessages.Add("Error: Vendor is required");

                    if (errorMessages.Count > 0)
                    {
                        // Use ViewData for same-page toast
                        TempData["ToastMessage"] = string.Join("<br>", errorMessages);
                        TempData["ToastStatus"] = (int)ToastStatusEnum.Fail;

                        await LoadOptionsAsync();
                        return Page();
                    }
                }
                if (!IsEditMode && Transaction.CreateDate == default)
                {
                    Transaction.CreateDate = DateTime.Today;
                }

                // Handle vendor creation if new
                if (!Transaction.VendorId.HasValue && !string.IsNullOrWhiteSpace(NewVendor?.VendorName))
                {
                    var vendorReq = new CreateVendorRequest
                    {
                        VendorName = NewVendor.VendorName,
                        Address = NewVendor.Address,
                    };

                    var vendor = await _vendorService.AddVendor(vendorReq);
                    Transaction.VendorId = vendor.Id;
                }

                if (IsEditMode)
                {
                    // Update Transaction
                    var updateReq = new UpdateTransactionRequest
                    {
                        Id = Id.Value,
                        UserId = CurrentUserId,
                        VendorId = Transaction.VendorId.Value,
                        CategoryId = Transaction.CategoryId,
                        TotalAmount = Transaction.TotalAmount,
                        TransactionType = Transaction.TransactionType
                    };

                    await _transactionService.UpdateTransaction(updateReq);

                    // Update items
                    if (Transaction.Items?.Any() == true)
                    {
                        foreach (var item in Transaction.Items)
                        {
                            if (item.Id.HasValue && item.Id.Value > 0)
                            {
                                // Update existing item
                                var updateItemReq = new UpdateTransactionItemRequest
                                {
                                    Id = item.Id.Value,
                                    TransactionId = Transaction.Id,
                                    Quantity = item.Quantity,
                                    ItemName = item.ItemName,
                                    Price = item.Price,
                                    Total = item.Total
                                };
                                await _transactionItemService.UpdateTransactionItem(updateItemReq);
                            }
                            else
                            {
                                // Add new item
                                var createItemReq = new CreateTransactionItemRequest
                                {
                                    TransactionId = Transaction.Id,
                                    Quantity = item.Quantity,
                                    ItemName = item.ItemName,
                                    Price = item.Price,
                                    Total = item.Total
                                };
                                await _transactionItemService.CreateTransactionItem(createItemReq);
                            }
                        }
                    }

                    TempData["ToastMessage"] = "Transaction updated successfully.";
                    TempData["ToastStatus"] = ToastStatusEnum.Success;
                }
                else
                {
                    // Create new transaction
                    var createReq = new CreateTransactionRequest
                    {
                        UserId = CurrentUserId,
                        VendorId = Transaction.VendorId.Value,
                        CreateDate = Transaction.CreateDate,
                        CategoryId = Transaction.CategoryId,
                        TotalAmount = Transaction.TotalAmount,
                        TransactionType = Transaction.TransactionType
                    };
                    var createdTx = await _transactionService.CreateTransaction(createReq);

                    if (Transaction.Items.Count > 0)
                    {
                        foreach (var item in Transaction.Items)
                        {
                            var createItemReq = new CreateTransactionItemRequest
                            {
                                TransactionId = createdTx.Id,
                                Quantity = item.Quantity,
                                ItemName = item.ItemName,
                                Price = item.Price,
                                Total = item.Total
                            };
                            await _transactionItemService.CreateTransactionItem(createItemReq);
                        }
                    }

                    TempData["ToastMessage"] = "Transaction created successfully.";
                    TempData["ToastStatus"] = ToastStatusEnum.Success;
                }
                return RedirectToPage("./Index");
            }
            catch (Exception ex)
            {
                TempData["ToastMessage"] = $"Operation failed: {ex.Message}";
                TempData["ToastStatus"] = ToastStatusEnum.Fail;
                return Page();
            }
        }

        private async Task LoadOptionsAsync()
        {
            TransactionTypeOptions = Enum.GetValues(typeof(TransactionTypeEnum))
                .Cast<TransactionTypeEnum>()
                .Select(e => new SelectListItem
                {
                    Value = ((int)e).ToString(),
                    Text = e.ToString(),
                    Selected = IsEditMode ? (Transaction?.TransactionType) == e : false
                }).ToList();


            var cats = await _transactionCategoryService.GetTransactionCategories();
            CategoryOptions = cats.Select(c => new SelectListItem
            {
                Value = c.Id.ToString(),
                Text = c.Name,
                Selected = IsEditMode ? Transaction?.Category.Id == c.Id : false
            }).ToList();

            var vendors = await _vendorService.GetVendors();
            VendorOptions = vendors.Select(v => new SelectListItem
            {
                Value = v.Id.ToString(),
                Text = v.Name,
                Selected = IsEditMode ? Transaction?.Vendor.Id == v.Id : false
            }).ToList();
        }
    }
}