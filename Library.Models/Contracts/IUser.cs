using System;
using System.Collections.Generic;
using System.Text;

namespace Library.Models.Contracts
{
    public interface IUser
    {
        string Username { get; }

        string Password { get; }

        List<IBook> CheckedOutBooks { get; }

        List<IBook> ReservedBooks { get; }

        List<string> ReservedBookMessage { get; }

        decimal LateFees { get; set; }

        string OverdueMessage { get; set; }
        void AddToCheckoutBooks(IBook book);
        void RemoveFromReservedBooks(IBook book);
    }
}
