using Library.Database;
using Library.Models.Models;
using Library.Services.Factories.Contracts;
using System.Linq;
using System.Threading.Tasks;

namespace Library.Services.Factories
{
    public class PublisherFactory : IPublisherFactory
    {
        private readonly LibraryContext _context;
        public PublisherFactory(LibraryContext context)
        {
            _context = context;
        }
        public async Task<Publisher> CreatePublisher(string name)
        {
            var existingPublisher = _context.Publishers.FirstOrDefault(p => string.Equals(p.Name, name, System.StringComparison.OrdinalIgnoreCase));

            if (existingPublisher is null)
            {
                var newPublisher = new Publisher { Name = name };
                _context.Publishers.Add(newPublisher);
                await _context.SaveChangesAsync().ConfigureAwait(false);

                return newPublisher;
            }
            else
                return existingPublisher;
        }
    }
}
