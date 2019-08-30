using Library.Models.Contracts;
using Library.Models.Models;
using System.Collections.Generic;

namespace Library.Services.Contracts
{
    public interface IConsoleFormatter
    {
        string Format(Book book);

        string Format(User user);

        string Format(IAccount account);

        string FormatCheckedoutBook(Book book);

        string FormatListOfBooks(List<Book> books);

        string FormatReservedBook(IBook book);

        string FormatCommandMessage(string message, string modelInfo);

        string FormatCommandMessage(string message);

        string FormatListOfUsers(List<User> users);

        string FormatListOfUsersShort(List<User> users);
        string CenterStringWithSymbols(string text, char symbol);
        string FormatListOfBooks(List<int> booksIds);
    }
}
