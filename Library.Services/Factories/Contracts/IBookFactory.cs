using Library.Models.Models;

namespace Library.Services.Factories.Contracts
{
    public interface IBookFactory
    {
        Book CreateBook(string author, string title, string isbn, string publisher, int year, int rack);
    }
}
