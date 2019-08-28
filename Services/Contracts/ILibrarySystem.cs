using Library.Models.Contracts;
using Library.Models.Models;
using System.Collections.Generic;

namespace Library.Services.Contracts
{
    public interface ILibrarySystem
    {
        void CheckCheckoutBooksQuota(IUser user);
        void CheckReservedBooksQuota(IUser user);

        // ------- Need update ↓ -------
        void AddBookToCheckoutBooks(IBook book, IUser user);
        void AddBookToReservedBooks(IBook book, IUser user);
        void AssignFee(IUser user);
        void CheckForOverdueBooks();
        void CheckForOverdueReservations();
        bool ReservedByUser(IUser user, IBook book);
        void DisplayMessageForOverdueBooks(IUser user);
        void DisplayMessageForOverdueReservations(IUser user);
        void PurgeOverdueReservations(IUser user);
        bool UserHasCheckedoutBooks(IUser user);
        bool UserHasReservedBooks(IUser user);
    }
}
