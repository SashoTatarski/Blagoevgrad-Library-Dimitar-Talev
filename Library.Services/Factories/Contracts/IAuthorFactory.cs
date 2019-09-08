using Library.Models.Models;

namespace Library.Services.Factories.Contracts
{
    public interface IAuthorFactory
    {
        Author CreateAuthor(string authorName);
    }
}
