#nullable disable
using System.ComponentModel.DataAnnotations;

namespace SIM.Core.DTOs.Responses
{
    public class VendorModel
    {
        public int Id { get; set; }
        [Required]
        public string Name { get; set; }
        [Display(Name = "Email")]
        public string ContactEmail { get; set; }
        [Display(Name = "Phone")]
        public string ContactPhone { get; set; }
        [Required]
        public string Address { get; set; }
    }
}
