using Library.Models.Models;

namespace Library.Services.Factories.Contracts
{
    public interface IUserFactory
    {
       User CreateUser(string username, string password);
    }
}
