using SIM.Core.Enums;

namespace SIM.Core.DTOs.Responses
{
    public class InvoiceModel
    {
        public decimal TotalAmount { get; set; }
        public InvoiceStatusEnum Status { get; set; }
        public InvoiceCategoryEnum Category { get; set; }
    }
}