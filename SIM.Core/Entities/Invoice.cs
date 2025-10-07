#nullable disable
using SIM.Core.Entities.Base;
using SIM.Core.Enums;

namespace SIM.Core.Entities
{
    public class Invoice : BaseEntity
    {
        public decimal TotalAmount { get; set; }
        public InvoiceStatusEnum Status { get; set; }
        public InvoiceCategoryEnum Category { get; set; } = InvoiceCategoryEnum.None;

        public int UserId { get; set; }
        public virtual User User { get; set; }

        public virtual ICollection<InvoiceItem> InvoiceItems { get; set; }
    }
}
