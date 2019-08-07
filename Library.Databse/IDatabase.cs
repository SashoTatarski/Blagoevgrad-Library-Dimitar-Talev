using Library.Models.Contracts;
using Library.Models.Models;
using System;
using System.Collections.Generic;
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
