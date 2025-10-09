namespace SIM.Core.Helpers
{
    public static class DateHelpers
    {
        public static (DateTime startDate, DateTime endDate) GetStartAndEndDateOfMonth(int month, int year) {
            DateTime startDate = new DateTime(year, month, 1);
            DateTime endDate = startDate.AddMonths(1).AddDays(-1);

            return (startDate, endDate);
        }
    }
}
