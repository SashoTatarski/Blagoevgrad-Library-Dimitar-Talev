using System;
using System.Collections.Generic;
using System.Text;

namespace Library.Models
{
    public static class GlobalConstants
    {
        // Commands        
        public const string NullOrEmptyNameErrorMessage = "Name cannot be null or empty";
        public const string NullCollectionOfParameters = "Collection of parameteres cannot be null";
        public const string InvalidCommandErrorMessage = "Invalid command name: {0}";
        public const string CompanyExistsErrorMessage = "Company {0} already exists";
    }
}
