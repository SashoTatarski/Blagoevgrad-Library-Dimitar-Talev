using Library.Models.Enums;
using Library.Models.Models;
using Library.Services.Contracts;
using Services.Contracts;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Services
{
    public class Search : ISearch
    {
        private readonly IDatabaseService _service;

        public Search(IDatabaseService service)
        {
            _service = service;
        }
        
        public void ListAllBooks() => DisplaySearchResults(_service.ReadBooks().Select(x => x));
        public void SearchByAuthor()
        {
            Console.Write("Enter author's name: ");
            string authorName = Console.ReadLine();

            var searchResults = _service.ReadBooks().Where(x => x.Author.Contains(authorName));

            DisplaySearchResults(searchResults);
        }

        public void SearchByTitle()
        {
            Console.Write("Enter title: ");
            string title = Console.ReadLine();

            var books = _service.ReadBooks();

            var searchResults = from book in books
                                where book.Title.StartsWith(title)
                                select book;

            DisplaySearchResults(searchResults);
        }

        public void SearchByYear()
        {
            Console.Write("Enter year: ");
            int year = int.Parse(Console.ReadLine());

            var searchResults = _service.ReadBooks().Where(x => x.Year == year);
            DisplaySearchResults(searchResults);
        }

        public void SearchBySubject()
        {
            Console.Write("Enter subject name: ");
            string subject = Console.ReadLine();

            var searchResults = _service.ReadBooks().Where(x => x.Genre.Contains(subject));
            DisplaySearchResults(searchResults);
        }

        public void DisplaySearchResults(IEnumerable<Book> searchResults)
        {
            if (searchResults.Any())
            {
                foreach (var item in searchResults)
                {
                    if (item.Status == BookStatus.Checkedout || item.Status == BookStatus.Reserved)
                        Console.ForegroundColor = ConsoleColor.Red;

                    if (item.Status == BookStatus.Available)
                        Console.ForegroundColor = ConsoleColor.Green;

                    Console.WriteLine($"\r\nID: {item.ID} || Author: {item.Author} || Title: {item.Title} || ISBN: {item.ISBN} ||  Subject: {item.Genre} || Publisher: {item.Publisher} || Year: {item.Year} || Rack:{item.Rack} || Status: {item.Status}");

                    Console.ResetColor();
                }
            }
            else
            {
                Console.WriteLine($"No books on found!");
            }
        }
    }
}
