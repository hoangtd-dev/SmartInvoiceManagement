namespace SIM.Core.DTOs.Responses
{
    public class IncomeExpenseModel
    {
        public decimal Income { get; set; }
        public decimal Expense { get; set; }
        public int Month { get; set; }
        public int Year { get; set; }
    }
}
