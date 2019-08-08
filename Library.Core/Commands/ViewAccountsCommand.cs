using Library.Core.Contracts;
using Library.Models.Contracts;
using Library.Services.Contracts;
using Services.Contracts;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;

namespace Library.Core.Commands
{
    public class ViewAccountsCommand : ICommand
    {
        private readonly IAuthenticationManager _account;
        private readonly IDatabaseService _service;
        private readonly IConsoleRenderer _renderer;

        public ViewAccountsCommand(IAuthenticationManager account, IDatabaseService service, IConsoleRenderer renderer)
        {
            _account = account;
            _service = service;
            _renderer = renderer;
        }

        public string Execute()
        {
            //var currentAccount = (ILibrarian)_account.CurrentAccount;

            //var allUsers = _service.ReadUsers();

            //foreach (var user in allUsers)
            //{
            //    CheckedoutBooks(user);
            //    ReservedBooks(user);
            //    Fees(user);
            //}

            return "";
        }


        private static void Fees(IUser user)
        {
            Console.WriteLine($"\r\nOutstanding fees: ");
            if (user.LateFees == 0.0m)
                Console.WriteLine($"{user.Username} has no fees.");
            else
                Console.WriteLine($"Fees: {user.LateFees.ToString("C", CultureInfo.CurrentCulture)}\r\n");
        }

        private static void ReservedBooks(IUser user)
        {
            Console.WriteLine($"\r\nBooks reservations: ");
            if (user.ReservedBooks.Any())
            {
                foreach (var book in user.ReservedBooks)
                    Console.WriteLine($"ID: {book.ID} || Author: {book.Author} || Title: {book.Title} || ISBN: {book.ISBN} || Year: {book.Year} || Reserved until: {book.ResevedDate.ToString("dd MMMM yyyy")}");
            }
            else
            {
                Console.WriteLine($"{user.Username} has no book reservations!");
            }
        }

        private static void CheckedoutBooks(IUser user)
        {
            Console.Write($"\r\nChecked out books by: {user.Username}");
            if (user.CheckedOutBooks.Any())
            {
                // If a book is overdue it's printed in red
                foreach (var book in user.CheckedOutBooks)
                {
                    if (book.DueDate < DateTime.Now)
                        Console.ForegroundColor = ConsoleColor.Red;

                    Console.WriteLine($"\r\nID: {book.ID} || Author: {book.Author} || Title: {book.Title} || ISBN: {book.ISBN} || Year: {book.Year} || Checkout date: {book.CheckoutDate.ToString("dd MMMM yyyy")} || Due date: {book.DueDate.ToString("dd MMMM yyyy")}");

                    Console.ResetColor();
                }
            }
            else
            {
                Console.WriteLine($"\r\n{user.Username} does not have any checked out books!");
            }
        }
    }
}
