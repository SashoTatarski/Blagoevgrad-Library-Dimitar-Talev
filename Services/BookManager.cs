using Dawn;
using Library.Database;
using Library.Database.Contracts;
using Library.Models.Contracts;
using Library.Models.Enums;
using Library.Models.Models;
using Library.Models.Utils;
using Library.Services.Contracts;
using Library.Services.Factories.Contracts;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Library.Services
{
    public class BookManager : IBookManager
    {
        private readonly IDataBase<Book> _bookDB;
        private readonly IDataBase<Author> _authorDb;
        private readonly IDataBase<Genre> _genreDb;
        private readonly IDataBase<Publisher> _publisherDb;
        private readonly IDataBase<BookGenre> _bookGenreDb;
        private readonly IBookFactory _bookFac;
        private readonly IAuthorFactory _authorFac;
        private readonly IGenreFactory _genreFac;
        private readonly IConsoleFormatter _formatter;
        private readonly LibraryContext _context;
        private readonly IConsoleRenderer _renderer;


        public BookManager(IDataBase<Book> bookDB, IDataBase<Author> authorDB,
            IDataBase<Genre> genreDb, IDataBase<Publisher> publisherDb, IDataBase<BookGenre> bookGenreDb, IBookFactory bookFac, IAuthorFactory authorFac, IGenreFactory genreFac, IConsoleFormatter formatter, LibraryContext context, IConsoleRenderer renderer)
        {
            _bookDB = bookDB;
            _authorDb = authorDB;
            _genreDb = genreDb;
            _publisherDb = publisherDb;
            _bookGenreDb = bookGenreDb;
            _bookFac = bookFac;
            _authorFac = authorFac;
            _genreFac = genreFac;
            _formatter = formatter;
            _context = context;
            _renderer = renderer;
        }

        public Book CreateBook(string authorName, string title, string isbn, string genres, string publisher, int year, int rack)
        {
            var book = _bookFac.CreateBook(authorName, title, isbn, publisher, year, rack);
            _bookDB.Create(book);

            var genreList = _genreFac.CreateGenreList(genres);
            this.MapBookToGenres(book, genreList);
            return book;
        }

        private void MapBookToGenres(Book book, List<Genre> genres)
        {
            foreach (var genre in genres)
            {
                _context.BookGenre.Add(new BookGenre { BookId = book.Id, GenreId = genre.Id });
            }
            _context.SaveChanges();
        }

        public void ListAllBooks()
        {
            var books = _bookDB.Read();

            foreach (var book in books)
            {
                if (book.Status == BookStatus.CheckedOut || book.Status == BookStatus.Reserved || book.Status == BookStatus.CheckedOut_and_Reserved)
                    Console.ForegroundColor = ConsoleColor.Red;

                if (book.Status == BookStatus.Available)
                    Console.ForegroundColor = ConsoleColor.Green;

                _renderer.Output(_formatter.Format(book));
                _renderer.Output("");
                Console.ResetColor();
            }
        }

        public Book FindBook(int id) => _bookDB.Find(id);
        // ------- Need update ↓ -------
        public void UpdateStatus(Book book, BookStatus status)
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
            var bookToUpdate = _bookDB.Find(bookId);
            Guard.Argument(bookToUpdate, nameof(bookToUpdate)).NotNull(message: GlobalConstants.BookToUpdateNull);

            bookToUpdate.Update(status, checkoutDate, dueDate);
            _bookDB.Update(bookToUpdate);
        }

        // Reserve Book
        public void UpdateBook(int bookId, BookStatus status, DateTime reservationDate, DateTime reservationDueDate, bool isReservation)
        {
            var bookToUpdate = _bookDB.Find(bookId);
            Guard.Argument(bookToUpdate, nameof(bookToUpdate)).NotNull(message: GlobalConstants.BookToUpdateNull);

            bookToUpdate.Update(status, reservationDate, reservationDueDate, true);
            _bookDB.Update(bookToUpdate);
        }


        public void RemoveBook(Book book) => _bookDB.Delete(book);



        public List<Book> GetAllBooks()
        {
            //return _database.Read();
            return null;
        }

        public int GetLastBookID()
        {
            var books = _bookDB.Read();
            return books.Max(b => b.Id);
        }



        public List<Book> GetSearchResult(string searchByParameter, string searchByText)
        {
            var books = _bookDB.Read();
            var authors = _authorDb.Read();
            var genres = _genreDb.Read();
            var publishers = _publisherDb.Read();
            var bookgenres = _bookGenreDb.Read();
            var sortedBooks = new List<Book>();

            switch (searchByParameter.ToLower())
            {
                case "author":
                    var authourId = authors.Where(a => a.Name == searchByText)
                                            .Select(a => a.Id)
                                            .FirstOrDefault();
                    sortedBooks = books.Where(b => b.AuthorId == authourId).ToList(); break;

                case "title":
                    sortedBooks = books.Where(b => b.Title.Contains(searchByText)).ToList();
                    break;
                case "genre":

                    var genreId = genres.Where(a => a.GenreName == searchByText)
                                            .Select(a => a.Id)
                                            .FirstOrDefault();
                    var bookGenreIds = bookgenres.Where(g => g.GenreId == genreId)
                                                .Select(g => g.BookId);

                    sortedBooks.AddRange(from bookGenre in bookGenreIds
                                         from book in books
                                         where bookGenre == book.Id
                                         select book);
                    // Same as the one above
                    //foreach (var bookGenre in bookGenreIds)
                    //    foreach (var book in books)
                    //        if (bookGenre == book.Id)
                    //            sortedBooks.Add(book);
                    break;
                case "year":
                    sortedBooks = books.Where(b => b.Year == int.Parse(searchByText)).ToList();
                    break;
                case "publisher":
                    var publisherId = publishers.Where(a => a.Name.Contains(searchByText))
                                                .Select(a => a.Id)
                                                .FirstOrDefault();
                    sortedBooks = books.Where(b => b.PublisherId == publisherId).ToList();
                    break;
                case "show all":
                    return books;
                default:
                    break;
            }
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

