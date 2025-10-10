#nullable disable
using SIM.Core.Enums;

namespace SIM.Core.DTOs.Responses
{
    public class TransactionModel
    {
        public int Id { get; set; }
        public TransactionTypeEnum Type { get; set; }
        public decimal TotalAmount { get; set; }
        public string CategoryName { get; set; }
        public string VendorName { get; set; }
        public DateTime CreateDate { get; set; }
    }
}