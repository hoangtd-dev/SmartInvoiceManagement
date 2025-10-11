#nullable disable

namespace SIM.Core.DTOs.Requests
{
    public class CreateProductRequest
    {
        public string ProductName { get; set; }
        public string Description { get; set; }
        public decimal Price { get; set; }
        public int StockQuantity { get; set; }
        public int VendorId { get; set; }
    }

    public class UpdateProductRequest : CreateProductRequest {
        public int Id { get; set; }
    }
}
