using Library.Models.Contracts;
using Library.Models.Enums;
using System;
using System.Collections.Generic;
using System.Text;

namespace Library.Models.Models
{
    public class User : Account, IUser
    {
        public User(string username, string password) : base(username, password)
        {
            this.Status = MemberStatus.Active;
            this.CheckedOutBooks = new List<IBook>();
            this.ReservedBooks = new List<IBook>();
            this.AllowedCommands = new List<string>
            {
                "CheckOutBook",
                "ReturnBook",
                "RenewBook",
                "ReserveBook",
                "RemoveReservation",
                "Search",
                "ViewAccount",
                "LogOut"
            };
        }

        public override IEnumerable<string> AllowedCommands { get; }
        public MemberStatus Status { get; set; }

        public List<IBook> CheckedOutBooks { get; }

        public List<IBook> ReservedBooks { get; }

        public List<string> ReservedBookMessage { get; } = new List<string>();

        public string OverdueMessage { get; set; }

        public decimal LateFees { get; set; }

    }
}
