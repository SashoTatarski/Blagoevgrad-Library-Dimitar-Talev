using Library.Core.Contracts;
using Library.Models.Utils;
using Library.Services.Contracts;
using Library.Services.Factories.Contracts;
using System;
using System.Collections.Generic;

namespace Library.Core.Commands
{
    public class EditBookCommand : ICommand
    {
        private readonly IConsoleRenderer _renderer;
        private readonly IBookManager _bookManager;
        private readonly IConsoleFormatter _formatter;
        private readonly IMenuFactory _menuFac;

        public EditBookCommand(IConsoleRenderer renderer, IBookManager bookManager, IConsoleFormatter formatter, IMenuFactory menuFac)
        {
            _renderer = renderer;
            _bookManager = bookManager;
            _formatter = formatter;
            _menuFac = menuFac;
        }

        public string Execute()
        {
            _renderer.Output(_formatter.CenterStringWithSymbols(GlobalConstants.EditBook, GlobalConstants.MiniDelimiterSymbol));
            _renderer.Output(_formatter.CenterStringWithSymbols(GlobalConstants.ChooseBook, '.'));

            // Show all the books user can select from
            _bookManager.ListAllBooks();

            // BookID Input
            var bookId = int.Parse(_renderer.InputParameters("ID"));

            // BookID validation
            //if (bookID < 1 || bookID > _bookManager.GetLastBookID())
            //    throw new ArgumentException(GlobalConstants.InvalidID);

            var bookToEdit = _bookManager.FindBook(bookId);

            // Generate menu with parameters
            var parameters = new List<string> { "Author", "Title", "ISBN code", "Genre", "Publisher", "Year", "Rack" };

            _renderer.Output(_menuFac.GenerateMenu(parameters, "parameter to edit:"));

            var parameterNumbers = _renderer.InputParameters("parameters number (split by ,)");

            var parameterNumbersList = parameterNumbers.Split(new string[] { ",", ", " }, StringSplitOptions.None);

            foreach (var parameter in parameterNumbersList)
            {
                switch (parameter)
                {
                    case "1":
                        _renderer.Output($"The old author name was: {bookToEdit.Author.Name}{GlobalConstants.NewLine}");

                        var authorName = _renderer.InputParameters("new author name",
                    s => s.Length < 1 || s.Length > 40);

                        _bookManager.UpdateBookAuthor(bookId, authorName);
                        break;

                    case "2":
                        _renderer.Output($"The old title was: {bookToEdit.Title}{GlobalConstants.NewLine}");

                        var title = _renderer.InputParameters("new title",
                            s => s.Length < 1 || s.Length > 100);

                        _bookManager.UpdateBookTitle(bookId, title);
                        break;

                    case "3":
                        _renderer.Output($"The old ISBN was: {bookToEdit.ISBN}{GlobalConstants.NewLine}");

                        var isbn = _renderer.InputParameters("new ISBN code");
                        _bookManager.UpdateBookISBN(bookId, isbn);
                        break;

                    case "4":
                    case "5":
                    case "6":
                    case "7":
                    default:
                        throw new ArgumentException(GlobalConstants.InvalidParameter);
                }
            }
            

            

            //

            //var genres = _renderer.InputParameters("new genre",
            //    g => g.Length < 1 || g.Length > 40);

            //var publisher = _renderer.InputParameters("new publisher",
            //    g => g.Length < 1 || g.Length > 40);

            //var year = int.Parse(_renderer.InputParameters("new year", y => int.Parse(y) < 1 || int.Parse(y) > DateTime.Now.Year));

            //var rack = int.Parse(_renderer.InputParameters("new rack", r => int.Parse(r) < 1));


          //  _bookManager.UpdateBook(bookId, authorName, title, isbn, genres, publisher, year, rack);

            //var bookToEdit = _bookManager.FindBook(bookId);

            //return _formatter.FormatCommandMessage(GlobalConstants.EditBookSuccess, _formatter.Format(bookToEdit));
            return "ok";
        }
    }
}
