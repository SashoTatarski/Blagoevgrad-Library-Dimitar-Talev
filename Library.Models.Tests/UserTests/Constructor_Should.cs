using Library.Models.Models;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Collections.Generic;
using System.Text;

namespace Library.Models.Tests.UserTests
{
    [TestClass]
    public class Constructor_Should
    {
        [TestMethod]
        public void CorrectlyAssignUserName()
        {
            var userName = "username";
           
            var sut = new User(userName, "password");

            Assert.AreEqual("username", sut.Username);
        }

        [TestMethod]
        public void CorrectlyAssignPassword()
        {            
            var password = "password";

            var sut = new User("username", password);

            Assert.AreEqual(password, sut.Password);
        }

        [TestMethod]
        [ExpectedException(typeof(NullReferenceException))]
        public void UserNameNull()
        {
            string userName = null;

            var sut = new User(userName, "password");            
        }

        [TestMethod]
        [ExpectedException(typeof(NullReferenceException))]
        public void PasswordNull()
        {
            string password = null;

            var sut = new User("username", password);
        }
    }
}
