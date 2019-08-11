using Library.Models.Contracts;
using Library.Models.Models;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;
using System;
using System.Collections.Generic;
using System.Text;

namespace Library.Models.Tests.AccountTests
{
    [TestClass]
    public class Properties_Should
    {
        [TestMethod]
        public void MemberStatus()
        {
            var sut = new User("username", "password");
            sut.Status = Enums.MemberStatus.Active;

            Assert.AreEqual(sut.Status, Enums.MemberStatus.Active);
        }

        [TestMethod]
        public void CheckedOutBooks()
        {
            var mockBook = new Mock<IBook>();

            var sut = new User("username", "password");
            sut.CheckedOutBooks.Add(mockBook.Object);

            Assert.IsTrue(sut.CheckedOutBooks.Count > 0);
        }

        [TestMethod]
        public void ReservedBooks()
        {
            var mockBook = new Mock<IBook>();

            var sut = new User("username", "password");
            sut.ReservedBooks.Add(mockBook.Object);

            Assert.IsTrue(sut.ReservedBooks.Count > 0);
        }

        //[TestMethod]
        //public void ReservedBookMessages()
        //{
        //    var mockBook = new Mock<IBook>();

        //    var sut = new User("username", "password");
        //    sut.ReservedBookMessages.Add("late book message");

        //    Assert.IsTrue(sut.ReservedBookMessages.Count > 0);
        //}

        //[TestMethod]
        //public void OverDueMessages()
        //{
        //    var mockBook = new Mock<IBook>();

        //    var sut = new User("username", "password");
        //    sut.OverdueMessages.Add("late book message");

        //    Assert.IsTrue(sut.OverdueMessages.Count > 0);
        //}

        [TestMethod]
        public void LateFees()
        {
            var mockBook = new Mock<IBook>();

            var sut = new User("username", "password");
            sut.LateFees = 10m;

            Assert.IsTrue(sut.LateFees > 0);
        }        

    }
}
