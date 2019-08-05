using Library.Models.Contracts;

namespace Library.Models.Models
{
    public class Librarian : Account, ILibrarian, IAccount
    {
        public Librarian(string username, string password) : base(username, password)
        {
        }
    }
}
