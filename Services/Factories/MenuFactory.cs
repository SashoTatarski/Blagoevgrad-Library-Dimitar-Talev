using Library.Services.Factories.Contracts;
using System.Collections.Generic;
using System.Text;

namespace Library.Services.Factories
{
    public class MenuFactory : IMenuFactory
    {
        public string GenerateMenu(List<string> parameters)
        {
            var strBuilder = new StringBuilder();
            var counter = 1;

            foreach (var param in parameters)
            {
                strBuilder.AppendLine($"{counter}. {param}");
                counter++;
            }
            return strBuilder.ToString();
        }
    }
}
