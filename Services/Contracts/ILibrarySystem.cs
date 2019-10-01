using Library.Models.Enums;
using Library.Models.Models;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Library.Services.Contracts
{
    public interface ILibrarySystem
    {
        Task ChangeBookStatusAsync(string bookId, BookStatus status);
        Task AddBookToCheckoutBooksAsync(string bookId, string userName);
        bool IsBookCheckedout(User user, string isbn);
        Task ReturnCheckedBookAsync(string userName, string bookId);
        Task ReturnResBookAsync(string userName, string bookId);
        Task<List<Book>> GetReservedBooksAsync(string userName);
        Task AccountCancel(string id);
        Task<bool> HasOverdueBooks(string id);

        bool IsMaxCheckedoutQuota(User user);
        Task AddBookToReservedBooksAsync(string bookId, User user);

        Task<bool> AreAllCopiesCheckedAsync(string isbn);
    }
}
