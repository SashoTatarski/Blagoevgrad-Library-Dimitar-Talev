using Library.Database.Contracts;
using Library.Models.Models;
using Library.Models.Utils;
using Library.Services.Factories.Contracts;

namespace Library.Services.Factories
{
    public class AccountFactory : IAccountFactory
    {
        private readonly IDatabase<Librarian> _librarianDB;
        private readonly IDatabase<User> _userDB;
        public AccountFactory(IDatabase<Librarian> librarianDB, IDatabase<User> userDB)
        {
            _librarianDB = librarianDB;
            _userDB = userDB;
        }
        public User CreateUser(string username, string password)
        {
            DataValidator.ValidateMinAndMaxLength(username, 3, 20, "Username");
            DataValidator.ValidateMinAndMaxLength(password, 3, 20, "Password");

            var newUser = new User(username, password);
            _userDB.Create(newUser);
            return newUser;
        }
        public Librarian CreateLibrarian(string username, string password)
        {
            DataValidator.ValidateMinAndMaxLength(username, 3, 20, "Username");
            DataValidator.ValidateMinAndMaxLength(password, 3, 20, "Password");

            var newLibrarian = new Librarian(username, password);
            _librarianDB.Create(newLibrarian);
            return newLibrarian;
        }
    }
}
