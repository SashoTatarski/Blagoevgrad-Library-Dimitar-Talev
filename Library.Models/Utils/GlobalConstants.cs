namespace Library.Models.Utils
{
    public static class GlobalConstants
    {
        // Commands        
        public const string NullOrEmptyNameErrorMessage = "Name cannot be null or empty";
        public const string NullCollectionOfParameters = "Collection of parameteres cannot be null";
        public const string InvalidCommandErrorMessage = "Invalid command name: {0}";
        public const string CompanyExistsErrorMessage = "Company {0} already exists";

        //Json Paths
        public const string catalogFilepath = @"..\..\..\..\catalog.json";
        public const string usersFilepath = @"..\..\..\..\users.json";
        public const string librariansFilepath = @"..\..\..\..\librarians.json";

        //Numbers
        public const decimal Fee = 0.5m;

        //Messages
        public const string OverdueMessage = "You have overdue book!\r\nID: {0} || Title: {1} || Author: {2}\r\nYou are late to return it by {3} days!";

    }
}
