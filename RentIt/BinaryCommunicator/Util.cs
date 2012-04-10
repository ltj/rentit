using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.ServiceModel;
using System.Text;
using RentItDatabase;

namespace RentIt
{
    using System.IO;

    using BinaryCommunicator;

    /// <author>Kenneth Søhrmann</author>
    /// <summary>
    /// This class provides convenient conversion methods of
    /// the two enums MediaType and Rating.
    /// </summary>
    internal static class Util
    {
        /// <auhtor>Kenneth Søhrmann</auhtor>
        /// <summary>
        /// Converts the image to the .jpeg-format and returns its bytes.
        /// </summary>
        /// <param name="imageIn"></param>
        /// <returns></returns>
        public static byte[] ImageToByteArray(System.Drawing.Image imageIn)
        {
            MemoryStream ms = new MemoryStream();
            imageIn.Save(ms, System.Drawing.Imaging.ImageFormat.Jpeg);
            return ms.ToArray();
        }

        /// <author>Kenneth Søhrmann</author>
        /// <summary>
        /// Converts the mediaType to its string representation.
        /// </summary>
        /// <param name="mediaType"></param>
        /// <returns></returns>
        public static string StringValueOfMediaType(MediaTypeUpload mediaType)
        {
            switch (mediaType)
            {
                case MediaTypeUpload.Book:
                    return "Book";
                case MediaTypeUpload.Movie:
                    return "Movie";
                case MediaTypeUpload.Album:
                    return "Album";
                case MediaTypeUpload.Song:
                    return "Song";
                default:
                    return "Any";
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
        public static int AddGenre(string genreName, MediaTypeUpload mediaType)
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