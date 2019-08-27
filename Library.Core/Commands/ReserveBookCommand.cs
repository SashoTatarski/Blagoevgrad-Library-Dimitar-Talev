using Library.Core.Contracts;
using Library.Database;
using Library.Models.Contracts;
using Library.Models.Enums;
using Library.Models.Utils;
using Library.Services.Contracts;
using Services.Contracts;
using System;
using System.Linq;

namespace Library.Core.Commands
{
    public class ReserveBookCommand : ICommand
    {
        private readonly IAuthenticationManager _authentication;
        private readonly IBookManager _bookManager;
        private readonly IConsoleRenderer _renderer;
        private readonly IConsoleFormatter _formatter;
        private readonly ILibrarySystem _system;


        public ReserveBookCommand(IAuthenticationManager authentication, IBookManager bookManager, IConsoleRenderer renderer, IConsoleFormatter formatter, ILibrarySystem system)
        {
            _authentication = authentication;
            _bookManager = bookManager;
            _renderer = renderer;
            _formatter = formatter;
            _system = system;
        }

        public string Execute()
        {
            //_renderer.Output(GlobalConstants.ReserveBook);

            //var user = (IUser)_authentication.CurrentAccount;

            //// check if user has reserved 5 books already
            //_system.CheckCheckoutBooksQuota(user);

            //_bookManager.ListAllBooks();

            //// BookID Input
            //var bookId = int.Parse(_renderer.InputParameters("ID"));

            //var bookToReserve = _bookManager.FindBook(bookId);

            //// ASK: Improve this (by extracting a method) ?
            //// Check Book Status 
            //// if the status is Available
            //if (bookToReserve.Status == BookStatus.Available)
            //{
            //    _system.AddBookToReservedBooks(bookToReserve, user); //user.AddBookToReservedBooks(bookToReserve);

            //    _bookManager.UpdateStatus(bookToReserve, BookStatus.Reserved);

            //    return _formatter.FormatCommandMessage(GlobalConstants.ReservedBookSuccessMsg, _formatter.Format(bookToReserve));
            //}
            //// if the book is already checked out
            //else if (bookToReserve.Status == BookStatus.CheckedOut)
            //{
            //    //after the other user return the book, this user will have 5 days to take it

            //    _system.AddBookToCheckoutBooks(bookToReserve, user); //user.AddBookToReservedBooks(bookToReserve);

            //    _bookManager.UpdateStatus(bookToReserve, BookStatus.CheckedOut_and_Reserved);
            //    //_bookManager.UpdateBook(bookID, BookStatus.CheckedOut_and_Reserved, VirtualDate.VirtualToday, DateTime.MaxValue, true);
            //    //_accountManager.UpdateUser(user);

            //    return _formatter.FormatCommandMessage(GlobalConstants.ReservedBookSuccessMsg, _formatter.Format(bookToReserve));
            //}
            //else if (bookToReserve.Status == BookStatus.Reserved)
            //{
            //    //var suchBookInReservedBooks =  user.ReservedBooks.FirstOrDefault(b => b.Id == bookToReserve.Id);

            //    if (_system.ReservedByUser(user, bookToReserve))
            //        return _formatter.FormatCommandMessage(GlobalConstants.ReservedBookAlreadyReserved);
            //    else
            //        return _formatter.FormatCommandMessage(GlobalConstants.ReservedBookAlreadyReservedOther);
            //}
            //else
            //{
            //    return _formatter.FormatCommandMessage(GlobalConstants.ReserveBookAlreadyCheckedout);
            //}
            return null;
        }
    }
}
