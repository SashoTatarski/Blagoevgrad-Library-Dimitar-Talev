using Library.Database.Contracts;
using Library.Models.Models;
using Library.Services.Factories.Contracts;

namespace Library.Services.Factories
{
    public class PublisherFactory : IPublisherFactory
    {
        private readonly IDatabase<Publisher> _database;
        public PublisherFactory(IDatabase<Publisher> database)
        {
            _database = database;
        }
        public Publisher CreatePublisher(string name)
        {
            var existingPublisher = _database.Find(name);

            if (existingPublisher is null)
            {
                var newPublisher = new Publisher { Name = name };
                _database.Create(newPublisher);
                return newPublisher;
            }
            else
                return existingPublisher;
        }
    }
}
