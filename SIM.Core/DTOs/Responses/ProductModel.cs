#nullable disable
using SIM.Core.Entities.Base;
using System.ComponentModel.DataAnnotations;

namespace SIM.Core.DTOs.Responses
{
    public class ProductModel
    {
        public int Id { get; set; }
        [Required]
        public string Name { get; set; }
        public string Description { get; set; }
        [Required]
        public decimal Price { get; set; }
        [Required]
        [Display(Name = "Quantity")]
        public int StockQuantity { get; set; }
        [Required]
        [Display(Name = "Vendor")]
        public int VendorId { get; set; }
        public VendorModel Vendor { get; set; }
    }
}
