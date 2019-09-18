using Library.Models.Models;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Library.Services.Contracts
{
    public interface IAccountManager
    {
        User Create(string username, string password);
        User Find(string username, string password);
    }
}
