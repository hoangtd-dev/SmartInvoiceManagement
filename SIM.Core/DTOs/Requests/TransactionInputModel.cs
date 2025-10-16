using SIM.Core.Enums;

public class TransactionInput
{
    public TransactionTypeEnum TransactionType { get; set; }
    public decimal TotalAmount { get; set; }
    public int CategoryId { get; set; }
    public int VendorId { get; set; }
    public List<TransactionItemInput> Items { get; set; } = new List<TransactionItemInput>();
}
