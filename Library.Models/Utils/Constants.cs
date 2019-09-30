using System;

namespace Library.Models.Utils
{
    public static class Constants
    {
        public const string DefaultRole = "user";

        // Commandnames        
        public const string CheckOutBook = "Check Out Book";
        public const string AddBook = "Add Book";
        public const string LogIn = "Log In";
        public const string ReserveBook = "Reserve Book";
        public const string RegisterUser = "Register User";
        public const string RegisterLibrarian = "Register Librarian";
        public const string ReturnBook = "Return Book";
        public const string RemoveUser = "Remove User";
        public const string EditBook = "Edit Book";
        public const string RemoveBook = "Remove Book";
        public const string Search = "Search For Book";
        public const string Travel = "Travel In Time";
        public const string View = "View Accounts";

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
        public const string ChooseUser = "Choose user by its username";
        public const string ChooseParameter = "Choose parameter";

        //Errors
        public const string UsernamePassIncorr = "username or password incorrect";
        public const string NoDefRoleDb = "No default role in DB";
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

        //Numbers
        public const decimal Fee = 0.5m;
        public const int MaxBookQuota = 5;
        public const int MaxCheckoutDays = 10;
        public const int MaxReserveDays = 5;
        public const int MaxFieldLength = 70;

        //Format
        public const char MiniDelimiterSymbol = '-';
        public const char DelimiterSymbol = '=';

        public static string Delimiter = new string(DelimiterSymbol, MaxFieldLength);
        public static string MiniDelimiter = new string(MiniDelimiterSymbol, MaxFieldLength);


        //Messages
        public const string MaxQuotaReached = "You have reached the max book quota!";
        public const string Goodbye = "Goodbye!";
        public const string DaysToSkip = "how many days you want to skip";
        public const string Welcome = "Welcome to Blagoevgrad Library - Dimitar Talev -";
        public const string RetBookSucc = "Book Successfully Returned";
        public const string ResBookSucc = "Book Successfully Reserved";
        public const string ChBookSucc = "Book Successfully Checked Out!";
        public const string ChBookMax = "You Cannot Checkout More Than 5 Books";
        public const string AcctDeact = "Account has been deactived";
        public const string AcctBan = "Account has been banned";
        public const string UsernameValid = "Username should be at least 5 symbols";
        public const string PasswordValid = "Password should be at least 5 symbols";
        public const string AcctCancelErr = "You have to return all books before cancelling your account";
        public const string AcctCancel = "You have successfully cancelled your account";
        public const string AcctCancelRetBks = "User must return all of his books";
    }
}
