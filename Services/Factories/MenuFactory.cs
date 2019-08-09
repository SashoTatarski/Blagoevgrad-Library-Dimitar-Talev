﻿using Library.Models.Contracts;
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

        public void CheckAuthenticationForCommand(string commandAsString)
        {
            if (_account.CurrentAccount is null)
            {
                if (commandAsString.ToLower() != "exit" && commandAsString.ToLower() != "login")
                {
                    throw new ArgumentException("Invalid Command");
                }
            }
            else
            {
                bool check = false;
                foreach (var command in _account.CurrentAccount.AllowedCommands)
                {
                    if (command.Replace(" ", "").ToLower() == commandAsString)
                    {
                        check = true;
                        break;
                    }
                }
                if (!check)
                {
                    throw new ArgumentException("Invalid Command");
                }
            }
        }


    }
}