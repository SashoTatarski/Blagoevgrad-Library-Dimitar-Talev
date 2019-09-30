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
        Task<List<Book>> GetCheckeoutBooksAsync(string userName);
        Task ReturnCheckedBookAsync(string userName, string bookId);
        Task ReturnResBookAsync(string userName, string bookId);
        Task<List<Book>> GetReservedBooksAsync(string userName);
        Task AccountCancel(string id);



        void CheckCheckoutBooksQuota(User user);
        void CheckReservedBooksQuota(User user);        
        ReservedBook AddBookToReservedBooks(Book book, User user);        
        void RemoveBookFromCheckoutBooks(Book book);
        void ManageOverdueReservations();
        bool HasIssuedBooks(User user);
        Task<bool> HasOverdueBooks(string id);
    }
}
