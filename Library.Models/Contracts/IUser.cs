using Library.Models.Enums;
using Library.Models.Models;
using System;
using System.Collections.Generic;
using System.Text;

namespace Library.Models.Contracts
{
    public interface IUser : IAccount
    {
        AccountStatus Status { get; set; }

        decimal LateFees { get; set; }

        int Id { get; set; }
              
        List<CheckoutBook> OverdueReservations { get; set; }
        void AddToOverdueBooks(List<CheckoutBook> overdueBooks);

        List<CheckoutBook> OverdueBooks { get; set; }

        //void AddBookToCheckoutBooks(IBook book);
        //void RemoveFromReservedBooks(IBook book);
        //void RemoveFromCheckedoutBooks(IBook book);
        //void Update(IUser otherUser);
        //string DisplayCheckedoutBooks();
        //void AddBookToReservedBooks(IBook book);


        //void AddOverdueReservations(List<IBook> overdueReservations);
        //void RemoveFromOverdueReservations(IBook book);
        //void RemoveFromOverdueBooks(IBook book);
        //void RemoveAllOverdueReservations();
        //bool HasOverdueReservations();
        //bool HasOverdueBooks();
    }
}
