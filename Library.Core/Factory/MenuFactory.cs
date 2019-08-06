using Library.Models.Contracts;
using Services.Contracts;
using System;
using System.Collections.Generic;
using System.Text;

namespace Library.Core.Factory
{
    public class MenuFactory : IMenuFactory
    {
        private readonly IAccountManager _account;
        public MenuFactory(IAccountManager account)
        {
            this._account = account;
        }
        public string GenerateMenu(IAccount account)
        {
            var strBuilder = new StringBuilder();

            if (_account.CurrentAccount is null)
            {
                strBuilder.AppendLine();
                strBuilder.AppendLine("1. LogIn");
                return strBuilder.ToString();
            }
            else
            {
                strBuilder.AppendLine();

                var counter = 1;
                foreach (var command in _account.CurrentAccount.AllowedCommands)
                {
                    strBuilder.AppendLine($"{counter}. {command}");
                    counter ++;
                }
                return strBuilder.ToString();
            }
        }
    }
}
