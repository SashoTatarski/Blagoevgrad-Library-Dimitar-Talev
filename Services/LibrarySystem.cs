using Library.Models.Contracts;
using Library.Models.Enums;
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
        private readonly IConsoleFormatter _formatter;
        private readonly IConsoleRenderer _renderer;
        private readonly IBookManager _bookManager;

        public LibrarySystem(IAccountManager accountManager, IConsoleFormatter formatter, IConsoleRenderer renderer, IBookManager bookManager)
        {
            _accountManager = accountManager;
            _formatter = formatter;
            _renderer = renderer;
            _bookManager = bookManager;
        }

        public void CheckForOverdueBooks()
        {
            //var usersWithCheckoutBooks = _accountManager.GetAllUsers().Where(users => users.CheckedOutBooks.Any()).ToList();

            //foreach (var user in usersWithCheckoutBooks)
            //{
            //    var overdueBooks = user.CheckedOutBooks.Where(b => b.DueDate < VirtualDate.VirtualToday).ToList();

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

        public void CheckIfMaxQuotaReached(List<IBook> books)
        {
            if (books.Count == GlobalConstants.MaxBookQuota)
                throw new ArgumentException(GlobalConstants.MaxQuotaReached);
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

