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


        public IReadOnlyCollection<Book> Search(string searchCriteria)
        {
            return _context.Books
                .Include(b => b.Author)
                .Include(b => b.Publisher)                
                .Include(b => b.BookGenres)
                    .ThenInclude(bg => bg.Genre)                
                .Where(b => b.Title.Contains(searchCriteria) || b.Author.Name.Contains(searchCriteria) || b.Publisher.Name.Contains(searchCriteria) || b.ISBN.Contains(searchCriteria))
                .ToList();
        }

        public async Task DeleteAsync(string isbn)
        {
            var booksToDelete = await _context.Books.Where(book => book.ISBN.ToString() == isbn).ToListAsync();


            foreach (var book in booksToDelete)
            {
                _context.Books.Remove(book);
                await _context.SaveChangesAsync();
            }
        }

        public async Task CreateBookAsync(string title, string isbn, int year, int rack, string authorId, string publisherId, List<int> genresIds, int copies)
        {
            var author =  await _context.Authors.FirstOrDefaultAsync(a => a.Id.ToString() == authorId).ConfigureAwait(false);

            var publisher = await _context.Publishers.FirstOrDefaultAsync(p => p.Id.ToString() == publisherId).ConfigureAwait(false);

            await _bookFac.CreateBook(title, isbn, year, rack, author, publisher, genresIds, copies).ConfigureAwait(false);
         }
        public async Task<List<Author>> GetAllAuthorsAsync() => await _context.Authors.ToListAsync();

        public async Task<Author> CreateAuthorAsync(string authorName) => await _authorFac.CreateAuthor(authorName);

        public async Task<List<Publisher>> GetAllPublishersAsync() => await _context.Publishers.ToListAsync().ConfigureAwait(false);

        public async Task<Publisher> CreatePublisherAsync(string publisherName) => await _publisherFac.CreatePublisher(publisherName);

        public async Task<List<Genre>> GetAllGenresAsync() => await _context.Genres.ToListAsync();

        public async Task<List<Genre>> CreateGenreAsync(string genre) => await _genreFac.CreateGenreList(genre);

        public async Task<Book> GetBook(string id)
        {
           return await _context.Books
                .Include(b => b.Author)                    
                .FirstOrDefaultAsync(book => book.ISBN == id);
        }



        public void ChangeBookStatus(Book book, BookStatus status) => throw new NotImplementedException();

        public Book FindBook(int id) => throw new NotImplementedException();
        public List<Book> GetAllBooks() => throw new NotImplementedException();
      
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

