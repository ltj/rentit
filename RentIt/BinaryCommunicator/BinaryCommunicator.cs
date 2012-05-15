using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace BinaryCommunicator
{
    using System.Drawing;
    using System.IO;
    using System.Net;

    using BinaryCommunicator.Properties;

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
        /// request get a "Bad Request 400"-error message. Please refer to the documentation
        /// of the GetMedia.aspx Web Page for more info.
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
        public static Uri DownloadMediaURL(Credentials credentials, int mediaId)
        {
            if (credentials == null)
            {
                throw new ArgumentNullException("credentials");
            }

            var uri = new StringBuilder();
            uri.Append("http://rentit.itu.dk/rentit01/GetMedia.aspx?");
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
        /// <exception cref="WebException">
        /// The WebExceptions is thrown if the download failed or if the specfied
        /// media id does not identify a media of the server database.
        /// </exception>
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
                tmpImage = Resources.missingThumb;
                //throw new WebException("Retrieval of the thumbnail failed: " + e.Message);
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
        /// Is thrown if the upload of the mediafile or media thumbnail failed.
        /// </exception>
        /// <exception cref="ArgumentException">
        /// Is thrown if the credentials are not authorized.
        /// </exception>
        public static void UploadBook(Credentials credentials, BookInfoUpload bookInfo)
        {
            var serviceClient = new RentItClient();
            var accountCredentials = new AccountCredentials {
                UserName = credentials.UserName,
                HashedPassword = credentials.HashedPassword
            };

            try
            {
                serviceClient.ValidateCredentials(accountCredentials);
            }
            catch (Exception)
            {
                throw new ArgumentException("Invalid credentials submitted.");
            }

            var bookMedia = new BookInfo()
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

            int bookMediaId = serviceClient.PublishMedia(bookMedia, accountCredentials);

            try
            {
                UploadMediaFile(bookInfo.FilePath, bookMediaId, credentials);
                UploadThumbnail(bookMediaId, bookInfo, credentials);
            }
            catch (Exception e)
            {
                // Upload failed, clean up database.

                serviceClient.DeleteMedia(bookMediaId, accountCredentials);
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
        /// Is thrown if the upload of one of the songs or the thumbnail failed.
        /// </exception>
        /// <exception cref="ArgumentException">
        /// Is thrown if the credentials are not authorized.
        /// </exception>
        public static void UploadAlbum(Credentials credentials, List<SongInfoUpload> songs, AlbumInfoUpload albumInfo)
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

            var serviceClient = new RentItClient();
            var accountCredentials = new AccountCredentials {
                UserName = credentials.UserName,
                HashedPassword = credentials.HashedPassword
            };

            try
            {
                serviceClient.ValidateCredentials(accountCredentials);
            }
            catch (Exception)
            {
                throw new ArgumentException("Invalid credentials submitted.");
            }

            var albumMedia = new AlbumInfo()
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

            int albumMediaId = serviceClient.PublishMedia(albumMedia, accountCredentials);

            // Upload the thumbnail of the album.
            try
            {
                UploadThumbnail(albumMediaId, albumInfo, credentials);
            }
            catch (Exception e)
            {
                serviceClient.DeleteMedia(albumMediaId, accountCredentials);
                throw new WebException("Upload failed: " + e.Message);
            }

            // For database clean up if upload fails.
            var publishedSongs = new List<int>();

            // Process each song file; upload data to database, and upload song files to
            // server.
            foreach (SongInfoUpload songInfo in songs)
            {
                var songMedia = new SongInfo()
                {
                    Title = songInfo.Title,
                    Type = MediaType.Song,
                    Genre = songInfo.Genre,
                    Price = songInfo.Price,
                    Publisher = songInfo.Publisher,
                    ReleaseDate = songInfo.ReleaseDate,

                    AlbumId = albumMediaId, // Set the album media id to the one received when publishing the album metadata.
                    Artist = songInfo.Artist,
                    Duration = songInfo.Duration
                };

                // publish the metadata of the song to the server.
                int songMediaId = serviceClient.PublishMedia(songMedia, accountCredentials);

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
                        serviceClient.DeleteMedia(songId, accountCredentials);
                    }

                    serviceClient.DeleteMedia(albumMediaId, accountCredentials);
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
        /// Is thrown if the upload of the movie failed.
        /// </exception>
        /// <exception cref="ArgumentException">
        /// Is thrown if the credentials are not authorized.
        /// </exception>
        public static void UploadMovie(Credentials credentials, MovieInfoUpload movieInfo)
        {
            if (!File.Exists(movieInfo.FilePath))
            {
                throw new ArgumentException("The specified file does not exist.");
            }
            if (!new FileInfo(movieInfo.FilePath).Extension.Equals(".mp4"))
            {
                throw new ArgumentException("The specified file does not have the supported extension, mp4.");
            }

            var serviceClient = new RentItClient();
            var accountCredentials = new AccountCredentials {
                UserName = credentials.UserName,
                HashedPassword = credentials.HashedPassword
            };

            try
            {
                serviceClient.ValidateCredentials(accountCredentials);
            }
            catch (Exception)
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
                movieId = serviceClient.PublishMedia(mInfo, accountCredentials);
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
            catch (Exception)
            {
                // Clean up server database if the upload failed.
                serviceClient.DeleteMedia(movieId, accountCredentials);
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
        private static void UploadMediaFile(string filePath, int mediaId, Credentials credentials)
        {
            var fileInfo = new FileInfo(filePath);
            using (var client = new WebClient())
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
        private static void UploadThumbnail(int mediaId, MediaInfoUpload info, Credentials credentials)
        {
            using (var client = new WebClient())
            {
                var uri = new StringBuilder();
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
            var credentials = new Credentials("publishCorp", "7110EDA4D09E062AA5E4A390B0A572AC0D2C0220");

            // Load the thumbnail to be uploaded into the memory.
            Image thumbnail = System.Drawing.Image.FromFile(@"C:\Users\Kenneth88\Desktop\gta\GtaThumb.jpg");

            // Construct the MovieInfo-object holding the metadata of the movie to be uploaded.
            var movieInfo = new MovieInfoUpload {
                FilePath = @"C:\Users\Kenneth88\Desktop\gta\GTA V - Debut Trailer.mp4",
                Title = "GTA V - Debut Trailer",
                Genre = "Trailer",
                Price = 0,
                ReleaseDate = DateTime.Now,
                Publisher = "Publish Corp. International",
                Thumbnail = thumbnail,
                Director = "Rockstar",
                Summary = "The very first trailer of the Grand Theft Auto V-game. Oh so sweet it is!",
                Duration = TimeSpan.FromSeconds(41D)
            };

            // Upload the movie by calling the UploadMovie method.
            Console.WriteLine("Started upload");
            UploadMovie(credentials, movieInfo);
            Console.WriteLine("Done uploading!");
        }
    }
}
