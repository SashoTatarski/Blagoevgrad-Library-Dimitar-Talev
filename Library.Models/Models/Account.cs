using Library.Models.Contracts;
using System;
using System.Collections.Generic;

namespace Library.Models.Models
{
    public abstract class Account : IAccount
    {
        private string username;
        private string password;

        public Account(string username, string password)
        {
            this.Username = username;
            this.Password = password;
        }

        public virtual string Username
        {
            get
            {
                return this.username;
            }
            private set
            {
                if (value.Length < 1 || value.Length > 30)
                {
                    throw new ArgumentException("The name should be between 1 and 30 characters");
                }
                this.username = value;
            }
        }
        public virtual string Password
        {
            get
            {
                return this.password;
            }
            private set
            {
                if (value.Length < 3 || value.Length > 20)
                {
                    throw new ArgumentException("The password should be between 3 and 20 characters");
                }
                this.password = value;
            }
        }

        public IEnumerable<string> AllowedCommands { get; }
    }
}
