using Library.Models.Contracts;
using System.Collections.Generic;

namespace Library.Core.Contracts
{
    public interface IFormatter
    {
        string Format(IBook book);
        string Format(IUser user);
        string FormatListOfBooks(List<IBook> books);
    }
}
