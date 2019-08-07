using Library.Core.Contracts;
using Library.Models.Contracts;
using Library.Services.Contracts;
using Services.Contracts;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Library.Core.Commands
{
    public class ReturnBookCommand : ICommand
    {
        private readonly IAccountManager _account;
        private readonly IDatabaseService _service;
        private readonly IConsoleRenderer _renderer;

        public ReturnBookCommand(IAccountManager account, IDatabaseService service, IConsoleRenderer renderer)
        {
            _account = account;
            _service = service;
            _renderer = renderer;
        }

        public string Execute()
        {
            var currentAccount = (IUser)_account.CurrentAccount;
            var users = _service.ReadUsers();
            var books = _service.ReadBooks();

            if (!currentAccount.CheckedOutBooks.Any())
                throw new ArgumentException("There are no books to return!");

            DisplayCheckedoutBooks(currentAccount);
            var input = int.Parse(_renderer.InputParameters("ID"));

            var user = users.FirstOrDefault(u => u.Username == currentAccount.Username);
            var bookToReturn = books.FirstOrDefault(b => b.ID == input);

            user.RemovedFromCheckedoutBooks(bookToReturn);

            _service.WriteBooks(books);
            _service.WriteUsers(users);

            var currBook = books.Where(b => b.ID == input).FirstOrDefault();
            return $"Successfully returned : {currBook.Title} - {currBook.Author}";
        }

        private static void DisplayCheckedoutBooks(IUser user)
        {
            Console.WriteLine("\r\nBooks you have checked out:");
            foreach (var book in user.CheckedOutBooks)
            {
                // If a book is overdue it's printed in red
                if (book.DueDate < DateTime.Now)
                    Console.ForegroundColor = ConsoleColor.Red;

                Console.WriteLine($"\r\nID: {book.ID} || Author: {book.Author} || Title: {book.Title} || ISBN: {book.ISBN} || Year: {book.Year} || Checkout date: {book.CheckoutDate.ToString("dd MMMM yyyy")} || Due date: {book.DueDate.ToString("dd MMMM yyyy")}");

                Console.ResetColor();
            }
        }
    }
}
