using Library.Core.Contracts;
using Services.Contracts;
using System;
using System.Collections.Generic;
using System.Text;

namespace Library.Core.Commands
{
    public class CheckOutBook : ICommand
    {
        private readonly IAccountManager _account;
        public CheckOutBook(IAccountManager account)
        {
            _account = account;
        }
        public string Execute()
        {
            var user = _account.CurrentAccount;

            return "TODO";
        }
    }
}
