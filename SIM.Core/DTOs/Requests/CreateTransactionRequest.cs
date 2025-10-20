using System.ComponentModel.DataAnnotations;
using SIM.Core.Enums;

namespace SIM.Core.DTOs.Requests
{
    public class CreateTransactionRequest
    {
        [Required]
        public int UserId { get; set; }

        [Required]
        public int VendorId { get; set; }

        [Required]
        public int CategoryId { get; set; }

        [Required]
        public decimal TotalAmount { get; set; }

        [Required]
        public DateTime CreateDate { get; set; }

        [Required]
        public TransactionTypeEnum TransactionType { get; set; }
    }
    public class UpdateTransactionRequest : CreateTransactionRequest
    {
        public int Id { get; set; }
    }
}
