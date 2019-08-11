using Library.Models.Models;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Collections.Generic;
using System.Text;

namespace Library.Models.Tests.LibrarianTests
{
    [TestClass]
    public class Properties_Should
    {
        [TestMethod]
        public void AllowedCommands()
        {
            var sut = new Librarian("username", "password");

            Assert.IsNotNull(sut.AllowedCommands);
            Assert.IsInstanceOfType(sut.AllowedCommands, typeof(IEnumerable<string>));
        }
    }
}
