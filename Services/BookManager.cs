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

        public void ChangeBookStatus(Book book, BookStatus status) => throw new NotImplementedException();
        public Book CreateBook(string authorName, string title, string isbn, string genres, string publisher, int year, int rack) => throw new NotImplementedException();
        public Book FindBook(int id) => throw new NotImplementedException();
        public List<Book> GetAllBooks() => throw new NotImplementedException();
        public List<int> GetBooksIDs() => throw new NotImplementedException();
        public List<Book> GetSearchResult(string searchByParameter, string searchByText) => throw new NotImplementedException();
        public void ListAllBooks() => throw new NotImplementedException();
        public void RemoveBook(Book book) => throw new NotImplementedException();
        public void UpdateBookAuthor(int bookId, string newAuthorName) => throw new NotImplementedException();
        public void UpdateBookGenre(int bookId, string newGenres) => throw new NotImplementedException();
        public void UpdateBookISBN(int bookId, string newISBN) => throw new NotImplementedException();
        public void UpdateBookPublisher(int bookId, string newPublisherName) => throw new NotImplementedException();
        public void UpdateBookRack(int bookId, int newRack) => throw new NotImplementedException();
        public void UpdateBookTitle(int bookId, string newTitle) => throw new NotImplementedException();
        public void UpdateBookYear(int bookId, int newYear) => throw new NotImplementedException();
    }
}

