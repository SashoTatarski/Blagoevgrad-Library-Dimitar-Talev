using Library.Models.Contracts;
using Library.Models.Enums;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Library.Models.Models
{
    public class User : Account, IAccount
    {
        // OOP: User class inherits Account base class
        // OOP: Encapsulation - properties with private set         
        public User(string username, string password) : base(username, password)
        {
            this.Status = AccountStatus.Active;
            this.Messages = new List<string>();
        }

        [NotMapped]
        public override IEnumerable<string> AllowedCommands => new List<string>
            {
                "Check Out Book",
                "Return Book",
                "Renew Book",
                "Reserve Book",
                "Search",
                "Travel In Time",
                "Log Out"
            };

        [Key]
        public int Id { get; set; }

        [Required]
        public AccountStatus Status { get; set; }
        public decimal LateFees { get; set; }
        public IList<CheckoutBook> CheckedoutBooks { get; }
        public IList<ReservedBook> ReservedBooks { get; }

        [NotMapped]
        public List<string> Messages { get; set; }
    }
}