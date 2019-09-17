using Library.Database;
using Library.Models.Models;
using Library.Models.Utils;
using Library.Services.Factories.Contracts;

namespace Library.Services.Factories
{
    public class AccountFactory : IAccountFactory
    {
        private readonly LibraryContext _context;
        public AccountFactory(LibraryContext context)
        {
            _context = context;
        }        
    }
}
