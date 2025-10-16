namespace SIM.Core.DTOs.Requests
{
    public class TransactionItemInputModel
    {
        public int Index { get; set; }

        public string ItemName { get; set; } = string.Empty;
        public int Quantity { get; set; }
        public decimal Price { get; set; }
        public decimal Total { get; set; }
    }
}
