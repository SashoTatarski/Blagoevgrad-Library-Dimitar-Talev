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

        Task<IReadOnlyCollection<Book>> SearchAsync(string searchCriteria);

        Task DeleteBookAsync(string id);

        Task EditBookAsync(string bookId, string title, string isbn, int year, int rack, string authorId, string publisherId, List<int> genresIds);                  
        
        Task<Book> GetBookAsync(string id);

        Task<List<Book>> GetBooksByAuthorAsync(string authorId);

        Task<Author> GetAuthorAsync(string id);
        Task<List<Book>> GetAllBooks();
    }
}