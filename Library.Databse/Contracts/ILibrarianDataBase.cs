using Library.Models.Contracts;
using System.Collections.Generic;

namespace Library.Database.Contracts
{
    public interface ILibrarianDataBase
    {
        void Create(ILibrarian librarian);
        ILibrarian Get(string username);
        void Update(ILibrarian librarian);
        void Delete(ILibrarian librarian);
        List<ILibrarian> Load();
        void Save();
    }
}
