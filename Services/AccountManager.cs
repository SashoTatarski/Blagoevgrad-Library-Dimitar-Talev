using Library.Models.Contracts;
using Services.Contracts;
using System;
using System.Collections.Generic;
using System.Text;

namespace Services
{
    public class AccountManager : IAccountManager
    {
        public IAccount CurrentAccount { get; private set; }

        public void LogIn(IAccount account)
        {
            CurrentAccount = account;
        }

        public void LogOut()
        {
            CurrentAccount = null;
        }
    }
}
