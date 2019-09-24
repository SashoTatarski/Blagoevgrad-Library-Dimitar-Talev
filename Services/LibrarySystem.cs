using Library.Database;
using Library.Models.Enums;
using Library.Models.Models;
using Library.Models.Utils;
using Library.Services.Contracts;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Library.Services
{
    public class LibrarySystem : ILibrarySystem
    {
        private readonly IBookManager _bookManager;
        private readonly IAccountManager _accountManager;
        private readonly LibraryContext _context;


        public LibrarySystem(IBookManager bookManager, IAccountManager accountManager, LibraryContext context)
        {
            _bookManager = bookManager;
            _accountManager = accountManager;
            _context = context;
        }

        public async Task AddBookToCheckoutBooksAsync(string bookId, string userName)
        {
            var user = await _accountManager.GetUserByUsernameAsync(userName).ConfigureAwait(false);

            var newBook = new CheckoutBook()
            {
                BookId = Guid.Parse(bookId),
                UserId = user.Id,
                CheckoutDate = DateTime.Today,
                DueDate = DateTime.Today.AddDays(Constants.MaxCheckoutDays)
            };

           await this.ChangeBookStatus(bookId, BookStatus.CheckedOut).ConfigureAwait(false);

            _context.CheckoutBooks.Add(newBook);
            await _context.SaveChangesAsync().ConfigureAwait(false);            
        }

        public async Task ChangeBookStatus(string bookId, BookStatus status)
        {
            var book = await _bookManager.GetBookByIdAsync(bookId).ConfigureAwait(false);

            book.Status = status;

            await _context.SaveChangesAsync().ConfigureAwait(false);
        }

        public ReservedBook AddBookToReservedBooks(Book book, User user) => throw new NotImplementedException();
        public void CheckCheckoutBooksQuota(User user) => throw new NotImplementedException();
        public void CheckReservedBooksQuota(User user) => throw new NotImplementedException();
        public List<CheckoutBook> GetCheckedOutBooks(User user) => throw new NotImplementedException();
        public bool HasIssuedBooks(User user) => throw new NotImplementedException();
        public void ManageOverdueReservations() => throw new NotImplementedException();
        public void RemoveBookFromCheckoutBooks(Book book) => throw new NotImplementedException();
    }
}

