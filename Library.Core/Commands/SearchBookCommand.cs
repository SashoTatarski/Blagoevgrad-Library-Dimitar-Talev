using Library.Core.Contracts;
using Library.Models.Utils;
using Library.Services.Contracts;
using Library.Services.Factories.Contracts;
using System;
using System.Collections.Generic;

namespace Library.Core.Commands
{
    public class SearchBookCommand : ICommand
    {
        private readonly IMenuFactory _menuFactory;
        private readonly IConsoleRenderer _renderer;
        private readonly IBookManager _bookManager;
        private readonly IConsoleFormatter _formatter;

        public SearchBookCommand(IMenuFactory menuFactory, IConsoleRenderer renderer, IBookManager bookManager, IConsoleFormatter formatter)
        {
            _menuFactory = menuFactory;
            _renderer = renderer;
            _bookManager = bookManager;
            _formatter = formatter;
        }

        public string Execute()
        {
            _renderer.Output(GlobalConstants.Search);

            var searchParameters = new List<string> { "Title", "Author", "Genre", "Publisher", "Year", "Show all", "Exit" };
            _renderer.Output(_menuFactory.GenerateMenu(searchParameters));

            var number = int.Parse(_renderer.Input());
            var parameter = this.GetSearchParameterByNumber(number, searchParameters);

            string searchBy = String.Empty;

            if (parameter == "Exit")
                throw new ArgumentException("");

            if (parameter != "Show all")
                searchBy = _renderer.InputParameters("search pattern");

            var searchResult = _bookManager.GetSearchResult(parameter, searchBy);

            return _formatter.FormatListOfBooks(searchResult);            
        }

        private string GetSearchParameterByNumber(int number, List<string> parameters)
        {
            if (number < 0 || number > parameters.Count + 1)
                throw new ArgumentException(GlobalConstants.InvalidParameter);

            return parameters[number - 1];
        }
    }
}
