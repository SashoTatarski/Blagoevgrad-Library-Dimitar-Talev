using Library.Models.Models;

namespace Library.Services.Factories.Contracts
{
    public interface ILibrarianFactory
    {
        Librarian CreateLibrarian(string username, string password);
    }
}
