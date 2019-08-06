using Library.Core.Contracts;
using Services.Contracts;
using System;
using System.Collections.Generic;
using System.Text;

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
            var user = _account.CurrentAccount.Username;

            _account.LogOut();

            return $"{user} succefully logged!";
        }
    }
}
