using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.ServiceModel;
using System.Text;

namespace RentIt
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
        /// An object containing the metadata for the specified item.
        /// Returns null if the specified id does not exist in the server
        /// database.
        /// </returns>
        /// <exception cref="System.ServiceModel.FaultException">
        /// The FaultException is parameterized with:
        /// System.Exception if a general database error occurs.
        /// System.ArgumentException if the specified book does not exist.
        /// </exception>
        [OperationContract]
        [FaultContract(typeof(System.ServiceModel.FaultException<System.Exception>))]
        [FaultContract(typeof(System.ServiceModel.FaultException<System.ArgumentException>))]
        BookInfo GetBookInfo(int id);

        /// <summary>
        /// Returns all meta data of a particular movie.
        /// </summary>
        /// <param name="id">
        /// The the database id of the media item.
        /// </param>
        /// <returns>
        /// An object containing the metadata for the specfied item.
        /// Returns null if the specified id does not exist in the server
        /// database.
        /// </returns>
        /// <exception cref="System.ServiceModel.FaultException">
        /// The FaultException is parameterized with:
        /// System.Exception if a general database error occurs.
        /// System.ArgumentException if the specified movie does not exist.
        /// </exception>
        [OperationContract]
        [FaultContract(typeof(System.ServiceModel.FaultException<System.Exception>))]
        [FaultContract(typeof(System.ServiceModel.FaultException<System.ArgumentException>))]
        MovieInfo GetMovieInfo(int id);

        /// <summary>
        /// Returns all meta data of a particular album.
        /// </summary>
        /// <param name="id">
        /// The the database id of the media item.
        /// </param>
        /// <returns>
        /// An object containing the metadata for the item.
        /// Returns null if the specified id does not exist in the server
        /// database.
        /// </returns>
        /// <exception cref="System.ServiceModel.FaultException">
        /// The FaultException is parameterized with:
        /// System.Exception if a general database error occurs.
        /// System.ArgumentException if the specified album does not exist.
        /// </exception>
        [OperationContract]
        [FaultContract(typeof(System.ServiceModel.FaultException<System.Exception>))]
        [FaultContract(typeof(System.ServiceModel.FaultException<System.ArgumentException>))]
        AlbumInfo GetAlbumInfo(int id);

        /// <summary>
        /// Returns collections of media items matching the supplied criteria.
        /// </summary>
        /// <param name="criteria">
        /// The criteria for the returned media items.
        /// </param>
        /// <returns>
        /// An object containing four lists of books, movies, albums and songs, respectively.
        /// Returns null if the specified criteria is a null reference.
        /// </returns>
        /// <exception cref="System.ServiceModel.FaultException">
        /// The FaultException is parameterized with:
        /// System.Exception if a general database error or an internal error occurs.
        /// System.ArgumentException if the criteria parameter is a null reference.
        /// </exception>
        [OperationContract]
        [FaultContract(typeof(System.ServiceModel.FaultException<System.Exception>))]
        [FaultContract(typeof(System.ServiceModel.FaultException<System.ArgumentException>))]
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
        /// <exception cref="System.ServiceModel.FaultException">
        /// The FaultException is parameterized with:
        /// System.Exception if a general database error occurs.
        /// System.ArgumentException if the specified media id does not exist.
        /// </exception>
        [OperationContract]
        [FaultContract(typeof(System.ServiceModel.FaultException<System.Exception>))]
        [FaultContract(typeof(System.ServiceModel.FaultException<System.ArgumentException>))]
        MediaItems GetAlsoRentedItems(int id);

        /// <summary>
        /// Validates whether the supplied username and password combination is valid for a user in the RentIt database.
        /// </summary>
        /// <param name="credentials">
        /// The user name and hashed password.
        /// </param>
        /// <returns>
        /// If the credentials are valid, an Account object representing the successfully validated account is returned.
        /// Otherwise, null is returnes.
        /// </returns>
        /// <exception cref="System.ServiceModel.FaultException">
        /// The FaultException is parameterized with:
        /// System.Exception if a general database error occurs.
        /// System.ArgumentException if the credentials parameter is a null reference.
        /// System.InvalidCredentialsException if the submitted credentials are not valid for an active account.
        /// </exception>
        [OperationContract]
        [FaultContract(typeof(System.ServiceModel.FaultException<System.Exception>))]
        [FaultContract(typeof(System.ServiceModel.FaultException<System.ArgumentException>))]
        [FaultContract(typeof(System.ServiceModel.FaultException<InvalidCredentialsException>))]
        Account ValidateCredentials(AccountCredentials credentials);

        /// <summary>
        /// Creates a new user account to be used in the service.
        /// </summary>
        /// <param name="newAccount">
        /// The account to be created and stored.
        /// To adhere to our validation guidelines, the submitted information must contain data as such:
        /// The user name, full name and hashed password must all be longer than zero characters.
        /// The e-mail address must adhere to the following regular expression:
        /// [a-z0-9!#$%&'*+/=?^_`{|}~-]+(?:\.[a-z0-9!#$%&'*+/=?^_`{|}~-]+)*@(?:[a-z0-9](?:[a-z0-9-]*[a-z0-9])?\.)+[a-z0-9](?:[a-z0-9-]*[a-z0-9])?
        /// The user name must not already be registered.
        /// </param>
        /// <returns>
        /// True if the account creation was successful, false otherwise.
        /// </returns>
        /// <exception cref="System.ServiceModel.FaultException">
        /// The FaultException is parameterized with:
        /// System.Exception if a general database error occurs.
        /// System.ArgumentException if the newAccount parameter is a null reference.
        /// System.UserCreationException if the submitted user information does not adhere to the above rules.
        /// </exception>
        [OperationContract]
        [FaultContract(typeof(System.ServiceModel.FaultException<System.Exception>))]
        [FaultContract(typeof(System.ServiceModel.FaultException<System.ArgumentException>))]
        [FaultContract(typeof(System.ServiceModel.FaultException<UserCreationException>))]
        bool CreateNewUser(Account newAccount);

        /// <summary>
        /// Gets all information stored about the specified user account, including credits and 
        /// rental history.
        /// </summary>
        /// <param name="credentials">
        /// The credentials of the user's account.
        /// </param>
        /// <returns>
        /// UserAccount instance representing all the user data of the specified user account.
        /// </returns>
        /// <exception cref="System.ServiceModel.FaultException">
        /// The FaultException is parameterized with:
        /// System.Exception if a general database error occurs.
        /// System.ArgumentException if the credentials parameter is a null reference.
        /// System.InvalidCredentialsException if the submitted credentials are not valid for an active account,
        /// or if the credentials are for a publisher account.
        /// </exception>
        [OperationContract]
        [FaultContract(typeof(System.ServiceModel.FaultException<System.Exception>))]
        [FaultContract(typeof(System.ServiceModel.FaultException<System.ArgumentException>))]
        [FaultContract(typeof(System.ServiceModel.FaultException<InvalidCredentialsException>))]
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
        /// <exception cref="System.ServiceModel.FaultException">
        /// The FaultException is parameterized with:
        /// System.Exception if a general database error occurs.
        /// System.ArgumentException if the credentials parameter is a null reference.
        /// System.InvalidCredentialsException if the submitted credentials are not valid for an active account,
        /// or if the credentials are not for a publisher account.
        /// </exception>
        [OperationContract]
        [FaultContract(typeof(System.ServiceModel.FaultException<System.Exception>))]
        [FaultContract(typeof(System.ServiceModel.FaultException<System.ArgumentException>))]
        [FaultContract(typeof(System.ServiceModel.FaultException<InvalidCredentialsException>))]
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
        /// <exception cref="System.ServiceModel.FaultException">
        /// The FaultException is parameterized with:
        /// System.Exception if a general database error occurs.
        /// System.ArgumentException if the account or credentials parameter is a null reference.
        /// System.InvalidCredentialsException if the submitted credentials are not valid for an active account.
        /// </exception>
        [OperationContract]
        [FaultContract(typeof(System.ServiceModel.FaultException<System.Exception>))]
        [FaultContract(typeof(System.ServiceModel.FaultException<System.ArgumentException>))]
        [FaultContract(typeof(System.ServiceModel.FaultException<InvalidCredentialsException>))]
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
        /// <exception cref="System.ServiceModel.FaultException">
        /// The FaultException is parameterized with:
        /// System.Exception if a general database error occurs.
        /// System.ArgumentException if the credentials parameter is a null reference.
        /// System.InvalidCredentialsException if the submitted credentials are not valid for an active account.
        /// </exception>
        [OperationContract]
        [FaultContract(typeof(System.ServiceModel.FaultException<System.Exception>))]
        [FaultContract(typeof(System.ServiceModel.FaultException<System.ArgumentException>))]
        [FaultContract(typeof(System.ServiceModel.FaultException<InvalidCredentialsException>))]
        bool AddCredits(AccountCredentials credentials, uint addAmount);

        /// <summary>
        /// Rents the media on a given account for 30 days.
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
        /// <exception cref="System.ServiceModel.FaultException">
        /// The FaultException is parameterized with:
        /// System.Exception if a general database error occurs.
        /// System.ArgumentException if the credentials parameter is a null reference.
        /// System.InvalidCredentialsException if the submitted credentials are not valid for an active user account.
        /// </exception>
        [OperationContract]
        [FaultContract(typeof(System.ServiceModel.FaultException<System.Exception>))]
        [FaultContract(typeof(System.ServiceModel.FaultException<System.ArgumentException>))]
        [FaultContract(typeof(System.ServiceModel.FaultException<InvalidCredentialsException>))]
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
        /// <exception cref="System.ServiceModel.FaultException">
        /// The FaultException is parameterized with:
        /// System.Exception if a general database error occurs.
        /// System.ArgumentException if the credentials parameter is a null reference.
        /// System.InvalidCredentialsException if the submitted credentials are not valid for an active account,
        /// or if the matched account is not a publisher account.
        /// </exception>
        [OperationContract]
        [FaultContract(typeof(System.ServiceModel.FaultException<System.Exception>))]
        [FaultContract(typeof(System.ServiceModel.FaultException<System.ArgumentException>))]
        [FaultContract(typeof(System.ServiceModel.FaultException<InvalidCredentialsException>))]
        int PublishMedia(MediaInfo info, AccountCredentials credentials);

        /// <summary>
        /// Deletes the account identified by the credentials.
        /// </summary>
        /// <param name="credentials">
        /// The credentials of the account to be deleted.
        /// </param>
        /// <returns>
        /// True if the deletion was successful, false otherwise.
        /// </returns>
        /// <exception cref="System.ServiceModel.FaultException">
        /// The FaultException is parameterized with:
        /// System.Exception if a general database error occurs.
        /// System.ArgumentException if the credentials parameter is a null reference.
        /// System.InvalidCredentialsException if the submitted credentials are not valid for an active account.
        /// </exception>
        [OperationContract]
        [FaultContract(typeof(System.ServiceModel.FaultException<System.Exception>))]
        [FaultContract(typeof(System.ServiceModel.FaultException<System.ArgumentException>))]
        [FaultContract(typeof(System.ServiceModel.FaultException<InvalidCredentialsException>))]
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
        /// <exception cref="System.ServiceModel.FaultException">
        /// The FaultException is parameterized with:
        /// System.Exception if a general database error occurs.
        /// System.ArgumentException if the credentials parameter is a null reference.
        /// System.InvalidCredentialsException if the submitted credentials are not valid for an active account,
        /// or if the matched account is not a publisher account.
        /// </exception>
        [OperationContract]
        [FaultContract(typeof(System.ServiceModel.FaultException<System.Exception>))]
        [FaultContract(typeof(System.ServiceModel.FaultException<System.ArgumentException>))]
        [FaultContract(typeof(System.ServiceModel.FaultException<InvalidCredentialsException>))]
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
        /// <exception cref="System.ServiceModel.FaultException">
        /// The FaultException is parameterized with:
        /// System.Exception if a general database error occurs.
        /// System.ArgumentException if the credentials parameter is a null reference.
        /// System.InvalidCredentialsException if the submitted credentials are not valid for an active account,
        /// or if the credentials are not valid for the publisher listed as the owner of the submitted media id.
        /// </exception>
        [OperationContract]
        [FaultContract(typeof(System.ServiceModel.FaultException<System.Exception>))]
        [FaultContract(typeof(System.ServiceModel.FaultException<System.ArgumentException>))]
        [FaultContract(typeof(System.ServiceModel.FaultException<InvalidCredentialsException>))]
        bool DeleteMedia(int mediaId, AccountCredentials credentials);

        /// <summary>
        /// Gets all the genres of the specified media type that the service currently
        /// have media item for.
        /// </summary>
        /// <param name="mediaType">
        /// The media type for which all the different genres at the service is returned.
        /// </param>
        /// <returns>
        /// A list containing all genres for the given media type.
        /// </returns>
        /// <exception cref="System.ServiceModel.FaultException">
        /// The FaultException is parameterized with:
        /// System.Exception if a general database error occurs.
        /// </exception>
        [OperationContract]
        [FaultContract(typeof(System.Exception))]
        System.Collections.Generic.List<string> GetAllGenres(MediaType mediaType);

        /// <summary>
        /// Submits a review written by a user.
        /// </summary>
        /// <param name="review">
        /// A MediaReview object representing the rating and review.
        /// </param>
        /// <param name="credentials">The credentials of the user who wrote
        /// the review. The user name must match the user name in the review.</param>
        /// <returns>
        /// true if the review was accepted, false otherwise.
        /// </returns>
        /// <exception cref="System.ServiceModel.FaultException">
        /// The FaultException is parameterized with:
        /// System.Exception if a general database error occurs.
        /// System.ArgumentException if the credentials parameter is a null reference, if the user name in
        /// the credentials does not match the user name in the review, if the media id in the review does
        /// not exist, or if the credentials match a publisher account instead of a user account.
        /// System.InvalidCredentialsException if the submitted credentials are not valid for an active account.
        /// </exception>
        [OperationContract]
        [FaultContract(typeof(System.ServiceModel.FaultException<System.Exception>))]
        [FaultContract(typeof(System.ServiceModel.FaultException<System.ArgumentException>))]
        [FaultContract(typeof(System.ServiceModel.FaultException<InvalidCredentialsException>))]
        bool SubmitReview(MediaReview review, AccountCredentials credentials);
    }
}
