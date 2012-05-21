using RentIt;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using Microsoft.VisualStudio.TestTools.UnitTesting.Web;

namespace RentItTest
{
    using System.ServiceModel;

    /// <summary>
    ///This is a test class for RentItServiceTest and is intended
    ///to contain all RentItServiceTest Unit Tests
    ///</summary>
    [TestClass()]
    public class RentItServiceTest
    {
        #region Additional test attributes
        // 
        //You can use the following additional attributes as you write your tests:
        //
        //Use ClassInitialize to run code before running the first test in the class
        //[ClassInitialize()]
        //public static void MyClassInitialize(TestContext testContext)
        //{
        //}
        //
        //Use ClassCleanup to run code after all tests in a class have run
        //[ClassCleanup()]
        //public static void MyClassCleanup()
        //{
        //}
        //
        //Use TestInitialize to run code before running each test
        //[TestInitialize()]
        //public void MyTestInitialize()
        //{
        //}
        //
        //Use TestCleanup to run code after each test has run
        //[TestCleanup()]
        //public void MyTestCleanup()
        //{
        //}
        //
        #endregion

        #region ValideCredentials tests

        /// <summary>
        /// A test for ValidateCredentials
        /// 
        /// Test a set of valid credentials and check that the correct
        /// account is returned.
        /// </summary>
        [TestMethod]
        public void ValidateCredentialsTest1()
        {
            RentItClient target = new RentItClient();

            // The following credentials do not exist in the database.
            AccountCredentials credentials = new AccountCredentials()
                {
                    UserName = "publishCorp",
                    HashedPassword = "7110EDA4D09E062AA5E4A390B0A572AC0D2C0220"
                };

            Account actual = target.ValidateCredentials(credentials);

            Assert.AreEqual("publishCorp", actual.UserName);
        }

        /// <summary>
        /// A test for ValidateCredentials
        /// 
        /// Validate credentials of an existing account but that is no
        /// longer active. FaultException is expected.
        /// </summary>
        [TestMethod]
        [ExpectedException(typeof(FaultException))]
        public void ValidateCredentialsTest2()
        {
            RentItClient target = new RentItClient();

            // The following credentials do not exist in the database.
            AccountCredentials credentials = new AccountCredentials()
            {
                UserName = "fluffi",
                HashedPassword = "1234"
            };

            Account actual = target.ValidateCredentials(credentials);
        }

        /// <summary>
        /// A test for ValidateCredentials
        /// 
        /// Validate credentials that are not correct.
        /// FaultException is expected.
        /// </summary>
        [TestMethod]
        [ExpectedException(typeof(FaultException))]
        public void ValidateCredentialsTest3()
        {
            RentItClient target = new RentItClient();

            // The following credentials do not exist in the database.
            AccountCredentials credentials = new AccountCredentials()
            {
                UserName = "publishCorp",
                HashedPassword = "1234"
            };

            Account actual = target.ValidateCredentials(credentials);
        }

        #endregion

        #region GetMediaItems test

        /// <summary>
        /// A test for GetMediaItems
        /// Tests that only Songs are returned when the media type
        /// is set to MediaType.Songs
        /// </summary>
        [TestMethod]
        public void GetMediaItemsTest1()
        {
            RentItClient target = new RentItClient();
            MediaCriteria criteria = new MediaCriteria()
                {
                    Limit = -1,
                    Type = MediaType.Song,
                };

            MediaItems actual = target.GetMediaItems(criteria);

            Assert.IsTrue(actual.Albums.Length == 0);
            Assert.IsTrue(actual.Songs.Length > 0);
            Assert.IsTrue(actual.Movies.Length == 0);
            Assert.IsTrue(actual.Books.Length == 0);
        }

        /// <summary>
        /// A test for GetMediaItems
        /// Tests that only Books are returned when the media type
        /// is set to MediaType.Book
        /// </summary>
        [TestMethod]
        public void GetMediaItemsTest2()
        {
            RentItClient target = new RentItClient();
            MediaCriteria criteria = new MediaCriteria()
            {
                Limit = -1,
                Type = MediaType.Book,
            };

            MediaItems actual = target.GetMediaItems(criteria);

            Assert.IsTrue(actual.Albums.Length == 0);
            Assert.IsTrue(actual.Songs.Length == 0);
            Assert.IsTrue(actual.Movies.Length == 0);
            Assert.IsTrue(actual.Books.Length > 0);
        }

        /// <summary>
        /// A test for GetMediaItems
        /// Tests that only Movies are returned when the media type
        /// is set to MediaType.Movie
        /// </summary>
        [TestMethod]
        public void GetMediaItemsTest3()
        {
            RentItClient target = new RentItClient();
            MediaCriteria criteria = new MediaCriteria()
            {
                Limit = -1,
                Type = MediaType.Movie,
            };

            MediaItems actual = target.GetMediaItems(criteria);

            Assert.IsTrue(actual.Albums.Length == 0);
            Assert.IsTrue(actual.Songs.Length == 0);
            Assert.IsTrue(actual.Movies.Length > 0);
            Assert.IsTrue(actual.Books.Length == 0);
        }

        /// <summary>
        /// A test for GetMediaItems
        /// Tests that only Albums are returned when the media type
        /// is set to MediaType.Album
        /// </summary>
        [TestMethod]
        public void GetMediaItemsTest4()
        {
            RentItClient target = new RentItClient();
            MediaCriteria criteria = new MediaCriteria()
            {
                Limit = -1,
                Type = MediaType.Album,
            };

            MediaItems actual = target.GetMediaItems(criteria);

            Assert.IsTrue(actual.Albums.Length > 0);
            Assert.IsTrue(actual.Songs.Length == 0);
            Assert.IsTrue(actual.Movies.Length == 0);
            Assert.IsTrue(actual.Books.Length == 0);
        }

        /// <summary>
        /// A test for GetMediaItems
        /// Tests that all media types are returned when the media 
        /// type is set to MediaType.Any
        /// </summary>
        [TestMethod]
        public void GetMediaItemsTest5()
        {
            RentItClient target = new RentItClient();
            MediaCriteria criteria = new MediaCriteria()
            {
                Limit = -1,
                Type = MediaType.Any,
            };

            MediaItems actual = target.GetMediaItems(criteria);

            Assert.IsTrue(actual.Albums.Length > 0);
            Assert.IsTrue(actual.Songs.Length > 0);
            Assert.IsTrue(actual.Movies.Length > 0);
            Assert.IsTrue(actual.Books.Length > 0);
        }

        /// <summary>
        /// A test for GetMediaItems
        /// Tests that all media types are returned if the
        /// media type is not specified.
        /// </summary>
        [TestMethod]
        public void GetMediaItemsTest6()
        {
            RentItClient target = new RentItClient();
            MediaCriteria criteria = new MediaCriteria()
            {
                Limit = -1,
            };

            MediaItems actual;
            actual = target.GetMediaItems(criteria);

            Assert.IsTrue(actual.Albums.Length > 0);
            Assert.IsTrue(actual.Songs.Length > 0);
            Assert.IsTrue(actual.Movies.Length > 0);
            Assert.IsTrue(actual.Books.Length > 0);
        }

        /// <summary>
        /// A test for GetMediaItems
        /// Test the search text portion of the MediaCriteria-class.
        /// </summary>
        [TestMethod]
        public void GetMediaItemsTest7()
        {
            RentItClient target = new RentItClient();
            MediaCriteria criteria = new MediaCriteria()
            {
                Limit = -1,
                Type = MediaType.Album,
                SearchText = "The CUre"
            };

            MediaItems actual;
            actual = target.GetMediaItems(criteria);

            Assert.IsTrue(actual.Albums.Length > 0);
            Assert.IsTrue(actual.Songs.Length == 0);
            Assert.IsTrue(actual.Movies.Length == 0);
            Assert.IsTrue(actual.Books.Length == 0);
        }

        /// <summary>
        /// A test for GetMediaItems
        /// Tests the ordering portion of the GetMediaItems method.
        /// </summary>
        [TestMethod]
        public void GetMediaItemsTest8()
        {
            RentItClient target = new RentItClient();
            MediaCriteria criteria = new MediaCriteria()
            {
                Limit = -1,
                Order = MediaOrder.AlphabeticalAsc,
            };

            MediaItems actual;
            actual = target.GetMediaItems(criteria);

            Assert.IsTrue(this.isSortedAsc(actual.Albums));
        }

        /// <summary>
        /// Helper method to determine if MediaItems are sorted, asc.
        /// </summary>
        /// <param name="list"></param>
        /// <returns></returns>
        private bool isSortedAsc(MediaInfo[] list)
        {
            MediaInfo first = list[0];

            for (int i = 1; i < list.Length; i++)
            {
                if (string.Compare(first.Title, list[i].Title) > 0)
                {
                    return false;
                }

                first = list[i];
            }
            return true;
        }

        #endregion

        #region GetMovieInfo tests

        /// <summary>
        /// A test for GetMovieInfo
        /// 
        /// Test that gets the movie with the media id 61
        /// </summary>
        [TestMethod]
        public void GetMovieInfoTest1()
        {
            RentItClient target = new RentItClient();
            int id = 61;

            MovieInfo actual;
            actual = target.GetMovieInfo(id);

            Assert.AreEqual("GTA V - Debut Trailer", actual.Title);
        }

        /// <summary>
        /// A test for GetMovieInfo
        /// 
        /// Test of media identified by the id -10 that does not
        /// exist in the database. FaultException is expected.
        /// </summary>
        [TestMethod]
        [ExpectedException(typeof(FaultException))]
        public void GetMovieInfoTest2()
        {
            RentItClient target = new RentItClient();
            int id = -10;

            MovieInfo actual;
            actual = target.GetMovieInfo(id);
        }

        /// <summary>
        /// A test for GetMovieInfo
        /// 
        /// Test of media identified by the id 78 that does
        /// exist in the database but is of a different media type. 
        /// FaultException is expected.
        /// </summary>
        [TestMethod]
        [ExpectedException(typeof(FaultException))]
        public void GetMovieInfoTest3()
        {
            RentItClient target = new RentItClient();
            int id = 78;

            MovieInfo actual;
            actual = target.GetMovieInfo(id);
        }

        #endregion

        #region GetBookInfo tests

        /// <summary>
        /// A test for GetBookInfo
        /// 
        /// Test that the book identified by media id 81 is returned.
        /// Test that the title of the book is the expected.
        /// </summary>
        [TestMethod]
        public void GetBookInfoTest1()
        {
            RentItClient target = new RentItClient();

            int id = 81;
            BookInfo actual;
            actual = target.GetBookInfo(id);

            Assert.AreEqual("Power Commands for Visual Studio", actual.Title);
        }

        /// <summary>
        /// A test for GetBookInfo
        /// 
        /// Test of a media identified by -10 that does not
        /// exist in the database. The FaulException is expected.
        /// </summary>
        [TestMethod]
        [ExpectedException(typeof(FaultException))]
        public void GetBookInfoTest2()
        {
            RentItClient target = new RentItClient();

            int id = -10;
            BookInfo actual;
            actual = target.GetBookInfo(id);
        }

        /// <summary>
        /// A test for GetBookInfo
        /// 
        /// Test of a media id that does exist in the database but is
        /// of a different media type. The FaultException is expected.
        /// </summary>
        [TestMethod]
        [ExpectedException(typeof(FaultException))]
        public void GetBookInfoTest3()
        {
            RentItClient target = new RentItClient();

            int id = 78;
            BookInfo actual;
            actual = target.GetBookInfo(id);
        }

        #endregion

        #region GetAlbumInfo tests

        /// <summary>
        /// A test for GetAlbumInfo.
        /// Tests that it can get the album info of an album that exists
        /// and that is identified by the id 78.
        /// 
        /// Test that the metadata are the ones expected.
        /// </summary>
        [TestMethod]
        public void GetAlbumInfoTest1()
        {
            RentItClient target = new RentItClient();

            // Get the album indentified by the media id 78.
            int id = 78;
            AlbumInfo actual;
            actual = target.GetAlbumInfo(id);

            Assert.AreNotEqual(null, actual);
            Assert.AreEqual("The Cure", actual.AlbumArtist);
            Assert.AreEqual("The Greatest Hits", actual.Title);
        }

        /// <summary>
        /// A test for GetAlbumInfo
        /// Tests the all the song of the album is in the return value.
        /// </summary>
        [TestMethod]
        public void GetAlbumInfoTest2()
        {
            RentItClient target = new RentItClient();

            // The album indentified by id 78 has only 2 songs.
            int id = 78;
            AlbumInfo actual;
            actual = target.GetAlbumInfo(id);

            Assert.AreEqual(2, actual.Songs.Length);
        }

        /// <summary>
        /// A test for GetAlbumInfo.
        /// 
        /// Test the GetAlbumInfo invoking with a media id that does not exist
        /// in the database.
        /// The FaultException is expected.
        /// </summary>
        [TestMethod]
        [ExpectedException(typeof(FaultException))]
        public void GetAlbumInfoTest3()
        {
            RentItClient target = new RentItClient();
            int id = -10;

            AlbumInfo actual;
            actual = target.GetAlbumInfo(id);
        }

        /// <summary>
        /// A test for GetAlbumInfo.
        /// 
        /// Test the GetAlbumInfo invoking with a media id that does exist
        /// in the database but if not an album.
        /// The FaultException is expected.
        /// </summary>
        [TestMethod]
        [ExpectedException(typeof(FaultException))]
        public void GetAlbumInfoTest4()
        {
            RentItClient target = new RentItClient();
            int id = 70;

            AlbumInfo actual;
            actual = target.GetAlbumInfo(id);
        }

        #endregion
    }
}
