using Library.Models.Contracts;
using Library.Models.Models;

namespace Library.Core.Factory
{
    public class BookFactory : IBookFactory
    {
        public IBook CreateBook(string author, string title, string isbn, string subject, string publisher, int year, int rack) => new Book(author, title, isbn, subject, publisher, year, rack);
    }
}
