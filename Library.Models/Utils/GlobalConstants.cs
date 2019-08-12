namespace Library.Models.Utils
{
    public static class GlobalConstants
    {
        // Commands     
        public const string NoSuchUserName = "No such username!";
        public const string InvalidPassword = "Wrong password!";
        public const string SuccessLogIn = "Succefully logged!";
        public const string SuccessLogOut = "succefully logged out!";
        public const string LibrarianRegisterSuccess = "Successfully created new librarian account: ";
        public const string UserRegisterSuccess = "Successfully created new user account: ";
        public const string RemoveBookSuccess = "You have successfully removed the book: ";
        public const string RemoveUserError = "You cannot remove user who has checkedout/reserved books";
        public const string RemoveUserInvalidUserName = "Invalid username!";
        public const string ReserveBookAlreadyCheckedout = "Book is already checked out and reserved for another user afterwards!";
        public const string ReservedBookAlreadyReservedOther = "Book is already reserved by another user!";
        public const string ReservedBookAlreadyReserved = "You have already reserved this book!";
        public const string ReservedBookSuccessMsg = "You have reserved the book:\r\n";
        public const string ReturnBookSuccessMsg = "Successfully returned : ";
        public const string CheckoutBookAlreadyRes = "Book is already reserved!";
        public const string CheckoutBookAlreadyChecked = "Book is already checked out!";
        public const string CheckoutBookInvalidID = "Invalid ID";    
                
        //Json Paths
        public const string catalogFilepath = @"..\..\..\..\catalog.json";
        public const string usersFilepath = @"..\..\..\..\users.json";
        public const string librariansFilepath = @"..\..\..\..\librarians.json";

        //Numbers
        public const decimal Fee = 0.5m;
        public const int MaxBookQuota = 5;

        //Messages
        public const string OverdueMessage = "You have overdue book!\r\nID: {0} || Title: {1} || Author: {2}\r\nYou are late to return it by {3} days!";
        public const string MaxQuotaReached = "You have reached the max quota of 5 books!";

    }
}
