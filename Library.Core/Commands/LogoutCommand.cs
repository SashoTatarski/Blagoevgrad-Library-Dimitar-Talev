using Library.Core.Contracts;
using Library.Models.Utils;
using Library.Services.Contracts;
using Services.Contracts;

namespace Library.Core.Commands
{
    public class LogoutCommand : ICommand
    {
        private readonly IAuthenticationManager _authentication;
        private readonly IConsoleFormatter _formatter;

        public LogoutCommand(IAuthenticationManager authentication, IConsoleFormatter formatter)
        {
            _authentication = authentication;
            _formatter = formatter;
        }

        public string Execute()
        {
            var user = _authentication.CurrentAccount;

            _authentication.LogOut();

            return $"{_formatter.Format(user)} {GlobalConstants.SuccessLogOut}";
        }
    }
}
