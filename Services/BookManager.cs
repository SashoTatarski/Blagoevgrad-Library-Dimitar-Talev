using Library.Database;
using Library.Database.Contracts;
using Library.Models.Enums;
using Library.Models.Models;
using Library.Models.Utils;
using Library.Services.Contracts;
using Library.Services.Factories.Contracts;
using System;
using System.Collections.Generic;
using System.Linq;

namespace Library.Services
{
    public class BookManager : IBookManager
    {
        private readonly IDatabase<Book> _bookDB;
        private readonly IDatabase<Author> _authorDb;
        private readonly IDatabase<Genre> _genreDb;
        private readonly IDatabase<Publisher> _publisherDb;
        private readonly BookGenreDataBase _bookGenreDb;
        private readonly IBookFactory _bookFac;
        private readonly IAuthorFactory _authorFac;
        private readonly IGenreFactory _genreFac;
        private readonly IPublisherFactory _publisherFac;
        private readonly IConsoleFormatter _formatter;
        private readonly IConsoleRenderer _renderer;


        public BookManager(IDatabase<Book> bookDB, IDatabase<Author> authorDB, IDatabase<Genre> genreDb, IDatabase<Publisher> publisherDb, BookGenreDataBase bookGenreDb, IBookFactory bookFac, IAuthorFactory authorFac, IGenreFactory genreFac, IPublisherFactory publisherFac, IConsoleFormatter formatter, IConsoleRenderer renderer)
        {
            _bookDB = bookDB;
            _authorDb = authorDB;
            _genreDb = genreDb;
            _publisherDb = publisherDb;
            _bookGenreDb = bookGenreDb;
            _bookFac = bookFac;
            _authorFac = authorFac;
            _genreFac = genreFac;
            _publisherFac = publisherFac;
            _formatter = formatter;
            _renderer = renderer;
        }

        public Book CreateBook(string authorName, string title, string isbn, string genres, string publisher, int year, int rack)
        {
            var book = _bookFac.CreateBook(authorName, title, isbn, publisher, year, rack);
            _bookDB.Create(book);

            var genreList = _genreFac.CreateGenreList(genres);
            _bookGenreDb.Create(book, genreList);
            return book;
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
                _renderer.Output(GlobalConstants.NewLine);
                Console.ResetColor();
            }
        }

        public Book FindBook(int id) => _bookDB.Find(id);

        public void UpdateBookAuthor(int bookId, string newAuthorName)
        {
            var updatedAuthor = _authorFac.CreateAuthor(newAuthorName);

            var book = _bookDB.Find(bookId);

            book.Author = updatedAuthor;
            _bookDB.Update();
        }

        public void UpdateBookPublisher(int bookId, string newPublisherName)
        {
            var updatedPublisher = _publisherFac.CreatePublisher(newPublisherName);

            var book = _bookDB.Find(bookId);

            book.Publisher = updatedPublisher;
            _bookDB.Update();
        }

        public void UpdateBookGenre(int bookId, string newGenres)
        {
            var updatedGenres = _genreFac.CreateGenreList(newGenres);

            var book = _bookDB.Find(bookId);

            _bookGenreDb.Update(book, updatedGenres);
            _bookDB.Update();
        }

        public void UpdateBookTitle(int bookId, string newTitle)
        {
            var book = _bookDB.Find(bookId);

            book.Title = newTitle;
            _bookDB.Update();
        }

        public void UpdateBookISBN(int bookId, string newISBN)
        {
            var book = _bookDB.Find(bookId);

            book.ISBN = newISBN;
            _bookDB.Update();
        }

        public void UpdateBookYear(int bookId, int newYear)
        {
            var book = _bookDB.Find(bookId);

            book.Year = newYear;
            _bookDB.Update();
        }

        public void UpdateBookRack(int bookId, int newRack)
        {
            var book = _bookDB.Find(bookId);

            book.Rack = newRack;
            _bookDB.Update();
        }

        public void ChangeBookStatus(Book book, BookStatus status)
        {
            switch (status)
            {
                case BookStatus.Available:
                    book.Status = status;
                    _bookDB.Update();
                    break;
                case BookStatus.CheckedOut:
                    if (book.Status == status || book.Status == BookStatus.CheckedOut_and_Reserved)
                    {
                        throw new ArgumentException(GlobalConstants.CheckoutBookAlreadyChecked);
                    }
                    else if (book.Status == BookStatus.Reserved)
                    {
                        throw new ArgumentException(GlobalConstants.ReservedBookAlreadyReservedOther);
                    }
                    else
                    {
                        book.Status = status;
                        _bookDB.Update();
                    }
                    break;
                case BookStatus.Reserved:
                    if (book.Status == BookStatus.CheckedOut)
                    {
                        book.Status = BookStatus.CheckedOut_and_Reserved;
                        _bookDB.Update();
                    }
                    else if (book.Status == BookStatus.Reserved || book.Status == BookStatus.CheckedOut_and_Reserved)
                    {
                        throw new ArgumentException(GlobalConstants.ReservedBookAlreadyReservedOther);
                    }
                    else
                    {
                        book.Status = status;
                        _bookDB.Update();
                    }
                    break;
                default:
                    break;
            }
        }

        public List<int> GetBooksIDs()
        {
            var books = _bookDB.Read();
            var list = new List<int>();
            foreach (var book in books)
            {
                list.Add(book.Id);
            }
            return list;
        }

        public void RemoveBook(Book book) => _bookDB.Delete(book);

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
                    var authourId = authors.Where(a => a.Name.Contains(searchByText))
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
    }
}

