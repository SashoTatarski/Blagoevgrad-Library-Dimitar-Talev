﻿using Library.Database;
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

        public async Task<List<Book>> GetAllBooks() => await
            _context.Books
            .Include(b => b.Author)
                .Include(b => b.Publisher)
                .Include(b => b.BookGenres)
                    .ThenInclude(bg => bg.Genre)
            .ToListAsync().ConfigureAwait(false);

        public async Task EditBookAsync(string bookId, string title, string isbn, int year, int rack, string authorId, string publisherId, List<int> genresIds)
        {
            var book = await _context.Books
                .Include(a => a.Author)
                .Where(b => b.Id.ToString() == bookId)
                .FirstOrDefaultAsync().ConfigureAwait(false);

            if (book.Title != title)
                book.Title = title;

            if (book.ISBN != isbn)
                book.ISBN = isbn;

            if (book.Year != year)
                book.Year = year;

            if (book.Rack != rack)
                book.Rack = rack;


            var newAuthor = await _context.Authors.FirstOrDefaultAsync(a => a.Id.ToString() == authorId).ConfigureAwait(false);
            book.AuthorId = newAuthor.Id;

            var newPublisher = await _context.Publishers.FirstOrDefaultAsync(p => p.Id.ToString() == publisherId).ConfigureAwait(false);
            book.PublisherId = newPublisher.Id;

            var genres = await _context.Genres.ToListAsync().ConfigureAwait(false);

            // Clean all genres for the book
            foreach (var bookGenre in _context.BookGenre)
            {
                if (bookGenre.BookId == book.Id)
                    _context.BookGenre.Remove(bookGenre);
            }
            await _context.SaveChangesAsync().ConfigureAwait(false);

            // Add the genres to the clean table
            foreach (var genre in genres)
            {
                foreach (var genreParam in genresIds)
                {
                    if (genre.Id == genreParam)                    
                    {
                         _context.BookGenre.Add(new BookGenre
                        {
                            BookId = book.Id,
                            GenreId = genreParam
                        });
                    }
                }
            }

            await _context.SaveChangesAsync().ConfigureAwait(false);
        }

        public async Task<IReadOnlyCollection<Book>> SearchAsync(string searchCriteria)
        {
            return await _context.Books
                .Include(b => b.Author)
                .Include(b => b.Publisher)
                .Include(b => b.BookGenres)
                    .ThenInclude(bg => bg.Genre)
                .Where(b => b.Title.Contains(searchCriteria) || b.Author.Name.Contains(searchCriteria) || b.Publisher.Name.Contains(searchCriteria) || b.ISBN.Contains(searchCriteria))
                .ToListAsync();
        }

        public async Task<List<Book>> GetBooksByAuthorAsync(string authorId)
        {
            return await _context.Books
                .Include(b => b.Author)
                .Include(b => b.Publisher)
                .Include(b => b.BookGenres)
                    .ThenInclude(bg => bg.Genre)
                .Where(b => b.Author.Id.ToString() == authorId)
                .ToListAsync()
                .ConfigureAwait(false);
        }

        public async Task DeleteBookAsync(string isbn)
        {
            var booksToDelete = await _context.Books.Where(book => book.ISBN == isbn).ToListAsync().ConfigureAwait(false);


            foreach (var book in booksToDelete)
            {
                _context.Books.Remove(book);
                await _context.SaveChangesAsync().ConfigureAwait(false);
            }
        }

        public async Task CreateBookAsync(string title, string isbn, int year, int rack, string authorId, string publisherId, List<int> genresIds, int copies)
        {
            var author = await _context.Authors.FirstOrDefaultAsync(a => a.Id.ToString() == authorId).ConfigureAwait(false);

            var publisher = await _context.Publishers.FirstOrDefaultAsync(p => p.Id.ToString() == publisherId).ConfigureAwait(false);

            await _bookFac.CreateBook(title, isbn, year, rack, author, publisher, genresIds, copies).ConfigureAwait(false);
        }
        public async Task<List<Author>> GetAllAuthorsAsync() => await _context.Authors.ToListAsync();

        public async Task<Author> CreateAuthorAsync(string authorName) => await _authorFac.CreateAuthor(authorName);

        public async Task<List<Publisher>> GetAllPublishersAsync() => await _context.Publishers.ToListAsync().ConfigureAwait(false);

        public async Task<Publisher> CreatePublisherAsync(string publisherName) => await _publisherFac.CreatePublisher(publisherName);

        public async Task<List<Genre>> GetAllGenresAsync() => await _context.Genres.ToListAsync();

        public async Task<List<Genre>> CreateGenreAsync(string genre) => await _genreFac.CreateGenreList(genre);

        public async Task<Book> GetBookAsync(string id)
            => await _context.Books
                 .Include(b => b.Author)
                .Include(b => b.Publisher)
                .Include(b => b.BookGenres)
                    .ThenInclude(bg => bg.Genre)
                 .FirstOrDefaultAsync(book => book.Id.ToString() == id)
                 .ConfigureAwait(false);


        public async Task<Author> GetAuthorAsync(string id)
            => await _context.Authors
            .Include(a => a.Books)
            .FirstOrDefaultAsync(a => a.Id.ToString() == id)
            .ConfigureAwait(false);
    }
}

