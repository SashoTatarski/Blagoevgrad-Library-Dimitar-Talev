﻿using Library.Models.Contracts;
using System.Collections.Generic;

namespace Library.Services.Contracts
{
    public interface ILibrarySystem
    {
        void AssignFee(IUser user);
        void CheckForOverdueBooks();
        void CheckForOverdueReservations();
        void CheckIfMaxQuotaReached(List<IBook> books);
        void DisplayMessageForOverdueBooks(IUser user);
        void DisplayMessageForOverdueReservations(IUser user);
    }
}
