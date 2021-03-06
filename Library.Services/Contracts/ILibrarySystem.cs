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
        void ManageOverdueReservations();
        bool HasIssuedBooks(User user);

        // ------- Need update ↓ -------


        //void CheckForOverdueBooks();
        //void CheckForOverdueReservations();

        //bool UserHasCheckedoutBooks(User user);
        //bool UserHasReservedBooks(User user);
        //void ManageOverdueReservations();
    }
}
