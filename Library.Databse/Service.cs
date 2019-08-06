using Library.Models.Contracts;
using Library.Models.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Library.Database
{
    public class Service : IService
    {
        private readonly List<Book> _books;
        private readonly List<User> _users;
        private readonly IDatabase _database;

        public Service(IDatabase database)
        {
            _database = database;            
        }

        public int CurrentID()
        {
            return _database.ReadBooks().Count();
        }
       

        public void AddBook(IBook book)
        {
            var books = _database.ReadBooks();
            books.Add((Book)book);

            _database.WriteBooks(books);
        }

        public List<Book> ReadBooks()
        {
            return _database.ReadBooks();            
        }

        public void AddUser(IUser user)
        {
            var users = _database.ReadUsers();
            users.Add((User)user);

            _database.WriteUsers(users);
        }

        public IAccount FindAccount(string userName)
        {
            return _database.ReadUsers().FirstOrDefault(u => u.Username == userName);
        }
    }
}
