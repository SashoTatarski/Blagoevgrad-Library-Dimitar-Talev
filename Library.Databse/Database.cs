using Library.Models.Contracts;
using System;
using System.Collections.Generic;
using System.Text;

namespace Library.Database
{
    public class Database : IDatabase
    {
        private readonly List<IBook> _books;

        public Database()
        {
            _books = new List<IBook>();
        }

        public List<IBook> Books => new List<IBook>(_books);

        public void AddBooks(IBook bookToAdd) => _books.Add(bookToAdd);

       
    }
}
