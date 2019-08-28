using Library.Models.Models;

namespace Library.Services.Factories.Contracts
{
    public interface IPublisherFactory
    {
        Publisher CreatePublisher(string name);
    }
}
