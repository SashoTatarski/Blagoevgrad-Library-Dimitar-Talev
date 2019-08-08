using Library.Core.Contracts;
using Services.Contracts;

namespace Library.Core.Commands
{
    public class LogoutCommand : ICommand
    {
        private readonly IAuthenticationManager _authentication;

        public LogoutCommand(IAuthenticationManager authentication)
        {
            _authentication = authentication;
        }

        public string Execute()
        {
            var user = _authentication.CurrentAccount;

            _authentication.LogOut();

            return $"{user.Username} succefully logged out!";
        }
    }
}
