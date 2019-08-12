using Library.Database.Contracts;
using Library.Models.Contracts;
using Library.Models.Enums;
using Library.Models.Models;
using Library.Models.Utils;
using Newtonsoft.Json;
using System.Collections.Generic;
using System.IO;
using System.Linq;

namespace Library.Database
{
    public class UserDataBase : IUserDataBase
    {
        private readonly IList<IUser> _internal;

        public UserDataBase()
        {
            _internal = this.Load();
        }

        public void Create(IUser user)
        {
            _internal.Add(user);
            this.Save();
        }

        public void Delete(IUser user)
        {
            user.Status = MemberStatus.Inactive;
            this.Save();
        }

        public IUser Get(string username) => _internal.FirstOrDefault(u => u.Username == username);

        public void Update(IUser updatedUser)
        {
            var userToUpdate = _internal.FirstOrDefault(u => u.Username == updatedUser.Username);
            userToUpdate.Update(updatedUser);
            this.Save();
        }
        public List<IUser> Load()
        {
            string content;
            using (var reader = new StreamReader(GlobalConstants.usersFilepath))
            {
                content = reader.ReadToEnd();
            }
            return JsonConvert
                .DeserializeObject<List<User>>(content, new JsonSerializerSettings
                {
                    TypeNameHandling = TypeNameHandling.Auto
                }).Cast<IUser>().ToList();
        }


        public void Save()
        {
            var serializer = this.CreateSerializer();
            using (var sw = new StreamWriter(GlobalConstants.usersFilepath))
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
