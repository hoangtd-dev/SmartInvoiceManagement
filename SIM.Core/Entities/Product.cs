using SIM.Core.Entities.Base;
#nullable disable
using SIM.Core.Enums;

namespace SIM.Core.Entities
{
    public class Product : BaseEntity
    {
        public string ProductName { get; set; }
        public string Description { get; set; }
        public decimal Price { get; set; }
        public int StockQuantity { get; set; }
        public ProductCategoryEnum Category { get; set; } = ProductCategoryEnum.None;
        public string ImageBase64 { get; set; } = string.Empty;

        public int VendorId { get; set; }
        public virtual Vendor Vendor { get; set; }

        public virtual ICollection<InvoiceItem> InvoiceItems { get; set; }
    }
}
