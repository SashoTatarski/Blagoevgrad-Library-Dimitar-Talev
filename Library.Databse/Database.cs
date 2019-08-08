﻿using Library.Models.Contracts;
using Library.Models.Models;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;

namespace Library.Database
{
    public class Database : IDatabase
    {
        private const string _catalogFilepath = @"..\..\..\..\catalog.json";
        private const string _usersFilepath = @"..\..\..\..\users.json";
        private const string _librariansFilepath = @"..\..\..\..\librarians.json";

        public List<Book> ReadBooks()
        {
            string content;
            using (var reader = new StreamReader(_catalogFilepath))
            {
                content = reader.ReadToEnd();
            }

            return JsonConvert.DeserializeObject<List<Book>>(content, new JsonSerializerSettings
            {
                TypeNameHandling = TypeNameHandling.Auto
            });/*.Cast<IBook>().ToList();*/
        }

        public void WriteBooks(IEnumerable<IBook> books)
        {
            var serializer = this.CreateSerializer();
            using (var sw = new StreamWriter(_catalogFilepath))
            {
                using (JsonWriter writer = new JsonTextWriter(sw))
                {
                    serializer.Serialize(writer, books);
                }
            }
        }

        public List<User> ReadUsers()
        {
            string content;
            using (var reader = new StreamReader(_usersFilepath))
            {
                content = reader.ReadToEnd();
            }
            return JsonConvert.DeserializeObject<List<User>>(content, new JsonSerializerSettings
            {
                TypeNameHandling = TypeNameHandling.Auto
            });
        }

        public void WriteUsers(IEnumerable<IUser> users)
        {
            var serializer = this.CreateSerializer();
            using (var sw = new StreamWriter(_usersFilepath))
            {
                using (JsonWriter writer = new JsonTextWriter(sw))
                {
                    serializer.Serialize(writer, users);
                }
            }
        }

        private  JsonSerializer CreateSerializer()
        {
            return new JsonSerializer
            {
                Formatting = Formatting.Indented,
                TypeNameHandling = TypeNameHandling.Objects,
                TypeNameAssemblyFormat = System.Runtime.Serialization.Formatters.FormatterAssemblyStyle.Simple
            };
        }

        public List<Librarian> ReadLibrarians()
        {
            string content;
            using (var reader = new StreamReader(_librariansFilepath))
            {
                content = reader.ReadToEnd();
            }
            return JsonConvert.DeserializeObject<List<Librarian>>(content, new JsonSerializerSettings
            {
                TypeNameHandling = TypeNameHandling.Auto
            });
        }

        public void WriteLibrarians(IEnumerable<ILibrarian> librarians)
        {
            var serializer = new JsonSerializer
            {
                Formatting = Formatting.Indented,
                TypeNameHandling = TypeNameHandling.Objects,
                TypeNameAssemblyFormat = System.Runtime.Serialization.Formatters.FormatterAssemblyStyle.Simple
            };
            using (var sw = new StreamWriter(_librariansFilepath))
            {
                using (JsonWriter writer = new JsonTextWriter(sw))
                {
                    serializer.Serialize(writer, librarians);
                }
            }
        }

    }
}
