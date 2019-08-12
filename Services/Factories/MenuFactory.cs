using Library.Models.Contracts;
using Services.Contracts;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Library.Services.Factory
{
    public class MenuFactory : IMenuFactory
    {
        private readonly IAuthenticationManager _account;

        public MenuFactory(IAuthenticationManager account)
        {
            _account = account;
        }

        public string GenerateMenu(IAccount account)
        {
            var strBuilder = new StringBuilder();

            if (_account.CurrentAccount is null)
            {
                strBuilder.AppendLine();
                strBuilder.AppendLine("1. LogIn");
                strBuilder.AppendLine("2. Exit");
                return strBuilder.ToString();
            }
            else
            {
                strBuilder.AppendLine();

                var counter = 1;
                foreach (var command in _account.CurrentAccount.AllowedCommands)
                {
                    strBuilder.AppendLine($"{counter}. {command}");
                    counter++;
                }
                return strBuilder.ToString();
            }
        }

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
