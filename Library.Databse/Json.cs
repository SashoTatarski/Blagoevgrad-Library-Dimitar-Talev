using Library.Models.Contracts;
using Library.Models.Models;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.IO;
using System.Text;

namespace Library.Database
{
    public class Json  : IJson
    {
        public List<Book> ReadBooks()
        {
            string content;
            using (var reader = new StreamReader(@"..\..\..\..\catalog.json"))
            {
                content = reader.ReadToEnd();
            }

            return JsonConvert.DeserializeObject<List<Book>>(content, new JsonSerializerSettings
            {
                TypeNameHandling = TypeNameHandling.Auto
            });
        }

        public void WriteBooks(IEnumerable<IBook> books)
        {
            var serializer = new JsonSerializer
            {
                Formatting = Formatting.Indented,
                TypeNameHandling = TypeNameHandling.Objects,
                TypeNameAssemblyFormat = System.Runtime.Serialization.Formatters.FormatterAssemblyStyle.Simple
            };
            using (var sw = new StreamWriter(@"..\..\..\..\catalog.json"))
            {
                using (JsonWriter writer = new JsonTextWriter(sw))
                {
                    serializer.Serialize(writer, books);
                }
            }
        }

        //public  List<IUser> ReadUsers()
        //{
        //    string content;
        //    using (var reader = new StreamReader("users.json"))
        //    {
        //        content = reader.ReadToEnd();
        //    }
        //    return JsonConvert.DeserializeObject<List<IUser>>(content, new JsonSerializerSettings
        //    {
        //        TypeNameHandling = TypeNameHandling.Auto
        //    });
        //}

        //public static void WriteUsers(List<IUser> users)
        //{
        //    var serializer = new JsonSerializer
        //    {
        //        Formatting = Formatting.Indented,
        //        TypeNameHandling = TypeNameHandling.Objects,
        //        TypeNameAssemblyFormat = System.Runtime.Serialization.Formatters.FormatterAssemblyStyle.Simple
        //    };
        //    using (var sw = new StreamWriter("users.json"))
        //    {
        //        using (JsonWriter writer = new JsonTextWriter(sw))
        //        {
        //            serializer.Serialize(writer, users);
        //        }
        //    }
        //}

    }
}
