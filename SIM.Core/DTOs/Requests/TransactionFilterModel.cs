using SIM.Core.DTOs.Responses;

namespace SIM.Core.DTOs.Requests
{
    public class TransactionFilterModel
    {
        public DateTime? FromDate { get; set; }
        public DateTime? ToDate { get; set; }
        public string TransactionType { get; set; } = string.Empty;
        public int? SelectedCategoryId { get; set; }

        public List<TransactionCategoryModel> Categories { get; set; } = new();
        public string Keyword { get; set; } = string.Empty;
        public decimal? AmountFrom { get; set; }
        public decimal? AmountTo { get; set; }
    }
}
