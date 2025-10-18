#nullable disable
using SIM.Core.Entities.Base;

namespace SIM.Core.Entities
{
    public class TransactionCategory : BaseEntity
    {
        public string Name { get; set; }
        public string Description { get; set; }
    }
}
