﻿using Library.Database.Contracts;
using Library.Models.Contracts;
using Library.Models.Models;
using Library.Models.Utils;
using Newtonsoft.Json;
using System.Collections.Generic;
using System.IO;
using System.Linq;

namespace Library.Database
{
    public class BookDatabase : IBookDatabase
    {
        private readonly IList<IBook> _internal;

        public BookDatabase()
        {
            //Kiro's work
            //_internal = new List<IBook>();
            _internal = this.Load();
        }

        public void Create(IBook book)
        {
            _internal.Add(book);
            this.Save();
        }

        public void Delete(IBook book)
        {
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

        ////Kiro's work
        //public void Load()
        //{
        //    var filePath = GlobalConstants.catalogFilepath;
        //    string content;
        //    using (var reader = new StreamReader(filePath))
        //    {
        //        content = reader.ReadToEnd();
        //    }
        //    _internal = JsonConvert
        //        .DeserializeObject<List<Book>>(content, new JsonSerializerSettings
        //        {
        //            TypeNameHandling = TypeNameHandling.Auto
        //        }).Cast<IBook>().ToList();
        //}

        public List<IBook> Load()
        {
            var filePath = GlobalConstants.catalogFilepath;
            string content;
            using (var reader = new StreamReader(filePath))
            {
                content = reader.ReadToEnd();
            }

            return JsonConvert
                .DeserializeObject<List<Book>>(content, new JsonSerializerSettings
                {
                    TypeNameHandling = TypeNameHandling.Auto
                }).Cast<IBook>().ToList();
        }

        public void Save()
        {
            var filePath = GlobalConstants.catalogFilepath;
            var serializer = this.CreateSerializer();
            using (var sw = new StreamWriter(filePath))
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
}