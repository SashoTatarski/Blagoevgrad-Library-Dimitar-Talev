using Library.Models.Contracts;
using System.Collections.Generic;

namespace Library.Services.Factory
{
    public interface IMenuFactory
    {
        string GenerateMenu(IAccount account);

        string GenerateMenu(List<string> parameters);
    }
}