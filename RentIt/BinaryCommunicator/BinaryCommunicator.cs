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
            RentItClient serviceClient = new RentItClient();

            try
            {
                serviceClient.ValidateCredentials(credentials);
            }
            catch (Exception e)
            {
                throw new ArgumentException("Invalid credentials submitted.");
            }

            BookInfo bookMedia = new BookInfo()
            {
                Title = bookInfo.Title,
                Type = MediaType.Book,
                Genre = bookInfo.Genre,
                Price = bookInfo.Price,
                Publisher = bookInfo.Publisher,
                ReleaseDate = bookInfo.ReleaseDate,

                Author = bookInfo.Author,
                Pages = bookInfo.Pages,
                Summary = bookInfo.Summary
            };

            int bookMediaId = serviceClient.PublishMedia(bookMedia, credentials);

            try
            {
                UploadMediaFile(bookInfo.FilePath, bookMediaId, credentials);
                UploadThumbnail(bookMediaId, bookInfo, credentials);
            }
            catch (Exception e)
            {
                // Upload failed, clean up database.

                serviceClient.DeleteMedia(bookMediaId, credentials);
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

            RentItClient serviceClient = new RentItClient();

            try
            {
                serviceClient.ValidateCredentials(credentials);
            }
            catch (Exception e)
            {
                throw new ArgumentException("Invalid credentials submitted.");
            }

            AlbumInfo albumMedia = new AlbumInfo()
                {
                    Title = albumInfo.Title,
                    Type = MediaType.Album,
                    Genre = albumInfo.Genre,
                    Price = albumInfo.Price,
                    Publisher = albumInfo.Publisher,
                    ReleaseDate = albumInfo.ReleaseDate,

                    AlbumArtist = albumInfo.AlbumArtist,
                    Description = albumInfo.Description,
                };

            int albumMediaId = serviceClient.PublishMedia(albumMedia, credentials);

            // Upload the thumbnail of the album.
            try
            {
                UploadThumbnail(albumMediaId, albumInfo, credentials);
            }
            catch (Exception e)
            {
                serviceClient.DeleteMedia(albumMediaId, credentials);
                throw new WebException("Upload failed: " + e.Message);
            }

            // For database clean up if upload fails.
            var publishedSongs = new List<int>();

            // Process each song file; upload data to database, and upload song files to
            // server.
            foreach (SongInfoUpload songInfo in songs)
            {
                SongInfo songMedia = new SongInfo()
                {
                    Title = songInfo.Title,
                    Type = MediaType.Song,
                    Genre = songInfo.Genre,
                    Price = songInfo.Price,
                    Publisher = songInfo.Publisher,
                    ReleaseDate = songInfo.ReleaseDate,

                    Artist = songInfo.Artist,
                    Duration = songInfo.Duration
                };

                // publish the metadata of the song to the server.
                int songMediaId = serviceClient.PublishMedia(songMedia, credentials);

                // For rollback if upload of binary data fails.
                publishedSongs.Add(songMediaId);

                try
                {
                    UploadMediaFile(songInfo.FilePath, songMediaId, credentials);

                    if (songInfo.Thumbnail != null)
                    {
                        UploadThumbnail(songMediaId, songInfo, credentials);
                    }
                }
                catch (Exception e)
                {
                    // Clean up server database upon upload failure.
                    foreach (int songId in publishedSongs)
                    {
                        serviceClient.DeleteMedia(songId, credentials);
                    }

                    serviceClient.DeleteMedia(albumMediaId, credentials);
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
                // Clean up server database if the upload failed.
                serviceClient.DeleteMedia(movieId, credentials);
                throw new WebException("Upload of media failed.");
            }
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
