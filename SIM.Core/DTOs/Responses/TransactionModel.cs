#nullable disable
using SIM.Core.Enums;
using SIM.Core.DTOs.Requests;

namespace SIM.Core.DTOs.Responses
{
    public class TransactionModel
    {
        public int Id { get; set; }
        public TransactionTypeEnum TransactionType { get; set; }
        public decimal TotalAmount { get; set; }

        public int? VendorId { get; set; }
        public VendorModel Vendor { get; set; }

        public int CategoryId { get; set; }

        public TransactionCategoryModel Category { get; set; }

        public DateTime CreateDate { get; set; }
        public List<TransactionItemModel> Items { get; set; } = new List<TransactionItemModel>();

    }
}