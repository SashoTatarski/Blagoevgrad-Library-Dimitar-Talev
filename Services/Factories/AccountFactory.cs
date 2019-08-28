using Library.Models.Models;
using Library.Models.Utils;
using Library.Services.Factories.Contracts;

namespace Library.Services.Factories
{
    public class AccountFactory : IAccountFactory
    {
        public User CreateUser(string username, string password)
        {
            DataValidator.ValidateMinAndMaxLength(username, 3, 20, "Username");
            DataValidator.ValidateMinAndMaxLength(password, 3, 20, "Password");

            return new User(username, password);
        }
        public Librarian CreateLibrarian(string username, string password)
        {
            DataValidator.ValidateMinAndMaxLength(username, 3, 20, "Username");
            DataValidator.ValidateMinAndMaxLength(password, 3, 20, "Password");

            return new Librarian(username, password);
        }
    }
}
