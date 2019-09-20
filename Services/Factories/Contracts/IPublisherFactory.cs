﻿using Library.Models.Models;
using System.Threading.Tasks;

namespace Library.Services.Factories.Contracts
{
    public interface IPublisherFactory
    {
        Task<Publisher> CreatePublisher(string name);
    }
}
