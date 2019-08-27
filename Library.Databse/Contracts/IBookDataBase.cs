using Library.Models.Contracts;
using Library.Models.Models;
using System.Collections.Generic;

namespace Library.Database.Contracts
{
    public interface IBookDatabase
    {
        void Create(IBook book);
        IBook Find(int bookId);
        void Update(IBook book);
        void Delete(IBook book);
        List<Book> Read();
    }
}
