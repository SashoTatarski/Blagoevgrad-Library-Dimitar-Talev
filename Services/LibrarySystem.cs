﻿using Library.Database;
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

        public async Task ReturnCheckedBookAsync(string isbn, string userName)
        {
            var user = await _accountManager.GetUserByUsernameAsync(userName);
            var booksByIsbn = await _bookManager.GetBooksByIsbnAsync(isbn);

            var bookToReturn = booksByIsbn.Where(b => b.CheckedoutBook != null).FirstOrDefault(b => b.CheckedoutBook.UserId == user.Id);

            if (bookToReturn is null)
            {
                throw new ArgumentException(Constants.NotCheckedOutThisBook);
            }

            await this.ChangeBookStatusAsync(bookToReturn.Id.ToString(), BookStatus.Available);

            var checkedoutBookToReturn = await _context.CheckoutBooks.FirstOrDefaultAsync(b => b.BookId == bookToReturn.Id).ConfigureAwait(false);
            _context.CheckoutBooks.Remove(checkedoutBookToReturn);
            await _context.SaveChangesAsync().ConfigureAwait(false);
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

        public async Task<bool> AreAllCopiesCheckedAsync(string isbn)
        {
            var books = await _bookManager.GetBooksByIsbnAsync(isbn);

            foreach (var book in books)
            {
                if (book.Status == BookStatus.Available || book.Status == BookStatus.Reserved)
                    return false;
            }

            return true;
        }

        public async Task AddBookToReservedBooksAsync(string isbn, string userName)
        {
            var user = await _accountManager.GetUserByUsernameAsync(userName);
            var booksByIsbn = await _bookManager.GetBooksByIsbnAsync(isbn);

            var bookToReserve = booksByIsbn.FirstOrDefault(b => b.Status != BookStatus.ToBeDeleted);

            if (bookToReserve is null)
            {
                throw new ArgumentException(Constants.BookToBeDeleted);
            }

            var newBook = new ReservedBook()
            {
                BookId = bookToReserve.Id,
                UserId = user.Id,
                ReservationDate = null,
                ReservationDueDate = null
            };

            await this.ChangeBookStatusAsync(bookToReserve.Id.ToString(), BookStatus.Reserved);

            _context.ReservedBooks.Add(newBook);
            await _context.SaveChangesAsync().ConfigureAwait(false);
        }

        public async Task AddBookToCheckoutBooksAsync(string isbn, string userName)
        {
            var user = await _accountManager.GetUserByUsernameAsync(userName);
            var booksByIsbn = await _bookManager.GetBooksByIsbnAsync(isbn);

            var bookToTake = booksByIsbn.FirstOrDefault(b => b.Status == BookStatus.Available);

            if (bookToTake is null)
            {
                throw new ArgumentException(Constants.NoAvailableBooks);
            }

            var newBook = new CheckoutBook()
            {
                BookId = bookToTake.Id,
                UserId = user.Id,
                CheckoutDate = DateTime.Today,
                DueDate = DateTime.Today.AddDays(Constants.MaxCheckoutDays)
            };

            await this.ChangeBookStatusAsync(bookToTake.Id.ToString(), BookStatus.CheckedOut);

            _context.CheckoutBooks.Add(newBook);
            await _context.SaveChangesAsync().ConfigureAwait(false);
        }

        public async Task ChangeBookStatusAsync(string bookId, BookStatus status)
        {
            var book = await _bookManager.GetBookByIdAsync(bookId).ConfigureAwait(false);

            if (book.Status == BookStatus.CheckedOut && status == BookStatus.Reserved)
                book.Status = BookStatus.CheckedOutAndReserved;
            else if ((book.Status == BookStatus.CheckedOutAndReserved || book.Status == BookStatus.Reserved) && status == BookStatus.Reserved)
            { }
            else if (book.Status == BookStatus.CheckedOutAndReserved)
                book.Status = BookStatus.Reserved;
            else
                book.Status = status;

            await _context.SaveChangesAsync().ConfigureAwait(false);
        }

        public async Task<string> ExtendBookDueDate(string id, string userName)
        {
            var user = await _accountManager.GetUserByUsernameAsync(userName);
            var bookToExtend = await _bookManager.GetBookByIdAsync(id);

            if (user.Wallet - Constants.ExtendCost < 0)
            {
                return Constants.NotEnoughMoney;
            }

            var newDueDate = bookToExtend.CheckedoutBook.DueDate.AddDays(Constants.ExtendPeriod);

            if (newDueDate > user.MembershipEndDate)
            {
                return Constants.MembershipExpirationWarning;
            }

            user.Wallet -= Constants.ExtendCost;
            bookToExtend.CheckedoutBook.DueDate = newDueDate;

            await _context.SaveChangesAsync().ConfigureAwait(false);

            var message = string.Format(Constants.ExtendBookNotification, user.Username, bookToExtend.Title, bookToExtend.Id);
            await this.AddNotificationAsync(message, await _accountManager.GetAdminAccountAsync());

            return Constants.ExtendSuccess;
            
        }

        public async Task<string> CancelReservation(string id, string userName)
        {
            var user = await _accountManager.GetUserByUsernameAsync(userName);

            var reservatedBookToCancel = await _context.ReservedBooks
                .Include(rb => rb.Book)
                .FirstOrDefaultAsync(resBook => resBook.BookId.ToString() == id && resBook.UserId == user.Id)
                .ConfigureAwait(false);

            _context.ReservedBooks.Remove(reservatedBookToCancel);
            await _context.SaveChangesAsync().ConfigureAwait(false);

            var message = string.Format(Constants.CancelReservationNotification, user.Username, reservatedBookToCancel.Book.Title, reservatedBookToCancel.BookId);
            await this.AddNotificationAsync(message, await _accountManager.GetAdminAccountAsync());
            

            return Constants.CancelReservationSuccess;
        }


        public async Task AddNotificationAsync(string message, User user)
        {
            var notification = new Notification
            {
                SentOn = DateTime.Now,
                IsSeen = false,
                Message = message,
                UserId = user.Id
            };

            _context.Notifications.Add(notification);
            await _context.SaveChangesAsync().ConfigureAwait(false);
        }
    }
}

