using Dawn;
using Library.Database;
using Library.Database.Contracts;
using Library.Models.Contracts;
using Library.Models.Enums;
using Library.Models.Models;
using Library.Models.Utils;
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
        private readonly IDataBase<Book> _database;
        private readonly IBookFactory _factory;
        private readonly IConsoleFormatter _formatter;
        private readonly LibraryContext _context;
        private readonly IConsoleRenderer _renderer;


        public BookManager(IDataBase<Book> database, IBookFactory bookfactory, IConsoleFormatter formatter, LibraryContext context, IConsoleRenderer renderer)
        {
            _database = database;
            _factory = bookfactory;
            _formatter = formatter;
            _context = context;
            _renderer = renderer;

        }

        public Book CreateBook(string authorName, string title, string isbn, string genres, string publisher, int year, int rack)
        {
           
            {
                _context.Authors.Add(new Author { Name = authorName });
            }

            var book = _factory.CreateBook(authorName, title, isbn, genres, publisher, year, rack);

            _database.Create(book);

            return book;
        }

        public void ListAllBooks()
        {
            var books = _database.Read();

            //foreach (var book in books)
            //{
            //    if (book.Status == BookStatus.CheckedOut || book.Status == BookStatus.Reserved || book.Status == BookStatus.CheckedOut_and_Reserved)
            //        Console.ForegroundColor = ConsoleColor.Red;

            //    if (book.Status == BookStatus.Available)
            //        Console.ForegroundColor = ConsoleColor.Green;

            //    _renderer.Output(_formatter.Format(book));
            //    _renderer.Output(_formatter.Format(book));

            //   // Console.WriteLine($"\r\nID: {book.Id} || Author: {book.Author} || Title: {book.Title} || Status: {book.Status}");

            //    Console.ResetColor();
            //}
        }
        // ------- Need update ↓ -------
        public void UpdateStatus(IBook book, BookStatus status)
        {
            var bookToUpdate = _context.Books.FirstOrDefault(b => b.Id == book.Id);
            bookToUpdate.Status = status;
            _context.SaveChanges();
        }

        // Edit Book
        public void UpdateBook(int bookId, string authorName, string title, string isbn, string category, string publisher, int year, int rack)
        {
            //var bookToUpdate = _database.Find(bookId);
            //Guard.Argument(bookToUpdate, nameof(bookToUpdate)).NotNull(message: GlobalConstants.BookToUpdateNull);

            //var updated = _factory.CreateBook(authorName, title, isbn, category, publisher, year, rack);

            //bookToUpdate.Update(updated);
            //_context.SaveChanges();
        }

        // CheckOut Book
        // OOP: Polymorphism - method overloading static polymorphism. In static polym. identification of the overloaded method to be executed is carried out at compile time
        public void UpdateBook(int bookId, BookStatus status, DateTime checkoutDate, DateTime dueDate)
        {
            var bookToUpdate = _database.Find(bookId);
            Guard.Argument(bookToUpdate, nameof(bookToUpdate)).NotNull(message: GlobalConstants.BookToUpdateNull);

            bookToUpdate.Update(status, checkoutDate, dueDate);
            _database.Update(bookToUpdate);
        }

        // Reserve Book
        public void UpdateBook(int bookId, BookStatus status, DateTime reservationDate, DateTime reservationDueDate, bool isReservation)
        {
            var bookToUpdate = _database.Find(bookId);
            Guard.Argument(bookToUpdate, nameof(bookToUpdate)).NotNull(message: GlobalConstants.BookToUpdateNull);

            bookToUpdate.Update(status, reservationDate, reservationDueDate, true);
            _database.Update(bookToUpdate);
        }


        public void RemoveBook(Book book) => _database.Delete(book);

        public Book FindBook(int id) => _database.Find(id);

        public List<Book> GetAllBooks()
        {
            //return _database.Read();
            return null;
        }

        public int GetLastBookID()
        {
            var books = _database.Read();
            return books.Max(b => b.Id);
        }



        public List<Book> GetSearchResult(string searchByParameter, string searchByText)
        {
            var books = _database.Read();
            var sortedBooks = new List<Book>();

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
            return sortedBooks;
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

