using Library.Models.Contracts;
using Library.Models.Models;
using Library.Services.Factories.Contracts;

namespace Library.Services.Factory
{
    public class LibrarianFactory : ILibrarianFactory
    {
        public Librarian CreateLibrarian(string username, string password) => new Librarian(username, password);
    }
}
