using Library.Core.Contracts;
using Library.Services.Contracts;
using Library.Services.Factory;
using Services.Contracts;

namespace Library.Core.Commands
{
    public class RegisterLibrarianCommand : ICommand
    {
        private readonly ILibrarianFactory _librarianfactory;
        private readonly IConsoleRenderer _renderer;
        private readonly IAccountManager _accountManager;
        private readonly IAuthenticationManager _authentication;

        public RegisterLibrarianCommand(ILibrarianFactory librarianfactory, IConsoleRenderer renderer, IAccountManager accountManager, IAuthenticationManager authentication)
        {
            _librarianfactory = librarianfactory;
            _renderer = renderer;
            _accountManager = accountManager;
            _authentication = authentication;
        }

        public string Execute()
        {
            var username = _renderer.InputParameters("username",
                s => s.Length < 1 || s.Length > 30);
            var password = _renderer.InputParameters("password",
                s => s.Length < 3 || s.Length > 20);

            _authentication.CheckForExistingUsername(username);
            var newLibrarian = _librarianfactory.CreateLibrarian(username, password);

            _accountManager.AddLibrarian(newLibrarian);

            return $"Successfully created new librarian account: {newLibrarian.Username}";
        }
    }
}
