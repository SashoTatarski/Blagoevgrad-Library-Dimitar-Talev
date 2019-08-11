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
       //List<string> ReservedBookMessages { get; }
        decimal LateFees { get; set; }
       // List<string> OverdueMessages { get; set; }
        List<IBook> OverdueReservations { get; set; }
        List<IBook> OverdueBooks { get; set; }

        void AddBookToCheckoutBooks(IBook book);
        void RemoveFromReservedBooks(IBook book);
        void RemoveFromCheckedoutBooks(IBook book);
        void Update(IUser otherUser);
        string DisplayCheckedoutBooks();
        void AddBookToReservedBooks(IBook book);
        void AddOverdueBooks(List<IBook> overdueBooks);
        void AddOverdueReservations(List<IBook> overdueReservations);
        void RemoveFromOverdueReservations(IBook book);
    }
}
