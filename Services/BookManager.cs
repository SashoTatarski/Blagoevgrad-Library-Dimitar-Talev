using Dawn;
using Library.Database;
using Library.Database.Contracts;
using Library.Models.Contracts;
using Library.Models.Enums;
using Library.Models.Models;
using Library.Services.Contracts;
using Library.Services.Factory;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Library.Services
{
    public class BookManager : IBookManager
    {
        private readonly IBookDatabase _database;
        private readonly IBookFactory _factory;
        private readonly IConsoleFormatter _formatter;
        private readonly LibraryContext _context;
        

        public BookManager(IBookDatabase database, IBookFactory bookfactory, IConsoleFormatter formatter, LibraryContext context)
        {
            _database = database;
            _factory = bookfactory;
            _formatter = formatter;
            _context = context;
        }

        public void UpdateStatus(IBook book, BookStatus status)
        {
            var bookToUpdate = _context.Books.FirstOrDefault(b => b.Id == book.Id);
            bookToUpdate.Status = status;
            _context.SaveChanges();
        }

        // Edit Book
        public void UpdateBook(int bookId, string authorName, string title, string isbn, string category, string publisher, int year, int rack)
        {
            var bookToUpdate = _database.GetOneBook(bookId);
            Guard.Argument(bookToUpdate, nameof(bookToUpdate)).NotNull(message: "Book to update is null");

            var updated = _factory.CreateBook(authorName, title, isbn, category, publisher, year, rack);

            bookToUpdate.Update(updated);
            _database.Update(bookToUpdate);
        }

        // CheckOut Book
        // OOP: Polymorphism - method overloading static polymorphism. In static polym. identification of the overloaded method to be executed is carried out at compile time
        public void UpdateBook(int bookId, BookStatus status, DateTime checkoutDate, DateTime dueDate)
        {
            var bookToUpdate = _database.GetOneBook(bookId);
            Guard.Argument(bookToUpdate, nameof(bookToUpdate)).NotNull(message: "Book to update is null");

            bookToUpdate.Update(status, checkoutDate, dueDate);
            _database.Update(bookToUpdate);
        }

        // Reserve Book
        public void UpdateBook(int bookId, BookStatus status, DateTime reservationDate, DateTime reservationDueDate, bool isReservation)
        {
            var bookToUpdate = _database.GetOneBook(bookId);
            Guard.Argument(bookToUpdate, nameof(bookToUpdate)).NotNull(message: "Book to update is null");

            bookToUpdate.Update(status, reservationDate, reservationDueDate, true);
            _database.Update(bookToUpdate);
        }

        public void AddBook(IBook book) => _database.Create(book);

        public void RemoveBook(IBook book) => _database.Delete(book);

        public IBook FindBook(int id) => _database.GetOneBook(id);

        public List<Book> GetAllBooks() => _database.Load();

        public int GetLastBookID()
        {
            var books = _database.Load();
            return books.Max(b => b.Id);
        }

        public void ListAllBooks()
        {
            var books = _database.Load();

            foreach (var book in books)
            {
                if (book.Status == BookStatus.CheckedOut || book.Status == BookStatus.Reserved)
                    Console.ForegroundColor = ConsoleColor.Red;

                if (book.Status == BookStatus.Available)
                    Console.ForegroundColor = ConsoleColor.Green;

                Console.WriteLine($"\r\nID: {book.Id} || Author: {book.Author} || Title: {book.Title} || Status: {book.Status}");

                Console.ResetColor();
            }
        }

        public List<IBook> GetSearchResult(string searchByParameter, string searchByText)
        {
            //var books = _database.Load();
            //var sortedBooks = new List<IBook>();

            //switch (searchByParameter.ToLower())
            //{
            //    case "author":
            //        sortedBooks = books.Where(b => b.Author.Contains(searchByText)).ToList();
            //        break;
            //    case "title":
            //        sortedBooks = books.Where(b => b.Title.Contains(searchByText)).ToList();
            //        break;
            //    case "genre":
            //        sortedBooks = books.Where(b => b.Genre.Contains(searchByText)).ToList();
            //        break;
            //    case "year":
            //        sortedBooks = books.Where(b => b.Genre.Contains(searchByText)).ToList();
            //        break;
            //    case "publisher":
            //        sortedBooks = books.Where(b => b.Genre.Contains(searchByText)).ToList();
            //        break;
            //    case "show all":
            //        return books;
            //    default:
            //        break;
            //}
            return null; //sortedBooks;
        }

        public string GetCheckedoutBooksInfo(IUser user)
        {
            var strBuilder = new StringBuilder();

            //if (user.CheckedOutBooks.Count == 0)
            //{
            //    throw new ArgumentException("There are no checked out books!");
            //}
            //else
            //{
            //    strBuilder.AppendLine("Books you have checked out:");

            //    foreach (var book in user.CheckedOutBooks)
            //        strBuilder.AppendLine(_formatter.FormatCheckedoutBook(book));
            //}
            return strBuilder.ToString();
        }

        public string GetOverdueBooksInfo(IUser user)
        {
            var strBuilder = new StringBuilder();

            //foreach (var book in user.OverdueBooks)
            //    strBuilder.AppendLine(_formatter.FormatCheckedoutBook(book));

            return strBuilder.ToString();
        }
    }
}
