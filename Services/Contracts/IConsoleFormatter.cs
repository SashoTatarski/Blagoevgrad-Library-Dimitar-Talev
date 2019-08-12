using Library.Models.Contracts;
using System.Collections.Generic;

namespace Library.Services.Contracts
{
    public interface IConsoleFormatter
    {
        string Format(IBook book);

        string Format(IUser user);

        string Format(IAccount account);

        string FormatCheckedoutBook(IBook book);

        string FormatListOfBooks(List<IBook> books);

        string FormatReservedBook(IBook book);
        
    }
}
