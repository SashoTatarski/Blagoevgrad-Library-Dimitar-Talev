using Library.Database.Contracts;
using Library.Models.Models;
using Library.Services.Factories.Contracts;
using System.Collections.Generic;
using System.Linq;

namespace Library.Services.Factories
{
    public class GenreFactory : IGenreFactory
    {
        private readonly IDataBase<Genre> _database;
        public GenreFactory(IDataBase<Genre> database)
        {
            _database = database;
        }

        public List<Genre> 
            CreateGenreList(string genres)
        {
            var genreListString = genres.Split(new string[] { ",", ", " }, System.StringSplitOptions.None).ToList();
            List<Genre> list = new List<Genre>();

            foreach (var genre in genreListString)
            {
                var existingGenre = _database.Find(genre);

                if (existingGenre is null)
                {
                    var newGenre = new Genre { GenreName = genre };
                    _database.Create(newGenre);
                    list.Add(newGenre);
                }
                else
                    list.Add(existingGenre);
            }

            return list;
        }
    }
}
