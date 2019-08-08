using Library.Models.Contracts;
using System.Collections.Generic;

namespace Library.Database.Contracts
{
    public interface IBookDatabase
    {
        void Create(IBook book);
        IBook Get(int bookId);
        void Update(IBook book);
        void Delete(IBook book);
        List<IBook> Load();
        void Save();
    }
}
