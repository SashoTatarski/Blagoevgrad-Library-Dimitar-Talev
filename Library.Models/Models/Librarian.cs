using Library.Models.Contracts;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Library.Models.Models
{
    // OOP: Inheritance - Librarian class inherits Account base class
    // OOP: Abstraction - Account is abstract, User "is a" Account
    public class Librarian : Account, IAccount
    {
        public Librarian(string username, string password) : base(username, password) { }

        // OOP: Polymorphism - method overriding dynamic/runtime polymorphism. In runtime polymorphism, the type of the object from which the overridden method will be called is identified at run time.

        [NotMapped]
        public override IEnumerable<string> AllowedCommands => new List<string>
            {
                "Add Book",
                "Edit Book",
                "Remove Book",
                "Register User",
                "Register Librarian",
                "Remove User",
                "Search",
                "Travel In Time",
                "Log Out"
            };

        [Key]
        public int Id { get; set; }
    }
}
