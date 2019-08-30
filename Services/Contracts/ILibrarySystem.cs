using Library.Models.Contracts;
using Library.Models.Models;
using System.Collections.Generic;

namespace Library.Services.Contracts
{
    public interface ILibrarySystem
    {
        void CheckCheckoutBooksQuota(User user);
        void CheckReservedBooksQuota(IUser user);

        // ------- Need update ↓ -------
        void AddBookToCheckoutBooks(Book book, User user);
        void AddBookToReservedBooks(IBook book, IUser user);
        void AssignFee(IUser user);
        void CheckForOverdueBooks();
        void CheckForOverdueReservations();
        bool ReservedByUser(User user, Book book);
        void DisplayMessageForOverdueBooks(IUser user);
        void DisplayMessageForOverdueReservations(IUser user);
        void PurgeOverdueReservations(IUser user);
        bool UserHasCheckedoutBooks(User user);
        bool UserHasReservedBooks(User user);
    }
}
