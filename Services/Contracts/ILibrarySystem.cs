using Library.Models.Enums;
using Library.Models.Models;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Library.Services.Contracts
{
    public interface ILibrarySystem
    {
        Task ChangeBookStatus(string bookId, BookStatus status);
        Task AddBookToCheckoutBooksAsync(string bookId, string userName);





        void CheckCheckoutBooksQuota(User user);
        void CheckReservedBooksQuota(User user);        
        ReservedBook AddBookToReservedBooks(Book book, User user);
        List<CheckoutBook> GetCheckedOutBooks(User user);
        void RemoveBookFromCheckoutBooks(Book book);
        void ManageOverdueReservations();
        bool HasIssuedBooks(User user);
    }
}
