using Library.Models.Contracts;
using System.Collections.Generic;

namespace Library.Models.Models
{
    public class Librarian : Account, ILibrarian, IAccount
    {
        public Librarian(string username, string password) : base(username, password)
        {
        }
        public override IEnumerable<string> AllowedCommands => new List<string>
            {
                "Add Book",
                "Edit Book",
                "Remove Book",
                "Register User",
                "Register Librarian",
                "Remove User",
                "View Accounts",
                "Search",
                "Log Out"
            };

        public void UpdateBook(IBook book) => throw new System.NotImplementedException();
    }
}
