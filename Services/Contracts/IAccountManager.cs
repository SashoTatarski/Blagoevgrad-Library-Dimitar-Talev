using Library.Models.Contracts;
using Library.Models.Models;
using System.Collections.Generic;

namespace Library.Services.Contracts
{
    //SOLID: Interface Segregation: consumer of this interface uses all of its methods. Interface is specific for the needs of the dependency
    public interface IAccountManager
    {
        IAccount FindAccount(string userName);
        User AddUser(string username, string password);
        Librarian AddLibrarian(string username, string password);
        void RemoveUser(User user);
        List<User> GetAllUsers();
        bool HasMessages(User user);
        string DisplayMessages(User user);
    }
}
