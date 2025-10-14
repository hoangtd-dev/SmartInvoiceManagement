#nullable disable

using System.ComponentModel.DataAnnotations;

namespace SIM.Core.DTOs.Responses
{
    public class BudgetModel
    {
        public int Id { get; set; }
        [Required]
        public decimal TotalAmount { get; set; }
        public decimal TotalExpense { get; set; }
        [Required]
        public DateTime StartDate { get; set; }
        [Required]
        public DateTime EndDate { get; set; }
        public int CategoryId { get; set; }
        public TransactionCategoryModel Category { get; set; }
    }
}
