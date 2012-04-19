using RentIt;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using Microsoft.VisualStudio.TestTools.UnitTesting.Web;
using System.Collections.Generic;

namespace RentItTest
{
	using System.Linq;
	using System.ServiceModel;

	using RentItDatabase;

	using Rating = RentIt.Rating;

	/// <summary>
	///This is a test class for RentItServiceJacobTest and is intended
	///to contain all RentItServiceJacobTest Unit Tests
	///</summary>
	[TestClass()]
	public class JacobTest
	{
		/// <summary>
		/// Test 20 - Pass valid media Id to service
		///</summary>
		[TestMethod()]
		public void GetAlsoRentedItemsTest()
		{
			RentItClient target = new RentItClient(); 
			int id = 1;
			
			MediaItems actual = target.GetAlsoRentedItems(id);

			Assert.IsTrue(actual.Books.ToList().Exists(x => x.Id == 2 && x.Title == "House of Leaves" && x.Author == "Mark Z. Danielewski"));
			Assert.IsTrue(actual.Movies.ToList().Exists(x => x.Id == 1 && x.Title == "The Room" && x.Director == "Tommy Wiseau"));
			Assert.IsTrue(actual.Albums.ToList().Exists(x => x.Id == 10 && x.Title == "The Man-Machine" && x.AlbumArtist == "Kraftwerk"));
			//Assert.IsTrue(actual.Songs.Count() == 0);
		}

		/// <summary>
		///  Test 21 - Pass invalid media Id to service
		/// </summary>
		[TestMethod()]
		[ExpectedException(typeof(FaultException))]
		public void GetAlsoRentedItemsExceptionTest() {
			RentItClient target = new RentItClient();
			target.GetAlsoRentedItems(-3);
		}

		/// <summary>
		/// Test 31 - Pass valid review and credentials for submission
		///</summary>
		[TestMethod()]
		public void SubmitReviewTest()
		{
			RentItClient target = new RentItClient(); 
			MediaReview review = new MediaReview {
			                                     	MediaId = 1, 
													Rating = Rating.Three, 
													ReviewText = "Bucket.", 
													Timestamp = DateTime.Now, 
													UserName = "user10"
			                                     }; 

			AccountCredentials credentials = new AccountCredentials {
			                                                        	UserName = "user10",
																		HashedPassword = "5BAA61E4C9B93F3F0682250B6CF8331B7EE68FD8"
			                                                        };  
			bool actual = target.SubmitReview(review, credentials);
			Assert.IsTrue(actual);
			DatabaseDataContext db;
			try
			{
				db = new DatabaseDataContext();
			}
			catch (Exception)
			{
				throw new FaultException<Exception>(
					new Exception("An internal error has occured. This is not related to the input."));
			}
		}

		/// <summary>
		///  Test 32 - Submit a review to a media item that already exists from that user.
		/// </summary>
		[TestMethod()]
		[ExpectedException(typeof(FaultException))]
		public void SubmitReviewExceptionTest() {
			this.SubmitReviewTest();
		}

		/// <summary>
		/// Test 18 - Pass valid publisher credentials to service
		///</summary>
		[TestMethod()]
		public void GetAllPublisherDataTest()
		{
			RentItClient target = new RentItClient();
			AccountCredentials credentials = new AccountCredentials {
			                                                        	UserName = "publishCorp", 
																		HashedPassword = "7110eda4d09e062aa5e4a390b0a572ac0d2c0220"
			                                                        };

			PublisherAccount actual = target.GetAllPublisherData(credentials);

			Assert.AreEqual("publishCorp", actual.UserName);
			Assert.AreEqual("publish@publishcorp.com", actual.Email);
			Assert.AreEqual("Publish Corp. Publishing Division", actual.FullName);
			Assert.AreEqual("7110EDA4D09E062AA5E4A390B0A572AC0D2C0220", actual.HashedPassword);
			Assert.AreEqual("Publish Corp. International", actual.PublisherName);
		}

		/// <summary>
		///  Test 17 - Pass valid user account credentials to service
		/// </summary>
		[TestMethod()]
		[ExpectedException(typeof(FaultException))]
		public void GetAllPublisherDataExceptionTestOne() {
			RentItClient target = new RentItClient();
			AccountCredentials credentials = new AccountCredentials {
			                                                        	UserName = "user21",
																		HashedPassword = "a1881c06eec96db9901c7bbfe41c42a3f08e9cb4"
			                                                        };
			target.GetAllPublisherData(credentials);
		}

		/// <summary>
		///  Test 19 - Pass invalid credentials to service
		/// </summary>
		[TestMethod()]
		[ExpectedException(typeof(FaultException))]
		public void GetAllPublisherDataExceptionTestTwo() {
			RentItClient target = new RentItClient();
			AccountCredentials credentials = new AccountCredentials
			{
				UserName = "Idonotexist",
				HashedPassword = "invalid"
			};
			target.GetAllPublisherData(credentials);
		}

		/// <summary>
		/// Test 13 - Pass valid user credentials to service
		///</summary>
		[TestMethod()]
		public void GetAllCustomerDataTest()
		{
			RentItClient target = new RentItClient(); 
			AccountCredentials credentials = new AccountCredentials {
			                                                        	UserName = "user21",
																		HashedPassword = "a1881c06eec96db9901c7bbfe41c42a3f08e9cb4"
			                                                        };
			UserAccount actual = target.GetAllCustomerData(credentials);
			Assert.AreEqual("user21", actual.UserName);
			Assert.AreEqual("user21@mail.com", actual.Email);
			Assert.AreEqual("user21", actual.FullName);
			Assert.AreEqual("a1881c06eec96db9901c7bbfe41c42a3f08e9cb4", actual.HashedPassword);
			Assert.AreEqual(0, actual.Credits);
		}

		/// <summary>
		///  Test 14 - Pass valid publisher credentials to service
		/// </summary>
		[TestMethod()]
		[ExpectedException(typeof(FaultException))]
		public void GetAllCustomerDataExceptionTestOne() {
			RentItClient target = new RentItClient();
			AccountCredentials credentials = new AccountCredentials
			{
				UserName = "publishCorp",
				HashedPassword = "7110eda4d09e062aa5e4a390b0a572ac0d2c0220"
			};
			target.GetAllCustomerData(credentials);
		}

		/// <summary>
		///  Test 15 - Pass invalid user credentials to service
		/// </summary>
		[TestMethod()]
		[ExpectedException(typeof(FaultException))]
		public void GetAllCustomerDataExceptionTestTwo()
		{
			RentItClient target = new RentItClient();
			AccountCredentials credentials = new AccountCredentials
			{
				UserName = "ExistIDoNot",
				HashedPassword = "Invalid"
			};
			target.GetAllCustomerData(credentials);
		}

	}
}
