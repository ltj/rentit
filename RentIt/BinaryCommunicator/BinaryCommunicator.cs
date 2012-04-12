using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace BinaryCommunicator
{
    using System.Drawing;
    using System.IO;
    using System.Net;

    using RentIt;

    using RentItDatabase;

    /// <summary>
    /// Event handler for the FileUploadedEvent.
    /// </summary>
    public delegate void FileUploaded();

    /// <author>Kenneth Søhrmann</author>
    /// <summary>
    /// The methods of this class is used to communicate binary data of the 
    /// RentItService-web service. With these methods it is possible to download
    /// thumbnails and media files and it is possible to upload data to the service,
    /// if the client is a registered publisher of the service.
    /// </summary>
    public static class BinaryCommuncator
    {
        /// <summary>
        /// The FileUploadedEvent that is fired everytime a file has been
        /// uploaded to the server.
        /// </summary>
        public static event FileUploaded FileUploadedEvent;

        /// <author>Kenneth Søhrmann</author>
        /// <summary>
        /// Compiles an URL which identifies the requested media item on the server.
        /// Be aware that creating a client with this URL might throw exceptions for bad
        /// requests, e.g. requests where the credentials are invalid, the media does not 
        /// exist or the user, identified by the credentials, have not currently rented the
        /// specified media. Invoking the URL under such circumstances will make the HTTP
        /// request get a "Bad Request 400"-error message.
        /// 
        /// Example:
        /// In order to play a downloaded media, the returned URL can be given as an parameter
        /// the control unit that is able to display/playback the media.
        /// </summary>
        /// <param name="credentials">
        /// The credentials of the user who have rented the specified media.
        /// </param>
        /// <param name="mediaId">
        /// The media requested for download.
        /// </param>
        /// <returns>
        /// An Uri-instance that represents the URL that identifies the requested media on the
        /// server.
        /// </returns>
        /// <exception cref="ArgumentNullException">
        /// Is thrown if the credentials parameter is a null reference.
        /// </exception>
        /// <exception cref="ArgumentException">
        /// Is thrown if the specified media id is does not exist.
        /// </exception>
        public static Uri DownloadMediaURL(AccountCredentials credentials, int mediaId)
        {
            if (credentials == null)
            {
                throw new ArgumentNullException("credentials parameter is null.");
            }

            // Check if the requested media exists.
            if (!new RentItDatabaseDataContext().Medias.Exists(media => media.id == mediaId))
            {
                throw new ArgumentException("The requested media does not exist.");
            }

            StringBuilder uri = new StringBuilder();
            uri.Append("http://rentit.itu.dk/rentit01/GetMediaData.aspx?");
            uri.Append("mediaId=" + mediaId);
            uri.Append("&userName=" + credentials.UserName);
            uri.Append("&password=" + credentials.HashedPassword);
            return new Uri(uri.ToString());
        }

        /// <author>Kenneth Søhrmann</author>
        /// <summary>
        /// Gets the thumbnail of the media specified by the given media id.
        /// </summary>
        /// <param name="mediaId">
        /// The media id of the thumbnail of the media.
        /// </param>
        /// <returns>
        /// An image-instance representing the thumbnail of the requested media.
        /// </returns>
        public static Image GetThumbnail(int mediaId)
        {
            Image tmpImage = null;
            try
            {
                string url = "http://rentit.itu.dk/rentit01/GetThumbnail.aspx?mediaId=" + mediaId;
                HttpWebRequest httpWebRequest = (HttpWebRequest)HttpWebRequest.Create(url);

                //httpWebRequest.AllowWriteStreamBuffering = true;
                WebResponse webResponse = httpWebRequest.GetResponse();
                Stream webStream = webResponse.GetResponseStream();
                tmpImage = Image.FromStream(webStream);
                webResponse.Close();
                webResponse.Close();
            }
            catch (Exception e)
            {
                throw new WebException("Retrieval of the thumbnail failed: " + e.Message);
            }

            return tmpImage;
        }

        /// <author>Kenneth Søhrmann</author>
        /// <summary>
        /// Convenience method for uploading a book to the server.
        /// The methods blocks during upload.
        /// </summary>
        /// <param name="credentials">
        /// The credentials of the publisher who is to upload the book.
        /// </param>
        /// <param name="bookInfo">
        /// The metadata of the book to be uploaded.
        /// </param>
        /// <exception cref="WebException">
        /// Is thrown if the upload failed. There can be several reasons for that:
        /// Either the user was not authenticated, an internal error happened or the
        /// upload of the file failed.
        /// </exception>
        /// <exception cref="ArgumentException">
        /// Is thrown if the credentials are not authorized.
        /// </exception>
        public static void UploadBook(AccountCredentials credentials, BookInfoUpload bookInfo)
        {
            var db = new RentItDatabaseDataContext();

            if (!db.Publisher_accounts.Exists(
                pub => pub.user_name.Equals(credentials.UserName) &&
                    pub.Account.password.Equals(credentials.HashedPassword)))
            {
                throw new ArgumentException("The specified credentials is not authorized to publish media.");
            }

            Util.AddGenre(bookInfo.Genre, MediaTypeUpload.Book);

            RentItDatabase.Book bookMedia = new Book()
            {
                Media = CompileBaseMedia(db, bookInfo, credentials),
                author = bookInfo.Author,
                pages = bookInfo.Pages,
                summary = bookInfo.Summary
            };
            db.Books.InsertOnSubmit(bookMedia);
            db.SubmitChanges();

            try
            {
                UploadMediaFile(bookInfo.FilePath, bookMedia.media_id, credentials);
                UploadThumbnail(bookMedia.media_id, bookInfo, credentials);
            }
            catch (Exception e)
            {
                // Upload failed, clean up database.
                db.Books.DeleteOnSubmit(bookMedia);
                db.Medias.DeleteOnSubmit(bookMedia.Media);

                // Sometimes the Media_file entity is created, sometimes not.
                if (bookMedia.Media.Media_file != null)
                {
                    db.Media_files.DeleteOnSubmit(bookMedia.Media.Media_file);
                }
                db.SubmitChanges();

                throw new WebException("Upload failed, please try again: " + e.Message);
            }
        }

        /// <author>Kenneth Søhrmann</author>
        /// <summary>
        /// Convenience method for uploading an album and its songs to the 
        /// server. 
        /// The methods blocks during upload.
        /// </summary>
        /// <param name="credentials">
        /// The credentials of the publisher who is uploading the specified album.
        /// </param>
        /// <param name="songs">
        /// A list of SongInfoUpload-objects each representing the metadata of the songs
        /// that is a part of the album.
        /// </param>
        /// <param name="albumInfo">
        /// Instance holding the metadata of the album to be uploaded.
        /// </param>
        /// <exception cref="WebException">
        /// Is thrown if the upload failed. There can be several reasons for that:
        /// Either the user was not authenticated, an internal error happened or the
        /// upload of the file failed.
        /// </exception>
        /// <exception cref="ArgumentException">
        /// Is thrown if the credentials are not authorized.
        /// </exception>
        public static void UploadAlbum(AccountCredentials credentials, List<SongInfoUpload> songs, AlbumInfoUpload albumInfo)
        {
            var db = new RentItDatabaseDataContext();

            // Check credentials
            if (!db.Publisher_accounts.Exists(
                pub => pub.user_name.Equals(credentials.UserName) &&
                    pub.Account.password.Equals(credentials.HashedPassword)))
            {
                throw new ArgumentException("The specified credentials is not authorized to publish media.");
            }

            // Check specified song files
            foreach (SongInfoUpload song in songs)
            {
                if (!File.Exists(song.FilePath))
                {
                    throw new ArgumentException("The file, " + song.FilePath + ", does not exist.");
                }
                if (!new FileInfo(song.FilePath).Extension.Equals(".mp3"))
                {
                    throw new ArgumentException("The file, " + song.FilePath + ", does not have the supported extension, mp3.");
                }
            }

            // Add the album metadata to the database.
            RentItDatabase.Album albumMedia = new Album()
                {
                    Media = CompileBaseMedia(db, albumInfo, credentials),
                    album_artist = albumInfo.AlbumArtist,
                    description = albumInfo.Description
                };
            db.Albums.InsertOnSubmit(albumMedia);
            db.SubmitChanges();

            // Upload the thumbnail of the album.
            try
            {
                UploadThumbnail(albumMedia.media_id, albumInfo, credentials);
            }
            catch (Exception e)
            {
                db.Albums.DeleteOnSubmit(albumMedia);
                db.Medias.DeleteOnSubmit(albumMedia.Media);
                db.SubmitChanges();
                throw new WebException("Upload failed: " + e.Message);
            }

            // Add the genres to the database if they are new.
            foreach (SongInfoUpload songInfos in songs)
            {
                Util.AddGenre(songInfos.Genre, MediaTypeUpload.Song);
            }

            // For database clean up if upload fails.
            var databaseSongs = new List<RentItDatabase.Song>();
            var databaseAlbumSongs = new List<RentItDatabase.Album_song>();

            // Process each song file; upload data to database, and upload song files to
            // server.
            foreach (SongInfoUpload songInfo in songs)
            {
                RentItDatabase.Song songMedia = new Song()
                    {
                        Media = CompileBaseMedia(db, songInfo, credentials),
                        artist = songInfo.Artist,
                        length = (int)songInfo.Duration.TotalSeconds
                    };

                db.Songs.InsertOnSubmit(songMedia);
                db.SubmitChanges();

                // For rollback if upload fails.
                databaseSongs.Add(songMedia);

                RentItDatabase.Album_song albumSong = new Album_song()
                    {
                        Album = albumMedia,
                        Song = songMedia
                    };

                db.Album_songs.InsertOnSubmit(albumSong);
                db.SubmitChanges();

                // For rollback if upload fails.
                databaseAlbumSongs.Add(albumSong);

                try
                {
                    UploadMediaFile(songInfo.FilePath, songMedia.media_id, credentials);
                    UploadThumbnail(songMedia.media_id, songInfo, credentials);
                }
                catch (Exception e)
                {
                    for (int i = 0; i < databaseSongs.Count; i++)
                    {
                        db.Album_songs.DeleteOnSubmit(databaseAlbumSongs[i]);
                        db.Songs.DeleteOnSubmit(databaseSongs[i]);
                        db.Medias.DeleteOnSubmit(databaseSongs[i].Media);

                        // Sometimes the Media_file entity is created, sometimes not.
                        if (databaseSongs[i].Media.Media_file != null)
                        {
                            db.Media_files.DeleteOnSubmit(databaseSongs[i].Media.Media_file);
                        }
                    }

                    db.Albums.DeleteOnSubmit(albumMedia);
                    db.Medias.DeleteOnSubmit(albumMedia.Media);
                    db.SubmitChanges();
                    throw new WebException("Upload failed: " + e.Message);
                }
            }
        }

        /// <author>Kenneth Søhrmann</author>
        /// <summary>
        /// Convenience method for uploading a movie to the server.
        /// The methods blocks during upload.
        /// </summary>
        /// <param name="credentials">
        /// The credentials of the publisher who is to upload the specified
        /// movie.
        /// </param>
        /// <param name="movieInfo">
        /// The metadata of the movie to be uploaded.
        /// </param>
        /// <exception cref="WebException">
        /// Is thrown if the upload failed. There can be several reasons for that:
        /// Either the user was not authenticated, an internal error happened or the
        /// upload of the file failed.
        /// </exception>
        /// <exception cref="ArgumentNullException">
        /// Is thrown if one of the arguments is a null reference.
        /// </exception>
        /// <exception cref="ArgumentException">
        /// Is thrown if the credentials are not authorized.
        /// </exception>
        public static void UploadMovie(AccountCredentials credentials, MovieInfoUpload movieInfo)
        {
            if (credentials == null)
            {
                throw new ArgumentNullException("Credentials parameter is a null reference.");
            }
            if (movieInfo == null)
            {
                throw new ArgumentNullException("The movieInfo parameter is a null reference.");
            }
            if (!File.Exists(movieInfo.FilePath))
            {
                throw new ArgumentException("The specified file does not exist.");
            }
            if (!new FileInfo(movieInfo.FilePath).Extension.Equals(".mp4"))
            {
                throw new ArgumentException("The specified file does not have the supported extension, mp4.");
            }

            var serviceClient = new RentItClient();
            try
            {
                serviceClient.ValidateCredentials(credentials);
            }
            catch (Exception e)
            {
                throw new ArgumentException("Invalid credentials submitted.");
            }

            /*
            var db = new RentItDatabaseDataContext();
            if (!db.Publisher_accounts.Exists(
                 pub => pub.user_name.Equals(credentials.UserName) &&
                  pub.Account.password.Equals(credentials.HashedPassword)))
            {
                throw new ArgumentException("The specified credentials is not authorized to publish media.");
            }
            
            Util.AddGenre(movieInfo.Genre, MediaTypeUpload.Song);

            RentItDatabase.Movie movieMedia = new Movie()
            {
                Media = CompileBaseMedia(db, movieInfo, credentials),
                director = movieInfo.Director,
                length = (int)movieInfo.Duration.TotalSeconds,
                summary = movieInfo.Summary
            };
            db.Movies.InsertOnSubmit(movieMedia);
            db.SubmitChanges();
            */

            //movieInfo.

            var mInfo = new MovieInfo()
                {
                    Title = movieInfo.Title,
                    Type = MediaType.Movie,
                    Genre = movieInfo.Genre,
                    Price = movieInfo.Price,
                    Publisher = movieInfo.Publisher,
                    ReleaseDate = movieInfo.ReleaseDate,

                    Director = movieInfo.Director,
                    Duration = movieInfo.Duration,
                    Summary = movieInfo.Summary,
                };

            int movieId;
            try
            {
                movieId = serviceClient.PublishMedia(mInfo, credentials);
            }
            catch (Exception e)
            {
                throw new Exception("Something went wrong: " + e.Message);
            }

            try
            {
                UploadMediaFile(movieInfo.FilePath, movieId, credentials);
                UploadThumbnail(movieId, movieInfo, credentials);
            }
            catch (Exception e)
            {
                serviceClient.DeleteMedia(movieId, credentials);

                throw new WebException("Upload of media failed.");
                /*
                // The upload failed, so clean up database. 
                db.Movies.DeleteOnSubmit(movieMedia);
                db.Medias.DeleteOnSubmit(movieMedia.Media);

                // Sometimes the Media_file entity is created, sometimes not.
                if (movieMedia.Media.Media_file != null)
                {
                    db.Media_files.DeleteOnSubmit(movieMedia.Media.Media_file);
                }

                db.SubmitChanges();
                throw new WebException("Upload failed", e);
                 * */
            }
        }

        /// <author>Kenneth Søhrmann</author>
        /// <summary>
        /// Helper method, used to create an RentItDatabase.Media instance with the data
        /// provided in mediaInfo.
        /// </summary>
        /// <param name="db">
        /// The database which the returned value is must be a member of.
        /// </param>
        /// <param name="mediaInfo">
        /// The data of the RentiDatabase.Media object to be created.
        /// </param>
        /// <param name="credentials">
        /// The credentials of the publisher who is to upload the song.
        /// </param>
        /// <returns>
        /// The created RentItDatabase.Media-object.
        /// </returns>
        private static RentItDatabase.Media CompileBaseMedia(RentItDatabaseDataContext db, MediaInfoUpload mediaInfo, AccountCredentials credentials)
        {
            RentItDatabase.Media baseMedia = new Media()
            {
                title = mediaInfo.Title,
                type_id = (from mType in db.Media_types
                           where mType.name.Equals(Util.StringValueOfMediaType(mediaInfo.MediaType))
                           select mType).First().id,
                genre_id = (from mType in db.Media_types
                            where mType.name.Equals(Util.StringValueOfMediaType(mediaInfo.MediaType))
                            select mType).First().id,
                price = mediaInfo.Price,
                release_date = mediaInfo.ReleaseDate,
                publisher_id = (from pub in db.Publisher_accounts
                                where pub.user_name.Equals(credentials.UserName)
                                select pub).First().publisher_id
            };

            return baseMedia;
        }

        /// <author>Kenneth Søhrmann</author>
        /// <summary>
        /// Helper method for uploading a file.
        /// </summary>
        /// <param name="filePath">
        /// The absolute path of a the file to be uploaded.
        /// </param>
        /// <param name="mediaId">
        /// The media id of the file to be uploaded.
        /// </param>
        /// <param name="credentials">
        /// The credentials of the publisher who is to upload the
        /// specified media.
        /// </param>
        private static void UploadMediaFile(string filePath, int mediaId, AccountCredentials credentials)
        {
            FileInfo fileInfo = new FileInfo(filePath);
            using (WebClient client = new WebClient())
            {
                StringBuilder uri = new StringBuilder();
                uri.Append("http://rentit.itu.dk/rentit01/UploadMedia.aspx?");
                uri.Append("mediaId=" + mediaId);
                uri.Append("&extension=" + fileInfo.Extension);
                uri.Append("&userName=" + credentials.UserName);
                uri.Append("&password=" + credentials.HashedPassword);

                client.UploadFile(uri.ToString(), filePath);

                // Fire the FileUploadedEvent
                if (FileUploadedEvent != null)
                {
                    FileUploadedEvent();
                }
            }
        }

        /// <author>Kenneth Søhrmann</author>
        /// <summary>
        /// Helper method for uploading a thumbnail to the specified media item.
        /// </summary>
        /// <param name="mediaId">
        /// The media item the thumbnail will be uploaded to.
        /// </param>
        /// <param name="info">
        /// The MediaInfo object holding the metadata of the specified media.
        /// </param>
        /// <param name="credentials">
        /// The credentials of the publisher who is to upload the thumbnail.
        /// </param>
        private static void UploadThumbnail(int mediaId, MediaInfoUpload info, AccountCredentials credentials)
        {
            using (WebClient client = new WebClient())
            {
                StringBuilder uri = new StringBuilder();
                uri.Append("http://rentit.itu.dk/rentit01/UploadThumbnail.aspx?");
                uri.Append("mediaId=" + mediaId);
                uri.Append("&userName=" + credentials.UserName);
                uri.Append("&password=" + credentials.HashedPassword);

                client.UploadData(uri.ToString(), Util.ImageToByteArray(info.Thumbnail));
            }
        }

        /// <summary>
        /// Used for initial testing.
        /// </summary>
        public static void Main()
        {
            // Set up the credentials of the publisher account.
            var credentials = new AccountCredentials()
                {
                    UserName = "publishCorp",
                    HashedPassword = "7110EDA4D09E062AA5E4A390B0A572AC0D2C0220" // The hashsed password.
                };

            // Load the thumbnail to be uploaded into the memory.
            Image thumbnail = System.Drawing.Image.FromFile(@"C:\Users\Kenneth88\Desktop\gta\GtaThumb.jpg");

            // Construct the MovieInfo-object holding the metadata of the movie to be uploaded.
            MovieInfoUpload movieInfo = new MovieInfoUpload();
            movieInfo.FilePath = @"C:\Users\Kenneth88\Desktop\gta\GTA V - Debut Trailer.mp4";
            movieInfo.Title = "GTA V - Debut Trailer";
            movieInfo.Genre = "Trailer";
            movieInfo.Price = 0;
            movieInfo.ReleaseDate = DateTime.Now;
            movieInfo.Publisher = "Publish Corp. International";
            movieInfo.Thumbnail = thumbnail;
            movieInfo.Director = "Rockstar";
            movieInfo.Summary = "The very first trailer of the Grand Theft Auto V-game. Oh so sweet it is!";
            movieInfo.Duration = TimeSpan.FromSeconds(41D);

            // Upload the movie by calling the UploadMovie method.
            Console.WriteLine("Started upload");
            UploadMovie(credentials, movieInfo);
            Console.WriteLine("Done uploading!");
        }
    }
}
