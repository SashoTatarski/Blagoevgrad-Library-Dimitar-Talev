using Library.Models.Contracts;
using Library.Models.Models;
using System;
using System.Collections.Generic;
using System.Text;

namespace Library.Database
{
    public class Database : IDatabase
    {
        private readonly List<Book> _books;
        private readonly List<User> _users;
        private readonly IJson _json;

        public Database(IJson json)
        {
            _json = json;
            _books = _json.ReadBooks();
            _users = _json.ReadUsers();
        }

        public IEnumerable<IBook> Books => new List<IBook>(_books);

        public void AddBookToList(IBook book)
        {
            _books.Add((Book)book);
        }

        public void WriteBooksToJson(IEnumerable<IBook> books)
        {
            _json.WriteBooks(books);
        }

        public void AddUserToList(IUser user)
        {
            _users.Add((User)user);
        }

        public void WriteUsersToJson(IEnumerable<IUser> users)
        {
            _json.WriteUsers(users);
        }
    }
}
