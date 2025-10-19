using System.ComponentModel.DataAnnotations;

namespace SIM.Core.DTOs.Requests
{
    public class CreateTransactionItemRequest
    {
        [Required]
        public int TransactionId { get; set; }

        [Required]
        public int Quantity { get; set; }

        [Required]
        public decimal Price { get; set; }

        [Required]
        public decimal Total { get; set; }
    }

    public class UpdateTransactionItemRequest : CreateTransactionItemRequest
    {
        public int Id { get; set; }
    }
}
