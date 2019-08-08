using Library.Models.Contracts;
using Library.Models.Models;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;

namespace Library.Database
{
    public interface IDatabase
    {
        List<Book> ReadBooks();
        List<Librarian> ReadLibrarians();
        List<User> ReadUsers();

        void WriteBooks(IEnumerable<IBook> books);

        void WriteLibrarians(IEnumerable<ILibrarian> librarians);

        void WriteUsers(IEnumerable<IUser> users);
    }
    public class BookDatabase : IBookDatabase
    {
        private IList<IBook> _internal;
        public BookDatabase()
        {
            _internal = new List<IBook>();
        }
        public void Create(IBook book)
        {
            _internal.Add(book);
            this.Save();
        }
        public void Delete(IBook book)
        {
            // should be
            // _internal.Remove(book);
            // but...
            var bookToRemove = _internal.FirstOrDefault(b => b.ID == book.ID);
            _internal.Remove(bookToRemove);
            this.Save();
        }
        public IBook Get(int bookId)
        {
            return _internal.FirstOrDefault(b => b.ID == bookId);
        }
        public void Update(IBook book)
        {
            var bookToUpdate = _internal.FirstOrDefault(b => b.ID == book.ID);
            bookToUpdate.Update(book);
            this.Save();
        }
        public void Load()
        {
            var _catalogFilepath = @"..\..\..\..\catalog.json";
            string content;
            using (var reader = new StreamReader(_catalogFilepath))
            {
                content = reader.ReadToEnd();
            }
            _internal = JsonConvert
                .DeserializeObject<List<Book>>(content, new JsonSerializerSettings
            {
                TypeNameHandling = TypeNameHandling.Auto
            }).Cast<IBook>().ToList();
        }
        public void Save()
        {
            var _catalogFilepath = @"..\..\..\..\catalog.json";
            var serializer = this.CreateSerializer();
            using (var sw = new StreamWriter(_catalogFilepath))
            {
                using (JsonWriter writer = new JsonTextWriter(sw))
                {
                    serializer.Serialize(writer, _internal);
                }
            }
        }
        private JsonSerializer CreateSerializer()
        {
            return new JsonSerializer
            {
                Formatting = Formatting.Indented,
                TypeNameHandling = TypeNameHandling.Objects,
                TypeNameAssemblyFormat = System.Runtime.Serialization.Formatters.FormatterAssemblyStyle.Simple
            };
        }
    }
    public interface IBookDatabase
    {
        void Create(IBook book);
        IBook Get(int bookId);
        void Update(IBook book);
        void Delete(IBook book);
        void Load();
        void Save();
    }
    //interface IDatabase<T>
    //{
    //    void Create(T x);
    //    void Read(T x);
    //    void Update(T x);
    //    void Delete(T x)
    //}
    //class UserDatabase<User> : IDatabase<User>
    //{
    //    public void Create(User x) => throw new NotImplementedException();
    //    public void Delete(User x) => throw new NotImplementedException();
    //    public void Read(User x) => throw new NotImplementedException();
    //    public void Update(User x) => throw new NotImplementedException();
    //}
    //class BookDatabase<Book> : IDatabase<Book>
    //{
    //    public void Create(Book x) => throw new NotImplementedException();
    //    public void Delete(Book x) => throw new NotImplementedException();
    //    public void Read(Book x) => throw new NotImplementedException();
    //    public void Update(Book x) => throw new NotImplementedException();
    //}

}
