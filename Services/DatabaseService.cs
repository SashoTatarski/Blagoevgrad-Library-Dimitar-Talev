using Library.Database;
using Library.Models.Contracts;
using Library.Models.Enums;
using Library.Models.Models;
using Library.Services.Contracts;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Library.Services
{
    public class DatabaseService : IDatabaseService
    {
        private readonly IDatabase _database;

        public DatabaseService(IDatabase database)
        {
            _database = database;
        }

        public int CurrentID() => _database.ReadBooks().Count();


        public void AddBook(IBook book)
        {
            var books = _database.ReadBooks();
            books.Add((Book)book);

            _database.WriteBooks(books);
        }

        public void RemoveBook(IBook book)
        {
            var books = _database.ReadBooks();
            books.RemoveAll(x => x.ID == book.ID);

            _database.WriteBooks(books);
        }

        public List<Book> ReadBooks() => _database.ReadBooks();

        public void WriteBooks(List<Book> books) => _database.WriteBooks(books);


        public void AddUser(IUser user)
        {
            var users = _database.ReadUsers();
            users.Add((User)user);

            _database.WriteUsers(users);
        }

        public void RemoveUser(string user)
        {
            var users = _database.ReadUsers();

            var userToRemove = users.Find(x => x.Username == user);
            userToRemove.Status = MemberStatus.Inactive;

            _database.WriteUsers(users);
        }

        public void AddLibrarian(ILibrarian librarian)
        {
            var librarians = _database.ReadLibrarians();
            librarians.Add((Librarian)librarian);

            _database.WriteLibrarians(librarians);
        }

        public List<User> ReadUsers() => _database.ReadUsers();
        
        public void WriteUsers(List<User> users) => _database.WriteUsers(users);

        public IAccount FindAccount(string userName)
        {
            var user = _database.ReadUsers().FirstOrDefault(u => u.Username == userName);
            var librarian = _database.ReadLibrarians().FirstOrDefault(l => l.Username == userName);

            if (user is null)
            {
                if (librarian is null)
                    return null;
                else
                    return librarian;
            }
            else
                return user;
        }

        public void ListAllBooks()
        {
            var allbooks = _database.ReadBooks();
            foreach (var book in allbooks)
            {
                if (book.Status == BookStatus.Checkedout || book.Status == BookStatus.Reserved)
                    Console.ForegroundColor = ConsoleColor.Red;

                if (book.Status == BookStatus.Available)
                    Console.ForegroundColor = ConsoleColor.Green;

                Console.WriteLine($"\r\nID: {book.ID} || Author: {book.Author} || Title: {book.Title} Status: {book.Status}");

                Console.ResetColor();
            }
        }
    }
}
