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
        private IList<ILibrarian> _internal;

        public LibrarianDataBase()
        {
            _internal = this.Load();
        }

        public void Create(ILibrarian librarian)
        {
            _internal.Add(librarian);
            this.Save();
        }

        // TODO: ?
        public void Update(ILibrarian librarian)
        {
            throw new NotImplementedException();
        }
        public void Delete(ILibrarian librarian)
        {
            throw new NotImplementedException();
        }

        public ILibrarian Get(string username)
        {
            return _internal.FirstOrDefault(l => l.Username == username);
        }

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
