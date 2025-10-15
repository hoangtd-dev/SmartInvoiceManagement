using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Moq;
using SIM.Core.DTOs.Responses;
using SIM.Core.Enums;
using SIM.Core.Interfaces.Services;
using SIM.Presentation.Pages.Dashboard;
using System.Security.Claims;

namespace SIM.UnitTest
{
    public class DashboardTests
    {
        [Fact]
        public async Task OnGet_LoadDashboardPage()
        {
            var mockTransactionService = new Mock<ITransactionService>();

            var latestTransactions = new List<TransactionModel>
            {
                new TransactionModel { Id = 1, TotalAmount = 100m, Type = TransactionTypeEnum.Expense },
                new TransactionModel { Id = 2, TotalAmount = 100m, Type = TransactionTypeEnum.Expense }
            };

            mockTransactionService.Setup(service => service.GetLastestTransactionsOfCurrentUser(1, 5))
                .ReturnsAsync(latestTransactions);

            var incomeExpense = new IncomeExpenseModel { Expense = 100, Income = 200, Month = 1, Year = 2025 };

            mockTransactionService.Setup(service => service.GetIncomeExpensesOfCurrentUserInMonth(It.IsAny<int>(), It.IsAny<int>(), It.IsAny<int>()))
                .ReturnsAsync(incomeExpense);

            var dashboardModel = new DashboardModel(mockTransactionService.Object);
            
            var httpContext = new DefaultHttpContext();
            httpContext.User = CreateUser();

            var pageContext = new PageContext
            {
                HttpContext = httpContext
            };

            dashboardModel.PageContext = pageContext;

            await dashboardModel.OnGetAsync();

            mockTransactionService.Verify(service => service.GetLastestTransactionsOfCurrentUser(It.IsAny<int>(), It.IsAny<int>()), Times.Once);
            mockTransactionService.Verify(service => service.GetIncomeExpensesOfCurrentUserInMonth(It.IsAny<int>(), It.IsAny<int>(), It.IsAny<int>()), Times.Exactly(14));

            Assert.NotNull(dashboardModel.Transactions);
            Assert.Equal(2, dashboardModel.Transactions.Count);

            Assert.NotNull(dashboardModel.CurrentIncomeOutcomeInMonth);

            Assert.NotNull(dashboardModel.IncomeOutcomeInYear);
            Assert.Equal(12, dashboardModel.IncomeOutcomeInYear.Count);
        }

        private ClaimsPrincipal CreateUser()
        {
            var claims = new List<Claim>
        {
            new Claim(ClaimTypes.NameIdentifier, "1"),
            new Claim(ClaimTypes.Name, "admin@gmail.com")
        };

            var identity = new ClaimsIdentity(claims, "TestAuth");
            return new ClaimsPrincipal(identity);
        }
    }
}
