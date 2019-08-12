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
        private readonly IAccountManager _accountManager;
<<<<<<< HEAD

        public LibrarySystem(IAccountManager accountManager)
=======
        private readonly IConsoleFormatter _formatter;
        private readonly IConsoleRenderer _renderer;

<<<<<<< HEAD
        public LibrarySystem(IAccountManager accountManager, IConsoleFormatter formatter)
>>>>>>> 577c83ee1bf19f6522d03062f01fbb692e35f41d
=======
        public LibrarySystem(IAccountManager accountManager, IConsoleFormatter formatter, IConsoleRenderer renderer)
>>>>>>> 2a1c34753d68ec88c965f3170e6869d397d5c72a
        {
            _accountManager = accountManager;
            _formatter = formatter;
            _renderer = renderer;
        }

        public void CheckForOverdueBooks()
        {
            var usersWithCheckoutBooks = _accountManager.GetAllUsers().Where(users => users.CheckedOutBooks.Any()).ToList();

            foreach (var user in usersWithCheckoutBooks)
            {
                var overdueBooks = user.CheckedOutBooks.Where(b => b.DueDate < VirtualDate.VirtualToday).ToList();

<<<<<<< HEAD
                if (overdueBooks.Count != 0)
                {
                    foreach (var book in overdueBooks)
                    {
                        int overdueDays = (int)(VirtualDate.VirtualToday - book.DueDate).TotalDays;
                        this.AssignFee(user, overdueDays);
                        this.AddOverdueMessage(user, overdueDays, book);
                    }
                }
=======
                user.AddOverdueBooks(overdueBooks);
                _accountManager.UpdateUser(user);
>>>>>>> 577c83ee1bf19f6522d03062f01fbb692e35f41d
            }
        }

        public void CheckForOverdueReservations()
        {
            var usersWithReservations = _accountManager.GetAllUsers().Where(users => users.ReservedBooks.Any()).ToList();

            foreach (var user in usersWithReservations)
            {
<<<<<<< HEAD
                user.LateFees = 0;
                user.OverdueMessages = null;
                foreach (var book in overdueBooks)
                {
                    int overdueDays = (int)(VirtualDate.VirtualToday - book.DueDate).TotalDays;
                    this.AssignFee(user, overdueDays);
                    this.AddOverdueMessage(user, overdueDays, book);
                }
=======
                var overdueReservations = user.ReservedBooks.Where(b => b.ResevationDueDate < VirtualDate.VirtualToday).ToList();
                user.AddOverdueReservations(overdueReservations);
                _accountManager.UpdateUser(user);
>>>>>>> 577c83ee1bf19f6522d03062f01fbb692e35f41d
            }
        }

        public void DisplayMessageForOverdueBooks(IUser user)
        {
            var strBuilder = new StringBuilder();
            strBuilder.AppendLine("You have overdue books!");

            foreach (var book in user.OverdueBooks)
            {
                strBuilder.AppendLine(_formatter.Format(book));
            }

            strBuilder.AppendLine("Return the books to be able to use the services of the library!");

            _renderer.Output(strBuilder.ToString());
        }

        public void DisplayMessageForOverdueReservations(IUser user)
        {
            var strBuilder = new StringBuilder();
            strBuilder.AppendLine("Your reservation for:");

            foreach (var book in user.OverdueReservations)
            {
                strBuilder.AppendLine(_formatter.Format(book));
            }

            strBuilder.AppendLine("has been expired!");

            user.RemoveAllOverdueReservations();

            _renderer.Output(strBuilder.ToString());
        }

        public void AssignFee(IUser user)
        {
            foreach (var book in user.OverdueBooks)
            {
                var overdueDays = (VirtualDate.VirtualToday - book.DueDate).TotalDays;
                user.LateFees += (decimal)overdueDays * GlobalConstants.Fee;
            }
        }

        public void CheckIfMaxQuotaReached(List<IBook> books)
        {
            if (books.Count == GlobalConstants.MaxBookQuota)
            {
                throw new ArgumentException(GlobalConstants.MaxQuotaReached);
            }
        }
    }
}
