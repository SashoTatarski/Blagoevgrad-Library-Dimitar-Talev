using Library.Models.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Library.Web.Models
{
    public class AccountsViewModel
    {
        public List<UserViewModel> Users { get; set; }
        public AccountsViewModel(IEnumerable<User> users)
        {
            this.Users = new List<UserViewModel>();

            foreach (var user in users)
            {
                this.Users.Add(new UserViewModel(user));
            }
        }
    }
}
