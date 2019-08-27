using Library.Models.Models;
using Library.Services.Factories.Contracts;
using System.Collections.Generic;

namespace Library.Services.Factory
{
    public class BookFactory : IBookFactory
    {
        public Book CreateBook(Author author, string title, string isbn, List<Genre> genres, Publisher publisher, int year, int rack)
        {
            return new Book(author, title, isbn, genres, publisher, year, rack);
        }
    }
}
