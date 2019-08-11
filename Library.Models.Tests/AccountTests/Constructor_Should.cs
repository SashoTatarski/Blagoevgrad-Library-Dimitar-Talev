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
    public class Constructor_Should
    {
        [TestMethod]
        public void CorrectlyAssignPassedValues()
        {
            var sut = new Mock<Account>("testuser");
            sut.SetupAllProperties();
            sut.Name = "test";
            Assert.AreEqual(sut.Name, "test");            
        }       
    }
}
