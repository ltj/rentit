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

    /// <author>Kenneth Søhrmann</author>
    /// <summary>
    /// The methods of this class is used to communicate binary data of the 
    /// RentItService-web service. With these methods it is possible to download
    /// thumbnails, media files and it is possible to upload data to the service,
    /// if the client is a registered publisher of the service.
    /// </summary>
    public class BinaryCommuncator
    {
        /// <summary>
        /// Convenience helper factory method for instantiatng a BookInfo instance.
        /// The parameters specify all the data of a BookInfo-object that needs
        /// to be instantiated before is it possible to upload a the media to the
        /// server.
        /// </summary>
        /// <param name="title"></param>
        /// <param name="genre"></param>
        /// <param name="price"></param>
        /// <param name="releaseDate"></param>
        /// <param name="publisher"></param>
        /// <param name="thumbnail"></param>
        /// <param name="author"></param>
        /// <param name="summary"></param>
        /// <param name="pages"></param>
        /// <returns></returns>
        public static BookInfo MovieInfoFactory(string title, string genre, int price, DateTime releaseDate, string publisher, System.Data.Linq.Binary thumbnail, string author, string summary, int pages)
        {
            return new BookInfo()
            {
                Title = title,
                Type = MediaType.Book,
                Genre = genre,
                Price = price,
                ReleaseDate = releaseDate,
                Publisher = publisher,
                Thumbnail = thumbnail,
                Author = author,
                Summary = summary,
                Pages = pages
            };
        }

        /// <summary>
        /// Convenience helper factory method for instantiatng a MovieInfo instance.
        /// The parameters specify all the data of a BookInfo-object that needs
        /// to be instantiated before is it possible to upload a the media to the
        /// server.
        /// </summary>
        /// <param name="title"></param>
        /// <param name="genre"></param>
        /// <param name="price"></param>
        /// <param name="releaseDate"></param>
        /// <param name="publisher"></param>
        /// <param name="thumbnail"></param>
        /// <param name="author"></param>
        /// <param name="summary"></param>
        /// <param name="pages"></param>
        /// <returns></returns>
        public static MovieInfo MovieInfoFactory(string title, string genre, int price, DateTime releaseDate, string publisher, System.Data.Linq.Binary thumbnail, string director, string summary, TimeSpan duration)
        {
            return new MovieInfo()
            {
                Title = title,
                Type = MediaType.Movie,
                Genre = genre,
                Price = price,
                ReleaseDate = releaseDate,
                Publisher = publisher,
                Thumbnail = thumbnail,
                Director = director,
                Summary = summary,
                Duration = duration
            };
        }

        /// <summary>
        /// Convenience helper factory method for instantiatng a AlbumInfo instance.
        /// The parameters specify all the data of a BookInfo-object that needs
        /// to be instantiated before is it possible to upload a the media to the
        /// server.
        /// </summary>
        /// <param name="title"></param>
        /// <param name="genre"></param>
        /// <param name="price"></param>
        /// <param name="releaseDate"></param>
        /// <param name="publisher"></param>
        /// <param name="thumbnail"></param>
        /// <param name="author"></param>
        /// <param name="summary"></param>
        /// <param name="pages"></param>
        /// <returns></returns>
        public static AlbumInfo AlbumInfoFactory(string title, string genre, int price, DateTime releaseDate, string publisher, System.Data.Linq.Binary thumbnail, string albumArtist, string description)
        {
            return new AlbumInfo()
            {
                Title = title,
                Type = MediaType.Album,
                Genre = genre,
                Price = price,
                ReleaseDate = releaseDate,
                Publisher = publisher,
                Thumbnail = thumbnail,
                AlbumArtist = albumArtist,
                Description = description
            };
        }

        /// <summary>
        /// Convenience helper factory method for instantiatng a SongInfo instance.
        /// The parameters specify all the data of a BookInfo-object that needs
        /// to be instantiated before is it possible to upload a the media to the
        /// server.
        /// </summary>
        /// <param name="title"></param>
        /// <param name="genre"></param>
        /// <param name="price"></param>
        /// <param name="releaseDate"></param>
        /// <param name="publisher"></param>
        /// <param name="thumbnail"></param>
        /// <param name="author"></param>
        /// <param name="summary"></param>
        /// <param name="pages"></param>
        /// <returns></returns>
        public static SongInfo SongInfoFactory(string title, string genre, int price, DateTime releaseDate, string publisher, System.Data.Linq.Binary thumbnail, string artist, TimeSpan length)
        {
            return new SongInfo()
            {
                Title = title,
                Type = MediaType.Song,
                Genre = genre,
                Price = price,
                ReleaseDate = releaseDate,
                Publisher = publisher,
                Thumbnail = thumbnail,
                Artist = artist,
                Duration = length
            };
        }


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
        public static Uri DownloadMedia(AccountCredentials credentials, int mediaId)
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
        /// 
        /// </summary>
        /// <param name="credentials"></param>
        /// <param name="bookFilePath"></param>
        /// <param name="bookInfo"></param>
        /// <exception cref="WebException">
        /// Is thrown if the upload failed. There can be several reasons for that:
        /// Either the user was not authenticated, an internal error happened or the
        /// upload of the file failed.
        /// </exception>
        /// <exception cref="ArgumentException">
        /// Is thrown if the credentials are not authorized.
        /// </exception>
        public static void UploadBook(AccountCredentials credentials, string bookFilePath, BookInfo bookInfo)
        {
            var db = new RentItDatabaseDataContext();

            if (!db.Publisher_accounts.Exists(
                pub => pub.user_name.Equals(credentials.UserName) &&
                    pub.Account.password.Equals(credentials.HashedPassword)))
            {
                throw new ArgumentException("The specified credentials is not authorized to publish media.");
            }

            Util.AddGenre(bookInfo.Genre, MediaType.Book);

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
                UploadMediaFile(bookFilePath, bookMedia.media_id, credentials);
                UploadThumbnail(bookMedia.media_id, bookInfo, credentials);
            }
            catch (Exception e)
            {
                // Upload failed, clean up database.
                db.Books.DeleteOnSubmit(bookMedia);
                db.Medias.DeleteOnSubmit(bookMedia.Media);
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
        /// 
        /// </summary>
        /// <param name="credentials">
        /// The credentials of the publisher who is uploading the specified album.
        /// </param>
        /// <param name="songs">
        /// Dictionary of the songs metadata and their file path. These are the songs
        /// that make up the album.
        /// The key represents the SongInfo of the song file, which file path is the 
        /// corresponding string value.
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
        public static void UploadAlbum(AccountCredentials credentials, Dictionary<SongInfo, string> songs, AlbumInfo albumInfo)
        {
            var db = new RentItDatabaseDataContext();

            if (!db.Publisher_accounts.Exists(
                pub => pub.user_name.Equals(credentials.UserName) &&
                    pub.Account.password.Equals(credentials.HashedPassword)))
            {
                throw new ArgumentException("The specified credentials is not authorized to publish media.");
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
            foreach (SongInfo songInfos in songs.Keys)
            {
                Util.AddGenre(songInfos.Genre, MediaType.Song);
            }

            // For database clean up if upload fails.
            var databaseSongs = new List<RentItDatabase.Song>();
            var databaseAlbumSongs = new List<RentItDatabase.Album_song>();

            // Process each song file; upload data to database, and upload song files to
            // server.
            foreach (SongInfo songInfo in songs.Keys)
            {
                RentItDatabase.Song songMedia = new Song()
                    {
                        Media = CompileBaseMedia(db, songInfo, credentials),
                        artist = songInfo.Artist,
                        length = (int)songInfo.Duration.TotalSeconds
                    };

                db.Songs.InsertOnSubmit(songMedia);
                db.SubmitChanges();
                databaseSongs.Add(songMedia);

                RentItDatabase.Album_song albumSong = new Album_song()
                    {
                        Album = albumMedia,
                        Song = songMedia
                    };

                db.Album_songs.InsertOnSubmit(albumSong);
                db.SubmitChanges();
                databaseAlbumSongs.Add(albumSong);

                try
                {
                    UploadMediaFile(songs[songInfo], songMedia.media_id, credentials);
                    UploadThumbnail(songMedia.media_id, songInfo, credentials);
                }
                catch (Exception e)
                {
                    for (int i = 0; i < databaseSongs.Count; i++)
                    {
                        db.Album_songs.DeleteOnSubmit(databaseAlbumSongs[i]);
                        db.Songs.DeleteOnSubmit(databaseSongs[i]);
                        db.Medias.DeleteOnSubmit(databaseSongs[i].Media);
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
        /// 
        /// </summary>
        /// <param name="credentials">
        /// The credentials of the publisher who is to upload the specified
        /// movie.
        /// </param>
        /// <param name="movieFilePath">
        /// The path of the movie to be uploaded.
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
        public static void UploadMovie(AccountCredentials credentials, string movieFilePath, MovieInfo movieInfo)
        {
            if (credentials == null)
            {
                throw new ArgumentNullException("Credentials parameter is a null reference.");
            }
            if (movieInfo == null)
            {
                throw new ArgumentNullException("The movieInfo parameter is a null reference.");
            }
            if (!File.Exists(movieFilePath))
            {
                throw new ArgumentException("The specified file does not exist.");
            }
            if (!new FileInfo(movieFilePath).Extension.Equals(".mp4"))
            {
                throw new ArgumentException("The specified file does not have the supported extension, mp4.");
            }

            var db = new RentItDatabaseDataContext();
            if (!db.Publisher_accounts.Exists(
                 pub => pub.user_name.Equals(credentials.UserName) &&
                  pub.Account.password.Equals(credentials.HashedPassword)))
            {
                throw new ArgumentException("The specified credentials is not authorized to publish media.");
            }
            // TODO check that genre exist
            Util.AddGenre(movieInfo.Genre, MediaType.Song);

            RentItDatabase.Movie movieMedia = new Movie()
            {
                Media = CompileBaseMedia(db, movieInfo, credentials),
                director = movieInfo.Director,
                length = (int)movieInfo.Duration.TotalSeconds,
                summary = movieInfo.Summary
            };
            db.Movies.InsertOnSubmit(movieMedia);
            db.SubmitChanges();

            try
            {
                UploadMediaFile(movieFilePath, movieMedia.media_id, credentials);
                UploadThumbnail(movieMedia.media_id, movieInfo, credentials);
            }
            catch (Exception e)
            {
                // The upload failed, so clean up database. 
                db.Movies.DeleteOnSubmit(movieMedia);
                db.Medias.DeleteOnSubmit(movieMedia.Media);
                if (movieMedia.Media.Media_file != null)
                {
                    db.Media_files.DeleteOnSubmit(movieMedia.Media.Media_file);
                }

                db.SubmitChanges();
                throw new WebException("Upload failed", e);
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
        /// <returns>
        /// The created RentItDatabase.Media-object.
        /// </returns>
        private static RentItDatabase.Media CompileBaseMedia(RentItDatabaseDataContext db, MediaInfo mediaInfo, AccountCredentials credentials)
        {
            RentItDatabase.Media baseMedia = new Media()
            {
                title = mediaInfo.Title,
                type_id = (from mType in db.Media_types
                           where mType.name.Equals(Util.StringValueOfMediaType(mediaInfo.Type))
                           select mType.id).First(),
                genre_id = (from mType in db.Media_types
                            where mType.name.Equals(Util.StringValueOfMediaType(mediaInfo.Type))
                            select mType.id).First(),
                price = mediaInfo.Price,
                release_date = mediaInfo.ReleaseDate,
                publisher_id = (from pub in db.Publisher_accounts
                                where pub.user_name.Equals(credentials.UserName)
                                select pub.publisher_id).First()
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

                client.UploadFile(uri.ToString(), filePath); // FullName might be incorrect here.
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
        private static void UploadThumbnail(int mediaId, MediaInfo info, AccountCredentials credentials)
        {
            using (WebClient client = new WebClient())
            {
                StringBuilder uri = new StringBuilder();
                uri.Append("http://rentit.itu.dk/rentit01/UploadThumbnail.aspx?");
                uri.Append("mediaId=" + mediaId);
                uri.Append("&userName=" + credentials.UserName);
                uri.Append("&password=" + credentials.HashedPassword);

                client.UploadData(uri.ToString(), info.Thumbnail.Bytes);
            }
        }


        public static void Main(string[] args)
        {
            var credentials = new AccountCredentials()
                {
                    UserName = "publishCorp",
                    HashedPassword = "7110EDA4D09E062AA5E4A390B0A572AC0D2C0220"
                };

            var thumbnail = new System.Data.Linq.Binary();
            thumbnail.Bytes = File.ReadAllBytes(@"C:\Users\Kenneth88\Desktop\gta\GtaThumb.jpg");

            RentIt.MovieInfo movieInfo = MovieInfoFactory(
                "Arrows in a box",
                "Misc",
                0,
                DateTime.Now,
                "publishCorp",
                thumbnail,
                "Rockstar",
                "Arrows in a box - isn't it amazing?",
                TimeSpan.FromSeconds(41D));

            Console.WriteLine("Started upload");
            UploadMovie(credentials, @"C:\Users\Kenneth88\Desktop\gta\arrows.mp4", movieInfo);
            Console.WriteLine("Done uploading!");
        }
    }
}
