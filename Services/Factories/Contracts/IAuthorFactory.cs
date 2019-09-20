﻿using Library.Models.Models;
using System.Threading.Tasks;

namespace Library.Services.Factories.Contracts
{
    public interface IAuthorFactory
    {
        Task<Author> CreateAuthor(string authorName);
    }
}
