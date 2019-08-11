using Library.Models.Contracts;
using Library.Services.Contracts;
using Services.Contracts;
using System;
using System.Collections.Generic;

namespace Services
{
    public class AuthenticationManager : IAuthenticationManager
    {
        private readonly IAccountManager _accountManager;

        public AuthenticationManager(IAccountManager accountManager)
        {
            _accountManager = accountManager;
            this.CurrentAccount = null;
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

        public List<string> GetAllowedCommands()
        {
            if (this.CurrentAccount is null)
            {
                return new List<string> { "Log In", "Exit" };
            }
            else
            {
                return (List<string>)this.CurrentAccount.AllowedCommands;
            }
        }

        public string GetCurrentAccountType()
        {
            if (this.CurrentAccount is null)
            {
                return null;
            }
            else if (this.CurrentAccount.GetType() == typeof(ILibrarian))
            {
                return "Librarian";
            }
            else
            {
                return "User";
            }
        }
    }
}
