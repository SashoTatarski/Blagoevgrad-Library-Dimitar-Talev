using Library.Services.Contracts;
using Library.Services.Factories.Contracts;
using System.Collections.Generic;
using System.Text;

namespace Library.Services.Factories
{
    public class MenuFactory : IMenuFactory
    {
        private readonly IConsoleFormatter _formatter;
        public MenuFactory(IConsoleFormatter formatter)
        {
            _formatter = formatter;
        }
        public string GenerateMenu(List<string> parameters, string parameter)
        {
            var strBuilder = new StringBuilder();
            var counter = 1;

            strBuilder.AppendLine(_formatter.CenterStringWithSymbols($"Choose {parameter}", '.'));

            foreach (var param in parameters)
            {
                strBuilder.AppendLine($"{counter}. {param}");
                counter++;
            }
            strBuilder.AppendLine();
            return strBuilder.ToString();
        }
    }
}