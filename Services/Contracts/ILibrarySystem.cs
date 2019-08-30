using Library.Models.Contracts;
using Library.Models.Models;
using System.Collections.Generic;

namespace Library.Services.Contracts
{
    public interface ILibrarySystem
    {
        void CheckCheckoutBooksQuota(User user);
        void CheckReservedBooksQuota(User user);
        CheckoutBook AddBookToCheckoutBooks(Book book, User user);
        ReservedBook AddBookToReservedBooks(Book book, User user);
        List<CheckoutBook> GetCheckedOutBooks(User user);
        void RemoveBookFromCheckoutBooks(Book book);

        // ------- Need update ↓ -------

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
