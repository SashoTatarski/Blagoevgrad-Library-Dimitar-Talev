using Library.Database.Contracts;
using Library.Models.Contracts;
using Library.Models.Models;
using Library.Models.Utils;
using Newtonsoft.Json;
using System.Collections.Generic;
using System.IO;
using System.Linq;

namespace Library.Database
{
    // SOLID: Liskov - we can substitute JSON with another type of DB
    public class BookDatabase : IBookDatabase
    {
        private readonly IList<IBook> _internal;
        private readonly LibraryContext _context;

        public BookDatabase(LibraryContext context)
        {
            _context = context;
            //_internal = this.Load();
        }

        public void Create(IBook book)
        {
            //_internal.Add(book);
            //this.Save();
            _context.Books.Add((Book)book);
            _context.SaveChanges();

        }

        public void Delete(IBook book)
        {
            var bookToRemove = _internal.FirstOrDefault(b => b.Id == book.Id);
            _internal.Remove(bookToRemove);
            this.Save();
        }

        public IBook GetOneBook(int bookId) => _context.Books.FirstOrDefault(b => b.Id == bookId);

        public void Update(IBook book)
        {
            var bookToUpdate = _internal.FirstOrDefault(b => b.Id == book.Id);
            bookToUpdate.Update(book);
            this.Save();
        }

        public List<Book> Load()
        {
           return  _context.Books.ToList();
            //string content;
            //using (var reader = new StreamReader(GlobalConstants.catalogFilepath))
            //{
            //    content = reader.ReadToEnd();
            //}

            //return JsonConvert
            //    .DeserializeObject<List<Book>>(content, new JsonSerializerSettings
            //    {
            //        TypeNameHandling = TypeNameHandling.Auto
            //    }).Cast<IBook>().ToList();
        }

        public void Save()
        {
            using (var sw = new StreamWriter(GlobalConstants.catalogFilepath))
            {
                using (JsonWriter writer = new JsonTextWriter(sw))
                {
                    CreateSerializer().Serialize(writer, _internal);
                }
            }
        }

        public static JsonSerializer CreateSerializer()
        {
            return new JsonSerializer
            {
                Formatting = Formatting.Indented,
                TypeNameHandling = TypeNameHandling.Objects,
                TypeNameAssemblyFormat = System.Runtime.Serialization.Formatters.FormatterAssemblyStyle.Simple
            };
        }

    }
}
