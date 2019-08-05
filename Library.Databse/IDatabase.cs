using Library.Models.Contracts;
using Library.Models.Models;
using System;
using System.Collections.Generic;
using System.Text;

namespace Library.Database
{
    public interface IDatabase
    {
        IEnumerable<IBook> Books { get; }

        void AddBookToList(IBook book);

        void AddUserToList(IUser user);

        void WriteBooksToJson(IEnumerable<IBook> books);

        void WriteUsersToJson(IEnumerable<IUser> users);

    }
}
