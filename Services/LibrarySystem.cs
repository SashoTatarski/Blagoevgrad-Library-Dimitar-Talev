using Library.Models.Contracts;
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
        private readonly IBookManager _bookManager;
        private readonly IAccountManager _accountManager;
        public LibrarySystem(IBookManager bookManager, IAccountManager accountManager)
        {
            _bookManager = bookManager;
            _accountManager = accountManager;
        }
        public void CheckForOverdueBooks()
        {
            var usersWithCheckoutBooks = _accountManager.GetAllUsers().Where(users => users.CheckedOutBooks.Any()).ToList();

            foreach (var user in usersWithCheckoutBooks)
            {
                var overdueBooks = user.CheckedOutBooks.Where(b => b.DueDate < VirtualDate.VirtualToday).ToList();

                if (overdueBooks.Count != 0)
                {
                    foreach (var book in overdueBooks)
                    {
                        int overdueDays = (int)((VirtualDate.VirtualToday - book.DueDate).TotalDays);
                        this.AssignFee(user, overdueDays);
                        this.AddOverdueMessage(user, overdueDays, book);
                    }
                }
            }
        }

        public void CheckForOverdueBooks(IUser user)
        {
            var overdueBooks = user.CheckedOutBooks.Where(b => b.DueDate < VirtualDate.VirtualToday).ToList();

            if (overdueBooks.Count != 0)
            {
                user.LateFees = 0;
                user.OverdueMessages = null;
                foreach (var book in overdueBooks)
                {
                    int overdueDays = (int)((VirtualDate.VirtualToday - book.DueDate).TotalDays);
                    this.AssignFee(user, overdueDays);
                    this.AddOverdueMessage(user, overdueDays, book);
                }
            }
        }

        public void CheckForOverDueReservations()
        {
            var users = _accountManager.GetAllUsers();

            foreach (var user in users)
            {
                var overdueReservations = user.ReservedBooks.Where(b => b.ResevationDueDate < VirtualDate.VirtualToday).ToList();
            }
        }

        private void AssignFee(IUser user, int overdueDays)
        {
            user.LateFees += overdueDays * GlobalConstants.Fee;
        }

        private void AddOverdueMessage(IUser user, int overdueDays, IBook book)
        {
            var message = (GlobalConstants.OverdueMessage, book.ID, book.Title, book.Author, overdueDays).ToString();

            user.OverdueMessages.Add(message);
        }
    }
}
