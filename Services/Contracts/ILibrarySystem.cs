using System;
using System.Collections.Generic;
using System.Text;

namespace Library.Services.Contracts
{
    public interface ILibrarySystem
    {
        void CheckForOverdueBooks();
        void CheckForOverdueReservations();
    }
}
