using SIM.Core.Enums;

namespace SIM.Core.DTOs.Responses
{
    public class TransactionModel
    {
        public decimal TotalAmount { get; set; }
        public TransactionTypeEnum Type { get; set; }
    }
}