using Library.Models.Enums;
using System;
using System.Collections.Generic;
using System.Text;

namespace Library.Models.Contracts
{
    public interface IUser : IAccount
    {
        MemberStatus Status { get; set; }
        List<IBook> CheckedOutBooks { get; }
        List<IBook> ReservedBooks { get; }
        List<string> ReservedBookMessages { get; }
        decimal LateFees { get; set; }
        List<string> OverdueMessages { get; }
        void AddBookToCheckoutBooks(IBook book);
        void RemoveFromReservedBooks(IBook book);
        void RemoveFromCheckedoutBooks(IBook book);
        void Update(IUser otherUser);
        string DisplayCheckedoutBooks();
    }
}
