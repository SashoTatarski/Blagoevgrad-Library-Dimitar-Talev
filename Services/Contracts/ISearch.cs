using Library.Models.Models;
using System;
using System.Collections.Generic;
using System.Text;

namespace Services.Contracts
{
    public interface ISearch
    {
        void DisplaySearchResults(IEnumerable<Book> searchResults);
        void ListAllBooks();
        void SearchByAuthor();
        void SearchBySubject();
        void SearchByTitle();
        void SearchByYear();
    }
}
