using SIM.Core.Entities.Base;

#nullable disable
namespace SIM.Core.Entities
{
    public class User : BaseEntity
    {
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string PasswordHash { get; set; }
        public string Email { get; set; }
        public string Phone { get; set; }
        public string Address { get; set; }


        public virtual ICollection<Transaction> Transactions { get; set; }
    }
}
