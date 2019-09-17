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
       
    }
}
