using Library.Database.Contracts;
using Library.Models.Contracts;
using Library.Models.Enums;
using Library.Models.Models;
using Library.Services.Contracts;
using Library.Services.Factories.Contracts;
using System.Collections.Generic;
using System.Linq;

namespace Library.Services
{
    // SOLID: DI principle - we program against Interfaces. High-level modules, which provide complex logic, should be easily reusable and unaffected by changes in low-level modules
    public class AccountManager : IAccountManager
    {
        private readonly IDatabase<User> _userDB;
        private readonly IDatabase<Librarian> _librarianDB;
        private readonly IAccountFactory _accountFac;

        public AccountManager(IDatabase<User> userDB, IDatabase<Librarian> librarianDB, IAccountFactory accountFac)
        {
            _userDB = userDB;
            _librarianDB = librarianDB;
            _accountFac = accountFac;
        }

        public IAccount FindAccount(string username)
        {
            var user = _userDB.Read().FirstOrDefault(u => u.Username == username);
            var librarian = _librarianDB.Read().FirstOrDefault(l => l.Username == username);

            if (user is null || user.Status == AccountStatus.Inactive)
                return librarian ?? null;
            else
                return user;
        }

        public List<User> GetAllUsers() => _userDB.Read().ToList();


        public User AddUser(string username, string password)
        {
            var newUser = _accountFac.CreateUser(username, password);
            return newUser;
        }

        public Librarian AddLibrarian(string username, string password)
        {
            var newLibrarian = _accountFac.CreateLibrarian(username, password);
            return newLibrarian;
        }

        public void RemoveUser(User user)
        {
            _userDB.Delete(user);
        }
    }
}
