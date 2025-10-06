
#nullable disable
using SIM.Core.Entities.Base;

namespace SIM.Core.Entities
{
    public class InvoiceItem : BaseEntity
    {
        public int Quantity { get; set; }
        public decimal Price { get; set; }
        public decimal Total { get; set; }

        public int InvoiceId { get; set; }
        public virtual Invoice Invoice { get; set; }
        public int ProductId { get; set; }
        public virtual Product Product { get; set; }
    }
}
