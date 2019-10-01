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


        public bool IsBookCheckedout(User user, string isbn) => user.CheckedoutBooks.Any(x => x.Book.ISBN == isbn);

        public bool IsMaxCheckedoutQuota(User user) => user.CheckedoutBooks.Count >= Constants.MaxBookQuota;

        public async Task AccountCancel(string id)
        {
            bool hasOverdueBooks = await this.HasOverdueBooks(id).ConfigureAwait(false);
            if (hasOverdueBooks)
                throw new Exception(Constants.AcctCancelRetBks);

            var user = await _accountManager.GetUserByIdAsync(id).ConfigureAwait(false);
            // It has to be "for" and not foreach because of await
            for (var i = 0; user.CheckedoutBooks.Count > 0;)
            {
                await this.ReturnCheckedBookAsync(user.Username, user.CheckedoutBooks[i].BookId.ToString()).ConfigureAwait(false);
            }

            for (var i = 0; user.ReservedBooks.Count > 0;)
            {
                await this.ReturnResBookAsync(user.Username, user.ReservedBooks[i].BookId.ToString()).ConfigureAwait(false);
            }

            //foreach (var book in user.CheckedoutBooks)
            //{
            //    await this.ReturnCheckedBookAsync(user.Username, book.BookId.ToString()).ConfigureAwait(false);
            //}

            await _accountManager.DeleteUserAsync(id).ConfigureAwait(false);
        }

        public async Task<bool> HasOverdueBooks(string id)
        {
            var user = await _accountManager.GetUserByIdAsync(id).ConfigureAwait(false);

            return (from book in user.CheckedoutBooks
                    where book.DueDate < DateTime.Today
                    select book).Any();
        }

        public async Task<List<Book>> GetCheckeoutBooksAsync(string userName)
        {
            var user = await _accountManager.GetUserByUsernameAsync(userName).ConfigureAwait(false);

            var checkedoutBooks = await _context.CheckoutBooks.Where(u => u.UserId == user.Id).ToListAsync().ConfigureAwait(false);

            var allBooks = await _bookManager.GetAllBooksAsync().ConfigureAwait(false);

            List<Book> booksToReturn = new List<Book>();
            foreach (var book in allBooks)
            {
                foreach (var chBook in checkedoutBooks)
                {
                    if (book.Id == chBook.BookId)
                        booksToReturn.Add(book);
                }
            }

            return booksToReturn;
        }

        public async Task<List<Book>> GetReservedBooksAsync(string userName)
        {
            var user = await _accountManager.GetUserByUsernameAsync(userName).ConfigureAwait(false);

            var reservedBooks = await _context.ReservedBooks.Where(u => u.UserId == user.Id).ToListAsync().ConfigureAwait(false);

            var allBooks = await _bookManager.GetAllBooksAsync().ConfigureAwait(false);

            List<Book> booksToReturn = new List<Book>();
            foreach (var book in allBooks)
            {
                foreach (var chBook in reservedBooks)
                {
                    if (book.Id == chBook.BookId)
                        booksToReturn.Add(book);
                }
            }

            return booksToReturn;
        }

        public async Task ReturnCheckedBookAsync(string userName, string bookId)
        {
            var booksCheckedByUser = await this.GetCheckeoutBooksAsync(userName).ConfigureAwait(false);

            var book = booksCheckedByUser.FirstOrDefault(x => x.Id.ToString() == bookId);

            book.Status = BookStatus.Available;

            var chBook = await _context.CheckoutBooks.FirstOrDefaultAsync(x => x.BookId.ToString() == bookId).ConfigureAwait(false);

            _context.CheckoutBooks.Remove(chBook);

            await _context.SaveChangesAsync()
                .ConfigureAwait(continueOnCapturedContext: false);
        }

        //    _context.Notifications.Add(new Notification()
        //    {
        //        IsSeen = false,
        //        Message = "Book returned",
        //        UserId = "nekvo admin id"
        //    });

        public async Task ReturnResBookAsync(string userName, string bookId)
        {
            var booksResByUser = await this.GetReservedBooksAsync(userName).ConfigureAwait(false);

            var book = booksResByUser.Find(x => x.Id.ToString() == bookId);

            book.Status = BookStatus.Available;

            var chBook = await _context.ReservedBooks.FirstOrDefaultAsync(x => x.BookId.ToString() == bookId).ConfigureAwait(false);

            _context.ReservedBooks.Remove(chBook);

            await _context.SaveChangesAsync().ConfigureAwait(false);
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
    }
}

