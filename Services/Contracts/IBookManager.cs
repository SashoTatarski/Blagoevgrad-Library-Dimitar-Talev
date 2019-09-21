using Library.Models.Enums;
using Library.Models.Models;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Library.Services.Contracts
{
    public interface IBookManager
    {
        Task<List<Author>> GetAllAuthorsAsync();

        Task<List<Publisher>> GetAllPublishersAsync();

        Task<Author> CreateAuthorAsync(string authorName);

        Task<Publisher> CreatePublisherAsync(string publisherName);

        Task<List<Genre>> CreateGenreAsync(string genre);

        Task<List<Genre>> GetAllGenresAsync();

        Task CreateBookAsync(string title, string isbn, int year, int rack, string authorId, string publisherId, List<int> genresIds, int copies);

        IReadOnlyCollection<Book> Search(string searchCriteria);

        List<Book> GetAllBooks();

        Task DeleteAsync(string id);








        void ListAllBooks();
        Book FindBook(int id);
        void RemoveBook(Book book);
        List<Book> GetSearchResult(string searchByParameter, string searchByText);
        void UpdateBookAuthor(int bookId, string newAuthorName);
        void UpdateBookTitle(int bookId, string newTitle);
        void UpdateBookISBN(int bookId, string newISBN);
        void UpdateBookRack(int bookId, int newRack);
        void UpdateBookYear(int bookId, int newYear);
        void UpdateBookPublisher(int bookId, string newPublisherName);
        void UpdateBookGenre(int bookId, string newGenres);
        void ChangeBookStatus(Book book, BookStatus status);
        Task<Book> GetBookAsync(string id);
        Task<List<Book>> GetBooksByAuthorAsync(string authorId);
        Task<Author> GetAuthorAsync(string id);
    }
}