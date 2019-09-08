using Library.Database;
using Library.Models.Enums;
using Library.Models.Models;
using Library.Models.Utils;
using Library.Services.Contracts;
using System;
using System.Collections.Generic;
using System.Linq;

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
        public void CheckCheckoutBooksQuota(User user)
        {
            var checkedoutBooks = _context.CheckoutBooks.Where(chb => chb.UserId == user.Id).ToList();

            if (checkedoutBooks.Count >= GlobalConstants.MaxBookQuota)
                throw new ArgumentException(GlobalConstants.MaxQuotaReached);
        }

        public void CheckReservedBooksQuota(User user)
        {
            var reservedBooks = _context.ReservedBooks.Where(chb => chb.UserId == user.Id).ToList();

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
            _context.CheckoutBooks.Add(bookToAdd);
            _context.SaveChanges();
            return bookToAdd;
        }

        public void RemoveBookFromCheckoutBooks(Book book)
        {
            if (book.Status == BookStatus.CheckedOutAndReserved)
            {
                _bookManager.ChangeBookStatus(book, BookStatus.Reserved);
            }
            else
            {
                _bookManager.ChangeBookStatus(book, BookStatus.Available);

            }
            var bookToRemove = _context.CheckoutBooks.FirstOrDefault(b => b.BookId == book.Id);
            _context.CheckoutBooks.Remove(bookToRemove);
            _context.SaveChanges();
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
            _context.ReservedBooks.Add(bookToAdd);
            _context.SaveChanges();
            return bookToAdd;
        }

        public List<CheckoutBook> GetCheckedOutBooks(User user)
        {
            return _context.CheckoutBooks.Where(chb => chb.UserId == user.Id).ToList();
        }

        private List<ReservedBook> CheckForOverdueReservations()
        {
            return _context.ReservedBooks.Where(rb => rb.ReservationDueDate < VirtualDate.VirtualToday).ToList();
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

                    var bookToRemove = _context.ReservedBooks.FirstOrDefault(b => b.BookId == book.BookId);
                    _context.ReservedBooks.Remove(bookToRemove);
                    _context.SaveChanges();
                }
            }
        }

        public bool HasIssuedBooks(User user)
        {
            var checkoutBooks = _context.CheckoutBooks.Where(chb => chb.UserId == user.Id).ToList();
            var reservedBooks = _context.ReservedBooks.Where(chb => chb.UserId == user.Id).ToList();

            if (checkoutBooks is null && reservedBooks is null)
            {
                return false;
            }
            else
                return true;
        }       
    }
}

