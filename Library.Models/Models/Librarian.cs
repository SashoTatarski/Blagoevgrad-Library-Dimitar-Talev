using Library.Models.Contracts;
using System.Collections.Generic;

namespace Library.Models.Models
{
    // OOP: Inheritance - Librarian class inherits Account base class

    // OOP: Abstraction - Account is abstract, User "is a" Account
    public class Librarian : Account, ILibrarian, IAccount
    {
        public Librarian(string username, string password) : base(username, password)
        {
        }

        // OOP: Polymorphism - method overriding dynamic/runtime polymorphism. In runtime polymorphism, the type of the object from which the overridden method will be called is identified at run time.
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
    }
}
