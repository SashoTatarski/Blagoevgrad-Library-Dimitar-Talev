using Library.Database;
using Library.Models.Models;
using Library.Models.Utils;
using Library.Services.Factories.Contracts;

namespace Library.Services.Factories
{
    public class AccountFactory : IAccountFactory
    {
        private readonly LibraryContext _context;
        public AccountFactory(LibraryContext context)
        {
            _context = context;
        }
        public User CreateUser(string username, string password)
        {
            DataValidator.ValidateMinAndMaxLength(username, 3, 20, "Username");
            DataValidator.ValidateMinAndMaxLength(password, 3, 20, "Password");

            var newUser = new User(username, password);
            _context.Users.Add(newUser);
            _context.SaveChanges();
            return newUser;
        }
        public Librarian CreateLibrarian(string username, string password)
        {
            DataValidator.ValidateMinAndMaxLength(username, 3, 20, "Username");
            DataValidator.ValidateMinAndMaxLength(password, 3, 20, "Password");

            var newLibrarian = new Librarian(username, password);
            _context.Librarians.Add(newLibrarian);
            _context.SaveChanges();
            return newLibrarian;
        }
    }
}
