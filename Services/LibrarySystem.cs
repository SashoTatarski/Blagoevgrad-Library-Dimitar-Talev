using Library.Database;
using Library.Database.Contracts;
using Library.Models.Contracts;
using Library.Models.Enums;
using Library.Models.Models;
using Library.Models.Utils;
using Library.Services.Contracts;
using System;
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
        
        public void AddBookToCheckoutBooks(Book book, User user)
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
        }

        public void AddBookToReservedBooks(Book book, User user)
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
        }


        // ------- Need update ↓ -------




        public bool ReservedByUser(User user, Book book)
        {
            return _context.ReservedBooks.Any(b => b.BookId == book.Id && b.UserId == user.Id);
        }

        public bool UserHasCheckedoutBooks(User user) => _context.CheckoutBooks.Any(x => x.UserId == user.Id);
        public bool UserHasReservedBooks(User user) => _context.ReservedBooks.Any(x => x.UserId == user.Id);



        public void CheckForOverdueBooks()
        {
            //var usersWithCheckoutBooks = _accountManager.GetAllUsers().Where(users => users.CheckedoutBooks.Any()).ToList();

            //foreach (var user in usersWithCheckoutBooks)
            //{
            //    var overdueBooks = _context.CheckoutBooks.Where(x => x.UserId == user.Id && x.DueDate < VirtualDate.VirtualToday).ToList(); //user.CheckedoutBooks.Where(b => b.DueDate < VirtualDate.VirtualToday).ToList();

            //    user.AddToOverdueBooks(overdueBooks);
            //    _accountManager.UpdateUser(user);
            //}
        }

        public void CheckForOverdueReservations()
        {
            //var usersWithReservations = _accountManager.GetAllUsers().Where(users => users.ReservedBooks.Any()).ToList();

            //foreach (var user in usersWithReservations)
            //{
            //    var overdueReservations = user.ReservedBooks.Where(b => b.ResevationDueDate < VirtualDate.VirtualToday).ToList();
            //    user.AddOverdueReservations(overdueReservations);
            //    _accountManager.UpdateUser(user);
            //}
        }

        public void DisplayMessageForOverdueBooks(IUser user)
        {
            var strBuilder = new StringBuilder();
            //strBuilder.AppendLine("You have overdue books!");

            //foreach (var book in user.OverdueBooks)
            //    strBuilder.AppendLine(_formatter.FormatCheckedoutBook(book));

            //strBuilder.AppendLine("Return the books to be able to use the services of the library!");

            _renderer.Output(strBuilder.ToString());
        }

        public void DisplayMessageForOverdueReservations(IUser user)
        {
            //var strBuilder = new StringBuilder();
            //strBuilder.AppendLine("Your reservation for:");

            //foreach (var book in user.OverdueReservations)
            //    strBuilder.AppendLine(_formatter.FormatReservedBook(book));

            //strBuilder.AppendLine("has been expired!");
            //_renderer.Output(strBuilder.ToString());
        }

        public void AssignFee(IUser user)
        {
            //foreach (var book in user.OverdueBooks)
            //{
            //    var overdueDays = (VirtualDate.VirtualToday - book.DueDate).TotalDays;
            //    user.LateFees += (decimal)overdueDays * GlobalConstants.Fee;
            //}
        }



        public void PurgeOverdueReservations(IUser user)
        {
            this.DisplayMessageForOverdueReservations(user);
            this.ClearOverdueReservations(user);
        }

        private void ClearOverdueReservations(IUser user)
        {
            //for (int i = 0; i < user.OverdueReservations.Count; i++)
            //{
            //    if (user.OverdueReservations[i].Status == BookStatus.Reserved)
            //    {
            //        var bookToUpdate = user.OverdueReservations[i];
            //        _bookManager.UpdateBook(bookToUpdate.Id, BookStatus.Available, DateTime.MinValue, DateTime.MinValue, true);
            //    }
            //}
            //user.RemoveAllOverdueReservations();
            _accountManager.UpdateUser(user);
        }
    }
}

