using RentIt;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using Microsoft.VisualStudio.TestTools.UnitTesting.Web;
// -----------------------------------------------------------------------
// <copyright file="LarsTest.cs" company="">
// TODO: Update copyright text.
// </copyright>
// -----------------------------------------------------------------------

namespace RentItTest
{
    using System;
    using System.Collections.Generic;
    using System.Diagnostics;
    using System.Linq;
    using System.Text;

    using Microsoft.VisualStudio.TestTools.UnitTesting;

    using RentIt;

    [TestClass]
    public class LarsTest
    {
        [TestMethod]
        public void TestMethod1()
        {
            RentItClient client = new RentItClient();

            Account acc = client.ValidateCredentials(new AccountCredentials() { UserName = "publishCorp", HashedPassword = "7110EDA4D09E062AA5E4A390B0A572AC0D2C0220" });

            Assert.AreNotEqual(null, acc);
        }

        /// <summary>
        ///A test for CreateNewUser
        ///</summary>
        // TODO: Ensure that the UrlToTest attribute specifies a URL to an ASP.NET page (for example,
        // http://.../Default.aspx). This is necessary for the unit test to be executed on the web server,
        // whether you are testing a page, web service, or a WCF service.
        [TestMethod()]
        [ExpectedException(typeof(UserCreationException))]
        public void CreateNewUserTest() {
            RentItClient target = new RentItClient();
            Account newAccount = new Account {
                UserName = "fyfeluf",
                FullName = "Test Testesen",
                Email = "test@test.com",
                HashedPassword = "1234"
            };
            bool expected = true;
            bool actual;
            actual = target.CreateNewUser(newAccount);
            Assert.AreEqual(expected, actual);
            target.CreateNewUser(newAccount);
        }

        /// <summary>
        ///A test for DeleteAccount
        ///</summary>
        // TODO: Ensure that the UrlToTest attribute specifies a URL to an ASP.NET page (for example,
        // http://.../Default.aspx). This is necessary for the unit test to be executed on the web server,
        // whether you are testing a page, web service, or a WCF service.
        [TestMethod()]
        public void DeleteAccountTest() {
            RentItClient target = new RentItClient();
            AccountCredentials credentials = new AccountCredentials {
                UserName = "fyfeluf",
                HashedPassword = "1234"
            };
            bool expected = true;
            bool actual;
            actual = target.DeleteAccount(credentials);
            Assert.AreEqual(expected, actual);
        }
    }
}
