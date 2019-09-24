using Library.Database;
using Library.Models.Models;
using Library.Services.Factories.Contracts;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Library.Services.Factories
{
    public class GenreFactory : IGenreFactory
    {
        private readonly LibraryContext _context;
        public GenreFactory(LibraryContext context)
        {
            _context = context;
        }

        public async Task<List<Genre>> CreateGenreList(string genres)
        {
            var genreListString = genres.Split(',').Select(g => g.Trim()).ToList();
            List<Genre> list = new List<Genre>();

            foreach (var genre in genreListString)
            {
                var existingGenre = _context.Genres.FirstOrDefault(g => string.Equals(g.Name, genre, System.StringComparison.OrdinalIgnoreCase)); ;

                if (existingGenre is null)
                {
                    var newGenre = new Genre { Name = genre };
                    _context.Genres.Add(newGenre);
                    await _context.SaveChangesAsync().ConfigureAwait(false);

                    list.Add(newGenre);
                }
                else
                    list.Add(existingGenre);
            }

            return list;
        }
    }
}