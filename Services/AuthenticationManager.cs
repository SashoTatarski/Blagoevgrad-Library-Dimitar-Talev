using Library.Models.Contracts;
using Library.Services.Contracts;
using Services.Contracts;
using System;

namespace Services
{
    public class AuthenticationManager : IAuthenticationManager
    {
        private readonly IAccountManager _accountManager;
        public AuthenticationManager(IAccountManager accountManager)
        {
            _accountManager = accountManager;
        }
        public IAccount CurrentAccount { get; private set; }

        public void LogIn(IAccount account)
        {
            this.CurrentAccount = account;
        }

        public void LogOut()
        {
            this.CurrentAccount = null;
        }

        public void CheckForExistingUsername(string username)
        {
            var account = _accountManager.FindAccount(username);

            if (account != null)
            {
                throw new ArgumentException("This username is already taken");
            }
        }
    }
}
