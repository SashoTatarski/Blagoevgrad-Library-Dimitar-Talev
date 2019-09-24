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
        private readonly LibraryContext _context;


        public LibrarySystem(IBookManager bookManager, LibraryContext context)
        {
            _bookManager = bookManager;
            _context = context;
        }



        public async Task AddBookToCheckoutBooksAsync(Book bookId, string userName)
        {
            var user = await _context.Users.FirstOrDefaultAsync(x => x.Username == userName).ConfigureAwait(false);

            var newBook = new CheckoutBook()
            {
                BookId = bookId.Id,
                UserId = user.Id,
                CheckoutDate = DateTime.Today,
                DueDate = DateTime.Today.AddDays(Constants.MaxCheckoutDays)
            };

           await this.ChangeBookStatus(bookId.Id.ToString(), BookStatus.CheckedOut).ConfigureAwait(false);

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

