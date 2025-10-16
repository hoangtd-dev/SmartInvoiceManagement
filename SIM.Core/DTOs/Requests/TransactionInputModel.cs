using SIM.Core.Enums;

namespace SIM.Core.DTOs.Requests
{
    public class TransactionInputModel
    {
        public TransactionTypeEnum TransactionType { get; set; }
        public decimal TotalAmount { get; set; }
        public int CategoryId { get; set; }
        public int VendorId { get; set; }
        public List<TransactionItemInputModel> Items { get; set; } = new List<TransactionItemInputModel>();
    }
}
