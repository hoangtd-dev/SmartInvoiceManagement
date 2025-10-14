#nullable disable
using SIM.Core.Entities.Base;
using SIM.Core.Enums;

namespace SIM.Core.Entities
{
    public class Budget : BaseEntity
    {
        public decimal TotalAmount { get; set; }
        public DateTime StartDate { get; set; }
        public DateTime EndDate { get; set; }
        public BudgetStatusEnum Status { get; set; }
        public int? CategoryId { get; set; }
        public virtual TransactionCategory Category { get; set; }
        public int UserId { get; set; }
        public virtual User User { get; set; }
    }
}
