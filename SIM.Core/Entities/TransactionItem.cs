
#nullable disable
using SIM.Core.Entities.Base;

namespace SIM.Core.Entities
{
    public class TransactionItem : BaseEntity
    {
        public int Quantity { get; set; }
        public decimal Price { get; set; }
        public decimal Total { get; set; }

        public int TransactionId { get; set; }
        public virtual Transaction Transtraction { get; set; }
        public int ProductId { get; set; }
        public virtual Product Product { get; set; }
    }
}
