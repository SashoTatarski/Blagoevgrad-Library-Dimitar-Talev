using Library.Database;
using Library.Models.Enums;
using Library.Models.Models;
using Library.Models.Utils;
using Library.Services.Contracts;
using Library.Services.Factories.Contracts;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;

namespace Library.Services
{
    public class BookManager : IBookManager
    {
        private readonly IBookFactory _bookFac;
        private readonly IAuthorFactory _authorFac;
        private readonly IGenreFactory _genreFac;
        private readonly IPublisherFactory _publisherFac;
        private readonly IConsoleFormatter _formatter;
        private readonly IConsoleRenderer _renderer;
        private readonly LibraryContext _context;
        public BookManager(LibraryContext context, IBookFactory bookFac, IAuthorFactory authorFac, IGenreFactory genreFac, IPublisherFactory publisherFac, IConsoleFormatter formatter, IConsoleRenderer renderer)
        {
            _context = context;
            _bookFac = bookFac;
            _authorFac = authorFac;
            _genreFac = genreFac;
            _publisherFac = publisherFac;
            _formatter = formatter;
            _renderer = renderer;
        }

        public List<Book> GetAllBooks()
        {
            return _context.Books
               .Include(b => b.Author)
               .Include(b => b.Publisher)
               .Include(b => b.BookGenres)
               .ThenInclude(bg => bg.Genre)
               .ToList();
        }

        public Book CreateBook(string authorName, string title, string isbn, string genres, string publisher, int year, int rack)
        {
            var book = _bookFac.CreateBook(authorName, title, isbn, publisher, year, rack);
            _context.Books.Add(book);
            _context.SaveChanges();

            var genreList = _genreFac.CreateGenreList(genres);
            foreach (var genre in genreList)
            {
                _context.BookGenre.Add(new BookGenre { BookId = book.Id, GenreId = genre.Id });
            }
            _context.SaveChanges();

            return book;
        }

        public void ListAllBooks()
        {
            var books = _context.Books
               .Include(b => b.Author)
               .Include(b => b.Publisher)
               .Include(b => b.BookGenres)
               .ThenInclude(bg => bg.Genre)
               .ToList();

            foreach (var book in books)
            {
                if (book.Status == BookStatus.CheckedOut || book.Status == BookStatus.Reserved || book.Status == BookStatus.CheckedOutAndReserved)
                    Console.ForegroundColor = ConsoleColor.Red;

                if (book.Status == BookStatus.Available)
                    Console.ForegroundColor = ConsoleColor.Green;

                _renderer.Output(_formatter.Format(book));
                _renderer.Output(GlobalConstants.NewLine);
                Console.ResetColor();
            }
        }

        public Book FindBook(int id)
        {
            return _context.Books
               .Include(b => b.Author)
               .Include(b => b.Publisher)
               .Include(b => b.BookGenres)
                    .ThenInclude(bg => bg.Genre)
               .FirstOrDefault(b => b.Id == id);
        }

        public void UpdateBookAuthor(int bookId, string newAuthorName)
        {
            var updatedAuthor = _authorFac.CreateAuthor(newAuthorName);

            var book = _context.Books
                .Include(b => b.Author)
                .FirstOrDefault(b => b.Id == bookId);

            book.Author = updatedAuthor;
            _context.SaveChanges();
        }

        public void UpdateBookPublisher(int bookId, string newPublisherName)
        {
            var updatedPublisher = _publisherFac.CreatePublisher(newPublisherName);

            var book = _context.Books
               .Include(b => b.Publisher)
               .FirstOrDefault(b => b.Id == bookId);

            book.Publisher = updatedPublisher;
            _context.SaveChanges();
        }

        public void UpdateBookGenre(int bookId, string newGenres)
        {
            var updatedGenres = _genreFac.CreateGenreList(newGenres);

            var book = _context.Books
                .Include(b => b.BookGenres)
                    .ThenInclude(bg => bg.Genre)
               .FirstOrDefault(b => b.Id == bookId);

            foreach (var bookGenre in _context.BookGenre)
            {
                if (bookGenre.BookId == book.Id)
                {
                    _context.BookGenre.Remove(bookGenre);
                }
            }

            foreach (var genre in updatedGenres)
            {
                _context.BookGenre.Add(new BookGenre { BookId = book.Id, GenreId = genre.Id });
            }
            _context.SaveChanges();
        }

        public void UpdateBookTitle(int bookId, string newTitle)
        {
            var book = _context.Books.FirstOrDefault(b => b.Id == bookId);

            book.Title = newTitle;
            _context.SaveChanges();
        }

        public void UpdateBookISBN(int bookId, string newISBN)
        {
            var book = _context.Books.FirstOrDefault(b => b.Id == bookId);

            book.ISBN = newISBN;
            _context.SaveChanges();
        }

        public void UpdateBookYear(int bookId, int newYear)
        {
            var book = _context.Books.FirstOrDefault(b => b.Id == bookId);

            book.Year = newYear;
            _context.SaveChanges();
        }

        public void UpdateBookRack(int bookId, int newRack)
        {
            var book = _context.Books.FirstOrDefault(b => b.Id == bookId);

            book.Rack = newRack;
            _context.SaveChanges();
        }

        public void ChangeBookStatus(Book book, BookStatus status)
        {
            switch (status)
            {
                case BookStatus.Available:
                    book.Status = status;
                    _context.SaveChanges();
                    break;
                case BookStatus.CheckedOut:
                    if (book.Status == status || book.Status == BookStatus.CheckedOutAndReserved)
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
                        _context.SaveChanges();
                    }
                    break;
                case BookStatus.Reserved:
                    if (book.Status == BookStatus.CheckedOut)
                    {
                        book.Status = BookStatus.CheckedOutAndReserved;
                        _context.SaveChanges();
                    }
                    else if (book.Status == BookStatus.Reserved || book.Status == BookStatus.CheckedOutAndReserved)
                    {
                        throw new ArgumentException(GlobalConstants.ReservedBookAlreadyReservedOther);
                    }
                    else
                    {
                        book.Status = status;
                        _context.SaveChanges();
                    }
                    break;
                default:
                    break;
            }
        }

        public List<int> GetBooksIDs()
        {
            var books = _context.Books.ToList();
            var list = new List<int>();
            foreach (var book in books)
            {
                list.Add(book.Id);
            }
            return list;
        }

        public void RemoveBook(Book book)
        {
            var bookToRemove = _context.Books.FirstOrDefault(b => b.Id == book.Id);

            _context.Books.Remove(bookToRemove);
            _context.SaveChanges();
        }

        public List<Book> GetSearchResult(string searchByParameter, string searchByText)
        {
            var books = _context.Books
               .Include(b => b.Author)
               .Include(b => b.Publisher)
               .Include(b => b.BookGenres)
               .ThenInclude(bg => bg.Genre)
               .ToList();
            var authors = _context.Authors;
            var genres = _context.Genres;
            var publishers = _context.Publishers;
            var bookgenres = _context.BookGenre;
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

