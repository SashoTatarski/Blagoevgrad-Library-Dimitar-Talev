using System;

namespace Library.Models.Utils
{
    public static class GlobalConstants
    {
        // Commandnames
        public const string CheckOutBook = "----------CheckOut Book----------\r\n";
        public const string AddBook = "----------Add Book----------\r\n";
        public const string LogIn = "----------Log In----------\r\n";
        public const string ReserveBook = "----------Reserve Book----------\r\n";
        public const string RegisterUser = "----------Register User----------\r\n";
        public const string RegisterLibrarian = "----------Register Librarian----------\r\n";
        public const string ReturnBook = "----------Return Book----------\r\n";
        public const string RemoveUser = "----------Remove User----------\r\n";
        public const string EditBook = "----------Edit Book----------\r\n";
        public const string RemoveBook = "----------Remove Book----------\r\n";
        public const string Search = "----------Search For Book----------\r\n";
        public const string Travel = "----------Travel In Time----------\r\n";
        public const string View = "----------View Accounts----------\r\n";

        // Commands     
        public const string NoSuchUserName = "No such username!";
        public const string InvalidPassword = "Wrong password!";
        public const string RemoveUserInvalidUserName = "Invalid username!";
        public const string ReserveBookAlreadyCheckedout = "Book is already checked out and reserved for another user afterwards!";
        public const string ReservedBookAlreadyReservedOther = "Book is already reserved by another user!";
        public const string ReservedBookAlreadyReserved = "You have already reserved this book!";
        public const string CheckoutBookAlreadyRes = "Book is already reserved!";
        public const string CheckoutBookAlreadyChecked = "Book is already checked out!";


        //Errors
        public const string NoSuchUser = "No such user!";
        public const string NoUsers = "There are no users!";
        public const string InvalidID = "Invalid ID!";
        public const string InvalidParameter = "Invalid parameter!";
        public const string CannotRemoveIssuedBook = "Cannot remove chechedout/reserved book!";
        public const string RemoveUserError = "Cannot remove user who has checkedout/reserved books!";

        //Success
        public const string CheckoutBookSuccess = "Successfully checked of the book:";
        public const string EditBookSuccess = "Successfully edited the book:";
        public const string SuccessLogIn = "Successfully logged in as:";
        public const string SuccessLogOut = "Successfully logged out as:";
        public const string LibrarianRegisterSuccess = "Successfully created new librarian account:";
        public const string UserRegisterSuccess = "Successfully created new user account:";
        public const string RemoveBookSuccess = "Successfully removed the book:";
        public const string RemoveUserSuccess = "Successfully removed the user:";
        public const string ReturnBookSuccessMsg = "Successfully returned the book:";
        public const string ReservedBookSuccessMsg = "You have reserved the book:";
        public const string AddBookSuccess = "Successfully added the book:";
        public const string TravelSuccess = "Success! Today is";


        //Json Paths
        public const string catalogFilepath = @"..\..\..\..\catalog.json";
        public const string usersFilepath = @"..\..\..\..\users.json";
        public const string librariansFilepath = @"..\..\..\..\librarians.json";

        //Numbers
        public const decimal Fee = 0.5m;
        public const int MaxBookQuota = 5;
        public const int MaxCheckoutDays = 10;
        public const int MaxReserveDays = 5;

        //Format
        public static string Delimiter = new string('=', 70);
        public static string MiniDelimiter = new string('-', 70);


        //Messages
        public const string OverdueMessage = "You have overdue book!\r\nID: {0} || Title: {1} || Author: {2}\r\nYou are late to return it by {3} days!";
        public const string MaxQuotaReached = "You have reached the max quota of 5 books!";

    }
}
