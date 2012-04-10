using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.ServiceModel;
using System.Text;
using RentItDatabase;

namespace RentIt
{

    /// <author>Kenneth Søhrmann</author>
    /// <summary>
    /// This class provides convenient conversion methods of
    /// the two enums MediaType and Rating.
    /// </summary>
    internal static class Util
    {

        /// <author>Kenneth Søhrmann</author>
        /// <summary>
        /// Converts a string representation of the MediaType-enum
        /// to MediaType. 
        /// </summary>
        /// <param name="mediaType">
        /// The string representation of the MediaType.
        /// If the string is null or does not match any of the 
        /// values in the MediaType-enum, MediaType.Any is returned.
        /// </param>
        /// <returns>
        /// The MediaType of the string representation specified.
        /// </returns>
        public static MediaType MediaTypeOfValue(string mediaType)
        {
            switch (mediaType)
            {
                case "Book":
                    return MediaType.Book;
                case "Movie":
                    return MediaType.Movie;
                case "Album":
                    return MediaType.Album;
                case "Song":
                    return MediaType.Song;
                default:
                    return MediaType.Any;
            }
        }

        /// <author>Kenneth Søhrmann</author>
        /// <summary>
        /// Convert the specified int representation of Rating to
        /// the Rating-type.
        /// </summary>
        /// <param name="rating">
        /// The int representation to be converted. If the specified 
        /// value is out of range of the Rating-type, Rating.One is
        /// returned.
        /// </param>
        /// <returns>
        /// The Rating of the int representation specified.
        /// </returns>
        public static Rating RatingOfValue(int rating)
        {
            switch (rating)
            {
                case 1:
                    return Rating.One;
                case 2:
                    return Rating.Two;
                case 3:
                    return Rating.Three;
                case 4:
                    return Rating.Four;
                case 5:
                    return Rating.Five;
                default:
                    return Rating.One;
            }
        }

        /// <author>Per Mortensen</author>
        /// <summary>
        /// Convert the specified Rating to int representation.
        /// </summary>
        /// <param name="rating">
        /// The Rating to be converted.
        /// </param>
        /// <returns>
        /// An int representation of the Rating object.
        /// </returns>
        public static int ValueOfRating(Rating rating)
        {
            switch (rating)
            {
                case Rating.One:
                    return 1;
                case Rating.Two:
                    return 2;
                case Rating.Three:
                    return 3;
                case Rating.Four:
                    return 4;
                case Rating.Five:
                    return 5;
                default:
                    return 1;
            }
        }

        public static string StringValueOfMediaType(MediaType mediaType)
        {
            switch (mediaType)
            {
                case MediaType.Book:
                    return "Book";
                case MediaType.Movie:
                    return "Movie";
                case MediaType.Album:
                    return "Album";
                case MediaType.Song:
                    return "Song";
                default:
                    return "Any";
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
        public static string GetMediaMetadataAsString(Media mediaItem)
        {
            var metadataString = new StringBuilder();

            metadataString.Append(mediaItem.title + " ");
            metadataString.Append(mediaItem.Publisher.title);

            switch (MediaTypeOfValue(mediaItem.Media_type.name))
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


        /// <author>Per Mortensen</author>
        /// <summary>
        /// Adds a new genre to the database, if it doesn't already exist.
        /// </summary>
        /// <param name="genreName">The name of the new genre.</param>
        /// <param name="mediaType">The media type for which the new genre will be available.</param>
        /// <returns>The id of the newly added genre. If the genre already exists, the
        /// existing genre id is returned.</returns>
        public static int AddGenre(string genreName, MediaType mediaType)
        {
            var db = new RentItDatabaseDataContext();

            // Get media type id
            int mediaTypeId = (from t in db.Media_types
                               where t.name.Equals(StringValueOfMediaType(mediaType))
                               select t.id).First();

            // Check if genre already exists for the given media type
            IQueryable<int> genreIds = from g in db.Genres
                                       where g.media_type == mediaTypeId && g.name.Equals(genreName)
                                       select g.id;
            if (genreIds.Count() > 0)
                return genreIds.First(); // Genre and media type combination already exists, return its genre id

            // add genre to database
            var newGenre = new RentItDatabase.Genre { media_type = mediaTypeId, name = genreName };
            db.Genres.InsertOnSubmit(newGenre);
            db.SubmitChanges();

            // Return the newly added genre id
            return (from g in db.Genres
                    where g.media_type == mediaTypeId && g.name.Equals(genreName)
                    select g.id).First();
        }
    }
}