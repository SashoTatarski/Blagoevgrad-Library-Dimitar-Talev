using Library.Core.Contracts;
using Library.Models.Utils;
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
        private readonly IConsoleFormatter _formatter;

        public RegisterLibrarianCommand(ILibrarianFactory librarianfactory, IConsoleRenderer renderer, IAccountManager accountManager, IAuthenticationManager authentication, IConsoleFormatter formatter)
        {
            _librarianfactory = librarianfactory;
            _renderer = renderer;
            _accountManager = accountManager;
            _authentication = authentication;
            _formatter = formatter;
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

            return $"{GlobalConstants.LibrarianRegisterSuccess}{_formatter.Format(newLibrarian)}";
        }
    }
}
