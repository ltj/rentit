namespace RentIt
{
    using System;
    using System.Collections.Generic;

    using RentItDatabase;
    using System.Data.Linq;
    using System.Linq;

    public class RentItService : IRentIt
    {
        /// <author>Kenneth Søhrmann</author>
        /// <summary>
        /// See the interface specification of the method in IRentIt.cs for documentation
        /// of this method. 
        /// </summary>
        public BookInfo GetBookInfo(int id)
        {
            var db = new DatabaseDataContext();

            // Get the book.
            RentItDatabase.Book book;
            try
            {
                book = (from b in db.Books
                        where b.media_id.Equals(id)
                        select b).First();
            }

            // If no entity was found, the specified id does not excist.
            catch (Exception)
            {
                return null;
            }

            // Get the rating data of the book.
            RentItDatabase.Rating rating = Util.GetMediaRating(book.media_id, db);

            // Get all the user reviews of the book.
            MediaRating mediaRating = Util.CollectMediaReviews(book.media_id, rating, db);

            return RentIt.BookInfo.ValueOf(book, mediaRating);
        }

        /// <author>Kenneth Søhrmann</author>
        /// <summary>
        /// See the interface specification of the method in IRentIt.cs for documentation
        /// of this method.
        /// </summary>
        public MovieInfo GetMovieInfo(int id)
        {
            var db = new DatabaseDataContext();

            // Get the movie.
            Movie movie;
            try
            {
                movie = (from m in db.Movies where m.media_id.Equals(id) select m).First();
            }

            // If no entity was found, the specified id does not excist.
            catch (Exception)
            {
                return null;
            }

            RentItDatabase.Rating rating = Util.GetMediaRating(movie.media_id, db);

            MediaRating mediaRating = Util.CollectMediaReviews(movie.media_id, rating, db);

            return RentIt.MovieInfo.ValueOf(movie, mediaRating);
        }

        /// <author>Kenneth Søhrmann</author>
        /// <summary>
        /// See the interface specification of the method in IRentIt.cs for documentation
        /// of this method.
        /// </summary>
        public AlbumInfo GetAlbumInfo(int id)
        {
            var db = new DatabaseDataContext();

            // Get the Album
            RentItDatabase.Album album;
            try
            {
                album = (from a in db.Albums
                         where a.media_id.Equals(id)
                         select a).First();
            }

            // If no entity was found, the specified id does not excist.
            catch (Exception)
            {
                return null;
            }

            RentItDatabase.Rating rating = Util.GetMediaRating(album.media_id, db);

            MediaRating mediaRating = Util.CollectMediaReviews(album.media_id, rating, db);

            // Collect album songs
            IQueryable<Song> songs = from s in db.Songs
                                     where s.media_id.Equals(album.media_id)
                                     select s;

            // Collect data of all the songs contained in the album.
            List<RentIt.SongInfo> albumSongs = songs.Select(song => Util.GetSongInfo(song.media_id)).ToList();

            return RentIt.AlbumInfo.ValueOf(album, albumSongs, mediaRating);
        }

        /// <author>Kenneth Søhrmann</author>
        /// <summary>
        /// See the interface specification of the method in IRentIt.cs for documentation
        /// of this method.
        /// </summary>
        public MediaItems GetMediaItems(MediaCriteria criteria)
        {
            // If the criteria is a null reference, return null.
            if (criteria == null)
            {
                return null;
            }

            var db = new DatabaseDataContext();

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
            IQueryable<Media> orderedMedias = Util.OrderMedia(medias, criteria);

            // Filter the above medias after the genre if specified in the criteria.
            if (!criteria.Genre.Equals(String.Empty))
            {
                orderedMedias = from media in orderedMedias
                                where
                                    media.Genre.name.Contains(
                                        criteria.Genre)
                                select media;
            }

            if (!criteria.SearchText.Equals(String.Empty))
            {
                orderedMedias =
                    orderedMedias.Where(
                        media => Util.GetMediaMetadataAsString(media).Contains(criteria.SearchText));
            }

            // Apply the offset and limit of the number of medias to return as specified.
            IQueryable<Media> finalMediaList =
                orderedMedias.Skip(criteria.Offset).Take(criteria.Limit);

            return Util.CompileMedias(finalMediaList, this);
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
                    case MediaType.Book: books.Add(GetBookInfo(rental.Media.Book.media_id));
                        break;
                    case MediaType.Movie: movies.Add(GetMovieInfo(rental.Media.Movie.media_id));
                        break;
                    case MediaType.Song: RentItDatabase.Rating songRatings = Util.GetMediaRating(rental.media_id, db);
                        MediaRating songRating = Util.CollectMediaReviews(rental.media_id, songRatings, db);
                        songs.Add(SongInfo.ValueOf(rental.Media.Song, songRating));
                        break;
                    case MediaType.Album: albums.Add(GetAlbumInfo(rental.Media.Album.media_id));
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
            if (credentials == null)
            {
                return null;
            }

            var db = new DatabaseDataContext();

            IQueryable<RentItDatabase.Account> accounts =
                                  from ac in db.Accounts
                                  where
                                      ac.user_name.Equals(credentials.UserName)
                                      && ac.password.Equals(credentials.HashedPassword)
                                      && ac.active
                                  select ac;

            if (accounts.Count() <= 0)
                return null;

            // The credentials has successfully been evaluated, return account details to caller.
            return Account.ValueOf(accounts.First());
        }

        /// <author>Kenneth Søhrmann</author>
        /// <summary>
        /// See the interface specification of the method in IRentIt.cs for documentation
        /// of this method.
        /// </summary>
        public bool CreateNewUser(Account newAccount)
        {
            if (newAccount == null)
            {
                return false;
            }

            var db = new DatabaseDataContext();

            // If there exist an account with the submitted user name...
            if (db.Accounts.Exists(ac => ac.user_name.Equals(newAccount.UserName)))
            {
                // ...the request is told so.
                throw new UserCreationException("The specified user name is already in use");
            }

            // The user name is free, create a new instance with the data provided.
            var baseAccount = new RentItDatabase.Account
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
        public UserAccount GetAllCustomerData(AccountCredentials credentials)
        {
            Account account = ValidateCredentials(credentials);
            var db = new DatabaseDataContext();
            User_account userAccount = (from user in db.User_accounts
                                        where user.Account.user_name.Equals(account.UserName)
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
                        mediaReviews.Add(new MediaReview(review.media_id, review.user_name, review.review1, (Rating)review.rating, review.timestamp));
                    }
                    //The rating of the rental. Used when creating a SongInfo object.
                    var mediaRating = new MediaRating(
                        rental.Media.Rating.ratings_count, (float)rental.Media.Rating.avg_rating, mediaReviews);
                    //Fills the userRentals-list with Rental-objects containing info from db.
                    switch (Util.MediaTypeOfValue(rental.Media.Media_type.name))
                    {
                        case MediaType.Book:
                            userRentals.Add(new Rental(GetBookInfo(rental.media_id), rental.start_time, rental.end_time));
                            break;
                        case MediaType.Movie:
                            userRentals.Add(new Rental(GetMovieInfo(rental.media_id), rental.start_time, rental.end_time));
                            break;
                        case MediaType.Song:
                            userRentals.Add(new Rental(SongInfo.ValueOf(rental.Media.Song, mediaRating), rental.start_time, rental.end_time));
                            break;
                        case MediaType.Album:
                            userRentals.Add(new Rental(GetAlbumInfo(rental.media_id), rental.start_time, rental.end_time));
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
        public PublisherAccount GetAllPublisherData(AccountCredentials credentials)
        {
            Account account = ValidateCredentials(credentials);
            var db = new DatabaseDataContext();
            Publisher_account publisherAccount = (from publisher in db.Publisher_accounts
                                                  where publisher.Account.user_name.Equals(account.UserName)
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
                            books.Add(GetBookInfo(media.Book.media_id));
                            break;
                        case MediaType.Movie:
                            movies.Add(GetMovieInfo(media.Movie.media_id));
                            break;
                        case MediaType.Song:
                            RentItDatabase.Rating songRatings = Util.GetMediaRating(media.id, db);
                            MediaRating songRating = Util.CollectMediaReviews(media.id, songRatings, db);
                            songs.Add(SongInfo.ValueOf(media.Song, songRating));
                            break;
                        case MediaType.Album:
                            albums.Add(GetAlbumInfo(media.Album.media_id));
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
            if (account == null)
            {
                return false;
            }
            if (ValidateCredentials(credentials) == null)
            {
                return false;
            }

            // The credentials was successfully validated.
            // Retrieve the corresponding account from the database.
            var db = new DatabaseDataContext();
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
            if (account == null) return false;

            var db = new DatabaseDataContext();
            var r = new RentItDatabase.Rental
            {
                user_name = account.UserName,
                media_id = mediaId,
                start_time = DateTime.Now,
                end_time = DateTime.Now.AddDays(30)
            };

            db.Rentals.InsertOnSubmit(r);
            db.SubmitChanges();
            return true;
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
            var db = new DatabaseDataContext();

            // fetch mediatype and genre id's
            Media_type mtype = (from t in db.Media_types
                                where t.name.Equals(info.Type)
                                select t).First();
            Genre genre = (from g in db.Genres
                           where g.name.Equals(info.Genre) && g.media_type.Equals(mtype)
                           select g).First();
            Publisher publisher = (from p in db.Publishers
                                   where p.title.Equals(info.Publisher)
                                   select p).First();

            var newMedia = new RentItDatabase.Media
            {
                title = info.Title,
                genre_id = genre.id,
                type_id = mtype.id,
                price = info.Price,
                release_date = info.ReleaseDate,
                publisher_id = publisher.id
            };

            db.Medias.InsertOnSubmit(newMedia);
            db.SubmitChanges();
            return true;
        }

        /// <author>Lars Toft Jacobsen</author>
        /// <summary>
        /// Delete user account from RentIt database. Actually the
        /// user is only invalidated by setting the active flag to false.
        /// </summary>
        /// <param name="credentials"></param>
        /// <returns></returns>
        public bool DeleteAccount(AccountCredentials credentials)
        {

            // validate credentials and return false if null
            Account account = ValidateCredentials(credentials);
            if (account == null) return false;

            var db = new DatabaseDataContext();
            RentItDatabase.Account acctResult = (from user in db.Accounts
                                                 where user.user_name.Equals(account.UserName)
                                                 select user).First();

            // set active flag to false (invalidate)
            acctResult.active = false;

            // submit changes
            try {
                db.SubmitChanges();
            }
            catch {
                return false;
            }

            // everything went well
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
            if (!Util.IsPublisherAuthorized(newData.Id, credentials, db, this))
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
            if (!Util.IsPublisherAuthorized(mediaId, credentials, db, this))
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

        public Uri GetMediaUri(string mediaId, AccountCredentials credentials)
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

        /// <author>Kenneth Søhrmann</author>
        public bool SubmitReview(MediaReview review, AccountCredentials credentials) {
            if(ValidateCredentials(credentials) == null)
                return false;

            var db = new DatabaseDataContext();
            RentItDatabase.Media media = (from m in db.Medias
                                          where m.id.Equals(review.MediaId)
                                          select m).First();

            RentItDatabase.User_account userAccount = (from u in db.User_accounts 
                                                       where u.user_name.Equals(review.UserName)
                                                       select u).First();

            var dbReview = new RentItDatabase.Review {
                                                           Media = media,
                                                           media_id = review.MediaId,
                                                           review1 = review.ReviewText,
                                                           rating = Util.ValueOfRating(review.Rating),
                                                           timestamp = review.Timestamp,
                                                           User_account = userAccount,
                                                           user_name = review.UserName
                                                       };

            db.Reviews.InsertOnSubmit(dbReview);

            // Calculate new average rating and rating count.
            double avgRating = media.Rating.avg_rating;
            int numOfRatings = media.Rating.ratings_count;
            media.Rating.avg_rating = ((avgRating*numOfRatings) + Util.ValueOfRating(review.Rating))/(numOfRatings + 1);
            media.Rating.ratings_count = numOfRatings + 1;

            db.SubmitChanges();
            return true;
        }
    }
}
