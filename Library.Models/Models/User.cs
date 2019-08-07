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
        }

        public override IEnumerable<string> AllowedCommands => new List<string>
            {
                "checkoutbook",
                "returnbook",
                "renewbook",
                "reserveBook",
                "removereservation",
                "search",
                "viewaccount",
                "logout"
            };
        public MemberStatus Status { get; set; }

        public List<IBook> CheckedOutBooks { get; private set; }

        public List<IBook> ReservedBooks { get; }

        public List<string> ReservedBookMessage { get; } = new List<string>();

        public string OverdueMessage { get; set; }

        public decimal LateFees { get; set; }

        public void AddToCheckoutBooks(IBook book)
        {
            this.CheckedOutBooks.Add(book);
            book.Status = BookStatus.Checkedout;
        }

        public void RemoveFromReservedBooks(IBook book) => this.ReservedBooks.Remove(book);

    }
}
