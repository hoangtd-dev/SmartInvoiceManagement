using SIM.Core.Entities;
using SIM.Core.Enums;

namespace SIM.Core.DTOs.Requests
{
    public class CreateTransactionRequest
    {
        public int UserId { get; set; }
        public int VendorId { get; set; }
        public int CategoryId { get; set; }
        public decimal TotalAmount { get; set; }
        public TransactionTypeEnum TransactionType { get; set; }
    }
    public class UpdateTransactionRequest : CreateTransactionRequest
    {
        public int Id { get; set; }
    }
}
