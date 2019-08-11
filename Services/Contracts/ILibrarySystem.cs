using Library.Models.Contracts;

namespace Library.Services.Contracts
{
    public interface ILibrarySystem
    {
        void AssignFee(IUser user);
        void CheckForOverdueBooks();
        void CheckForOverdueReservations();
        void CheckIfMaxQuotaReached(System.Collections.Generic.List<IBook> books);
        string DisplayCheckedoutBooks(IUser user);
        string DisplayOverdueBooks(IUser user);
        string GetMessageForOverdueBooks(IUser user);
        string GetMessageForOverdueReservations(IUser user);
        bool HasOverdueBooks(IUser user);
        bool HasOverdueReservations(IUser user);
    }
}
