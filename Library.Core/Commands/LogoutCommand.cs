using Library.Core.Contracts;
using Services.Contracts;

namespace Library.Core.Commands
{
    public class LogoutCommand : ICommand
    {
        private readonly IAccountManager _account;

        public LogoutCommand(IAccountManager account)
        {            
            _account = account;
        }

        public string Execute()
        {
            var user = _account.CurrentAccount;

            _account.LogOut();

            return $"{user.Username} succefully logged out!";
        }
    }
}
