using Library.Models.Contracts;
using System;
using System.Collections.Generic;
using System.Text;

namespace Library.Database
{
    public interface IDatabase
    {
        List<IBook> Books { get; }

        void AddBooks(IBook bookToAdd);
    }
}
