namespace RentIt
{
    using System;
    using System.Collections.Generic;
    using System.Data.Linq;

    using RentItDatabase;

    using System.Linq;

    public class RentItService : IRentIt
    {
        #region Interface implementation

        /// <author>Kenneth Søhrmann</author>
        /// <summary>
        /// 
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public BookInfo GetBookInfo(int id)
        {
            var db = new DatabaseDataContext();

            // Get the book.
            RentItDatabase.Book book = (from b in db.Books where b.media_id.Equals(id) select b).First();

            // Get the rating data of the book.
            RentItDatabase.Rating rating = GetMediaRating(book.media_id, db);

            // Get all the user reviews of the book.
            RentIt.MediaRating mediaRating = CollectMediaReviews(book.media_id, rating, db);

            return RentIt.BookInfo.ValueOf(book, mediaRating);
        }

        /// <author>Kenneth Søhrmann</author>
        /// <summary>
        /// 
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public MovieInfo GetMovieInfo(int id)
        {
            var db = new DatabaseDataContext();

            // Get the movie.
            RentItDatabase.Movie movie = (from m in db.Movies where m.media_id.Equals(id) select m).First();

            RentItDatabase.Rating rating = GetMediaRating(movie.media_id, db);

            RentIt.MediaRating mediaRating = CollectMediaReviews(movie.media_id, rating, db);

            return RentIt.MovieInfo.ValueOf(movie, mediaRating);
        }

        /// <author>Kenneth Søhrmann</author>
        /// <summary>
        /// 
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public AlbumInfo GetAlbumInfo(int id)
        {
            var db = new DatabaseDataContext();

            // Get the Album
            RentItDatabase.Album album = (from a in db.Albums where a.media_id.Equals(id) select a).First();

            RentItDatabase.Rating rating = GetMediaRating(album.media_id, db);

            RentIt.MediaRating mediaRating = CollectMediaReviews(album.media_id, rating, db);

            // Collect album songs
            IQueryable<RentItDatabase.Song> songs = from s in db.Songs
                                                    where s.media_id.Equals(album.media_id)
                                                    select s;

            // Collect data of all the songs contained in the album.
            var albumSongs = new List<RentIt.SongInfo>();
            foreach (RentItDatabase.Song song in songs)
            {
                RentItDatabase.Rating songRatings = GetMediaRating(album.media_id, db);
                RentIt.MediaRating songRating = CollectMediaReviews(song.media_id, songRatings, db);

                albumSongs.Add(SongInfo.ValueOf(song, songRating));
            }

            return RentIt.AlbumInfo.ValueOf(album, albumSongs, mediaRating);
        }

        public MediaItems GetMediaItems(MediaCriteria criteria)
        {
            throw new NotImplementedException();
        }

        public MediaItems GetAlsoRentedItems(int id)
        {
            throw new NotImplementedException();
        }

        public Account ValidateCredentials(AccountCredentials credentials)
        {
            var db = new DatabaseDataContext();

            IQueryable<RentItDatabase.Account> result = from ac in db.Accounts
                                                        where ac.user_name.Equals(credentials.UserName)
                                                        select ac;

            // If the result contains no accounts, there do not exist an account in the database with the user name
            // that is provided in the credentials...
            if (result.Count() == 0)
            {
                // ... throw an exception to inform the caller.
                throw new InvalidCredentialsException("Submitted user name does not exist.");
            }

            RentItDatabase.Account account = result.First();

            // If the submitted hashed password does not match the one stored in the database...
            if (!account.password.Equals(credentials.HashedPassword))
            {
                // ... throw an exception to inform the caller.
                throw new InvalidCredentialsException("Submitted password is incorrect.");
            }

            // The credentials has successfully been evaluated, return account details to caller.
            return RentIt.Account.ValueOf(account);
        }

        public bool CreateNewUser(Account newAccount)
        {
            var db = new DatabaseDataContext();

            // If there exist an account with the submitted user name...
            if (db.Accounts.Exists(ac => ac.user_name.Equals(newAccount.UserName)))
            {
                // ...the request is told so.
                throw new UserCreationException("The specified user name is already in use");
            }

            // The user name is free, create a new instance with the data provided.
            var baseAccount = new RentItDatabase.Account {
                    user_name = newAccount.UserName,
                    full_name = newAccount.FullName,
                    email = newAccount.Email,
                    password = newAccount.HashedPassword
                };
            var userAccount = new User_account { credit = 0, Account = baseAccount };

            db.User_accounts.InsertOnSubmit(userAccount);
            db.SubmitChanges();
            return true;
        }

        public UserAccount GetAllCustomerData(AccountCredentials credentials)
        {
            throw new NotImplementedException();
        }

        public PublisherAccount GetAllPublisherData(AccountCredentials credentials)
        {
            throw new NotImplementedException();
        }

        public bool UpdateAccountInfo(AccountCredentials credentials, Account account)
        {
            throw new NotImplementedException();
        }

        /// <author>Per Mortensen</author>
        /// <summary>
        /// 
        /// </summary>
        /// <param name="credentials"></param>
        /// <param name="addAmount"></param>
        /// <returns></returns>
        public bool AddCredits(AccountCredentials credentials, uint addAmount)
        {
            Account account = ValidateCredentials(credentials);
            var db = new DatabaseDataContext();

            IQueryable<User_account> userAccount = from user in db.User_accounts
                                                   where user.user_name.Equals(account.UserName)
                                                   select user;
            if(userAccount.Count() <= 0)
                return false;

            userAccount.First().credit += (int)addAmount;
            db.SubmitChanges();
            return true;
        }

        public bool RentMedia(int mediaId, AccountCredentials credentials)
        {
            throw new NotImplementedException();
        }

        public bool PublishMedia(MediaInfo info, AccountCredentials credentials)
        {
            throw new NotImplementedException();
        }

        public bool DeleteAccount(AccountCredentials credentials)
        {
            throw new NotImplementedException();
        }

        /// <author>Per Mortensen</author>
        /// <summary>
        /// 
        /// </summary>
        /// <param name="newData"></param>
        /// <param name="credentials"></param>
        /// <returns></returns>
        public bool UpdateMediaMetadata(MediaInfo newData, AccountCredentials credentials)
        {
            var db = new DatabaseDataContext();
            if(!IsPublisherAuthorized(newData.Id, credentials, db))
                return false;

            // find media based on id
            IQueryable<Media> mediaResult = from m in db.Medias
                                            where m.id == newData.Id
                                            select m;
            if(mediaResult.Count() <= 0) // media was not found
                return false;
            Media media = mediaResult.First();

            // find new genre id based on genre name
            IQueryable<int> genreResult = from g in db.Genres
                                          where g.name.Equals(newData.Genre)
                                          select g.id;
            if(genreResult.Count() <= 0) // genre was not found
                return false;
            int genreId = genreResult.First();

            // update general metadata
            media.genre_id = genreId;
            media.price = newData.Price;
            media.release_date = newData.ReleaseDate;
            media.title = newData.Title;

            // update type-specific metadata
            switch(newData.Type) {
                case MediaType.Album:
                    var newAlbumData = (AlbumInfo)newData;
                    Album album = media.Album;
                    album.album_artist = newAlbumData.AlbumArtist;
                    album.description = newAlbumData.Description;
                    break;
                case MediaType.Book:
                    var newBookData = (BookInfo)newData;
                    Book book = media.Book;
                    book.author = newBookData.Author;
                    book.pages = newBookData.Pages;
                    book.summary = newBookData.Summary;
                    break;
                case MediaType.Movie:
                    var newMovieData = (MovieInfo)newData;
                    Movie movie = media.Movie;
                    movie.director = newMovieData.Director;
                    movie.length = (int)newMovieData.Duration.TotalSeconds;
                    movie.summary = newMovieData.Summary;
                    break;
                case MediaType.Song:
                    var newSongData = (SongInfo)newData;
                    Song song = media.Song;
                    song.artist = newSongData.Artist;
                    song.length = (int)newSongData.Duration.TotalSeconds;
                    break;
            }

            db.SubmitChanges();
            return true;
        }

        /// <author>Per Mortensen</author>
        /// <summary>
        /// 
        /// </summary>
        /// <param name="mediaId"></param>
        /// <param name="credentials"></param>
        /// <returns></returns>
        public bool DeleteMedia(int mediaId, AccountCredentials credentials)
        {
            var db = new DatabaseDataContext();
            if(!IsPublisherAuthorized(mediaId, credentials, db))
                return false;

            // find media based on id
            IQueryable<Media> mediaResult = from m in db.Medias
                                            where m.id == mediaId
                                            select m;
            if(mediaResult.Count() <= 0) // media was not found
                return false;

            Media media = mediaResult.First();
            db.Medias.DeleteOnSubmit(media);
            db.SubmitChanges();
            return true;
        }

        public Uri GetMediaUrl(string mediaId, AccountCredentials credentials)
        {
            throw new NotImplementedException();
        }

        /// <author>Per Mortensen</author>
        /// <summary>
        /// 
        /// </summary>
        /// <param name="mediaType"></param>
        /// <returns></returns>
        public List<string> GetAllGenres(MediaType mediaType)
        {
            var db = new DatabaseDataContext();

            string typeString = Util.StringValueOfMediaType(mediaType);

            IQueryable<string> genreResult;
            if(!typeString.Equals(Util.StringValueOfMediaType(MediaType.Any))) // find genres for specific media type
                genreResult = from t in db.Genres
                              where t.Media_type1.name.Equals(typeString)
                              select t.name;
            else // get all genres (any media type)
                genreResult = from t in db.Genres
                              select t.name;

            return genreResult.ToList();
        }

        #endregion

        #region Helper methods

        /// <author>Kenneth Søhrmann</author>
        /// <summary>
        /// Gets the media rating of a specified media item.
        /// The returned value contains the number of ratings and 
        /// the average rating.
        /// </summary>
        /// <param name="mediaId"></param>
        /// <param name="db"></param>
        /// <returns></returns>
        private RentItDatabase.Rating GetMediaRating(int mediaId, DatabaseDataContext db)
        {
            return (from r in db.Ratings where r.media_id.Equals(mediaId) select r).First();
        }

        /// <author>Kenneth Søhrmann</author>
        /// <summary>
        /// Collects all the user reviews registered for the specified media item.
        /// </summary>
        /// <param name="mediaId">
        /// The id of the media item to collect reviews for.
        /// </param>
        /// <param name="rating">
        /// Value holding the review counts and the average rating.
        /// Used for building the return value of this method.
        /// </param>
        /// <param name="db">
        /// The database context to retrieve the data from.
        /// </param>
        /// <returns>
        /// A MediaRating-object holding all review data of the specified
        /// media item.
        /// </returns>
        private RentIt.MediaRating CollectMediaReviews(int mediaId, RentItDatabase.Rating rating, DatabaseDataContext db)
        {
            // Get all the user reviews of the book.
            List<Review> reviews = (from r in db.Reviews where r.media_id.Equals(mediaId) select r).ToList();

            var mediaReviews = new List<MediaReview>();

            foreach (Review review in reviews)
            {
                mediaReviews.Add(
                    new MediaReview(
                        (DateTime)review.timestamp,
                        review.user_name,
                        review.review1,
                        Util.RatingOfValue((int)review.rating)));
            }

            var mediaRating = new MediaRating(rating.ratings_count, (int)rating.avg_rating, mediaReviews);

            return mediaRating;
        }

        /// <author>Per Mortensen</author>
        /// <summary>
        /// Determines whether the publisher account with the supplied credentials are authorized
        /// to change the media item's metadata.
        /// </summary>
        /// <param name="mediaId">
        /// The id of the media item to be changed.
        /// </param>
        /// <param name="credentials">
        /// The credentials of the publisher account.
        /// </param>
        /// <param name="db">
        /// The database context to retrieve data from.
        /// </param>
        /// <returns>
        /// true if the publisher can change the media, false otherwise.
        /// </returns>
        private bool IsPublisherAuthorized(int mediaId, AccountCredentials credentials, DatabaseDataContext db) {
            try {
                ValidateCredentials(credentials); // throws an exception if credentials are incorrect
            } catch(InvalidCredentialsException) {
                return false;
            }

            IQueryable<Media> mediaResult = from m in db.Medias
                                            where m.id == mediaId
                                            select m;

            if(mediaResult.Count() <= 0) // if the media with the specified id was not found
                return false;

            Media media = mediaResult.First();

            // find out whether this publisher is authorized to delete this media
            IQueryable<Publisher_account> p = from publisher in db.Publisher_accounts
                                              where publisher.user_name.Equals(credentials.UserName) && publisher.publisher_id == media.publisher_id
                                              select publisher;

            // if this publisher does not have permission to update the media
            return p.Count() > 0;
        }

        #endregion
    }
}
