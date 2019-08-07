using Library.Models.Contracts;
using System;
using System.Collections.Generic;

namespace Library.Models.Models
{
    public abstract class Account : IAccount
    {
        private string _username;
        private string _password;

        public Account(string username, string password)
        {
            this.Username = username;
            this.Password = password;
        }

        public virtual string Username
        {
            get => _username;
            private set
            {
                if (value.Length < 1 || value.Length > 30)
                    throw new ArgumentException("The name should be between 1 and 30 characters");

                _username = value;
            }
        }
        public virtual string Password
        {
            get => _password;
            private set
            {
                if (value.Length < 3 || value.Length > 20)
                    throw new ArgumentException("The password should be between 3 and 20 characters");

                _password = value;
            }
        }

        public virtual IEnumerable<string> AllowedCommands { get; }
    }
}
