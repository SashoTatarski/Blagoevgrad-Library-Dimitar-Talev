using Library.Database;
using Library.Models.Models;
using Library.Services.Factories.Contracts;
using System.Collections.Generic;
using System.Linq;

namespace Library.Services.Factories
{
    public class GenreFactory : IGenreFactory
    {
        private readonly LibraryContext _context;
        public GenreFactory(LibraryContext context)
        {
            _context = context;
        }

        public List<Genre> CreateGenreList(string genres)
        {
            var genreListString = genres.Split(',').Select(g => g.Trim()).ToList();
            List<Genre> list = new List<Genre>();

            foreach (var genre in genreListString)
            {
                var existingGenre = _context.Genres.FirstOrDefault(g => g.GenreName.ToLower() == genre.ToLower()); ;

                if (existingGenre is null)
                {
                    var newGenre = new Genre { GenreName = genre };
                    _context.Genres.Add(newGenre);
                    _context.SaveChanges();

                    list.Add(newGenre);
                }
                else
                    list.Add(existingGenre);
            }

            return list;
        }
    }
}