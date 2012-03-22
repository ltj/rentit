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
            Book book = (from b in db.Books where b.media_id.Equals(id) select b).First();

            // Get the rating data of the book.
            RentItDatabase.Rating rating = Util.GetMediaRating(book.media_id, db);

            // Get all the user reviews of the book.
            MediaRating mediaRating = Util.CollectMediaReviews(book.media_id, rating, db);

            return BookInfo.ValueOf(book, mediaRating);
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
            Movie movie = (from m in db.Movies where m.media_id.Equals(id) select m).First();

            RentItDatabase.Rating rating = Util.GetMediaRating(movie.media_id, db);

            MediaRating mediaRating = Util.CollectMediaReviews(movie.media_id, rating, db);

            return MovieInfo.ValueOf(movie, mediaRating);
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
            Album album = (from a in db.Albums where a.media_id.Equals(id) select a).First();

            RentItDatabase.Rating rating = Util.GetMediaRating(album.media_id, db);

            MediaRating mediaRating = Util.CollectMediaReviews(album.media_id, rating, db);

            // Collect album songs
            IQueryable<Song> songs = from s in db.Songs
                                                    where s.media_id.Equals(album.media_id)
                                                    select s;

            // Collect data of all the songs contained in the album.
            var albumSongs = new List<SongInfo>();
            foreach (Song song in songs)
            {
                RentItDatabase.Rating songRatings = Util.GetMediaRating(album.media_id, db);
                MediaRating songRating = Util.CollectMediaReviews(song.media_id, songRatings, db);

                albumSongs.Add(SongInfo.ValueOf(song, songRating));
            }

            return AlbumInfo.ValueOf(album, albumSongs, mediaRating);
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
            IQueryable<Media> medias;
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

        public MediaItems GetAlsoRentedItems(int id)
        {
            throw new NotImplementedException();
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
            return Account.ValueOf(accounts.First());
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

        public UserAccount GetAllCustomerData(AccountCredentials credentials)
        {
            throw new NotImplementedException();
        }

        public PublisherAccount GetAllPublisherData(AccountCredentials credentials)
        {
            throw new NotImplementedException();
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

        #endregion
    }
}
