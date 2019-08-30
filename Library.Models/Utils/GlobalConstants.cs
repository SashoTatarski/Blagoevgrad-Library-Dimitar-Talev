using System;

namespace Library.Models.Utils
{
    public static class GlobalConstants
    {
        // Commandnames
        public static string CheckOutBook = "Check Out Book";
        public static string AddBook = "Add Book";
        public static string LogIn = "Log In";
        public static string ReserveBook = "Reserve Book";
        public static string RegisterUser = "Register User";
        public static string RegisterLibrarian = "Register Librarian";
        public static string ReturnBook = "Return Book";
        public static string RemoveUser = "Remove User";
        public static string EditBook = "Edit Book";
        public static string RemoveBook = "Remove Book";
        public static string Search = "Search For Book";
        public static string Travel = "Travel In Time";
        public static string View = "View Accounts";

        // Static
        public static string NewLine = Environment.NewLine;

        // Commands     
        public const string NoSuchUserName = "No such username!";
        public const string InvalidPassword = "Wrong password!";
        public const string RemoveUserInvalidUserName = "Invalid username!";
        public const string ReserveBookAlreadyCheckedout = "Book is already checked out and reserved for another user afterwards!";
        public const string ReservedBookAlreadyReservedOther = "Book is already reserved by another user!";
        public const string ReservedBookAlreadyReserved = "You have already reserved this book!";
        public const string CheckoutBookAlreadyRes = "Book is already reserved!";
        public const string CheckoutBookAlreadyChecked = "Book is already checked out!";
        public const string ChooseBook = "Choose book by its ID";
        public const string ChooseParameter = "Choose parameter";

        //Errors
        public const string NoSuchUser = "No such user!";
        public const string NoUsers = "There are no users!";
        public const string InvalidID = "Invalid ID!";
        public const string InvalidParameter = "Invalid parameter!";
        public const string CannotRemoveIssuedBook = "Cannot remove chechedout/reserved book!";
        public const string RemoveUserError = "Cannot remove user who has checkedout/reserved books!";
        public const string BookToUpdateNull = "Book to update is null";
        public const string UserNameTaken = "This username is already taken";
        public const string AuthorNameLimit = "The author name should be less than 40 symbols!";
        public const string PublisherNameLimit = "The publisher name should be less than 40 symbols!";
        public const string BookYearLimit = "The publication year should be between 1629 and 2019";
        public const string BookTitleLimit = "The title should be less than 40 symbols!";
        public const string BookRackLimit = "The rack number cannot be zero or negative!";
        public const string GenreNameLimit = "The genre name should be less than 40 symbols!";

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
        public static int MaxFieldLength = 70;

        //Format
        public static char MiniDelimiterSymbol = '-';
        public static char DelimiterSymbol = '=';

        public static string Delimiter = new string(DelimiterSymbol, MaxFieldLength);
        public static string MiniDelimiter = new string(MiniDelimiterSymbol, MaxFieldLength);


        //Messages
        public const string OverdueMessage = "You have overdue book!\r\nID: {0} || Title: {1} || Author: {2}\r\nYou are late to return it by {3} days!";
        public const string MaxQuotaReached = "You have reached the max quota of 5 books!";
        public const string Goodbye = "Goodbye!";
        public const string DaysToSkip = "how many days you want to skip";



    }
}
