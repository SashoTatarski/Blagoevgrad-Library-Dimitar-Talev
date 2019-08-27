using Library.Models.Models;
using System.Collections.Generic;

namespace Library.Services.Factories.Contracts
{
    public interface IBookFactory
    {
        Book CreateBook(Author author, string title, string isbn, List<Genre> genres, Publisher publisher, int year, int rack);
    }
}
