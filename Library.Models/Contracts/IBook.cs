using Library.Models.Enums;
using System;

namespace Library.Models.Contracts
{    
    public interface IBook
    {
        int Id { get; }

        string Author { get; }

        string Title { get; }

        string ISBN { get; }

        string Genre { get; }

        string Publisher { get; }

        int Year { get; }

        int Rack { get; }

        BookStatus Status { get; }

        //DateTime CheckoutDate { get; }

        //DateTime DueDate { get; }

        //DateTime ResevedDate { get; }

        //DateTime ResevationDueDate { get; set; }

        void Update(IBook otherBook);

        void Update(BookStatus status, DateTime today, DateTime dueDate);

        void Update(BookStatus status, DateTime reservationDate, DateTime reservationDueDate, bool ifReservation);
    }
}
