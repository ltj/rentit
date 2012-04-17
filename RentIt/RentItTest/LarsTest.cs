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
    }
}
