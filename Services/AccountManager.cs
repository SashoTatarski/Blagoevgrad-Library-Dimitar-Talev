using Dawn;
using Library.Database.Contracts;
using Library.Models.Contracts;
using Library.Models.Enums;
using Library.Services.Contracts;
using System.Collections.Generic;

namespace Library.Services
{
    // SOLID: DI principle - we program against Interfaces. High-level modules, which provide complex logic, should be easily reusable and unaffected by changes in low-level modules
    public class AccountManager : IAccountManager
    {
        private readonly IUserDataBase _userDB;
        private readonly ILibrarianDataBase _librarianDB;
        private readonly IConsoleRenderer _renderer;
        private readonly IConsoleFormatter _formatter;

        public AccountManager(IUserDataBase userDB, ILibrarianDataBase librarianDB, IConsoleRenderer renderer, IConsoleFormatter formatter)
        {
            _userDB = userDB;
            _librarianDB = librarianDB;
            _renderer = renderer;
            _formatter = formatter;
        }

        public void GetListAllUsers()
        {
            var users = _userDB.Load();
            _renderer.Output(_formatter.FormatListOfUsersShort(users));
        }

        public List<IUser> GetAllUsers() => _userDB.Load();

        // 1. Take the user from the DB -> .Get
        // 2. Update user in the User class
        // 3. Save modified user in the DB
        public void UpdateUser(IUser user)
        {
            var userToUpdate = _userDB.Get(user.Username);
            Guard.Argument(userToUpdate, nameof(userToUpdate)).NotNull(message: "User to update is null");

            userToUpdate.Update(user);
            _userDB.Update(userToUpdate);
        }

        public void RemoveUser(IUser user)
        {
            var userToRemove = _userDB.Get(user.Username);
            _userDB.Delete(userToRemove);
        }

        public void AddUser(IUser user) => _userDB.Create(user);

        public void AddLibrarian(ILibrarian librarian) => _librarianDB.Create(librarian);

        public IAccount FindAccount(string userName)
        {
            var user = _userDB.Get(userName);
            var librarian = _librarianDB.Get(userName);

            if (user is null || user.Status == MemberStatus.Inactive)
                return librarian ?? null;
            else
                return user;
        }
    }
}
