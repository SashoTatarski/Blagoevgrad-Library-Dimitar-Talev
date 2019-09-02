using Library.Database;
using Library.Database.Contracts;
using Library.Models.Contracts;
using Library.Models.Enums;
using Library.Models.Models;
using Library.Models.Utils;
using Library.Services.Contracts;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Library.Services
{
    public class LibrarySystem : ILibrarySystem
    {
        private readonly IAccountManager _accountManager;
        private readonly IConsoleRenderer _renderer;
        private readonly LibraryContext _context;
        private readonly IBookManager _bookManager;
        private readonly IssuedBookDataBase _issuedBookDb;


        public LibrarySystem(IAccountManager accountManager, IConsoleRenderer renderer, LibraryContext context, IBookManager bookManager, IssuedBookDataBase issuedBookDb)
        {
            _accountManager = accountManager;
            _bookManager = bookManager;
            _issuedBookDb = issuedBookDb;
            _renderer = renderer;
            _context = context;
        }
        public void CheckCheckoutBooksQuota(User user)
        {
            var checkedoutBooks = _issuedBookDb.GetCheckOutBooks(user);

            if (checkedoutBooks.Count >= GlobalConstants.MaxBookQuota)
                throw new ArgumentException(GlobalConstants.MaxQuotaReached);
        }

        public void CheckReservedBooksQuota(User user)
        {
            var reservedBooks = _issuedBookDb.GetReservedBooks(user);

            if (reservedBooks.Count >= GlobalConstants.MaxBookQuota)
                throw new ArgumentException(GlobalConstants.MaxQuotaReached);
        }

        public CheckoutBook AddBookToCheckoutBooks(Book book, User user)
        {
            var bookToAdd = new CheckoutBook()
            {
                BookId = book.Id,
                UserId = user.Id,
                CheckoutDate = VirtualDate.VirtualToday,
                DueDate = VirtualDate.VirtualToday.AddDays(GlobalConstants.MaxCheckoutDays)
            };

            _bookManager.ChangeBookStatus(book, BookStatus.CheckedOut);
            _issuedBookDb.AddCheckedOutBook(bookToAdd);
            return bookToAdd;
        }

        public void RemoveBookFromCheckoutBooks(Book book)
        {
            if (book.Status == BookStatus.CheckedOut_and_Reserved)
            {
                _bookManager.ChangeBookStatus(book, BookStatus.Reserved);
            }
            else
            {
                _bookManager.ChangeBookStatus(book, BookStatus.Available);

            }
            _issuedBookDb.RemoveCheckedOutBook(book.Id);
        }

        public ReservedBook AddBookToReservedBooks(Book book, User user)
        {
            var bookToAdd = new ReservedBook()
            {
                BookId = book.Id,
                UserId = user.Id,
                ReservationDate = VirtualDate.VirtualToday,
                ReservationDueDate = VirtualDate.VirtualToday.AddDays(GlobalConstants.MaxCheckoutDays)
            };

            _bookManager.ChangeBookStatus(book, BookStatus.Reserved);
            _issuedBookDb.AddReservedBook(bookToAdd);
            return bookToAdd;
        }

        public List<CheckoutBook> GetCheckedOutBooks(User user)
        {
            return _issuedBookDb.GetCheckOutBooks(user);
        }

        private List<ReservedBook> CheckForOverdueReservations()
        {
            return _issuedBookDb.GetReservedBooks().Where(rb => rb.ReservationDueDate < VirtualDate.VirtualToday).ToList();
        }

        public void ManageOverdueReservations()
        {
            var overdueReservations = this.CheckForOverdueReservations();
            if (overdueReservations != null)
            {
                foreach (var book in overdueReservations)
                {
                    book.User.Messages.Add($"The book {book.Book.Title} is no longer reserved by you");
                    _bookManager.ChangeBookStatus(book.Book, BookStatus.Available);
                    _issuedBookDb.RemoveReservedBook(book.BookId);
                }
            }
        }

        public bool HasIssuedBooks(User user)
        {
            var checkoutBooks = _issuedBookDb.GetCheckOutBooks(user);
            var reservedBooks = _issuedBookDb.GetReservedBooks(user);

            if (checkoutBooks is null && reservedBooks is null)
            {
                return false;
            }
            else
                return true;
        }
        // ------- Need update ↓ -------

       
      
    }
}

