using Library.Models.Contracts;
using Library.Models.Utils;
using System.Collections.Generic;

namespace Library.Models.Models
{
    // SOLID: Liskov - we can add substitute for another type of account
    // SOLID: OPEN/CLOSED - we can can add more accounts without breaking our code
    // OOP: Abstraction - Account class is abstract, User "is a" Account
    public abstract class Account : IAccount
    {
        private string _username;
        private string _password;

        // Changed "public" to protected (abstract type should not have public contructors)
        protected Account(string username, string password)
        {
            this.Username = username;
            this.Password = password;
        }

        public virtual string Username
        {
            get => _username;
            private set
            {
                DataValidator.ValidateMinAndMaxLength(value, 3, 20, "Username");
                _username = value;
            }
        }
        public virtual string Password
        {
            get => _password;
            private set
            {
                DataValidator.ValidateMinAndMaxLength(value, 3, 20, "Password");
                _password = value;
            }
        }

        public virtual IEnumerable<string> AllowedCommands { get; }
    }
}
