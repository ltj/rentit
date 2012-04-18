namespace RentItTest
{
    using System;
    using System.ServiceModel;

    using RentIt;
    using Microsoft.VisualStudio.TestTools.UnitTesting;

    [TestClass]
    public class PerTest
    {
        /// <summary>
        ///A test for UpdateAccountInfo
        ///</summary>
        [TestMethod()]
        public void UpdateAccountInfoTest() {
            RentItClient target = new RentItClient();
            AccountCredentials credentials = new AccountCredentials {
                UserName = "per",
                HashedPassword = "7110eda4d09e062aa5e4a390b0a572ac0d2c0220"
            };
            Account account = target.GetAllCustomerData(credentials);
            string oldEmail = account.Email;
            account.Email += "+";

            bool expected = true;
            bool actual = target.UpdateAccountInfo(credentials, account);
            Assert.AreEqual(expected, actual);

            string newEmail = target.GetAllCustomerData(credentials).Email;
            Assert.AreEqual(oldEmail + "+", newEmail);
        }

        /// <summary>
        ///A test for AddCredits
        ///</summary>
        [TestMethod()]
        public void AddCreditsTest() {
            RentItClient target = new RentItClient();
            AccountCredentials credentials = new AccountCredentials {
                UserName = "per",
                HashedPassword = "7110eda4d09e062aa5e4a390b0a572ac0d2c0220"
            };
            int oldCredits = target.GetAllCustomerData(credentials).Credits;

            uint addAmount = 100;
            bool expected = true;
            bool actual = target.AddCredits(credentials, addAmount);
            Assert.AreEqual(expected, actual);

            int newCredits = target.GetAllCustomerData(credentials).Credits;
            Assert.AreEqual(oldCredits + addAmount, newCredits);
        }

        /// <summary>
        ///A test for UpdateMediaMetadata
        ///</summary>
        [TestMethod()]
        public void UpdateMediaMetadataTest() {
            RentItClient target = new RentItClient();
            MediaInfo newData = null;
            AccountCredentials credentials = null;
            bool expected = false;
            bool actual = target.UpdateMediaMetadata(newData, credentials);
            Assert.AreEqual(expected, actual);
            Assert.Inconclusive("Verify the correctness of this test method.");
        }

        /// <summary>
        ///A test for GetAllGenres
        ///</summary>
        [TestMethod()]
        public void GetAllGenresTest() {
            RentItClient target = new RentItClient();
            MediaType mediaType = MediaType.Any;
            string[] expected = new string[0];
            string[] actual = target.GetAllGenres(mediaType); //List<string> actual = target.GetAllGenres(mediaType);
            Assert.AreNotEqual(expected, actual);
        }

        /// <summary>
        ///A test for DeleteMedia
        ///</summary>
        [TestMethod()]
        [ExpectedException(typeof(FaultException))]
        public void DeleteMediaTest1() {
            RentItClient target = new RentItClient();
            int mediaId = 1; // "The Room"
            AccountCredentials credentials = new AccountCredentials {
                UserName = "publishCorp",
                HashedPassword = "7110eda4d09e062aa5e4a390b0a572ac0d2c0220"
            };
            bool expected = true;
            bool actual = target.DeleteMedia(mediaId, credentials);
            Assert.AreEqual(expected, actual);
            target.GetMovieInfo(mediaId);
        }
    }
}
