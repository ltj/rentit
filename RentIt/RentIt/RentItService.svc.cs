namespace RentIt
{
    using System;
    using System.Collections.Generic;
    using RentItDatabase;
    using System.Linq;

    public class RentItService : IRentIt {
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
            DatabaseDataContext db = new DatabaseDataContext();

            // Get the book.
            RentItDatabase.Book book = (from b in db.Books
                                        where b.media_id.Equals(id)
                                        select b).First();

            // Get the rating data of the book.
            RentItDatabase.Rating rating = this.GetMediaRating(book.media_id, db);

            // Get all the user reviews of the book.
            RentIt.MediaRating mediaRating = this.CollectMediaReviews(book.media_id, rating, db);

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
            DatabaseDataContext db = new DatabaseDataContext();

            // Get the movie.
            RentItDatabase.Movie movie = (from m in db.Movies
                                          where m.media_id.Equals(id)
                                          select m).First();

            RentItDatabase.Rating rating = this.GetMediaRating(movie.media_id, db);

            RentIt.MediaRating mediaRating = this.CollectMediaReviews(movie.media_id, rating, db);

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
            DatabaseDataContext db = new DatabaseDataContext();

            // Get the Album
            RentItDatabase.Album album = (from a in db.Albums
                                          where a.media_id.Equals(id)
                                          select a).First();

            RentItDatabase.Rating rating = this.GetMediaRating(album.media_id, db);

            RentIt.MediaRating mediaRating = this.CollectMediaReviews(album.media_id, rating, db);

            // Collect album songs
            IQueryable<RentItDatabase.Song> songs = from s in db.Songs
                                                    where s.media_id.Equals(album.media_id)
                                                    select s;

            // Collect data of all the songs contained in the album.
            List<RentIt.SongInfo> albumSongs = new List<RentIt.SongInfo>();
            foreach (RentItDatabase.Song song in songs)
            {
                RentItDatabase.Rating songRatings = this.GetMediaRating(album.media_id, db);
                RentIt.MediaRating songRating = this.CollectMediaReviews(song.media_id, songRatings, db);

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
            DatabaseDataContext db = new DatabaseDataContext();

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
            DatabaseDataContext db = new DatabaseDataContext();

            // If there exist an account with the submitted user name...
            if (db.Accounts.Exists(ac => ac.user_name.Equals(newAccount.UserName)))
            {
                // ...the request is told so.
                throw new UserCreationException("The specified user name is already in use");
            }

            // The user name is free, create a new instance with the data provided.
            RentItDatabase.Account baseAccount = new RentItDatabase.Account()
            {
                user_name = newAccount.UserName,
                full_name = newAccount.FullName,
                email = newAccount.Email,
                password = newAccount.HashedPassword
            };
            User_account userAccount = new User_account()
            {
                credit = 0,
                Account = baseAccount
            };

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

        public bool AddCredits(AccountCredentials credentials, uint addAmount)
        {
            Account account = ValidateCredentials(credentials);
            var db = new DatabaseDataContext();

            User_account userAccount = (from user in db.User_accounts
                                        where user.user_name.Equals(account.UserName)
                                        select user).First();
            userAccount.credit += (int)addAmount;
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

        public bool UpdateMediaMetadata(MediaInfo newData, AccountCredentials credentials)
        {
            throw new NotImplementedException();
        }

        public bool DeleteMedia(int mediaId, AccountCredentials credentials)
        {
            throw new NotImplementedException();
        }

        public Uri GetMediaUrl(string mediaId, AccountCredentials credentials)
        {
            throw new NotImplementedException();
        }

        public List<string> GetAllGenres(MediaType mediaType)
        {
            throw new NotImplementedException();
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
            return (from r in db.Ratings
                    where r.media_id.Equals(mediaId)
                    select r).First();
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
        private RentIt.MediaRating CollectMediaReviews(
            int mediaId,
            RentItDatabase.Rating rating,
            DatabaseDataContext db)
        {
            // Get all the user reviews of the book.
            List<Review> reviews = (from r in db.Reviews
                                    where r.media_id.Equals(mediaId)
                                    select r).ToList();

            List<RentIt.MediaReview> mediaReviews = new List<MediaReview>();

            foreach (Review review in reviews)
            {
                mediaReviews.Add(new MediaReview(
                    (DateTime)review.timestamp,
                    review.user_name,
                    review.review1,
                    Util.RatingOfValue((int)review.rating)));
            }

            MediaRating mediaRating = new MediaRating(
                    (int)rating.ratings_count,
                    (int)rating.avg_rating,
                     mediaReviews);

            return mediaRating;
        }
        #endregion
    }
}
