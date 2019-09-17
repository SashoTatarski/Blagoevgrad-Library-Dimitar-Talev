using Library.Database;
using Library.Models.Enums;
using Library.Models.Models;
using Library.Models.Utils;
using Library.Services.Contracts;
using Library.Services.Factories.Contracts;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Library.Services
{
    public class AccountManager : IAccountManager
    {
        private readonly LibraryContext _context;
        private readonly IAccountFactory _accountFac;
        public AccountManager(LibraryContext context, IAccountFactory accountFac)
        {
            _context = context;
            _accountFac = accountFac;
        }

     
    }
}
