using Library.Models.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Library.Web.Models.AccountManagement
{
    public class UserViewModel
    {
        public string Username { get; set; }

        public string UserId { get; set; }

        public DateTime MembershipStartDate { get; set; }

        public DateTime MembershipEndDate { get; set; }

        public double Wallet { get; set; }
        public AccountStatus Status { get; set; }
    }
}
