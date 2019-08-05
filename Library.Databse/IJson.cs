using Library.Models.Contracts;
using Library.Models.Models;
using System;
using System.Collections.Generic;
using System.Text;

namespace Library.Database
{
    public interface IJson
    {
        List<Book> ReadBooks();

        List<User> ReadUsers();

        void WriteBooks(IEnumerable<IBook> books);

        void WriteUsers(IEnumerable<IUser> users);

    }
}
