using Library.Models.Contracts;
using Library.Models.Models;
using Library.Models.Utils;
using Newtonsoft.Json;
using System.Collections.Generic;
using System.IO;
using System.Linq;

namespace Library.Database.JsonHandler
{
    public class UsersJsonHandler<User> : IJsonHandler<User>
    {
        public List<User> Load()
        {
            var filePath = GlobalConstants.usersFilepath;
            string content;
            using (var reader = new StreamReader(filePath))
            {
                content = reader.ReadToEnd();
            }
            return JsonConvert
                .DeserializeObject<List<User>>(content, new JsonSerializerSettings
                {
                    TypeNameHandling = TypeNameHandling.Auto
                })
                .ToList();
        }

        public void Save(List<User> collection)
        {
            var filePath = GlobalConstants.usersFilepath;

            using (var sw = new StreamWriter(filePath))
            {
                using (JsonWriter writer = new JsonTextWriter(sw))
                {
                    CreateSerializer.SerializerFormat().Serialize(writer, collection);
                }
            }
        }
    }

    public class LibrariansJsonHandler<Librarian> : IJsonHandler<Librarian>
    {
        public List<Librarian> Load()
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
                })
                .ToList();
        }

        public void Save(List<Librarian> collection)
        {
            var filePath = GlobalConstants.usersFilepath;

            using (var sw = new StreamWriter(filePath))
            {
                using (JsonWriter writer = new JsonTextWriter(sw))
                {
                    CreateSerializer.SerializerFormat().Serialize(writer, collection);
                }
            }
        }
    }

    public class BooksJsonHandler<Book> : IJsonHandler<Book>
    {
        public List<Book> Load()
        {
            var filePath = GlobalConstants.catalogFilepath;
            string content;
            using (var reader = new StreamReader(filePath))
            {
                content = reader.ReadToEnd();
            }
            return JsonConvert
                .DeserializeObject<List<Book>>(content)
                .ToList();
        }

        public void Save(List<Book> collection)
        {
            var filePath = GlobalConstants.usersFilepath;

            using (var sw = new StreamWriter(filePath))
            {
                using (JsonWriter writer = new JsonTextWriter(sw))
                {
                    CreateSerializer.SerializerFormat().Serialize(writer, collection);
                }
            }
        }
    }

    static class CreateSerializer
    {
        public static JsonSerializer SerializerFormat()
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
