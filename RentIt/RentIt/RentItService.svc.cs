namespace RentIt
{
    using System;
    using System.Collections.Generic;
    using System.IO;
    using System.Security.Authentication;
    using System.Text;
    using RentItDatabase;

    using System.Linq;

    public class RentItService : IRentIt
    {
        #region Interface implementation

        /// <author>Kenneth Søhrmann</author>
        /// <summary>
        /// See the interface specification of the method in IRentIt.cs for documentation
        /// of this method.
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
        /// See the interface specification of the method in IRentIt.cs for documentation
        /// of this method.
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
        /// See the interface specification of the method in IRentIt.cs for documentation
        /// of this method.
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

        /// <author>Kenneth Søhrmann</author>
        /// <summary>
        /// See the interface specification of the method in IRentIt.cs for documentation
        /// of this method.
        /// </summary>
        public MediaItems GetMediaItems(MediaCriteria criteria)
        {
            DatabaseDataContext db = new DatabaseDataContext();

            // Get medias based on the media type.
            IQueryable<RentItDatabase.Media> medias;
            string mediaType = Util.StringValueOfMediaType(criteria.Type);

            // If the value is specified to "any", all medias of the database is retrieved.
            if (mediaType.Equals("any"))
            {
                medias = from media in db.Medias
                         select media;
            }
            else
            {
                medias = from media in db.Medias
                         where media.Media_type.name.Equals(mediaType)
                         select media;
            }

            // Sort the above medias as requested in the criterias.
            IQueryable<RentItDatabase.Media> orderedMedias = this.OrderMedia(medias, criteria);

            // Filter the above medias after the genre if specified in the criteria.
            if (!criteria.Genre.Equals(string.Empty))
            {
                orderedMedias = from media in orderedMedias
                                where
                                    media.Genre.name.Contains(
                                        criteria.Genre)
                                select media;
            }

            if (!criteria.SearchText.Equals(string.Empty))
            {
                orderedMedias =
                    orderedMedias.Where(
                        media => this.GetMediaMetadataAsString(media).Contains(criteria.SearchText));
            }

            // Apply the offset and limit of the number of medias to return as specified.
            IQueryable<RentItDatabase.Media> finalMediaList =
                orderedMedias.Skip(criteria.Offset).Take(criteria.Limit);

            return this.CompileMedias(finalMediaList);
        }

        /// <author>Jacob Rasmussen.</author>
        /// <summary>
        /// 
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public MediaItems GetAlsoRentedItems(int id)
        {
            //Issue 101: As of yet the service finds the first user on the list of users, who have rented the media, 
            //and returns lists of all other rentals by that user. What if the user has no other rented items? 
            //Should it keep finding new users until a specific number of related media items have been found?
            //TODO: Find a specific number of media items and handle cases where the media has not been rented by anyone before.
            var db = new DatabaseDataContext();
            //Finds the user who rented the media.
            User_account user = (from rental in db.Rentals
                                 where rental.media_id == id
                                 select rental.User_account).First();
            var books = new List<BookInfo>();
            var movies = new List<MovieInfo>();
            var songs = new List<SongInfo>();
            var albums = new List<AlbumInfo>();
            foreach (var rental in user.Rentals)
            {
                //Adds each rental to their respective lists as *Info objects.
                switch (Util.MediaTypeOfValue(rental.Media.Media_type.name))
                {
                    case MediaType.Book: books.Add(this.GetBookInfo(rental.Media.Book.media_id));
                        break;
                    case MediaType.Movie: movies.Add(this.GetMovieInfo(rental.Media.Movie.media_id));
                        break;
                    case MediaType.Song: RentItDatabase.Rating songRatings = this.GetMediaRating(rental.media_id, db);
                        MediaRating songRating = this.CollectMediaReviews(rental.media_id, songRatings, db);
                        songs.Add(SongInfo.ValueOf(rental.Media.Song, songRating));
                        break;
                    case MediaType.Album: albums.Add(this.GetAlbumInfo(rental.Media.Album.media_id));
                        break;
                }
            }
            return new MediaItems(books, movies, albums, songs);
        }

        /// <author>Kenneth Søhrmann</author>
        /// <summary>
        /// See the interface specification of the method in IRentIt.cs for documentation
        /// of this method.
        /// </summary>
        public Account ValidateCredentials(AccountCredentials credentials)
        {
            var db = new DatabaseDataContext();

            IQueryable<RentItDatabase.Account> accounts =
                                  from ac in db.Accounts
                                  where
                                      ac.user_name.Equals(credentials.UserName)
                                      && ac.password.Equals(credentials.HashedPassword)
                                      && ac.active
                                  select ac;

            if(accounts.Count() <= 0)
                return null;

            // The credentials has successfully been evaluated, return account details to caller.
            return RentIt.Account.ValueOf(accounts.First());
        }

        /// <author>Kenneth Søhrmann</author>
        /// <summary>
        /// See the interface specification of the method in IRentIt.cs for documentation
        /// of this method.
        /// </summary>
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
            RentItDatabase.Account baseAccount = new RentItDatabase.Account()
                {
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

        /// <author>Jacob Rasmussen.</author>
        /// <summary>
        /// 
        /// </summary>
        /// <param name="credentials"></param>
        /// <returns></returns>
        public UserAccount GetAllCustomerData(AccountCredentials credentials)
        {
            Account account = this.ValidateCredentials(credentials);
            var db = new DatabaseDataContext();
            User_account userAccount = (from user in db.User_accounts
                                        where user.Account.email.Equals(account.Email)
                                        select user).First();
            //List of rentals made by the user. 
            var userRentals = new List<Rental>();
            if (userAccount.Rentals.Count > 0)
            {
                foreach (var rental in userAccount.Rentals)
                {
                    //List of mediareviews to be passed in with the new customer object.
                    var mediaReviews = new List<MediaReview>();
                    //Fills the mediareviews-list with MediaReview-objects containing info from db.
                    foreach (var review in rental.Media.Reviews)
                    {
                        mediaReviews.Add(new MediaReview(review.timestamp, review.user_name, review.review1, (Rating)review.rating));
                    }
                    //The rating of the rental. Used when creating a SongInfo object.
                    var mediaRating = new MediaRating(
                        rental.Media.Rating.ratings_count, (float)rental.Media.Rating.avg_rating, mediaReviews);
                    //Fills the userRentals-list with Rental-objects containing info from db.
                    switch (Util.MediaTypeOfValue(rental.Media.Media_type.name))
                    {
                        case MediaType.Book:
                            userRentals.Add(new Rental(this.GetBookInfo(rental.media_id), rental.start_time, rental.end_time));
                            break;
                        case MediaType.Movie:
                            userRentals.Add(new Rental(this.GetMovieInfo(rental.media_id), rental.start_time, rental.end_time));
                            break;
                        case MediaType.Song:
                            userRentals.Add(new Rental(SongInfo.ValueOf(rental.Media.Song, mediaRating), rental.start_time, rental.end_time));
                            break;
                        case MediaType.Album:
                            userRentals.Add(new Rental(this.GetAlbumInfo(rental.media_id), rental.start_time, rental.end_time));
                            break;
                    }
                }
            }
            return new UserAccount(userAccount.user_name, userAccount.Account.full_name, userAccount.Account.email,
                userAccount.Account.password, userAccount.credit, userRentals);
        }

        /// <author>Jacob Rasmussen.</author>
        /// <summary>
        /// 
        /// </summary>
        /// <param name="credentials"></param>
        /// <returns></returns>
        public PublisherAccount GetAllPublisherData(AccountCredentials credentials)
        {
            Account account = this.ValidateCredentials(credentials);
            var db = new DatabaseDataContext();
            Publisher_account publisherAccount = (from publisher in db.Publisher_accounts
                                                  where publisher.Account.email.Equals(account.Email)
                                                  select publisher).First();
            //Medias published by the given publisher account.
            IQueryable<RentItDatabase.Media> publishedMedias = from media in db.Medias
                                                               where media.publisher_id.Equals(publisherAccount.publisher_id)
                                                               select media;
            var books = new List<BookInfo>();
            var movies = new List<MovieInfo>();
            var songs = new List<SongInfo>();
            var albums = new List<AlbumInfo>();
            if (publishedMedias.Count() > 0)
            {
                foreach (var media in publishedMedias)
                {
                    //Fills each list with the respective *Info-objects.
                    switch (Util.MediaTypeOfValue(media.Media_type.name))
                    {
                        case MediaType.Book:
                            books.Add(this.GetBookInfo(media.Book.media_id));
                            break;
                        case MediaType.Movie:
                            movies.Add(this.GetMovieInfo(media.Movie.media_id));
                            break;
                        case MediaType.Song:
                            RentItDatabase.Rating songRatings = this.GetMediaRating(media.id, db);
                            MediaRating songRating = this.CollectMediaReviews(media.id, songRatings, db);
                            songs.Add(SongInfo.ValueOf(media.Song, songRating));
                            break;
                        case MediaType.Album:
                            albums.Add(this.GetAlbumInfo(media.Album.media_id));
                            break;
                    }
                }
            }
            //Object containing the four lists of published items. Passed in with the new PublisherAccount-object.
            var mediaItems = new MediaItems(books, movies, albums, songs);
            return new PublisherAccount(publisherAccount.user_name, publisherAccount.Account.full_name,
                publisherAccount.Account.email, publisherAccount.Account.password, publisherAccount.Publisher.title, mediaItems);
        }

        /// <author>Kenneth Søhrmann</author>
        /// <summary>
        /// See the interface specification of the method in IRentIt.cs for documentation
        /// of this method.
        /// </summary>
        public bool UpdateAccountInfo(AccountCredentials credentials, Account account)
        {
            try
            {
                this.ValidateCredentials(credentials);
            }
            catch (Exception)
            {
                throw new InvalidCredentialException("Submitted credentials are invalid.");
            }

            // The credentials was successfully validated.
            // Retrieve the corresponding account from the database.
            DatabaseDataContext db = new DatabaseDataContext();
            RentItDatabase.Account dbAccount = (from acc in db.Accounts
                                                where acc.user_name.Equals(credentials.UserName)
                                                select acc).First();

            // Update the database with the new submitted data.
            dbAccount.full_name = account.FullName;
            dbAccount.email = account.Email;
            dbAccount.password = account.HashedPassword;

            // Submit the changes to the database.
            db.SubmitChanges();
            return true;
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
            if (userAccount.Count() <= 0)
                return false;

            userAccount.First().credit += (int)addAmount;
            db.SubmitChanges();
            return true;
        }

        /// <author>Lars Toft Jacobsen</author>
        /// <summary>
        /// Rent media
        /// </summary>
        /// <param name="mediaId"></param>
        /// <param name="credentials"></param>
        /// <returns></returns>
        public bool RentMedia(int mediaId, AccountCredentials credentials)
        {
            Account account = ValidateCredentials(credentials);
            var db = new DatabaseDataContext();
            throw new NotImplementedException();
        }

        /// <author>Lars Toft Jacobsen</author>
        /// <summary>
        /// Publish new media
        /// </summary>
        /// <param name="info"></param>
        /// <param name="credentials"></param>
        /// <returns></returns>
        public bool PublishMedia(MediaInfo info, AccountCredentials credentials)
        {
            //var db = new DatabaseDataContext();



            //RentItDatabase.Media baseMedia = new RentItDatabase.Media() {
            //    title = info.Title,
            //    genre_id = info.Genre,
            //    type_id = info.Type,

            //};

            throw new NotImplementedException();
        }

        /// <author>Lars Toft Jacobsen</author>
        /// <summary>
        /// Delete user account from RentIt database
        /// </summary>
        /// <param name="credentials"></param>
        /// <returns></returns>
        public bool DeleteAccount(AccountCredentials credentials)
        {

            // TODO: mark account inactive in stead of 
            Account account = ValidateCredentials(credentials);
            var db = new DatabaseDataContext();

            IQueryable<User_account> userResult = from user in db.User_accounts
                                                  where user.user_name.Equals(account.UserName)
                                                  select user;
            IQueryable<RentItDatabase.Account> acctResult = from user in db.Accounts
                                                            where user.user_name.Equals(account.UserName)
                                                            select user;

            if (userResult.Count() <= 0 || acctResult.Count() <= 0) return false;

            User_account use = userResult.First();
            RentItDatabase.Account acc = acctResult.First();
            db.User_accounts.DeleteOnSubmit(use);
            db.Accounts.DeleteOnSubmit(acc);
            db.SubmitChanges();
            return true;
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
            if (!IsPublisherAuthorized(newData.Id, credentials, db))
                return false;

            // find media based on id
            IQueryable<Media> mediaResult = from m in db.Medias
                                            where m.id == newData.Id
                                            select m;
            if (mediaResult.Count() <= 0) // media was not found
                return false;
            Media media = mediaResult.First();

            // find new genre id based on genre name
            IQueryable<int> genreResult = from g in db.Genres
                                          where g.name.Equals(newData.Genre)
                                          select g.id;
            if (genreResult.Count() <= 0) // genre was not found
                return false;
            int genreId = genreResult.First();

            // update general metadata
            media.genre_id = genreId;
            media.price = newData.Price;
            media.release_date = newData.ReleaseDate;
            media.title = newData.Title;

            // update type-specific metadata
            switch (newData.Type)
            {
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
            if (!IsPublisherAuthorized(mediaId, credentials, db))
                return false;

            // find media based on id
            IQueryable<Media> mediaResult = from m in db.Medias
                                            where m.id == mediaId
                                            select m;
            if (mediaResult.Count() <= 0) // media was not found
                return false;

            Media media = mediaResult.First();
            db.Medias.DeleteOnSubmit(media);
            db.SubmitChanges();
            return true;
        }

        public System.Uri GetMediaUri(string mediaId, AccountCredentials credentials)
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
            if (!typeString.Equals(Util.StringValueOfMediaType(MediaType.Any))) // find genres for specific media type
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
        /// Helper method for collection all data of a specified
        /// Song-item.
        /// </summary>
        /// <param name="id">
        /// The id of the song requested.
        /// </param>
        /// <returns>'
        /// A instance of SongInfo holding all data of the specified
        /// song item.
        /// </returns>
        private SongInfo GetSongInfo(int id)
        {
            DatabaseDataContext db = new DatabaseDataContext();

            // Get the song.
            RentItDatabase.Song song = (from m in db.Songs
                                        where m.media_id.Equals(id)
                                        select m).First();

            RentItDatabase.Rating songRatings = this.GetMediaRating(song.media_id, db);

            RentIt.MediaRating songRating = this.CollectMediaReviews(song.media_id, songRatings, db);

            return SongInfo.ValueOf(song, songRating);
        }

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
        private RentIt.MediaRating CollectMediaReviews(
            int mediaId, RentItDatabase.Rating rating, DatabaseDataContext db)
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

        /// <author>Lars Toft Jacobsen</author>
        /// <summary>
        /// Checks whether user is publisher or not
        /// </summary>
        /// <param name="acct"></param>
        /// <returns></returns>
        private bool isPublisher(Account acct) {
            var db = new DatabaseDataContext();

            IQueryable<Publisher_account> pubResult = from user in db.Publisher_accounts
                                                      where user.user_name.Equals(acct.UserName)
                                                      select user;

            if (pubResult.Count() <= 0) return false;
            return true;
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
        private bool IsPublisherAuthorized(int mediaId, AccountCredentials credentials, DatabaseDataContext db)
        {
            try
            {
                ValidateCredentials(credentials); // throws an exception if credentials are incorrect
            }
            catch (InvalidCredentialsException)
            {
                return false;
            }

            IQueryable<Media> mediaResult = from m in db.Medias where m.id == mediaId select m;

            if (mediaResult.Count() <= 0) // if the media with the specified id was not found
                return false;

            Media media = mediaResult.First();

            // find out whether this publisher is authorized to delete this media
            IQueryable<Publisher_account> p = from publisher in db.Publisher_accounts
                                              where
                                                  publisher.user_name.Equals(credentials.UserName)
                                                  && publisher.publisher_id == media.publisher_id
                                              select publisher;

            // if this publisher does not have permission to update the media
            return p.Count() > 0;
        }

        /// <author>Kenneth Søhrmann</author>
        /// <summary>
        /// 
        /// </summary>
        /// <param name="mediaItems"></param>
        /// <param name="criteria"></param>
        /// <returns></returns>
        private IQueryable<RentItDatabase.Media> OrderMedia(IQueryable<RentItDatabase.Media> mediaItems, MediaCriteria criteria)
        {
            DatabaseDataContext db = new DatabaseDataContext();

            switch (criteria.Order)
            {
                case MediaOrder.AlphabeticalAsc:
                    return from media in mediaItems
                           orderby media.title
                           select media;
                case MediaOrder.AlphabeticalDesc:
                    return from media in mediaItems
                           orderby media.title descending
                           select media;
                case MediaOrder.PopularityAsc:
                    // Collect structures with media id an the number of rentals for the media id.
                    var ac = from m in mediaItems
                             select new
                                 {
                                     m.id,
                                     RentalCount = (from mr in db.Rentals
                                                    where mr.media_id == m.id
                                                    select mr).Count()
                                 };

                    // Order the media items after rentals, ascending order.
                    return from media in mediaItems
                           from me in ac
                           where media.id == me.id
                           orderby me.RentalCount
                           select media;

                case MediaOrder.PopularityDesc:
                    // Collect structures with media id an the number of rentals for the media id.
                    var ac2 = from m in mediaItems
                              select new
                              {
                                  m.id,
                                  RentalCount = (from mr in db.Rentals
                                                 where mr.media_id == m.id
                                                 select mr).Count()
                              };

                    // Order the media items after rentals, descending order.
                    return from media in mediaItems
                           from me in ac2
                           where media.id == me.id
                           orderby me.RentalCount descending
                           select media;

                case MediaOrder.RatingAsc:
                    return from media in mediaItems
                           orderby media.Rating.avg_rating
                           select media;

                case MediaOrder.RatingDesc:
                    return from media in mediaItems
                           orderby media.Rating.avg_rating descending
                           select media;

                case MediaOrder.ReleaseDateAsc:
                    return from media in mediaItems
                           orderby media.release_date
                           select media;

                case MediaOrder.ReleaseDateDesc:
                    return from media in mediaItems
                           orderby media.release_date descending
                           select media;

                case MediaOrder.PriceAsc:
                    return from media in mediaItems
                           orderby media.price
                           select media;

                case MediaOrder.PriceDesc:
                    return from media in mediaItems
                           orderby media.price descending
                           select media;

                default:
                    return mediaItems;
            }
        }

        /// <author>Kenneth Søhrmann</author>
        /// <summary>
        /// Returns a string, which is a concatenation of the metadata stored in the database
        /// about a media. Utilized to easily apply search text the a media item.
        /// </summary>
        /// <param name="mediaItem">
        /// 
        /// </param>
        /// <returns>
        /// 
        /// </returns>
        private string GetMediaMetadataAsString(Media mediaItem)
        {
            StringBuilder metadataString = new StringBuilder();

            metadataString.Append(mediaItem.title + " ");
            metadataString.Append(mediaItem.Publisher.title);

            DatabaseDataContext db = new DatabaseDataContext();
            switch (Util.MediaTypeOfValue(mediaItem.Media_type.name))
            {
                case MediaType.Book:
                    RentItDatabase.Book book = mediaItem.Book;
                    metadataString.Append(book.author + " ");
                    metadataString.Append(book.summary);
                    return metadataString.ToString();
                case MediaType.Movie:
                    RentItDatabase.Movie movie = mediaItem.Movie;
                    metadataString.Append(movie.director + " ");
                    metadataString.Append(movie.summary);
                    return metadataString.ToString();
                case MediaType.Song:
                    RentItDatabase.Song song = mediaItem.Song;
                    metadataString.Append(song.artist + " ");
                    return metadataString.ToString();
                case MediaType.Album:
                    RentItDatabase.Album album = mediaItem.Album;
                    metadataString.Append(album.album_artist + " ");
                    metadataString.Append(album.description);
                    return metadataString.ToString();
                default:
                    return metadataString.ToString();
            }
        }

        /// <author>Kenneth Søhrmann</author>
        /// <summary>
        /// Converts a list of media-instances from the database to an instance of
        /// MediaItems with all data of the various media items submitted.
        /// </summary>
        /// <param name="mediaList">
        /// The collection of media-instances to be converted to a MediaItems-
        /// instance.
        /// </param>
        /// <returns>
        /// A MediaItems-instance holding all metadata of all the medias submitted.
        /// </returns>
        private MediaItems CompileMedias(IQueryable<RentItDatabase.Media> mediaList)
        {
            List<RentIt.BookInfo> bookInfos = new List<BookInfo>();
            List<RentIt.MovieInfo> movieInfos = new List<MovieInfo>();
            List<RentIt.SongInfo> songInfos = new List<SongInfo>();
            List<RentIt.AlbumInfo> albumInfos = new List<AlbumInfo>();

            foreach (RentItDatabase.Media media in mediaList)
            {
                switch (Util.MediaTypeOfValue(media.Media_type.name))
                {
                    case MediaType.Book:
                        bookInfos.Add(this.GetBookInfo(media.id));
                        break;
                    case MediaType.Movie:
                        movieInfos.Add(this.GetMovieInfo(media.id));
                        break;
                    case MediaType.Song:
                        songInfos.Add(this.GetSongInfo(media.id));
                        break;
                    case MediaType.Album:
                        albumInfos.Add(this.GetAlbumInfo(media.id));
                        break;
                }
            }
            return new MediaItems(bookInfos, movieInfos, albumInfos, songInfos);
        }

        #endregion
    }
}
