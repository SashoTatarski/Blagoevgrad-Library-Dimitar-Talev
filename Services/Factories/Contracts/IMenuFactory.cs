using Library.Models.Contracts;
using System.Collections.Generic;

namespace Library.Services.Factories.Contracts
{
    public interface IMenuFactory
    {
        string GenerateMenu(List<string> parameters);
    }
}