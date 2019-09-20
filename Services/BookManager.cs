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
using System.Threading.Tasks;

namespace Library.Services
{
    public class BookManager : IBookManager
    {
        private readonly IBookFactory _bookFac;
        private readonly IAuthorFactory _authorFac;
        private readonly IGenreFactory _genreFac;
        private readonly IPublisherFactory _publisherFac;
        private readonly LibraryContext _context;
        public BookManager(LibraryContext context, IBookFactory bookFac, IAuthorFactory authorFac, IGenreFactory genreFac, IPublisherFactory publisherFac)
        {
            _context = context;
            _bookFac = bookFac;
            _authorFac = authorFac;
            _genreFac = genreFac;
            _publisherFac = publisherFac;
        }

        public async Task CreateBookAsync(string title, string isbn, int year, int rack, string authorId, string publisherId, List<int> genresIds, int copies)
        {
            var author =  await _context.Authors.FirstOrDefaultAsync(a => a.Id.ToString() == authorId).ConfigureAwait(false);

            var publisher = await _context.Publishers.FirstOrDefaultAsync(p => p.Id.ToString() == publisherId).ConfigureAwait(false);

            await _bookFac.CreateBook(title, isbn, year, rack, author, publisher, genresIds, copies).ConfigureAwait(false);
         }
        public async Task<List<Author>> GetAllAuthors() => await _context.Authors.ToListAsync();

        public async Task<Author> CreateAuthorAsync(string authorName) => await _authorFac.CreateAuthor(authorName);

        public async Task<List<Publisher>> GetAllPublishers() => await _context.Publishers.ToListAsync().ConfigureAwait(false);

        public async Task<Publisher> CreatePublisher(string publisherName) => await _publisherFac.CreatePublisher(publisherName);

        public async Task<List<Genre>> GetAllGenres() => await _context.Genres.ToListAsync();

        public async Task<List<Genre>> CreateGenre(string genre) => await _genreFac.CreateGenreList(genre);




        public void ChangeBookStatus(Book book, BookStatus status) => throw new NotImplementedException();

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

