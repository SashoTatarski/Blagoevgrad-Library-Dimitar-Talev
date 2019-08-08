using Library.Models.Contracts;

namespace Library.Services.Contracts
{
    public interface IAccountManager
    {
        void AddLibrarian(ILibrarian librarian);
        void AddUser(IUser user);
        IAccount FindAccount(string userName);
        void ListAllUsers();
        void RemoveUser(IUser user);
        void UpdateUser(IUser user);
    }
}
