#nullable disable
using SIM.Core.Entities.Base;

namespace SIM.Core.Entities
{
    public class Vendor : BaseEntity
    {
        public string VendorName { get; set; }
        public string ContactEmail { get; set; }
        public string ContactPhone { get; set; }
        public string Address { get; set; }
    }
}
