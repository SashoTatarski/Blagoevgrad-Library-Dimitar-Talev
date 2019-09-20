using Library.Models.Models;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Library.Services.Factories.Contracts
{
    public interface IGenreFactory
    {
        Task<List<Genre>> CreateGenreList(string genres);

    }
}
