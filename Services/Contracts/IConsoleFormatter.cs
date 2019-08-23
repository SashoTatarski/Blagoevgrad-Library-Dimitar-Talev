﻿using Library.Models.Contracts;
using Library.Models.Models;
using System.Collections.Generic;

namespace Library.Services.Contracts
{
    public interface IConsoleFormatter
    {
        string Format(IBook book);

        string Format(IUser user);

        string Format(IAccount account);

        string FormatCheckedoutBook(IBook book);

        string FormatListOfBooks(List<Book> books);

        string FormatReservedBook(IBook book);

        string FormatCommandMessage(string message, string modelInfo);

        string FormatCommandMessage(string message);

        string FormatListOfUsers(List<IUser> users);

        string FormatListOfUsersShort(List<User> users);
    }
}
