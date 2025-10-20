using Microsoft.AspNetCore.Mvc;
using SIM.Core.DTOs.Requests;
using SIM.Core.Enums;
using SIM.Core.Exceptions;
using SIM.Core.Interfaces.Services;
using SIM.Presentation.Pages.Base;

namespace SIM.Presentation.Pages.TransactionCategory
{
    public class UpsertModel : BasePageModel
    {
        [BindProperty(SupportsGet = true)]
        public int? Id { get; set; }
        [BindProperty]
        public Core.DTOs.Responses.TransactionCategoryModel Category { get; set; } = new Core.DTOs.Responses.TransactionCategoryModel();

        private readonly ITransactionCategoryService _transactionCategoryService;

        public UpsertModel(ITransactionCategoryService transactionCategoryService)
        {
            _transactionCategoryService = transactionCategoryService;
        }
        public async Task<IActionResult> OnGetAsync()
        {
            if (!IsAuthenticated) return RedirectToPage("/Login");

            if (Id.HasValue)
            {
                try
                {
                    Category = await _transactionCategoryService.GetCategoryById(Id.Value);
                }
                catch (NotFoundException ex)
                {
                    TempData["ToastStatus"] = ToastStatusEnum.Fail;
                    TempData["ToastMessage"] = ex.Message;
                    return RedirectToPage("/TransactionCategory/Index");
                }
                catch (Exception)
                {
                    TempData["ToastStatus"] = ToastStatusEnum.Fail;
                    TempData["ToastMessage"] = "System Error !!!";
                }
            }

            return Page();
        }

        public IActionResult OnPostBack()
        {
            return RedirectToPage("/TransactionCategory/Index");
        }

        public async Task<IActionResult> OnPostSaveAsync()
        {
            if (!ModelState.IsValid) return Page();
            try
            {
                if (Id.HasValue)
                {
                    var transactionCategory = new UpdateCategoryRequest { 
                        Id = Id.Value,
                        Name = Category.Name,
                        Description = Category.Description
                    };
                    await _transactionCategoryService.UpdateCategory(transactionCategory);
                    TempData["ToastStatus"] = ToastStatusEnum.Success;
                    TempData["ToastMessage"] = "Category updated successfully!";
                }
                else
                {
                    var transactionCategory = new CreateCategoryRequest
                    {
                        Name = Category.Name,
                        Description = Category.Description
                    };
                    await _transactionCategoryService.AddCategory(transactionCategory);
                    TempData["ToastStatus"] = ToastStatusEnum.Success;
                    TempData["ToastMessage"] = "Category created successfully!";
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
