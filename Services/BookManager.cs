﻿using Dawn;
using Library.Database.Contracts;
using Library.Models.Contracts;
using Library.Models.Enums;
using Library.Services.Contracts;
using Library.Services.Factory;
using System;
using System.Linq;

namespace Library.Services
{
    public class BookManager : IBookManager
    {
        private readonly IBookDatabase _database;
        private readonly IBookFactory _factory;

        public BookManager(IBookDatabase database, IBookFactory bookfactory, IConsoleRenderer renderer)
        {
            _database = database;
            _factory = bookfactory;
        }

        // Edit Book
        public void UpdateBook(int bookId, string authorName, string title, string isbn, string category, string publisher, int year, int rack)
        {
            var bookToUpdate = _database.Get(bookId);
            Guard.Argument(bookToUpdate, nameof(bookToUpdate)).NotNull(message: "Book to update is null");

            var updated = _factory.CreateBook(bookId, authorName, title, isbn, category, publisher, year, rack);

            _database.Update(updated);
        }

        // CheckOut Book, Reserve Book, Return Book....
        public void UpdateBook(int bookId, BookStatus status, DateTime today, DateTime dueDate)
        {
            var bookToUpdate = _database.Get(bookId);
            Guard.Argument(bookToUpdate, nameof(bookToUpdate)).NotNull(message: "Book to update is null");

            bookToUpdate.Update(status, today, dueDate);
            _database.Update(bookToUpdate);
        }

        public void AddBook(IBook book)
        {
            _database.Create(book);
        }

        public void RemoveBook(IBook book)
        {
            _database.Delete(book);
        }

        public IBook FindBook(int id)
        {
            return _database.Get(id);
        }
        public int GetLastBookID()
        {
            var books = _database.Load();
            return books.Max(b => b.ID);
        }

        public void ListAllBooks()
        {
            var books = _database.Load();

            foreach (var book in books)
            {
                if (book.Status == BookStatus.CheckedOut || book.Status == BookStatus.Reserved)
                {
                    Console.ForegroundColor = ConsoleColor.Red;
                }

                if (book.Status == BookStatus.Available)
                {
                    Console.ForegroundColor = ConsoleColor.Green;
                }

                Console.WriteLine($"\r\nID: {book.ID} || Author: {book.Author} || Title: {book.Title} || Status: {book.Status}");

                Console.ResetColor();
            }
        }
    }
}
