using Library.Core.Contracts;
using Library.Models.Utils;
using Library.Services.Contracts;
using System;

namespace Library.Core.Commands
{
    // SOLID: OPEN/CLOSE - We can add more commands, thus making the code open for extension
    public class AddBookCommand : ICommand
    {
        private readonly IBookManager _bookManager;
        private readonly IConsoleRenderer _renderer;
        private readonly IConsoleFormatter _formatter;

        public AddBookCommand(IBookManager bookManager, IConsoleRenderer renderer, IConsoleFormatter formatter)
        {
            _bookManager = bookManager;
            _renderer = renderer;
            _formatter = formatter;
        }

        public string Execute()
        {
            _renderer.Output(_formatter.CenterStringWithSymbols(GlobalConstants.AddBook, GlobalConstants.MiniDelimiterSymbol));

            // ASK: How to improve this since it exists also in EditBookCommand
            var authorName = _renderer.InputParameters("author name",
                s => s.Length < 1 || s.Length > 40);

            var title = _renderer.InputParameters("title",
                s => s.Length < 1 || s.Length > 100);

            var isbn = _renderer.InputParameters("ISBN code");

            var genres = _renderer.InputParameters("genres (split by ,)",
                g => g.Length < 1 || g.Length > 40);

            var publisher = _renderer.InputParameters("publisher",
                g => g.Length < 1 || g.Length > 40);

            var year = int.Parse(_renderer.InputParameters("year",
                y => int.Parse(y) < 1 || int.Parse(y) > DateTime.Now.Year));

            var rack = int.Parse(_renderer.InputParameters("rack",
                r => int.Parse(r) < 1));


            var bookToCreate = _bookManager.CreateBook(authorName, title, isbn, genres, publisher, year, rack);

            // return _formatter.FormatCommandMessage(GlobalConstants.AddBookSuccess, _formatter.Format(bookToCreate));
            return null;
        }
    }
}
