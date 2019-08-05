using Library.Models.Enums;
using System.Collections.Generic;

namespace Library.Models.Contracts
{
    public interface IMember : IAccount
    {
        string CardNumber { get; }
        MemberStatus Status { get; set; }
        ICollection<IBook> CheckedOutBooks { get; }
        ICollection<IBook> ReservedBooks { get; }
    }
}
