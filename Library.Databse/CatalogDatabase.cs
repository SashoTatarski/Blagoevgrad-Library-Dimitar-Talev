using Library.Models.Contracts;
using Library.Models.Models;
using System;
using System.Collections.Generic;
using System.Text;

namespace Library.Database
{
    public class CatalogDatabase : IDatabase
    {
        private readonly List<Book> _books;
        private readonly IJson _json;

        public CatalogDatabase(IJson json)
        {
            _json = json;
            _books = _json.ReadBooks();
        }


        public IEnumerable<IBook> Books => new List<IBook>(_books);

        public void AddBookToList(IBook book)
        {
            _books.Add((Book)book);
        }

        public void WriteToJson(IEnumerable<IBook> books)
        {
            _json.WriteBooks(books);
        }
    }
}
