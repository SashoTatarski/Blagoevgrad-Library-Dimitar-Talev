using Library.Models.Models;

namespace Library.Services.Factories.Contracts
{
    public interface IAccountFactory
    {
        User CreateUser(string username, string password);
        Librarian CreateLibrarian(string username, string password);
    }
}
