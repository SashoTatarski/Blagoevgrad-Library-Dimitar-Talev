using Library.Database;
using Library.Models.Contracts;
using Library.Models.Enums;
using Library.Models.Models;
using Library.Models.Utils;
using Library.Services.Contracts;
using Library.Services.Factories.Contracts;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Library.Services
{
    // SOLID: DI principle - we program against Interfaces. High-level modules, which provide complex logic, should be easily reusable and unaffected by changes in low-level modules
    public class AccountManager : IAccountManager
    {
        private readonly LibraryContext _context;
        private readonly IAccountFactory _accountFac;
        public AccountManager(LibraryContext context, IAccountFactory accountFac)
        {
            _context = context;
            _accountFac = accountFac;
        }

        public IAccount FindAccount(string username)
        {
            var user = _context.Users.FirstOrDefault(u => u.Username == username);
            var librarian = _context.Librarians.FirstOrDefault(l => l.Username == username);

            if (user is null || user.Status == AccountStatus.Inactive)
                return librarian ?? null;
            else
                return user;
        }

        public List<User> GetAllUsers() => _context.Users.ToList();


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
            var userToRemove = _context.Users.FirstOrDefault(x => x.Id == user.Id);

            userToRemove.Status = AccountStatus.Inactive;
            _context.SaveChanges();
        }

        public bool HasMessages(User user)
        {
            if (user is null)
            {
                throw new ArgumentException(GlobalConstants.NoSuchUser);
            }
            if (user.Messages.Count == 0)
            {
                return false;
            }
            else return true;
        }

        public string DisplayMessages(User user)
        {
            var strBuilder = new StringBuilder();

            if (user is null)
            {
                throw new ArgumentException(GlobalConstants.NoSuchUser);
            }

            foreach (var message in user.Messages)
            {
                strBuilder.AppendLine(message);
            }

            user.Messages.Clear();
            return strBuilder.ToString();
        }
    }
}
