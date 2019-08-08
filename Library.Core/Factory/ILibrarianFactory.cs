﻿using Library.Models.Contracts;
using System;
using System.Collections.Generic;
using System.Text;

namespace Library.Core.Factory
{
    public interface ILibrarianFactory
    {
        ILibrarian CreateLibrarian(string username, string password);
    }
}