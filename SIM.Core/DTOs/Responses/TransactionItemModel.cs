namespace SIM.Core.DTOs.Responses
{
    public class TransactionItemModel
    {
        public int Id { get; set; }
        public int TransactionId { get; set; }
        public int Quantity { get; set; }
        public decimal Price { get; set; }
        public decimal Total { get; set; }
    }
}
