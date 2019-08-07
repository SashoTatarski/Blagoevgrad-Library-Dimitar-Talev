using Library.Core.Contracts;
using Library.Core.Factory;
using Library.Services.Contracts;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Library.Core.Commands
{
    public class RemoveUserCommand : ICommand
    {
        private readonly IDatabaseService _service;
        private readonly IUserFactory _userfactory;

        public RemoveUserCommand(IUserFactory userfactory, IDatabaseService service)
        {
            _userfactory = userfactory;
            _service = service;
        }

        public string Execute()
        {
            var allusers = _service.ReadUsers();

            if (!allusers.Any())
                throw new ArgumentNullException("There are no users!");

            foreach (var user in allusers)
                Console.WriteLine($"\r\nUsername: {user.Username} || Password: {user.Password}");

            Console.Write("\r\nSelect username to delete or type 'exit' to go back: ");
            var input = Console.ReadLine();

            _service.RemoveUser(input);

            return $"User {input} removed!";
        }
    }
}
