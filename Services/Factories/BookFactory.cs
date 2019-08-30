using Library.Models.Models;
using Library.Services.Factories.Contracts;

namespace Library.Services.Factories
{
    public class BookFactory : IBookFactory
    {
        private readonly IAuthorFactory _authorFac;
        private readonly IPublisherFactory _publisherFac;
        public BookFactory(IAuthorFactory authorFac, IPublisherFactory publisherFac)
        {
            _authorFac = authorFac;
            _publisherFac = publisherFac;
        }
        public Book CreateBook(string author, string title, string isbn, string publisher, int year, int rack)
        {
            var authorType = _authorFac.CreateAuthor(author);
            var publisherType = _publisherFac.CreatePublisher(publisher);

            return new Book(authorType, title, isbn, publisherType, year, rack);
        }
    }
}
