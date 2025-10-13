using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using SIM.Core.DTOs.Requests;
using SIM.Core.Enums;
using SIM.Core.Exceptions;
using SIM.Core.Interfaces.Services;
using SIM.Presentation.Pages.Base;

namespace SIM.Presentation.Pages.Budget
{
    public class UpsertModel : BasePageModel
    {
        [BindProperty(SupportsGet = true)]
        public int? Id { get; set; }
        public List<SelectListItem> Categories { get; set; } = new List<SelectListItem>();
        [BindProperty]
        public Core.DTOs.Responses.BudgetModel Budget { get; set; } = new Core.DTOs.Responses.BudgetModel();

        private readonly ITransactionCategoryService _transactionCategoryService;
        private readonly IBudgetService _budgetService;

        public UpsertModel(ITransactionCategoryService transactionCategoryService, IBudgetService budgetService)
        {
            _budgetService = budgetService;
            _transactionCategoryService = transactionCategoryService;
        }
        public async Task<IActionResult> OnGetAsync()
        {
            if (!IsAuthenticated) return RedirectToPage("/Login");

            await GetCategories();

            if (Id.HasValue)
            {
                try
                {
                    Budget = await _budgetService.GetBudgetById(Id.Value);
                }
                catch (NotFoundException ex)
                {
                    TempData["ToastStatus"] = ToastStatusEnum.Fail;
                    TempData["ToastMessage"] = ex.Message;
                    return RedirectToPage("/Budget/Index");
                }
                catch (Exception)
                {
                    TempData["ToastStatus"] = ToastStatusEnum.Fail;
                    TempData["ToastMessage"] = "System Error !!!";
                }
            }
            return Page();
        }

        private async Task GetCategories()
        {
            try
            {
                var categories = await _transactionCategoryService.GetTransactionCategories();
                foreach (var category in categories)
                {
                    Categories.Add(new SelectListItem { 
                        Value = category.Id.ToString(),
                        Text = category.Name
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
            return RedirectToPage("/Budget/Index");
        }

        public async Task<IActionResult> OnPostSaveAsync()
        {
            if (!ModelState.IsValid) return Page();
            try
            {
                if (Id.HasValue)
                {
                    var budget = new UpdateBudgetRequest { 
                        Id = Id.Value,
                        CategoryId = Budget.CategoryId,
                        StartDate = Budget.StartDate,
                        EndDate = Budget.EndDate,
                        TotalAmount = Budget.TotalAmount
                    };
                    await _budgetService.UpdateBudget(budget);
                    TempData["ToastStatus"] = ToastStatusEnum.Success;
                    TempData["ToastMessage"] = "Budget updated successfully!";
                }
                else
                {
                    var budget = new CreateBudgetRequest
                    {
                        CategoryId = Budget.CategoryId,
                        StartDate = Budget.StartDate,
                        EndDate = Budget.EndDate,
                        TotalAmount = Budget.TotalAmount
                    };
                    await _budgetService.AddBudget(budget);
                    TempData["ToastStatus"] = ToastStatusEnum.Success;
                    TempData["ToastMessage"] = "Budget created successfully!";
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
