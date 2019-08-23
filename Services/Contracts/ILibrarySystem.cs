using Library.Models.Contracts;
using Library.Models.Models;
using System.Collections.Generic;

namespace Library.Services.Contracts
{
    public interface ILibrarySystem
    {
        void AddBookToCheckoutBooks(IBook book, IUser user);
        void AssignFee(IUser user);

        void CheckForOverdueBooks();

        void CheckForOverdueReservations();

        void CheckIfMaxQuotaReached(List<IBook> books);

        void DisplayMessageForOverdueBooks(IUser user);

        void DisplayMessageForOverdueReservations(IUser user);
        void PurgeOverdueReservations(IUser user);
    }
}
