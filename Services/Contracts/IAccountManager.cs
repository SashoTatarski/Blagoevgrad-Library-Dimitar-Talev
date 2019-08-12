using Library.Models.Contracts;
using System.Collections.Generic;

namespace Library.Services.Contracts
{
    public interface IAccountManager
    {
        void AddLibrarian(ILibrarian librarian);
        void AddUser(IUser user);
        IAccount FindAccount(string userName);
        List<IUser> GetAllUsers();
        void GetListAllUsers();
        void RemoveUser(IUser user);
        void UpdateUser(IUser user);
    }
}
