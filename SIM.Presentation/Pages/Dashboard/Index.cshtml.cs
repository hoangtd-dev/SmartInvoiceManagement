using Microsoft.AspNetCore.Mvc;
using SIM.Core.DTOs.Responses;
using SIM.Core.Interfaces.Services;
using SIM.Presentation.Pages.Base;

namespace SIM.Presentation.Pages.Dashboard
{
    public class DashboardModel : BasePageModel
    {
        public ICollection<TransactionModel> Transactions { get; set; }
        public IncomeExpenseModel CurrentIncomeOutcomeInMonth { get; set; }
        public List<IncomeExpenseModel> IncomeOutcomeInYear { get; set; } = new List<IncomeExpenseModel>();
        public List<decimal> IncomesInMonth => IncomeOutcomeInYear.Select(x => x.Income).ToList();
        public List<decimal> ExpensesInMonth => IncomeOutcomeInYear.Select(x => x.Expense).ToList();

        public decimal? MonthlyIncomeGrowthPercentage { get; set; }
        public decimal? MonthlyExpenseGrowthPercentage { get; set; }

        private readonly ITransactionService _transactionService;
        public DashboardModel(ITransactionService transactionService)
        {
            _transactionService = transactionService;
        }

        public async Task<IActionResult> OnGetAsync()
        {
            if (!IsAuthenticated) return RedirectToPage("/Login");

            await GetIncomeOutcomeInYear();
            await GetCurrentIncomeOutcomeInMonth();
            await GetLatestTransactions();
            return Page();
        }

        private async Task GetLatestTransactions()
        {
            Transactions = await _transactionService.GetLastestTransactionsOfCurrentUser(CurrentUserId, 5);
        }

        private async Task GetIncomeOutcomeInYear()
        {
            int year = DateTime.Now.Year;
            List<int> months = new List<int> { 1, 2, 3, 4, 5, 6, 7, 8, 9, 10, 11, 12 };

            foreach (var month in months)
            {
                var incomeExpenseInMonth = await _transactionService.GetIncomeExpensesOfCurrentUser(CurrentUserId, month, year);
                IncomeOutcomeInYear.Add(incomeExpenseInMonth);
            }
        }

        private async Task GetCurrentIncomeOutcomeInMonth()
        {
            int month = DateTime.Now.Month;
            int previousMonth = DateTime.Now.AddMonths(-1).Month;
            int year = DateTime.Now.Year;

            CurrentIncomeOutcomeInMonth = await _transactionService.GetIncomeExpensesOfCurrentUser(CurrentUserId, month, year);
            var incomeOutcomeInPreviousMonth = await _transactionService.GetIncomeExpensesOfCurrentUser(CurrentUserId, previousMonth, year);

            if (incomeOutcomeInPreviousMonth.Income == 0)
            {
                MonthlyIncomeGrowthPercentage = null;
            }
            else
            {             
                MonthlyIncomeGrowthPercentage = Math.Round(((CurrentIncomeOutcomeInMonth.Income / incomeOutcomeInPreviousMonth.Income) - 1) * 100, 1);
            }

            if (incomeOutcomeInPreviousMonth.Expense == 0)
            {
                MonthlyExpenseGrowthPercentage = null;
            }
            else
            { 
                MonthlyExpenseGrowthPercentage = Math.Round(((CurrentIncomeOutcomeInMonth.Expense / incomeOutcomeInPreviousMonth.Expense) - 1) * 100, 1);
            }
        }
    }
}
