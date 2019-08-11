using Library.Models.Contracts;
using Library.Models.Utils;
using Library.Services.Contracts;
using Library.Services.Factory;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Library.Services
{
    public class LibrarySystem : ILibrarySystem
    {
        private readonly IBookManager _bookManager;
        private readonly IAccountManager _accountManager;
        private readonly IConsoleFormatter _formatter;
        private readonly IMenuFactory _menuFactory;
        private readonly IConsoleRenderer _renderer;
        public LibrarySystem(IBookManager bookManager, IAccountManager accountManager, IConsoleFormatter formatter, IMenuFactory menuFactory, IConsoleRenderer renderer)
        {
            _bookManager = bookManager;
            _accountManager = accountManager;
            _formatter = formatter;
            _menuFactory = menuFactory;
            _renderer = renderer;
        }
        public void CheckForOverdueBooks()
        {
            var usersWithCheckoutBooks = _accountManager.GetAllUsers().Where(users => users.CheckedOutBooks.Any()).ToList();

            foreach (var user in usersWithCheckoutBooks)
            {
                var overdueBooks = user.CheckedOutBooks.Where(b => b.DueDate < VirtualDate.VirtualToday).ToList();

                user.AddOverdueBooks(overdueBooks);
                _accountManager.UpdateUser(user);
            }
        }

        public void CheckForOverdueReservations()
        {
            var usersWithReservations = _accountManager.GetAllUsers().Where(users => users.ReservedBooks.Any()).ToList();

            foreach (var user in usersWithReservations)
            {
                var overdueReservations = user.ReservedBooks.Where(b => b.ResevationDueDate < VirtualDate.VirtualToday).ToList();
                user.AddOverdueReservations(overdueReservations);
                _accountManager.UpdateUser(user);
            }
        }

        public void SendMessageForOverdueBooks(IUser user)
        {
            if (user.OverdueBooks.Count != 0)
            {
                var strBuilder = new StringBuilder();
                strBuilder.AppendLine("You have overdue books!");

                foreach (var book in user.OverdueBooks)
                {
                    strBuilder.AppendLine(_formatter.Format(book));
                }

                strBuilder.AppendLine("Return the books to be able to use the services of the library!");
            }
        }

        public void SendMessageForOverdueReservations(IUser user)
        {
            if (user.OverdueReservations.Count != 0)
            {
                var strBuilder = new StringBuilder();
                strBuilder.AppendLine("Your reservation for:");

                foreach (var book in user.OverdueReservations)
                {
                    user.RemoveFromOverdueReservations(book);
                    strBuilder.AppendLine(_formatter.Format(book));
                }

                strBuilder.AppendLine("has been expired!");
            }
        }

        private void AssignFee(IUser user, int overdueDays)
        {
            user.LateFees += overdueDays * GlobalConstants.Fee;
        }

        public bool HasOverdueBooks(IUser user)
        {
            if (user.OverdueBooks.Count != 0)
            {
                return true;
            }
            else return false;
        }
    }
}
