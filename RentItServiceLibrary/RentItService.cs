using System;
using System.Collections.Generic;

namespace RentItServiceLibrary {
	public class RentItService : IRentIt {
		public BookInfo GetBookInfo(int id) {
			throw new NotImplementedException();
		}

		public MovieInfo GetMovieInfo(int id) {
			throw new NotImplementedException();
		}

		public AlbumInfo GetAlbumInfo(int id) {
			throw new NotImplementedException();
		}

		public MediaItems GetMediaItems(MediaCriteria criteria) {
			throw new NotImplementedException();
		}

		public MediaItems GetAlsoRentedItems(int id) {
			throw new NotImplementedException();
		}

		public Account ValidateCredentials(string userName, string password) {
			if(userName.Equals("admin") && password.Equals("1234"))
				return new Account("admin", "Admin Adminson", "admin@adminmail.com", "7110eda4d09e062aa5e4a390b0a572ac0d2c0220");
			throw new InvalidCredentialsException("The credentials were wrong! Bad user! >:-(");
		}

		public bool CreateNewUser(Account newAccount) {
			throw new NotImplementedException();
		}

		public UserAccount GetAllCustomerData(AccountCredentials credentials) {
			throw new NotImplementedException();
		}

		public PublisherAccount GetAllPublisherData(AccountCredentials credentials) {
			throw new NotImplementedException();
		}

		public bool UpdateAccountInfo(AccountCredentials credentials, Account account) {
			throw new NotImplementedException();
		}

		public bool AddCredits(AccountCredentials credentials, uint addAmount) {
			throw new NotImplementedException();
		}

		public bool RentMedia(int mediaId, AccountCredentials credentials) {
			throw new NotImplementedException();
		}

		public bool PublishMedia(MediaInfo info, AccountCredentials credentials) {
			throw new NotImplementedException();
		}

		public bool DeleteAccount(AccountCredentials credentials) {
			throw new NotImplementedException();
		}

		public bool UpdateMediaMetadata(MediaInfo newData, AccountCredentials credentials) {
			throw new NotImplementedException();
		}

		public bool DeleteMedia(int mediaId, AccountCredentials credentials) {
			throw new NotImplementedException();
		}

		public Uri GetMediaUrl(string mediaId, AccountCredentials credentials) {
			throw new NotImplementedException();
		}

		public List<string> GetAllGenres(MediaType mediaType) {
			throw new NotImplementedException();
		}
	}
}
