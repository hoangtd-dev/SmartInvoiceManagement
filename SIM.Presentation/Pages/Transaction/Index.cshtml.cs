using Microsoft.AspNetCore.Mvc;
using SIM.Core.Interfaces.Services;
using SIM.Presentation.Pages.Base;
using SIM.Core.DTOs.Responses;
using SIM.Core.DTOs.Requests;
using System.Text.RegularExpressions;

namespace SIM.Presentation.Pages.Transactions
{
    public class IndexModel : BasePageModel
    {
        private readonly ITransactionService _transactionService;
        private readonly IBudgetService _budgetService;
        private readonly ITransactionCategoryService _transactionCategoryService;

        public ICollection<TransactionModel> Transactions { get; set; } = new List<TransactionModel>();
        public int OverBudgetCount { get; set; }

        [BindProperty(SupportsGet = true)]
        public TransactionFilterModel FilterModel { get; set; } = new();

        public IndexModel(
            ITransactionService transactionService,
            IBudgetService budgetService,
            ITransactionCategoryService transactionCategoryService)
        {
            _transactionService = transactionService;
            _budgetService = budgetService;
            _transactionCategoryService = transactionCategoryService;
        }

        public async Task<IActionResult> OnGetAsync()
        {
            if (!IsAuthenticated) return RedirectToPage("/Login");

            FilterModel.Categories = (await _transactionCategoryService.GetTransactionCategories()).ToList();

            Transactions = await _transactionService.GetTransactions();

            OverBudgetCount = await _budgetService.OverBudgetCount(CurrentUserId);

            if (FilterModel.FromDate.HasValue)
                Transactions = Transactions.Where(t => t.CreateDate.Date >= FilterModel.FromDate.Value.Date).ToList();

            if (FilterModel.ToDate.HasValue)
                Transactions = Transactions.Where(t => t.CreateDate.Date <= FilterModel.ToDate.Value.Date).ToList();

            if (!string.IsNullOrEmpty(FilterModel.TransactionType))
                Transactions = Transactions.Where(t => t.TransactionType.ToString() == FilterModel.TransactionType).ToList();

            if (FilterModel.SelectedCategoryId != null)
            {
                Transactions = Transactions.Where(t => t.CategoryId.ToString() == FilterModel.SelectedCategoryId.ToString()).ToList();
            }
            if (!string.IsNullOrWhiteSpace(FilterModel.Keyword))
            {
                var pattern = FilterModel.Keyword.Trim();
                Regex regex;

                try
                {
                    regex = new Regex(pattern, RegexOptions.IgnoreCase | RegexOptions.Compiled);
                }
                catch (ArgumentException)
                {
                    var keywordLower = pattern.ToLower();
                    Transactions = Transactions
                        .Where(t => t.Items != null &&
                            t.Items.Any(i => i.ItemName != null && i.ItemName.ToLower().Contains(keywordLower)))
                        .ToList();
                    goto EndRegexFilter;
                }

                Transactions = Transactions
                    .Where(t => t.Items != null &&
                        t.Items.Any(i => i.ItemName != null && regex.IsMatch(i.ItemName)))
                    .ToList();

            EndRegexFilter:;
            }

            if (FilterModel.AmountFrom.HasValue)
                Transactions = Transactions.Where(t => t.TotalAmount >= FilterModel.AmountFrom.Value).ToList();

            if (FilterModel.AmountTo.HasValue)
                Transactions = Transactions.Where(t => t.TotalAmount <= FilterModel.AmountTo.Value).ToList();

            return Page();
        }
    }
}
