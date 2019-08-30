using Library.Database.Contracts;
using Library.Models.Models;
using Library.Services.Factories.Contracts;

namespace Library.Services.Factories
{
    public class AuthorFactory : IAuthorFactory
    {
        private readonly IDatabase<Author> _database;
        public AuthorFactory(IDatabase<Author> database)
        {
            _database = database;
        }
        public Author CreateAuthor(string authorName)
        {
            var existingAuthor = _database.Find(authorName);

            if (existingAuthor is null)
            {
                var newAuthor = new Author { Name = authorName };
                _database.Create(newAuthor);
                return newAuthor;
            }
            else
                return existingAuthor;
        }
    }
}
