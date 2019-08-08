using Library.Database.Contracts;
using Library.Models.Contracts;
using Library.Models.Enums;
using Library.Models.Models;
using Library.Services.Contracts;
using System;
using System.Collections.Generic;
using System.Linq;

namespace Library.Services
{
    public class DatabaseService : IDatabaseService
    {
        private readonly IDatabase _database;

        public DatabaseService(IDatabase database)
        {
            _database = database;
        }

        //public int CurrentID() => _database.ReadBooks().Count();


        //public void AddBook(IBook book)
        //{
        //    var books = _database.ReadBooks();
        //    books.Add((Book)book);

        //    _database.WriteBooks(books);
        //}

        //public void RemoveBook(IBook book)
        //{
        //    var books = _database.ReadBooks();
        //    books.RemoveAll(x => x.ID == book.ID);

        //    _database.WriteBooks(books);
        //}

        //public List<Book> ReadBooks() => _database.ReadBooks();

        //public void WriteBooks(List<Book> books) => _database.WriteBooks(books);


        //public void AddUser(IUser user)
        //{
        //    var users = _database.ReadUsers();
        //    users.Add((User)user);

        //    _database.WriteUsers(users);
        //}

        //public void RemoveUser(string user)
        //{
        //    var users = _database.ReadUsers();

        //    var userToRemove = users.Find(x => x.Username == user);
        //    userToRemove.Status = MemberStatus.Inactive;

        //    _database.WriteUsers(users);
        //}

        //public void AddLibrarian(ILibrarian librarian)
        //{
        //    var librarians = _database.ReadLibrarians();
        //    librarians.Add((Librarian)librarian);

        //    _database.WriteLibrarians(librarians);
        //}

        //public List<User> ReadUsers() => _database.ReadUsers();
        
        //public void WriteUsers(List<User> users) => _database.WriteUsers(users);

        //

        //public void ListAllBooks()
        //{
        //    
        //}
    }
}
