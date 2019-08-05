using Library.Models.Contracts;
using Library.Models.Enums;
using System;
using System.Collections.Generic;

namespace Library.Models.Models
{
    public class Member : Account, IMember, IAccount
    {
        public Member(string username, string password) : base(username, password)
        {
            this.Status = MemberStatus.Active;
            this.CheckedOutBooks = new List<IBook>();
            this.ReservedBooks = new List<IBook>();
        }

        public string CardNumber => throw new NotImplementedException();

        public MemberStatus Status { get => throw new NotImplementedException(); set => throw new NotImplementedException(); }

        public ICollection<IBook> CheckedOutBooks { get; private set; }

        public ICollection<IBook> ReservedBooks { get; private set; }
    }
}
