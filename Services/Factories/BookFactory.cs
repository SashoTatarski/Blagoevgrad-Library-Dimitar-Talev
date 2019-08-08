using Library.Models.Contracts;
using Library.Models.Models;

namespace Library.Services.Factory
{
    public class BookFactory : IBookFactory
    {
        public IBook CreateBook(int currentId, string author, string title, string isbn, string subject, string publisher, int year, int rack) => new Book(currentId, author, title, isbn, subject, publisher, year, rack);
    }
}
