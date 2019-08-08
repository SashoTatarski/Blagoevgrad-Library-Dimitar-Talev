namespace Library.Models.Contracts
{
    public interface ILibrarian : IAccount {
        void UpdateBook(IBook book);
    }
}
