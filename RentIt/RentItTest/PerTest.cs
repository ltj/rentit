namespace RentItTest
{
    using System.Linq;
    using System.ServiceModel;

    using RentIt;
    using Microsoft.VisualStudio.TestTools.UnitTesting;

    [TestClass]
    public class PerTest
    {
        /*
         * DB changes:
         * Media:
         *     id 3: active = false
         * Album:
         *     id 10: albumartist + "+"
         * Movie:
         *     id 1: director + "+"
         * Book:
         *     id 75: author + "+"
         * Account:
         *     user "per": email + "+"
         * UserAccount:
         *     user "per": credits + 100
         */

        /// <summary>
        ///A test for UpdateAccountInfo
        ///</summary>
        [TestMethod]
        public void UpdateAccountInfoTest1() {
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

        /*
        /// <summary>
        ///A test for UpdateAccountInfo
        ///</summary>
        [TestMethod()]
        [ExpectedException(typeof(FaultException))]
        public void UpdateAccountInfoTest2() {
            RentItClient target = new RentItClient();
            AccountCredentials credentials = new AccountCredentials {
                UserName = "per",
                HashedPassword = "7110eda4d09e062aa5e4a390b0a572ac0d2c0220"
            };
            Account account = target.GetAllCustomerData(credentials);
            string oldEmail = account.Email;
            account.Email += "+";
            account.UserName = "jacob";

            target.UpdateAccountInfo(credentials, account); // InvalidCredentialsException
        }
        */

        /// <summary>
        ///A test for AddCredits
        ///</summary>
        [TestMethod]
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
        [TestMethod]
        public void UpdateMediaMetadataTest() {
            RentItClient target = new RentItClient();
            AccountCredentials credentials = new AccountCredentials {
                UserName = "publishCorp",
                HashedPassword = "7110eda4d09e062aa5e4a390b0a572ac0d2c0220"
            };

            int mediaId = 10; // Kraftwerk - "The Man-Machine" album
            AlbumInfo newAlbumData = target.GetAlbumInfo(mediaId);
            string albumArtist = newAlbumData.AlbumArtist;
            newAlbumData.AlbumArtist = newAlbumData.AlbumArtist == "Kraftwerk" ? "Craftwork" : "Kraftwerk";
            bool actual = target.UpdateMediaMetadata(newAlbumData, credentials);
            string updatedAlbumArtist = target.GetAlbumInfo(mediaId).AlbumArtist;
            Assert.IsTrue(actual);
            Assert.IsTrue(albumArtist == "Kraftwerk"? "Craftwork" == updatedAlbumArtist : "Kraftwerk" == updatedAlbumArtist);

            mediaId = 1; // "The Room"
            MovieInfo newMovieData = target.GetMovieInfo(mediaId);
            string director = newMovieData.Director;
            newMovieData.Director = newMovieData.Director == "Tommy Wiseau" ? "Timmy Wiseau" : "Tommy Wiseau";
            actual = target.UpdateMediaMetadata(newMovieData, credentials);
            string updatedDirector = target.GetMovieInfo(mediaId).Director;
            Assert.IsTrue(actual);
            Assert.IsTrue(director == "Tommy Wiseau" ? "Timmy Wiseau" == updatedDirector : "Tommy Wiseau" == updatedDirector);

            mediaId = 2; // "House of Leaves"
            BookInfo newBookData = target.GetBookInfo(mediaId);
            string author = newBookData.Author;
            newBookData.Author = newBookData.Author == "Mark Z. Danielewsky" ? "Mak Z. Danielewsky" : "Mark Z. Danielewsky";
            actual = target.UpdateMediaMetadata(newBookData, credentials);
            string updatedAuthor = target.GetBookInfo(mediaId).Author;
            Assert.IsTrue(actual);
            Assert.IsTrue(author == "Mark Z. Danielewsky" ? "Mak Z. Danielewsky" == updatedAuthor : "Mark Z. Danielewsky" == updatedAuthor);
        }

        /// <summary>
        ///A test for GetAllGenres
        ///</summary>
        [TestMethod]
        public void GetAllGenresTest() {
            RentItClient target = new RentItClient();
            MediaType mediaType = MediaType.Any;
            string[] expected = new string[0]; // empty list
            string[] actual = target.GetAllGenres(mediaType); // List<string> actual = target.GetAllGenres(mediaType);
            Assert.AreNotEqual(expected.Length, actual.Length); // list should not be empty

            mediaType = MediaType.Book;
            actual = target.GetAllGenres(mediaType);
            Assert.AreNotEqual(expected.Length, actual.Length); // list should not be empty

            mediaType = MediaType.Movie;
            actual = target.GetAllGenres(mediaType);
            Assert.AreNotEqual(expected.Length, actual.Length); // list should not be empty

            mediaType = MediaType.Album;
            actual = target.GetAllGenres(mediaType);
            Assert.AreNotEqual(expected.Length, actual.Length); // list should not be empty

            mediaType = MediaType.Song;
            actual = target.GetAllGenres(mediaType);
            Assert.AreNotEqual(expected.Length, actual.Length); // list should not be empty
        }

        /// <summary>
        ///A test for DeleteMedia
        ///</summary>
        [TestMethod]
        public void DeleteMediaTest1() {
            RentItClient target = new RentItClient();
            int mediaId = 3; // Kraftwerk - The Robots
            AccountCredentials credentials = new AccountCredentials {
                UserName = "publishCorp",
                HashedPassword = "7110eda4d09e062aa5e4a390b0a572ac0d2c0220"
            };
            bool expected = true;
            bool actual = target.DeleteMedia(mediaId, credentials);
            Assert.AreEqual(expected, actual);

            var db = new RentItTestDatabase.RentItDatabaseDataContext();
            

            // clean up
            var deletedMedia = (from m in db.Medias
                                where m.id == mediaId
                                select m).Single();
            deletedMedia.active = true;
            db.SubmitChanges();
        }

        /// <summary>
        ///A test for DeleteMedia
        ///</summary>
        [TestMethod]
        [ExpectedException(typeof(FaultException))]
        public void DeleteMediaTest2() {
            RentItClient target = new RentItClient();
            int mediaId = 2; // "House of Leaves"
            AccountCredentials credentials = new AccountCredentials {
                UserName = "publishCorp",
                HashedPassword = "wrongpassword"
            };

            target.DeleteMedia(mediaId, credentials); // InvalidCredentialsException
        }

        /// <summary>
        ///A test for DeleteMedia
        ///</summary>
        [TestMethod]
        [ExpectedException(typeof(FaultException))]
        public void DeleteMediaTest3() {
            RentItClient target = new RentItClient();
            int mediaId = 90000; // non-existant
            AccountCredentials credentials = new AccountCredentials {
                UserName = "publishCorp",
                HashedPassword = "7110eda4d09e062aa5e4a390b0a572ac0d2c0220"
            };
            target.DeleteMedia(mediaId, credentials); // InvalidCredentialsException
        }
    }
}
