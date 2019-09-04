using Library.Models.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Library.Web.Models
{
    public class UserViewModel
    {
        public UserViewModel(User user)
        {
            this.Username = user.Username;
        }

        public string Username { get; set; }
    }
}
