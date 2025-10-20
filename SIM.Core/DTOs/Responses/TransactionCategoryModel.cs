
#nullable disable
using System.ComponentModel.DataAnnotations;

namespace SIM.Core.DTOs.Responses
{
    public class TransactionCategoryModel
    {
        public int Id { get; set; }
        [Required]
        public string Name { get; set; }
        public string Description { get; set; }
    }
}
