using Library.Models.Contracts;
using Library.Models.Enums;
using Library.Models.Utils;
using Library.Services.Contracts;
using System.Collections.Generic;

using System.Text;
using System.Linq;
using Library.Models.Models;
using System;

namespace Library.Services
{
    public class ConsoleFormatter : IConsoleFormatter
    {
        public string Format(IAccount account)
        {
            var strBuilder = new StringBuilder();
            strBuilder.AppendLine($"Username: {account.Username}");

            return strBuilder.ToString();
        }

        public string Format(IBook book)
        {
            var strBuilder = new StringBuilder();
            strBuilder.AppendLine($"ID: {book.Id} || Status: {book.Status}");
            strBuilder.AppendLine($"Title: {book.Title} || Author: {book.Author}");
            strBuilder.AppendLine($"Genre: {book.Genre}");
            strBuilder.AppendLine($"Publisher: {book.Publisher} || Year: {book.Year} || Location: {book.Rack} rack");

            return strBuilder.ToString();
        }

        public string Format(IUser user)
        {
            var strBuilder = new StringBuilder();
            //strBuilder.AppendLine($"Username: {user.Username}");
            //strBuilder.AppendLine($"Checked out books:");
            //strBuilder.AppendLine(this.FormatListOfBooks(user.CheckedOutBooks));
            //strBuilder.AppendLine($"Reserved books:");
            //strBuilder.AppendLine(this.FormatListOfBooks(user.ReservedBooks));

            return strBuilder.ToString();
        }

        public string FormatListOfBooks(List<Book> books)
        {
            var strBuilder = new StringBuilder();

            //if (books.Count == 0)
            //    return "There are no books!";

            //foreach (var book in books)
            //    strBuilder.AppendLine(this.Format(book));

            return strBuilder.ToString();
        }

        public string FormatCheckedoutBook(IBook book)
        {
            var strBuilder = new StringBuilder();

            //strBuilder.AppendLine($"ID: {book.Id} || Status: {book.Status}");
            //strBuilder.AppendLine($"Title: {book.Title} || Author: {book.Author}");
            //strBuilder.AppendLine($"CheckedOut Date: {book.CheckoutDate.ToString("dd MM yyyy")}");
            //strBuilder.AppendLine($"Due Date: {book.DueDate.ToString("dd MM yyyy")}");

            return strBuilder.ToString();
        }

        public string FormatReservedBook(IBook book)
        {
            var strBuilder = new StringBuilder();

            //strBuilder.AppendLine($"ID: {book.Id} || Status: {book.Status}");
            //strBuilder.AppendLine($"Title: {book.Title} || Author: {book.Author}");
            //strBuilder.AppendLine($"Reservation Date: {book.ResevedDate.ToString("dd MM yyyy")}");
            //strBuilder.AppendLine($"Due Date: {book.ResevationDueDate.ToString("dd MM yyyy")}");

            return strBuilder.ToString();
        }

        public string FormatCommandMessage(string message, string modelInfo)
        {
            var strBuilder = new StringBuilder();

            strBuilder.AppendLine();
            strBuilder.AppendLine(GlobalConstants.Delimiter);
            strBuilder.AppendLine(message);
            strBuilder.AppendLine(GlobalConstants.MiniDelimiter);
            strBuilder.Append(modelInfo);
            strBuilder.AppendLine(GlobalConstants.Delimiter);

            return strBuilder.ToString();
        }

        public string FormatCommandMessage(string message)
        {
            var strBuilder = new StringBuilder();

            strBuilder.AppendLine();
            strBuilder.AppendLine(GlobalConstants.Delimiter);
            strBuilder.AppendLine(message);
            strBuilder.AppendLine(GlobalConstants.Delimiter);

            return strBuilder.ToString();
        }

        public string FormatListOfUsers(List<IUser> users)
        {
            var strBuilder = new StringBuilder();

            if (users.Count == 0)
                return GlobalConstants.NoUsers;

            foreach (var user in users)
            {
                if (user.Status != AccountStatus.Inactive)
                {
                    strBuilder.Append(this.Format(user));
                    strBuilder.AppendLine(GlobalConstants.MiniDelimiter);
                }
            }
            return strBuilder.ToString();
        }

        public string FormatListOfUsersShort(List<User> users)
        {
            var strBuilder = new StringBuilder();

            if (users.Count == 0)
                return GlobalConstants.NoUsers;

            //foreach (var user in users)
            //{
            //    if (user.Status != MemberStatus.Inactive)
            //        strBuilder.Append(this.FormatShort(user));
            //}

            return strBuilder.ToString();
        }

        //private string FormatShort(IUser user) => $"Username: {user.Username} || CheckedOut Books: {user.CheckedOutBooks.Count} || Reserved Books: {user.ReservedBooks.Count}\r\n";

        public string CenterStringWithSymbols(string text, char symbol)
        {
            int numberOfSymbols = (GlobalConstants.MaxFieldLength - text.Length) / 2;
            string symbols = new String(symbol, numberOfSymbols);
            return GlobalConstants.NewLine + symbols + text + symbols + GlobalConstants.NewLine;
        }

    }
}
