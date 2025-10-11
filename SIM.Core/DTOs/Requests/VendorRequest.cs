#nullable disable

namespace SIM.Core.DTOs.Requests
{
    public class CreateVendorRequest
    {
        public string VendorName { get; set; }
        public string ContactEmail { get; set; }
        public string ContactPhone { get; set; }
        public string Address { get; set; }
    }

    public class UpdateVendorRequest : CreateVendorRequest
    {
        public int Id { get; set; }
    }
}
