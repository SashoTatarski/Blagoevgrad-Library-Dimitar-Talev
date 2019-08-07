using Library.Core.Contracts;
using Library.Services.Contracts;
using Services.Contracts;
using System;
using System.Collections.Generic;
using System.Text;

namespace Library.Core.Commands
{
    public class SearchBookCommand : ICommand
    {
        private readonly ISearch _search;

        public SearchBookCommand(ISearch search)
        {            
            _search = search;
        }

        public string Execute()
        {
            string input;
            do
            {
                Console.WriteLine("\n\r=============================================================");
                Console.WriteLine("1.Search by Author");
                Console.WriteLine("2.Search by Title");
                Console.WriteLine("3.Search by Year");
                Console.WriteLine("4.Search by Subject");
                Console.WriteLine("5.List all books in the library");
                Console.WriteLine("6.Exit Search Engine");
                Console.WriteLine("=============================================================");
                input = Console.ReadLine();

                switch (input)
                {
                    case "1":
                        _search.SearchByAuthor();
                        break;
                    case "2":
                        _search.SearchByTitle();
                        break;
                    case "3":
                        _search.SearchByYear();
                        break;
                    case "4":
                        _search.SearchBySubject();
                        break;
                    case "5":
                        _search.ListAllBooks();
                        break;
                    default:
                        break;
                }

            } while (input != "6");          

            return "";
        }
    }
}
