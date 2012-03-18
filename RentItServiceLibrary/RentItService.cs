using System;
using System.Collections.Generic;
using RentItDatabase;

namespace RentItServiceLibrary
{
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
            DatabaseDataContext db = new DatabaseDataContext();

            // Get the book.
            RentItDatabase.Book book = (from b in db.Books
                                        where b.media_id.Equals(id)
                                        select b).First();

            // Get the rating data of the book.
            RentItDatabase.Rating rating = this.GetMediaRating(book.media_id, db);

            // Get all the user reviews of the book.
            RentItServiceLibrary.MediaRating mediaRating = this.CollectMediaReviews(book.media_id, rating, db);

            return RentItServiceLibrary.BookInfo.ValueOf(book, mediaRating);
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

            RentItServiceLibrary.MediaRating mediaRating = this.CollectMediaReviews(movie.media_id, rating, db);

            return RentItServiceLibrary.MovieInfo.ValueOf(movie, mediaRating);
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

            RentItServiceLibrary.MediaRating mediaRating = this.CollectMediaReviews(album.media_id, rating, db);

            // Collect album songs
            IQueryable<RentItDatabase.Song> songs = from s in db.Songs
                                                    where s.media_id.Equals(album.media_id)
                                                    select s;

            // Collect data of all the songs contained in the album.
            List<RentItServiceLibrary.SongInfo> albumSongs = new List<SongInfo>();
            foreach (RentItDatabase.Song song in songs)
            {
                RentItDatabase.Rating songRatings = this.GetMediaRating(album.media_id, db);
                RentItServiceLibrary.MediaRating songRating = this.CollectMediaReviews(song.media_id, songRatings, db);

                albumSongs.Add(SongInfo.ValueOf(song, songRating));
            }

            return RentItServiceLibrary.AlbumInfo.ValueOf(album, albumSongs, mediaRating);
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
            //test;1234
            if (credentials.UserName.Equals("test") && credentials.HashedPassword.Equals("7110eda4d09e062aa5e4a390b0a572ac0d2c0220"))
                return new Account("test", "Test T. Testy", "testtesty@testland.com", "7110eda4d09e062aa5e4a390b0a572ac0d2c0220");
            throw new InvalidCredentialsException("The credentials were wrong! Bad user! >:-(");
        }

        public bool CreateNewUser(Account newAccount)
        {
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
            throw new NotImplementedException();
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

        #endregion
        #region Helper methods
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
        private RentItServiceLibrary.MediaRating CollectMediaReviews(
            int mediaId,
            RentItDatabase.Rating rating,
            DatabaseDataContext db)
        {
            // Get all the user reviews of the book.
            List<Review> reviews = (from r in db.Reviews
                                    where r.media_id.Equals(mediaId)
                                    select r).ToList();

            List<RentItServiceLibrary.MediaReview> mediaReviews = new List<MediaReview>();

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
