using Library.Models.Contracts;
using System;
using System.Collections.Generic;
using System.Text;

namespace Library.Models.Models
{
    public class User : IUser
    {
        public User(string username, string password)
        {
            Username = username;
            Password = password;
            CheckedOutBooks = CheckedOutBooks;
            ReservedBooks = ReservedBooks;
            ReservedBookMessage = ReservedBookMessage;
        }

        public string Username { get; }
        public string Password { get; }

        public List<IBook> CheckedOutBooks { get; } = new List<IBook>();

        public List<IBook> ReservedBooks { get; } = new List<IBook>();

        public List<string> ReservedBookMessage { get; } = new List<string>();

        public decimal LateFees { get; set; }

        public string OverdueMessage { get; set; }
    }
}
