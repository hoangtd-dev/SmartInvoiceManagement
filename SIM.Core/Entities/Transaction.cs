#nullable disable
using SIM.Core.Entities.Base;
using SIM.Core.Enums;

namespace SIM.Core.Entities
{
    public class Transaction : BaseEntity
    {
        public decimal TotalAmount { get; set; }
        public TransactionTypeEnum TransactionType { get; set; }
        public int CategoryId { get; set; }
        public TransactionCategory Category { get; set; }

        public int UserId { get; set; }
        public virtual User User { get; set; }

        public int VendorId { get; set; }
        public virtual Vendor Vendor { get; set; }

        public virtual ICollection<TransactionItem> TransactionItems { get; set; }
    }
}
