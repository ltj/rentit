﻿using System;
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
    public static class Util
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

        /// <summary>
        /// Gets a media type's string value.
        /// </summary>
        /// <param name="mediaType">The MediaType to get the string value of.</param>
        /// <returns>The string value.</returns>
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
        /// Gets the media rating of a specified media item.
        /// The returned value contains the number of ratings and 
        /// the average rating.
        /// </summary>
        /// <param name="mediaId">The media id to get ratings for.</param>
        /// <param name="db">The database context to use.</param>
        /// <returns>A rating record for the media.</returns>
        public static RentItDatabase.Rating GetMediaRating(int mediaId, DatabaseDataContext db)
        {
            if (!db.Ratings.Exists(r => r.media_id.Equals(mediaId)))
                return default(RentItDatabase.Rating);

            IQueryable<RentItDatabase.Rating> ratings = from r in db.Ratings
                                                        where r.media_id.Equals(mediaId)
                                                        select r;
            return ratings.Single();
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
        public static RentIt.MediaRating CollectMediaReviews(
            int mediaId, RentItDatabase.Rating rating, DatabaseDataContext db)
        {
            // Get all the user reviews of the book.
            List<Review> reviews = (from r in db.Reviews where r.media_id.Equals(mediaId) select r).ToList();

            var mediaReviews = new List<MediaReview>();

            foreach (Review review in reviews)
            {
                mediaReviews.Add(
                    new MediaReview(
                        review.media_id,
                        review.user_name,
                        review.review1,
                        RatingOfValue(review.rating),
                        review.timestamp));
            }

            int rcount = rating == null ? 0 : rating.ratings_count;
            float rrate = (float)(rating == null ? 0.0 : rating.avg_rating);
            var mediaRating = new MediaRating(rcount, rrate, mediaReviews);

            return mediaRating;
        }

        /// <author>Lars Toft Jacobsen</author>
        /// <summary>
        /// Checks whether user is publisher or not
        /// </summary>
        /// <param name="acct">The account to check.</param>
        /// <returns>True if the supplied account is a publisher account, false otherwise.</returns>
        public static bool IsPublisher(Account acct)
        {
            var db = new DatabaseDataContext();

            if (!db.Publisher_accounts.Exists(p => p.user_name.Equals(acct.UserName) && p.Account.active))
                return false;
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
        /// <param name="service">
        /// The service on which the ValidateCredentials should be called.
        /// </param>
        /// <returns>
        /// true if the publisher can change the media, false otherwise.
        /// </returns>
        public static bool IsPublisherAuthorized(int mediaId, AccountCredentials credentials, DatabaseDataContext db, RentItService service)
        {
            try
            {
                service.ValidateCredentials(credentials);
            }
            catch (Exception)
            {
                return false;
            }

            if (!db.Medias.Exists(m => m.id == mediaId && m.active))
                return false; // if the media with the specified id was not found

            Media media = (from m in db.Medias
                           where m.id == mediaId && m.active
                           select m).Single();

            // find out whether this publisher is authorized to delete this media
            if (db.Publisher_accounts.Exists(p => p.user_name.Equals(credentials.UserName)
                                                  && p.publisher_id == media.publisher_id))
                return true;

            // if this publisher does not have permission to update the media
            return false;
        }

        /// <auhtor>Kenneth Søhrmann</auhtor>
        /// <summary>
        /// Determine if the serachString is contained in the source string.
        /// The determination is case-insensive.
        /// </summary>
        /// <param name="source">
        /// The string in which the search string will be determined to
        /// be contained in.
        /// </param>
        /// <param name="searchString">
        /// The string to be determined if it exists in the source string.
        /// </param>
        /// <returns>
        /// True if the search string is contained in the source string.
        /// </returns>
        public static bool ContainsIgnoreCase(string source, string searchString)
        {
            string sourceLower = source.ToLower();
            string searchStringLower = searchString.ToLower();

            return sourceLower.Contains(searchStringLower);
        }

        /// <author>Kenneth Søhrmann</author>
        /// <summary>
        /// Orders the supplied media collection with a specific ordering.
        /// </summary>
        /// <param name="db">The database context to use.</param>
        /// <param name="mediaItems">The media items to order.</param>
        /// <param name="criteria">The criteria containing the order to use.</param>
        /// <returns>The collection of media items in the specified order.</returns>
        public static IQueryable<RentItDatabase.Media> OrderMedia(
            DatabaseDataContext db, IQueryable<RentItDatabase.Media> mediaItems, MediaCriteria criteria)
        {
            switch (criteria.Order)
            {
                case MediaOrder.AlphabeticalAsc:
                    return from media in mediaItems orderby media.title select media;

                case MediaOrder.AlphabeticalDesc:
                    return from media in mediaItems orderby media.title descending select media;

                case MediaOrder.PopularityAsc:
                    return from media in mediaItems
                           join rental in db.Rentals on media.id equals rental.media_id into mediaRentals
                           orderby mediaRentals.Count()
                           select media;

                case MediaOrder.PopularityDesc:
                    return from media in mediaItems
                           join rental in db.Rentals on media.id equals rental.media_id into mediaRentals
                           orderby mediaRentals.Count() descending
                           select media;

                case MediaOrder.RatingAsc:
                    return from media in mediaItems orderby media.Rating.avg_rating select media;

                case MediaOrder.RatingDesc:
                    return from media in mediaItems orderby media.Rating.avg_rating descending select media;

                case MediaOrder.ReleaseDateAsc:
                    return from media in mediaItems orderby media.release_date select media;

                case MediaOrder.ReleaseDateDesc:
                    return from media in mediaItems orderby media.release_date descending select media;

                case MediaOrder.PriceAsc:
                    return from media in mediaItems orderby media.price select media;

                case MediaOrder.PriceDesc:
                    return from media in mediaItems orderby media.price descending select media;

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
        /// The media item to get the metadata of.
        /// </param>
        /// <returns>
        /// The metadata of the media, concatenated to a single string.
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

        /// <author>Kenneth Søhrmann</author>
        /// <summary>
        /// Converts a list of media-instances from the database to an instance of
        /// MediaItems with all data of the various media items submitted.
        /// </summary>
        /// <param name="mediaList">
        /// The collection of media-instances to be converted to a MediaItems-
        /// instance.
        /// </param>
        /// <param name="service">
        /// The service on which the GetXxxInfo methods should be called.
        /// </param>
        /// <returns>
        /// A MediaItems-instance holding all metadata of all the medias submitted.
        /// </returns>
        public static MediaItems CompileMedias(IQueryable<RentItDatabase.Media> mediaList, RentItService service)
        {
            var bookInfos = new List<BookInfo>();
            var movieInfos = new List<MovieInfo>();
            var songInfos = new List<SongInfo>();
            var albumInfos = new List<AlbumInfo>();

            foreach (RentItDatabase.Media media in mediaList)
            {
                switch (MediaTypeOfValue(media.Media_type.name))
                {
                    case MediaType.Book:
                        bookInfos.Add(service.GetBookInfo(media.id));
                        break;
                    case MediaType.Movie:
                        movieInfos.Add(service.GetMovieInfo(media.id));
                        break;
                    case MediaType.Song:
                        songInfos.Add(GetSongInfo(media.id));
                        break;
                    case MediaType.Album:
                        albumInfos.Add(service.GetAlbumInfo(media.id));
                        break;
                }
            }
            return new MediaItems(bookInfos, movieInfos, albumInfos, songInfos);
        }

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
        public static SongInfo GetSongInfo(int id)
        {
            var db = new DatabaseDataContext();

            // Get the song.
            RentItDatabase.Song song = (from m in db.Songs
                                        where m.media_id.Equals(id) && m.Media.active
                                        select m).Single();

            RentItDatabase.Rating songRatings = GetMediaRating(song.media_id, db);

            RentIt.MediaRating songRating = CollectMediaReviews(song.media_id, songRatings, db);

            return SongInfo.ValueOf(song, songRating);
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
            var db = new DatabaseDataContext();

            // Get media type id
            int mediaTypeId = (from t in db.Media_types
                               where t.name.Equals(StringValueOfMediaType(mediaType))
                               select t).Single().id;

            // Check if genre already exists for the given media type
            if (db.Genres.Exists(g => g.media_type == mediaTypeId && g.name.ToLower().Equals(genreName.ToLower())))
                return (from g in db.Genres
                        where g.media_type == mediaTypeId && g.name.ToLower().Equals(genreName.ToLower())
                        select g).Single().id; // Genre and media type combination already exists, return its genre id

            // add genre to database
            var newGenre = new RentItDatabase.Genre { media_type = mediaTypeId, name = genreName };
            db.Genres.InsertOnSubmit(newGenre);
            db.SubmitChanges();

            // Return the newly added genre id
            return newGenre.id;
        }
    }
}