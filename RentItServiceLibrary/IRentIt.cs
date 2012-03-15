﻿using System.ServiceModel;

namespace RentItServiceLibrary
{
    [ServiceContract]
    public interface IRentIt
    {
        /// <summary>
        /// Returns all meta data of a particular book.
        /// </summary>
        /// <param name="id">
        /// The the database id of the media item.
        /// </param>
        /// <returns>
        /// An object containing the metadata for the item.
        /// </returns>
        BookInfo GetBookInfo(int id);

        /// <summary>
        /// Returns all meta data of a particular movie.
        /// </summary>
        /// <param name="id">
        /// The the database id of the media item.
        /// </param>
        /// <returns>
        /// An object containing the metadata for the item.
        /// </returns>
        MovieInfo GetMovieInfo(int id);

        /// <summary>
        /// Returns all meta data of a particular album.
        /// </summary>
        /// <param name="id">
        /// The the database id of the media item.
        /// </param>
        /// <returns>
        /// An object containing the metadata for the item.
        /// </returns>
        AlbumInfo GetAlbumInfo(int id);

        /// <summary>
        /// Returns collections of media items matching the supplied criteria.
        /// </summary>
        /// <param name="criteria">
        /// The criteria for the returned media items.
        /// </param>
        /// <returns>
        /// An object containing four lists of books, movies, albums and songs, respectively.
        /// </returns>
        MediaItems GetMediaItems(MediaCriteria criteria);

        /// <summary>
        /// Returns collections of media items that have been rented by customers who rented the supplied media item.
        /// </summary>
        /// <param name="id">
        /// The id of the media item.
        /// </param>
        /// <returns>
        /// An object containing four lists of books, movies, albums and songs, respectively.
        /// </returns>
        MediaItems GetAlsoRentedItems(int id);

        /// <summary>
        /// Validates whether the supplied username and password combination is valid for a user in the RentIt database.
        /// </summary>
        /// <param name="userName">
        /// The user name.
        /// </param>
        /// <param name="password">
        /// The SHA1-hashed password.
        /// </param>
        /// <returns>
        /// An Account object representing the successfully validated account.
        /// </returns>
        /// <exception cref="InvalidCredentialsException">
        /// If the supplied credentials are invalid.
        /// </exception>
        Account ValidateCredentials(string userName, string password);

        /// <summary>
        /// Creates a new user account to be used in the service.
        /// </summary>
        /// <param name="newAccount">
        /// The account to be created and stored.
        /// </param>
        /// <returns>
        /// True if the account creation was successful, false otherwise.
        /// </returns>
        /// <exception cref="UserCreationException">
        /// If the user creation failed.
        /// </exception>
        bool CreateNewUser(Account newAccount);

        /// <summary>
        /// Gets all information stored about the specified user account, including credits and 
        /// rental history.
        /// </summary>
        /// <param name="credentials">
        /// The credentials of the user's account.
        /// </param>
        /// <returns>
        /// UserAccount-instance representing all the user data of the specified user account.
        /// </returns>
        UserAccount GetAllCustomerData(AccountCredentials credentials);

        /// <summary>
        /// Gets all information stored about the specified publisher account, including publisher name and 
        /// published medias.
        /// </summary>
        /// <param name="credentials">
        /// The credentials of the publisher's account.
        /// </param>
        /// <returns>
        /// PublisherAccount-instance representing all the publisher data of the specified publisher account.
        /// </returns>
        PublisherAccount GetAllPublisherData(AccountCredentials credentials);

        /// <summary>
        /// Updates the full name, e-mail and password stored for the supplied account. 
        /// The id, user name and password hash in the Account object must be valid for the update to succeed.
        /// </summary>
        /// <param name="account">
        /// Changed account information.
        /// </param>
        /// <param name="credentials">
        /// The account credentials for account identified by the account-parameter
        /// </param>
        /// <returns>
        /// True if the update was successful (credentials were valid and the database record was changed), 
        /// false otherwise (credentials were invalid or another error in the supplied information was found).
        /// </returns>
        bool UpdateAccountInfo(AccountCredentials credentials, Account account);

        /// <summary>
        /// Adds an amount of credits to the account's balance.
        /// </summary>
        /// <param name="credentials">
        /// The account credentials for the account.
        /// </param>
        /// <param name="addAmount">
        /// The amount of credits to be added.
        /// </param>
        /// <returns>
        /// True if the update was successful (credentials were valid and the database record was changed), 
        /// false otherwise (credentials were invalid or another error in the supplied information was found).
        /// </returns>
        bool AddCredits(AccountCredentials credentials, uint addAmount);

        /// <summary>
        /// Rents the media on a given account.
        /// </summary>
        /// <param name="mediaId">
        /// The id of the media to be rented.
        /// </param>
        /// <param name="credentials">
        /// The credentials of the account renting the media.
        /// </param>
        /// <returns>
        /// True if the rent was successful, false otherwise.
        /// </returns>
        bool RentMedia(int mediaId, AccountCredentials credentials);

        /// <summary>
        /// Publishes the media, making it available to other users.
        /// </summary>
        /// <param name="info">
        /// The metadata of the uploaded media.
        /// </param>
        /// <param name="credentials">
        /// The credentials of the uploader's account.
        /// </param>
        /// <returns>
        /// True if the media was successfully published, false otherwise.
        /// </returns>
        bool PublishMedia(MediaInfo info, AccountCredentials credentials); // How to upload the media file has not been figured out.

        /// <summary>
        /// Deletes the account identified by the credentials.
        /// </summary>
        /// <param name="credentials">
        /// The credentials of the account to be deleted.
        /// </param>
        /// <returns>
        /// True if the deletion was successful, false otherwise.
        /// </returns>
        bool DeleteAccount(AccountCredentials credentials);

        /// <summary>
        /// Changes the metadata of the specified media item.
        /// Used by the publisher to e.g. change the prize of a media or other data.
        /// </summary>
        /// <param name="newData">
        /// The updated metadata for the specified media item.
        /// </param>
        /// <param name="credentials">
        /// The credentials of the publisher wishing to change the metadata of the specified media item.
        /// Only media that the publisher has ownership of can be changed.
        /// </param>
        /// <returns>
        /// True if the update was successful, false otherwise.
        /// </returns>
        bool UpdateMediaMetadata(MediaInfo newData, AccountCredentials credentials);

        /// <summary>
        /// Deletes the specified media from the service.
        /// </summary>
        /// <param name="mediaId">
        /// The id of the media item to be deleted.
        /// </param>
        /// <param name="credentials">
        /// The credentials of the publisher.
        /// </param>
        /// <returns>
        /// True if the deletion was successful, false otherwise.
        /// </returns>
        bool DeleteMedia(int mediaId, AccountCredentials credentials);

        /// <summary>
        /// When a media has been paid for and is an active rental, this service method is used
        /// to get the URL of the specified media.
        /// </summary>
        /// <param name="mediaId">
        /// The media to be obtained identified by this media id.
        /// </param>
        /// <param name="credentials">
        /// The credentials of the user account used to verify that the media has been paid for prior
        /// downloading the specified media.
        /// </param>
        System.Uri GetMediaUrl(string mediaId, AccountCredentials credentials);

        /// <summary>
        /// Gets all the genres of the specified media type that the service currently
        /// have media item for.
        /// </summary>
        /// <param name="mediaType">
        /// The media type for which all the different genres at the service is returned.
        /// </param>
        System.Collections.Generic.List<string> GetAllGenres(MediaType mediaType);
    }
}
