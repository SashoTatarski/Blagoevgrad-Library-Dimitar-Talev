using Library.Models.Contracts;
using Library.Models.Utils;
using Library.Services.Contracts;
using System.Linq;
using System.Text;

namespace Library.Services
{
    public class LibrarySystem : ILibrarySystem
    {
        private readonly IBookManager _bookManager;
        private readonly IAccountManager _accountManager;
        public LibrarySystem(IBookManager bookManager, IAccountManager accountManager, IConsoleFormatter formatter)
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

        }

        public void SendMessageForOverdueReservations(IUser user)
        {
            if (user.OverdueReservations.Count != 0)
            {
                var strBuilder = new StringBuilder();

                foreach (var book in user.OverdueReservations)
                {
                    strBuilder.AppendLine()
                }
            }
        }

        private void AssignFee(IUser user, int overdueDays)
        {
            user.LateFees += overdueDays * GlobalConstants.Fee;
        }


    }
}
