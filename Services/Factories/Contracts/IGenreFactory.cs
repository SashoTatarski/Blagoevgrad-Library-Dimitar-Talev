using Library.Models.Models;
using System.Collections.Generic;

namespace Library.Services.Factories.Contracts
{
    public interface IGenreFactory
    {
        List<Genre> CreateGenreList(string genres);

    }
}
