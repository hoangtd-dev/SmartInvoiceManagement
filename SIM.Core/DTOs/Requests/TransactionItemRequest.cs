namespace SIM.Core.DTOs.Requests
{
    public class CreateTransactionItemRequest
    {
        public int TransactionId { get; set; }
        public int Quantity { get; set; }
        public decimal Price { get; set; }
        public decimal Total { get; set; }
    }

    public class UpdateTransactionItemRequest : CreateTransactionItemRequest
    {
        public int Id { get; set; }
    }
}
