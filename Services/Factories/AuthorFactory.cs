using Library.Database;
using Library.Models.Models;
using Library.Services.Factories.Contracts;
using System.Linq;
using System.Threading.Tasks;

namespace Library.Services.Factories
{
    public class AuthorFactory : IAuthorFactory
    {
        private readonly LibraryContext _context;
        public AuthorFactory(LibraryContext context)
        {
            _context = context;
        }
        public async Task<Author> CreateAuthor(string authorName)
        {
            var existingAuthor = _context.Authors.FirstOrDefault(a => a.Name.ToLower() == authorName.ToLower());

            if (existingAuthor is null)
            {
                var newAuthor = new Author { Name = authorName };
                _context.Authors.Add(newAuthor);
                await _context.SaveChangesAsync();
                return newAuthor;
            }
            else
                return existingAuthor;
        }
    }
}
