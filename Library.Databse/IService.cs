using Library.Models.Contracts;
using Library.Models.Models;
using System;
using System.Collections.Generic;
using System.Text;

namespace Library.Database
{
    public interface IService
    {
        void AddBook(IBook book);

        void AddUser(IUser user);

        IAccount FindAccount(string userName);
        int CurrentID();
        List<Book> ReadBooks();
    }
}
