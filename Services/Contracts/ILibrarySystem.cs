using Library.Models.Contracts;
using Library.Models.Models;
using System.Collections.Generic;

namespace Library.Services.Contracts
{
    public interface ILibrarySystem
    {
        void AddBookToCheckoutBooks(IBook book, IUser user);
        void AddBookToReservedBooks(IBook book, IUser user);
        void AssignFee(IUser user);

        void CheckForOverdueBooks();

        void CheckForOverdueReservations();

        void CheckIfMaxQuotaReached(IUser user);
        bool ReservedByUser(IUser user, IBook book);
        void DisplayMessageForOverdueBooks(IUser user);

        void DisplayMessageForOverdueReservations(IUser user);
        void PurgeOverdueReservations(IUser user);
        bool UserHasCheckedoutBooks(IUser user);
        bool UserHasReservedBooks(IUser user);
    }
}
