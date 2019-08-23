using Library.Database.Contracts;
using Library.Models.Contracts;
using Library.Models.Models;
using Library.Models.Utils;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;

namespace Library.Database
{
    public class LibrarianDataBase : ILibrarianDataBase
    {
        private readonly IList<ILibrarian> _internal;
        private readonly LibraryContext _context;


        public LibrarianDataBase(LibraryContext context)
        {
            _internal = this.Load();
            _context = context;
        }

        public void Create(ILibrarian librarian)
        {
            _context.Librarians.Add((Librarian)librarian);
            _context.SaveChanges();
            _internal.Add(librarian);
            this.Save();
        }

        // TODO: ?
        public void Update(ILibrarian librarian) => throw new NotImplementedException();
        public void Delete(ILibrarian librarian) => throw new NotImplementedException();

        public ILibrarian Get(string username) => _context.Librarians.FirstOrDefault(l => l.Username == username);
        //_internal.FirstOrDefault(u => u.Username == username);
        public List<ILibrarian> Load()
        {
            var filePath = GlobalConstants.librariansFilepath;
            string content;
            using (var reader = new StreamReader(filePath))
            {
                content = reader.ReadToEnd();
            }
            return JsonConvert
                .DeserializeObject<List<Librarian>>(content, new JsonSerializerSettings
                {
                    TypeNameHandling = TypeNameHandling.Auto
                }).Cast<ILibrarian>().ToList();
        }

        public void Save()
        {
            var filePath = GlobalConstants.librariansFilepath;
            
            using (var sw = new StreamWriter(filePath))
            {
                using (JsonWriter writer = new JsonTextWriter(sw))
                {
                    BookDatabase.CreateSerializer().Serialize(writer, _internal);
                }
            }
        }
    }
}
