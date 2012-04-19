﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.ServiceModel;
using System.Text;
using System.Text.RegularExpressions;
using RentItDatabase;

namespace RentIt
{
    public class RentItService : IRentIt
    {
        /// <author>Kenneth Søhrmann</author>
        /// <summary>
        /// See the interface specification of the method in IRentIt.cs for documentation
        /// of this method. 
        /// </summary>
        public BookInfo GetBookInfo(int id)
        {
            DatabaseDataContext db;
            try
            {
                db = new DatabaseDataContext();
            }
            catch (Exception e)
            {
                // Might be thrown if the service cannot communicate with the database properly.
                throw new FaultException<Exception>(
                    new Exception("An internal error has occured. This is not related to the input."));
            }

            // Get the book.
            RentItDatabase.Book book;
            try
            {
                book = (from b in db.Books
                        where b.media_id.Equals(id) && b.Media.active
                        select b).First();
            }

            // If no entity was found, the specified id does not excist.
            catch (Exception)
            {
                throw new FaultException<ArgumentException>(
                    new ArgumentException("The specified book id does not exist."));
            }

            // The BookInfo-instance to be compiled and returned to the client.
            RentIt.BookInfo bookInfo;
            try
            {
                // Get the rating data of the book.
                RentItDatabase.Rating rating = Util.GetMediaRating(book.media_id, db);

                // Get all the user reviews of the book.
                MediaRating mediaRating = Util.CollectMediaReviews(book.media_id, rating, db);

                bookInfo = RentIt.BookInfo.ValueOf(book, mediaRating);
            }
            catch (Exception e)
            {
                // Might be thrown if the service cannot communicate with the database properly.
                throw new FaultException<Exception>(
                  new Exception("An internal error has occured. This is not related to the input."));
            }

            return bookInfo;
        }

        /// <author>Kenneth Søhrmann</author>
        /// <summary>
        /// See the interface specification of the method in IRentIt.cs for documentation
        /// of this method.
        /// </summary>
        public MovieInfo GetMovieInfo(int id)
        {
            DatabaseDataContext db;
            try
            {
                db = new DatabaseDataContext();
            }
            catch (Exception e)
            {
                throw new FaultException<Exception>(
                    new Exception("An internal error has occured. This is not related to the input."));
            }

            // Get the movie.
            Movie movie;
            try
            {
                movie = (from m in db.Movies
                         where m.media_id.Equals(id) && m.Media.active
                         select m).First();
            }
            catch (Exception)
            {
                throw new FaultException<ArgumentException>(
                    new ArgumentException("The specified movie id does not exist."));
            }

            // The MovieInfo-instance to be compiled and returned to the client.
            RentIt.MovieInfo movieInfo;

            try
            {
                RentItDatabase.Rating rating = Util.GetMediaRating(movie.media_id, db);
                MediaRating mediaRating = Util.CollectMediaReviews(movie.media_id, rating, db);
                movieInfo = RentIt.MovieInfo.ValueOf(movie, mediaRating);
            }
            catch (Exception e)
            {
                // Might be thrown if the service cannot communicate with the database properly.
                throw new FaultException<Exception>(
                    new Exception("An internal error has occured. This is not related to the input."));
            }

            return movieInfo;
        }

        /// <author>Kenneth Søhrmann</author>
        /// <summary>
        /// See the interface specification of the method in IRentIt.cs for documentation
        /// of this method.
        /// </summary>
        public AlbumInfo GetAlbumInfo(int id)
        {
            DatabaseDataContext db;
            try
            {
                db = new DatabaseDataContext();
            }
            catch (Exception e)
            {
                // Might be thrown if the service cannot communicate with the database properly.
                throw new FaultException<Exception>(
                    new Exception("An internal error has occured. This is not related to the input."));
            }

            // Get the Album
            RentItDatabase.Album album;
            try
            {
                album = (from a in db.Albums
                         where a.media_id.Equals(id) && a.Media.active
                         select a).First();
            }
            catch (Exception)
            {
                // If no entity was found, the specified id does not excist.
                throw new FaultException<ArgumentException>(
                    new ArgumentException("The specified album id does not exist."));
            }

            // The albumInfo instance to be compiled and returned to the client.
            RentIt.AlbumInfo albumInfo;
            try
            {
                RentItDatabase.Rating rating = Util.GetMediaRating(album.media_id, db);
                MediaRating mediaRating = Util.CollectMediaReviews(album.media_id, rating, db);

                // Collect album songs
                IQueryable<Album_song> songs = from s in db.Album_songs where s.album_id.Equals(album.media_id) select s;

                // Collect data of all the songs contained in the album.
                List<RentIt.SongInfo> albumSongs = songs.Select(song => Util.GetSongInfo(song.song_id)).ToList();
                albumInfo = RentIt.AlbumInfo.ValueOf(album, albumSongs, mediaRating);
            }
            catch (Exception e)
            {
                // Might be thrown if the service cannot communicate with the database properly.
                throw new FaultException<Exception>(
                    new Exception("An internal error has occured. This is not related to the input."));
            }

            return albumInfo;
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
                throw new FaultException<ArgumentException>(
                   new ArgumentException("Null-value submittet as input."));
            }

            DatabaseDataContext db;
            try
            {
                db = new DatabaseDataContext();
            }
            catch (Exception e)
            {
                throw new FaultException<Exception>(
                    new Exception("An internal error has occured. This is not related to the input. " + e.Message));
            }

            // The MediaItems-instance to be compiled and returned to the client.
            RentIt.MediaItems mediaItems;
            try
            {
                // Get medias based on the media type.
                IQueryable<RentItDatabase.Media> medias;

                // Determine if the MediaType-value is MediaType.Any (null-value indicates MediaType.Any)
                bool mediaTypeAny = true;
                if (criteria.Type != null)
                {
                    mediaTypeAny = criteria.Type == MediaType.Any;
                }

                // If the value is specified to "any", all medias of the database is retrieved.
                if (mediaTypeAny)
                {
                    medias = from media in db.Medias
                             where media.active
                             select media;
                }
                else
                {
                    medias = from media in db.Medias
                             where media.Media_type.name.Equals(Util.StringValueOfMediaType(criteria.Type)) && media.active
                             select media;
                }

                // Sort the above medias as requested in the criterias.
                IQueryable<Media> orderedMedias = Util.OrderMedia(medias, criteria);

                // Filter the above medias after the genre if specified in the criteria.
                if (!string.IsNullOrEmpty(criteria.Genre))
                {
                    orderedMedias = from media in orderedMedias
                                    where media.Genre.name.Equals(criteria.Genre)
                                    select media;
                }

                if (!string.IsNullOrEmpty(criteria.SearchText))
                {
                    List<Media> list = new List<Media>();
                    foreach (Media m in orderedMedias)
                    {
                        if (Util.ContainsIgnoreCase(Util.GetMediaMetadataAsString(m), criteria.SearchText))
                        {
                            list.Add(m);
                        }
                    }

                    orderedMedias = list.AsQueryable();

                    /*
                    This was the original code, but it could not be translated to SQL statements by LINQ to SQL:
                    orderedMedias =
                        orderedMedias.Where(
                        media => Util.ContainsIgnoreCase(Util.GetMediaMetadataAsString(media), criteria.SearchText));
                    */
                }

                // Apply the offset and limit of the number of medias to return as specified.
                IQueryable<Media> finalMediaList;
                if (criteria.Limit < 0)
                    finalMediaList = orderedMedias.Skip(criteria.Offset);
                else
                    finalMediaList = orderedMedias.Skip(criteria.Offset).Take(criteria.Limit);

                mediaItems = Util.CompileMedias(finalMediaList, this);
            }
            catch (Exception e)
            {
                // Might be thrown if the service cannot communicate with the database properly.
                throw new FaultException<Exception>(
                    new Exception("An internal error has occured. This is not related to the input: " + e.Message));
            }

            return mediaItems;
        }

        /// <author>Jacob Rasmussen.</author>
        /// <summary>
        /// 
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public MediaItems GetAlsoRentedItems(int id)
        {
            if (id < 0)
                throw new FaultException<ArgumentException>(
                    new ArgumentException("The id was less than zero."));

            //Returns rentals made by users who also rented the given media item.
            //TODO: Return a maximum number of media items of each media type. Should the method only return media of the same type as the given media - discuss 27.03
            DatabaseDataContext db;
            try
            {
                db = new DatabaseDataContext();
            }
            catch (Exception e)
            {
                throw new FaultException<Exception>(
                    new Exception("Something unexpected went wrong (internally)."));
            }
            //Finds the user accounts who rented the media.
            IQueryable<string> users = from rental in db.Rentals
                                       where rental.media_id == id
                                       select rental.User_account.user_name;
            //Finds the rented medias made by all the users who rented the media.
            IQueryable<RentItDatabase.Media> rentals = (from rental in db.Rentals
                                                        where users.Contains(rental.User_account.user_name)
                                                        select rental.Media).Distinct();
            //Returns a MediaItems object containing lists of all the medias rented by the users who also rented the media with the given id.
            return Util.CompileMedias(rentals, this);
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
                throw new FaultException<ArgumentException>(
                    new ArgumentException("The credentials-parameter is a null reference."));
            }

            if (string.IsNullOrEmpty(credentials.UserName) || string.IsNullOrEmpty(credentials.HashedPassword))
            {
                throw new FaultException<ArgumentException>(
                    new ArgumentException("Invalid credentials."));
            }

            DatabaseDataContext db;
            try
            {
                db = new DatabaseDataContext();
            }
            catch (Exception e)
            {
                throw new FaultException<Exception>(
                    new Exception("An internal error has occured. This is not related to the input."));
            }

            // The Account-instance to be retrieved from the database.
            RentItDatabase.Account account;
            try
            {
                account = (from ac in db.Accounts
                           where
                               ac.user_name.Equals(credentials.UserName)
                               && ac.password.Equals(credentials.HashedPassword) && ac.active
                           select ac).First();
            }
            catch (Exception)
            {
                throw new FaultException<InvalidCredentialsException>(
                    new InvalidCredentialsException(), "The submitted Credentials are invalid");
            }

            // The credentials has successfully been evaluated, return account details to caller.
            return Account.ValueOf(account);
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
                throw new FaultException<ArgumentException>(
                   new ArgumentException("The newAccount-parameter is a null reference."));
            }

            // All information must be longer than 0 characters.
            if (newAccount.UserName.Length == 0 || newAccount.FullName.Length == 0 || newAccount.HashedPassword.Length == 0)
                throw new FaultException<UserCreationException>(
                    new UserCreationException("One or more pieces of information were empty."));


            // The e-mail address must adhere to a regex found on http://www.regular-expressions.info/email.html (reduced official standard)
            string emailRegex = @"[a-z0-9!#$%&'*+/=?^_`{|}~-]+(?:\.[a-z0-9!#$%&'*+/=?^_`{|}~-]+)*@(?:[a-z0-9](?:[a-z0-9-]*[a-z0-9])?\.)+[a-z0-9](?:[a-z0-9-]*[a-z0-9])?";
            if (!Regex.IsMatch(newAccount.Email, emailRegex))
                throw new FaultException<UserCreationException>(
                    new UserCreationException("The e-mail address is not valid."));

            DatabaseDataContext db;
            try
            {
                db = new DatabaseDataContext();
            }
            catch (Exception e)
            {
                throw new FaultException<Exception>(
                    new Exception("An internal error has occured. This is not related to the input."));
            }

            // If there exist an account with the submitted user name...
            if (db.Accounts.Exists(ac => ac.user_name.Equals(newAccount.UserName)))
            {
                // ...the request is told so.
                throw new FaultException<UserCreationException>(
                    new UserCreationException("The specified user name is already in use."));
            }

            // The user name is free, create a new instance with the data provided.
            var baseAccount = new RentItDatabase.Account
                {
                    user_name = newAccount.UserName,
                    full_name = newAccount.FullName,
                    email = newAccount.Email,
                    password = newAccount.HashedPassword,
                    active = true
                };
            var userAccount = new User_account { credit = 0, Account = baseAccount };

            try
            {
                db.User_accounts.InsertOnSubmit(userAccount);
                db.SubmitChanges();
            }
            catch (Exception e)
            {
                throw new FaultException<Exception>(
                    new Exception("An internal error has occured. This is not related to the input."));
            }

            return true;
        }

        /// <author>Jacob Rasmussen.</author>
        /// <summary>
        /// 
        /// </summary>
        /// <param name="credentials"></param>
        public UserAccount GetAllCustomerData(AccountCredentials credentials)
        {
            Account account = ValidateCredentials(credentials);

            if (Util.IsPublisher(account))
                throw new FaultException<InvalidCredentialsException>(
                    new InvalidCredentialsException("Credentials must not belong to a publisher account."));

            User_account userAccount;
            try
            {
                var db = new DatabaseDataContext();
                userAccount = (from user in db.User_accounts
                               where user.Account.user_name.Equals(account.UserName)
                               select user).First();
            }
            catch (Exception e)
            {
                throw new FaultException<Exception>(
                    new Exception("Something unexpected went wrong (internally)."));
            }
            //List of rentals made by the user. 
            var userRentals = new List<Rental>();
            if (userAccount.Rentals.Count > 0)
            {
                foreach (var rental in userAccount.Rentals)
                {
                    //Fills the userRentals-list with Rental-objects containing info from db.
                    MediaType mediaType = Util.MediaTypeOfValue(rental.Media.Media_type.name);
                    switch (mediaType)
                    {
                        case MediaType.Song:
                            break;
                        default:
                            userRentals.Add(new Rental(rental.media_id, mediaType, rental.start_time, rental.end_time));
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
        public PublisherAccount GetAllPublisherData(AccountCredentials credentials)
        {
            Account account = ValidateCredentials(credentials);

            DatabaseDataContext db;
            Publisher_account publisherAccount;
            try
            {
                db = new DatabaseDataContext();
                publisherAccount = (from publisher in db.Publisher_accounts
                                    where publisher.Account.user_name.Equals(account.UserName)
                                    select publisher).First();
            }
            catch (Exception e)
            {
                throw new FaultException<Exception>(
                    new Exception("Something unexpected went wrong (internally)."));
            }
            //Medias published by the given publisher account.
            IQueryable<RentItDatabase.Media> publishedMedias = from media in db.Medias
                                                               where media.publisher_id.Equals(publisherAccount.publisher_id)
                                                               select media;
            //Object containing the four lists of published items. Passed in with the new PublisherAccount-object.
            var mediaItems = Util.CompileMedias(publishedMedias, this);
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
                throw new FaultException<ArgumentException>(
                    new ArgumentException("The account-parameter is a null reference."));
            }

            ValidateCredentials(credentials);

            // The credentials was successfully validated.
            // Retrieve the corresponding account from the database.
            try
            {
                var db = new DatabaseDataContext();

                RentItDatabase.Account dbAccount =
                    (from acc in db.Accounts
                     where acc.user_name.Equals(credentials.UserName) && acc.active
                     select acc).First();

                // Update the database with the new submitted data.
                if (account.FullName.Length > 0)
                    dbAccount.full_name = account.FullName;
                string emailRegex = @"[a-z0-9!#$%&'*+/=?^_`{|}~-]+(?:\.[a-z0-9!#$%&'*+/=?^_`{|}~-]+)*@(?:[a-z0-9](?:[a-z0-9-]*[a-z0-9])?\.)+[a-z0-9](?:[a-z0-9-]*[a-z0-9])?";
                if (account.Email.Length > 0)
                {
                    if (Regex.IsMatch(account.Email, emailRegex))
                        dbAccount.email = account.Email;
                    else
                        throw new ArgumentException("The e-mail address was not valid.");
                }
                if (account.HashedPassword.Length > 0)
                    dbAccount.password = account.HashedPassword;

                // Submit the changes to the database.
                db.SubmitChanges();
            }
            catch (ArgumentException e)
            {
                throw new FaultException<ArgumentException>(e);
            }
            catch (Exception e)
            {
                throw new FaultException<Exception>(
                    new Exception("An internal error has occured. This is not related to the input."));
            }

            return true;
        }

        /// <author>Per Mortensen</author>
        public bool AddCredits(AccountCredentials credentials, uint addAmount)
        {
            ValidateCredentials(credentials);

            try
            {
                var db = new DatabaseDataContext();

                IQueryable<User_account> userAccount = from user in db.User_accounts
                                                       where user.user_name.Equals(credentials.UserName)
                                                       select user;

                userAccount.First().credit += (int)addAmount;
                db.SubmitChanges();
            }
            catch (Exception e)
            {
                throw new FaultException<Exception>(
                    new Exception("An internal error has occured. This is not related to the input."));
            }

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

            try
            {
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
            }
            catch (Exception e)
            {
                throw new FaultException<Exception>(
                    new Exception("An internal error has occured. This is not related to the input."));
            }

            return true;
        }

        /// <author>Lars Toft Jacobsen</author>
        /// <summary>
        /// Publish new media
        /// </summary>
        /// <param name="info"></param>
        /// <param name="credentials"></param>
        /// <returns></returns>
        public int PublishMedia(MediaInfo info, AccountCredentials credentials)
        {
            Account account = ValidateCredentials(credentials);
            if (account == null)
            {
                throw new FaultException<InvalidCredentialsException>(
                    new InvalidCredentialsException("Invalid credentials submitted.")); ;
            }

            if (!Util.IsPublisher(account))
                throw new FaultException<InvalidCredentialsException>(
                    new InvalidCredentialsException("This user is not a publisher."));


            var db = new DatabaseDataContext();
            Genre genre;

            // fetch mediatype and genre id's
            if (!db.Media_types.Exists(t => t.name.Equals(info.Type)))
            {
                throw new FaultException<ArgumentException>(
                    new ArgumentException("Invalid media type parameter"));
            }
            Media_type mtype = (from t in db.Media_types
                                where t.name.Equals(info.Type)
                                select t).First();

            // Check if the specfied genre exists.
            if (!db.Genres.Exists(g => g.name.Equals(info.Genre)))
            {
                Util.AddGenre(info.Genre, info.Type);
            }

            genre = (from g in db.Genres
                     where g.name.Equals(info.Genre) // && g.Media_type1.id.Equals(mtype.id)
                     select g).First();

            // Check if the specified publisher exists.
            if (!db.Publishers.Exists(p => p.title.Equals(info.Publisher)))
            {
                throw new FaultException<ArgumentException>(
                    new ArgumentException("Invalid publisher parameter"));
            }

            Publisher publisher = (from p in db.Publishers
                                   where p.title.Equals(info.Publisher)
                                   select p).First();

            try
            {
                var newMedia = new RentItDatabase.Media
                {
                    title = info.Title,
                    genre_id = genre.id,
                    type_id = mtype.id,
                    price = info.Price,
                    release_date = info.ReleaseDate,
                    publisher_id = publisher.id,
                    active = true
                };

                switch (info.Type)
                {
                    case MediaType.Album:
                        AlbumInfo albumInfo = (AlbumInfo)info;
                        RentItDatabase.Album newAlbum = new Album()
                        {
                            Media = newMedia,
                            album_artist = albumInfo.AlbumArtist,
                            description = albumInfo.Description
                        };
                        db.Albums.InsertOnSubmit(newAlbum);
                        break;
                    case MediaType.Book:
                        BookInfo bookInfo = (BookInfo)info;
                        RentItDatabase.Book newBook = new Book()
                        {
                            Media = newMedia,
                            author = bookInfo.Author,
                            pages = bookInfo.Pages,
                            summary = bookInfo.Summary
                        };
                        db.Books.InsertOnSubmit(newBook);
                        break;
                    case MediaType.Movie:
                        MovieInfo movieInfo = (MovieInfo)info;
                        RentItDatabase.Movie newMovie = new Movie()
                        {
                            Media = newMedia,
                            director = movieInfo.Director,
                            length = (int)movieInfo.Duration.TotalSeconds,
                            summary = movieInfo.Summary
                        };
                        db.Movies.InsertOnSubmit(newMovie);
                        break;
                    case MediaType.Song:
                        SongInfo songInfo = (SongInfo)info;
                        RentItDatabase.Song newSong = new Song()
                        {
                            Media = newMedia,
                            artist = songInfo.Artist,
                            length = (int)songInfo.Duration.TotalSeconds
                        };

                        db.Songs.InsertOnSubmit(newSong);

                        RentItDatabase.Album_song albumSong = new Album_song()
                        {
                            album_id = songInfo.AlbumId,
                            Song = newSong
                        };

                        db.Album_songs.InsertOnSubmit(albumSong);
                        break;
                }

                db.Medias.InsertOnSubmit(newMedia);
                db.SubmitChanges();

                var newRating = new RentItDatabase.Rating
                {
                    media_id = newMedia.id,
                    avg_rating = 0.0,
                    ratings_count = 0
                };
                db.Ratings.InsertOnSubmit(newRating);
                db.SubmitChanges();

                return newMedia.id;
            }
            catch (Exception e)
            {
                throw new FaultException<Exception>(
                    new Exception("An internal error has occured. This is not related to the input: " + e.Message));
            }
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
            // validate credentials
            Account account = ValidateCredentials(credentials);
            if (account == null) return false;

            try
            {
                var db = new DatabaseDataContext();
                RentItDatabase.Account acctResult = (from user in db.Accounts
                                                     where user.user_name.Equals(account.UserName)
                                                     select user).First();
                // set active flag
                acctResult.active = false;
                db.SubmitChanges();
            }
            catch (Exception e)
            {
                throw new FaultException<Exception>(
                    new Exception("An internal error has occured. This is not related to the input."));
            }

            return true;
        }

        /// <author>Per Mortensen</author>
        public bool UpdateMediaMetadata(MediaInfo newData, AccountCredentials credentials)
        {
            ValidateCredentials(credentials);

            DatabaseDataContext db;
            try
            {
                db = new DatabaseDataContext();
            }
            catch (Exception e)
            {
                throw new FaultException<Exception>(
                    new Exception("An internal error has occured. This is not related to the input."));
            }

            // Is publisher authorized for this media?
            if (!Util.IsPublisherAuthorized(newData.Id, credentials, db, this))
                throw new FaultException<InvalidCredentialsException>(
                    new InvalidCredentialsException("This user is not authorized to update this media."));

            try
            {
                // find media based on id
                IQueryable<Media> mediaResult = from m in db.Medias
                                                where m.id == newData.Id && m.active
                                                select m;
                if (mediaResult.Count() <= 0) // media was not found
                    return false;
                Media media = mediaResult.First();

                // add genre to database if it doesn't exist and get its genre id
                int genreId = Util.AddGenre(newData.Genre, newData.Type);

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
            }
            catch (Exception e)
            {
                throw new FaultException<Exception>(
                    new Exception("An internal error has occured. This is not related to the input."));
            }

            return true;
        }

        /// <author>Per Mortensen</author>
        public bool DeleteMedia(int mediaId, AccountCredentials credentials)
        {
            ValidateCredentials(credentials);

            DatabaseDataContext db;
            try
            {
                db = new DatabaseDataContext();
            }
            catch (Exception e)
            {
                throw new FaultException<Exception>(
                    new Exception("An internal error has occured. This is not related to the input."));
            }

            try
            {
                // find media based on id
                Media media = (from m in db.Medias
                               where m.id == mediaId && m.active
                               select m).First();

                // Is publisher authorized for this media?
                if (!Util.IsPublisherAuthorized(mediaId, credentials, db, this))
                    throw new FaultException<InvalidCredentialsException>(
                        new InvalidCredentialsException("This user is not authorized to delete this media."));

                // if refs to media in rentals/reviews only mark inactive
                if (db.Reviews.Exists(r => r.media_id.Equals(mediaId)) || db.Rentals.Exists(r => r.media_id.Equals(mediaId)))
                {
                    media.active = false;
                    db.SubmitChanges();
                    return true;
                }

                // media type of the media
                MediaType mediaType = Util.MediaTypeOfValue(media.Media_type.name);

                // delete type-specific media record
                switch (mediaType)
                {
                    case MediaType.Book:
                        Book book = (from m in db.Books
                                     where m.media_id == mediaId
                                     select m).First();
                        db.Books.DeleteOnSubmit(book);
                        break;
                    case MediaType.Movie:
                        Movie movie = (from m in db.Movies
                                       where m.media_id == mediaId
                                       select m).First();
                        db.Movies.DeleteOnSubmit(movie);
                        break;
                    case MediaType.Album:
                        Album album = (from m in db.Albums
                                       where m.media_id == mediaId
                                       select m).First();

                        // get album/song junctions
                        IQueryable<Album_song> albumSongs = from m in db.Album_songs
                                                            where m.album_id == media.id
                                                            select m;

                        // delete all album/song junctions and songs
                        foreach (var albumSong in albumSongs)
                        {
                            db.Album_songs.DeleteOnSubmit(albumSong);
                            db.Songs.DeleteOnSubmit(albumSong.Song);
                        }

                        // delete album
                        db.Albums.DeleteOnSubmit(album);
                        break;
                    case MediaType.Song:
                        Song song = (from m in db.Songs
                                     where m.media_id == mediaId
                                     select m).First();

                        // get album/song junctions
                        IQueryable<Album_song> albumSongs2 = from m in db.Album_songs
                                                             where m.song_id == media.id
                                                             select m;

                        // delete all album/song junctions and songs
                        foreach (var albumSong in albumSongs2)
                            db.Album_songs.DeleteOnSubmit(albumSong);

                        db.Songs.DeleteOnSubmit(song);
                        break;
                    default:
                        throw new ArgumentOutOfRangeException();
                }

                RentItDatabase.Rating rating = media.Rating;
                db.Ratings.DeleteOnSubmit(rating);

                if (media.Media_file != null)
                {
                    db.Media_files.DeleteOnSubmit(media.Media_file);
                }

                db.Medias.DeleteOnSubmit(media);
                db.SubmitChanges();
            }
            catch (ArgumentNullException)
            { // Media was not found
                throw new FaultException<ArgumentException>(
                    new ArgumentException("The Media with id " + mediaId + " does not exist."));
            }
            catch (Exception e)
            {
                throw new FaultException<Exception>(
                    new Exception("An internal error has occured. This is not related to the input."));
            }

            return true;
        }

        /*
        /// <author>Lars Toft Jacobsen</author>
        /// <summary>
        /// Fetches a file with metadata from the database by id
        /// </summary>
        /// <param name="mediaId"></param>
        /// <param name="credentials"></param>
        /// <returns>byte array representation of file data</returns>
        public MediaFile GetMediaData(string mediaId, AccountCredentials credentials)
        {
            Account account = ValidateCredentials(credentials);
            if (account == null) throw new InvalidCredentialsException();

            DatabaseDataContext db;
            try
            {
                db = new DatabaseDataContext();
            }
            catch (Exception e)
            {
                throw new FaultException<Exception>(
                    new Exception("Could not connect to database", e));
            }

            // query db for file entity with id == mediaId
            IQueryable<RentItDatabase.Media_file> mfiles = from f in db.Media_files
                                                           where f.id.Equals(mediaId)
                                                           select f;

            if (mfiles.Count() <= 0) throw new FaultException("File id not found in database");

            MediaFile file = new MediaFile(mfiles.First().data.ToArray(), mfiles.First().name, mfiles.First().extension);
            // return byte array from Linq.Binary object
            return file;
        }

        /// <summary>
        /// Upload new media binary to database
        /// </summary>
        /// <param name="mediaId">the id of the media the file is related to. If it exists the record will be updated
        /// with the new data.</param>
        /// <param name="mfile">The new file object</param>
        /// <param name="credentials">User credentials</param>
        /// <returns></returns>
        public bool UploadMediaData(int mediaId, MediaFile mfile, AccountCredentials credentials)
        {

            Account account = ValidateCredentials(credentials);
            if (account == null) return false;

            if (!Util.IsPublisher(account))
                throw new FaultException<InvalidCredentialsException>(
                    new InvalidCredentialsException("This user is not a publisher."));

            DatabaseDataContext db;
            try
            {
                db = new DatabaseDataContext();
            }
            catch (Exception e)
            {
                throw new FaultException<Exception>(
                    new Exception("Could not connect to database", e));
            }

            // query the database for existing media id
            IQueryable<Media_file> fs = from f in db.Media_files
                                        where f.id.Equals(mediaId)
                                        select f;

            if (fs.Count() > 0)
            {
                // update media file
                RentItDatabase.Media_file efile = fs.First();
                efile.data = mfile.FileData;
                efile.name = mfile.FileName;
                efile.extension = mfile.Extension;
            }
            else
            {
                // insert new media file
                var newm = new Media_file
                {
                    id = mediaId,
                    data = new System.Data.Linq.Binary(mfile.FileData),
                    name = mfile.FileName,
                    extension = mfile.Extension
                };
                db.Media_files.InsertOnSubmit(newm);
            }

            try
            {
                db.SubmitChanges();
                return true;
            }
            catch (Exception e)
            {
                throw new FaultException<Exception>(
                    new Exception("Could not update/insert record", e));
            }

        }
        */

        /// <author>Per Mortensen</author>
        public List<string> GetAllGenres(MediaType mediaType)
        {
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

            string typeString = Util.StringValueOfMediaType(mediaType);
            IQueryable<string> genreResult;

            try
            {
                if (!typeString.Equals(Util.StringValueOfMediaType(MediaType.Any)))
                    // find genres for specific media type
                    genreResult = from t in db.Genres
                                  where string.Compare(t.Media_type1.name, typeString, true) == 0
                                  select t.name;
                else // get all genres (any media type)
                    genreResult = from t in db.Genres
                                  select t.name;
            }
            catch (Exception e)
            {
                throw new FaultException<Exception>(
                    new Exception("An internal error has occured. This is not related to the input."));
            }

            return genreResult.ToList();
        }

        /// <author>Kenneth Søhrmann</author>
        public bool SubmitReview(MediaReview review, AccountCredentials credentials)
        {
            // Validate the credentials.
            ValidateCredentials(credentials);

            // Check if the user name of the credentials match the one of the review.
            if (!credentials.UserName.Equals(review.UserName))
            {
                throw new FaultException<ArgumentException>(
                    new ArgumentException("The user name specified in review does not match that of the credentials."));
            }

            DatabaseDataContext db;
            try
            {
                db = new DatabaseDataContext();
            }
            catch (Exception e)
            {
                throw new FaultException<Exception>(
                    new Exception("1. An internal error has occured. This is not related to the input. " + e.Message));
            }

            // Get the media instance reviewed.
            RentItDatabase.Media media;
            try
            {
                media = (from m in db.Medias
                         where m.id.Equals(review.MediaId)
                         select m).First();
            }
            catch (Exception)
            {
                throw new FaultException<ArgumentException>(
                    new ArgumentException("The specified media id does not exist."));
            }

            // Get the account of the reviewer.
            RentItDatabase.User_account userAccount;
            try
            {
                userAccount = (from u in db.User_accounts
                               where u.user_name.Equals(review.UserName)
                               select u).First();
            }
            catch (Exception)
            {
                throw new FaultException<ArgumentException>(
                    new ArgumentException("Only user accounts can add reviews. If it fails with a user account, an internal error has occured."));
            }

            // Create a new database-entry of the submitted review.
            var dbReview = new RentItDatabase.Review
            {
                Media = media,
                media_id = review.MediaId,
                review1 = review.ReviewText,
                rating = Util.ValueOfRating(review.Rating),
                timestamp = review.Timestamp,
                User_account = userAccount,
                user_name = review.UserName
            };

            try
            {
                db.Reviews.InsertOnSubmit(dbReview);

                // Calculate new average rating and rating count.
                double avgRating = media.Rating.avg_rating;
                int numOfRatings = media.Rating.ratings_count;
                media.Rating.avg_rating = ((avgRating * numOfRatings) + Util.ValueOfRating(review.Rating))
                                          / (numOfRatings + 1);
                media.Rating.ratings_count = numOfRatings + 1;

                db.SubmitChanges();
            }
            catch (Exception e)
            {
                throw new FaultException<Exception>(
                    new Exception("2 An internal error has occured. This is not related to the input. " + e.Message));
            }

            return true;
        }
    }
}
