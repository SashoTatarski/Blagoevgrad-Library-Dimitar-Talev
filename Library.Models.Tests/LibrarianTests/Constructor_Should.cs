using Library.Models.Models;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Collections.Generic;
using System.Text;

namespace Library.Models.Tests.LibrarianTests
{
    [TestClass]
    public class Constructor_Should
    {
        [TestMethod]
        public void CorrectlyAssignUserName()
        {
            var userName = "username";

            var sut = new Librarian(userName, "password");

            Assert.AreEqual("username", sut.Username);
        }

        [TestMethod]
        public void CorrectlyAssignPassword()
        {
            var password = "password";

            var sut = new Librarian("username", password);

            Assert.AreEqual(password, sut.Password);
        }

        [TestMethod]
        [ExpectedException(typeof(NullReferenceException))]
        public void UserNameNull()
        {
            string librarian = null;

            var sut = new Librarian(librarian, "password");
        }

        [TestMethod]
        [ExpectedException(typeof(NullReferenceException))]
        public void PasswordNull()
        {
            string password = null;

            var sut = new Librarian("username", password);
        }
    }
}
