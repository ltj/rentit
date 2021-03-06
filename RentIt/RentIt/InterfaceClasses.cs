﻿using System;
using System.Collections.Generic;
using System.Runtime.Serialization;
using System.Linq;

namespace RentIt
{
    /// <summary>
    /// The type of a media item.
    /// </summary>
    [DataContract]
    public enum MediaType
    {
        [EnumMember]
        Any,

        [EnumMember]
        Book,

        [EnumMember]
        Movie,

        [EnumMember]
        Album,

        [EnumMember]
        Song
    }

    /// <summary>
    /// The order of the media items returned in a web service query.
    /// When a web service method that uses a MediaCriteria object returns a collection of media items,
    /// the MediaOrder enum can specify in which order the media items should be sorted.
    /// Each ordering method has an ascending and a descending version, which result in opposite ordering.
    /// </summary>
    [DataContract]
    public enum MediaOrder
    {
        /// <summary>
        /// Sorts items in an unspecified (default) manner, decided by the web service.
        /// </summary>
        [EnumMember]
        Default,

        /// <summary>
        /// Sorts items by their release date, from oldest to newest.
        /// </summary>
        [EnumMember]
        ReleaseDateAsc,

        /// <summary>
        /// Sorts items by their release date, from newest to oldest.
        /// </summary>
        [EnumMember]
        ReleaseDateDesc,

        /// <summary>
        /// Sorts items by their average user rating, from lowest to highest.
        /// </summary>
        [EnumMember]
        RatingAsc,

        /// <summary>
        /// Sorts items by their average user rating, from highest to lowest.
        /// </summary>
        [EnumMember]
        RatingDesc,

        /// <summary>
        /// Sorts items by their popularity, from lowest to highest.
        /// Popularity is defined as the total number of rentals since the beginning of time.
        /// </summary>
        [EnumMember]
        PopularityAsc,

        /// <summary>
        /// Sorts items by their popularity, from highest to lowest.
        /// Popularity is defined as the total number of rentals since the beginning of time.
        /// </summary>
        [EnumMember]
        PopularityDesc,

        /// <summary>
        /// Sorts items by their name, using .NET's default sorting for strings, generally from A to Z.
        /// </summary>
        [EnumMember]
        AlphabeticalAsc,

        /// <summary>
        /// Sorts items by their name, using the reverse of .NET's default sorting for strings, generally from Z to A.
        /// </summary>
        [EnumMember]
        AlphabeticalDesc,

        /// <summary>
        /// Sorts items by their price, from lowest to highest.
        /// </summary>
        [EnumMember]
        PriceAsc,

        /// <summary>
        /// Sorts items by their price, from highest to lowest.
        /// </summary>
        [EnumMember]
        PriceDesc
    }

    /// <summary>
    /// The individual rating of a media item, from 1 (worst) to 5 (best).
    /// Defined as an enum to limit the range of values.
    /// </summary>
    [DataContract]
    public enum Rating
    {
        [EnumMember]
        One = 1,

        [EnumMember]
        Two = 2,

        [EnumMember]
        Three = 3,

        [EnumMember]
        Four = 4,

        [EnumMember]
        Five = 5
    }

    /// <summary>
    /// Defines a set of criteria for searching and browsing the collection of media on the server.
    /// These criteria includes the media type, genre, search text, ordering of the results,
    /// a limit on the number of returned rows and an offset that specifies how many items to skip
    /// before collecting and returning items.
    /// The specified criteria form a conjunction, not a disjunction.
    /// As an example, a search with the type MOVIE and the search text "madonna" will return only
    /// movies where "madonna" appears in its metadata; it will not return all movies plus all
    /// media with "madonna" in its metadata.
    /// In other words, the criteria are AND-based, not OR-based, and, as such, the more criteria
    /// specified, the narrower the search will become.
    /// </summary>
    [DataContract]
    public class MediaCriteria
    {
        /// <summary>
        /// The type of the media items returned.
        /// Used to filter results by type, e.g. to only return books.
        /// </summary>
        [DataMember]
        public MediaType Type;

        /// <summary>
        /// The ordering of the media items.
        /// </summary>
        [DataMember]
        public MediaOrder Order;

        /// <summary>
        /// The genre of the media items, e.g. "Rock" will match any media item whose metadata specifies "Rock" as its genre.
        /// An empty string ("") will match any genre (i.e. no genre criteria is specified).
        /// </summary>
        [DataMember]
        public string Genre;

        /// <summary>
        /// The free-form search text used to match media items, e.g. "fight club" or "alfred hitchcock" will match.
        /// An empty string ("") will match anything (i.e. the search text will not be used).
        /// The metadata searched is: title, author, director, artist, album artist, summary, description, publisher.
        /// Metadata fields vary between media types, as shown in our initial data model.
        /// </summary>
        [DataMember]
        public string SearchText;

        /// <summary>
        /// The maximum number of media items returned.
        /// A negative limit (limit less than 0) means that the number of returned items is unlimited.
        /// A limit of 0 means that no items will be returned.
        /// This is especially useful for charts: criteria containing the type BOOK, the order POPULARITY_DESC and a
        /// limit of 10 will return the 10 most popular books in the system.
        /// </summary>
        [DataMember]
        public int Limit;

        /// <summary>
        /// The number of media items to skip before starting to collect and return items.
        /// An offset of 0 or less (offset less than or equal to 0) means that no items will be skipped.
        /// A positive offset n means that the first returned item will be item n+1.
        /// This could be used in conjunction with Limit to load large collections of media items gradually,
        /// e.g. such that only 20 items will be returned at a time (with limit = 20 and an accumulating offset incrementing by 20).
        /// </summary>
        [DataMember]
        public int Offset;

        /// <summary>
        /// The constructor for MediaCriteria objects.
        /// Be advised that all parameters are optional. A parameter-less MediaCriteria object will match any media type with
        /// any genre and any content, with no special ordering, no offset, and no limit on the number of items.
        /// In other words, it will match any and all media items.
        /// </summary>
        /// <param name="type">
        /// The media type.
        /// </param>
        /// <param name="order">
        /// The ordering.
        /// </param>
        /// <param name="genre">
        /// The genre.
        /// </param>
        /// <param name="searchText">
        /// The free-form search query.
        /// </param>
        /// <param name="limit">
        /// The maximum number of items.
        /// </param>
        /// <param name="offset">
        /// The number of items skipped at the beginning.
        /// </param>
        public MediaCriteria(MediaType type = MediaType.Any, MediaOrder order = MediaOrder.Default, string genre = "", string searchText = "", int limit = -1, int offset = 0)
        {
            Type = type;
            Order = order;
            Genre = genre;
            SearchText = searchText;
            Limit = limit;
            Offset = offset;
        }
    }

    /// <summary>
    /// An individual rating, optionally accompanied by a written review, by a single user.
    /// </summary>
    [DataContract]
    public struct MediaReview
    {
        /// <summary>
        /// The id of the media which this review is about.
        /// </summary>
        [DataMember]
        public readonly int MediaId;

        /// <summary>
        /// The time this rating was submitted.
        /// </summary>
        [DataMember]
        public readonly System.DateTime Timestamp;

        /// <summary>
        /// The user name of the reviewer.
        /// </summary>
        [DataMember]
        public readonly string UserName;

        /// <summary>
        /// The optional written review, as a single string.
        /// </summary>
        [DataMember]
        public readonly string ReviewText;

        /// <summary>
        /// The rating of 1 - 5 that this user gave.
        /// </summary>
        [DataMember]
        public readonly Rating Rating;

        /// <summary>
        /// Constructor for MediaReview objects.
        /// </summary>
        /// <param name="mediaId">
        /// The media id.
        /// </param>
        /// <param name="userName">
        /// The user name.
        /// </param>
        /// <param name="reviewText">
        /// The written review.
        /// </param>
        /// <param name="rating">
        /// The rating of 1 - 5.
        /// </param>
        public MediaReview(int mediaId, string userName, string reviewText, Rating rating)
            : this(mediaId, userName, reviewText, rating, DateTime.Now) { }

        internal MediaReview(int mediaId, string userName, string reviewText, Rating rating, DateTime timestamp)
        {
            MediaId = mediaId;
            Timestamp = timestamp;
            UserName = userName;
            ReviewText = reviewText;
            Rating = rating;
        }
    }

    /// <summary>
    /// Complete rating information about a single media item.
    /// </summary>
    [DataContract]
    public class MediaRating
    {
        /// <summary>
        /// The total number of ratings for this media item.
        /// </summary>
        [DataMember]
        public readonly int RatingsCount;

        /// <summary>
        /// A pre-computed average rating for this media item, based on all user ratings.
        /// </summary>
        [DataMember]
        public readonly float AverageRating;

        /// <summary>
        /// A list of individual ratings and their accompanying reviews.
        /// </summary>
        [DataMember]
        public readonly System.Collections.Generic.List<MediaReview> Reviews;

        /// <summary>
        /// The constructor for MediaRating objects.
        /// </summary>
        /// <param name="ratingsCount">
        /// The number of ratings.
        /// </param>
        /// <param name="averageRating">
        /// The average rating.
        /// </param>
        /// <param name="reviews">
        /// The collection of user reviews.
        /// </param>
        public MediaRating(int ratingsCount, float averageRating, System.Collections.Generic.List<MediaReview> reviews)
        {
            RatingsCount = ratingsCount;
            AverageRating = averageRating;
            Reviews = reviews;
        }
    }

    /// <summary>
    /// Holds 4 lists, each containing a number of BookInfo, MovieInfo, AlbumInfo and SongInfo, respectively.
    /// An object of this class is typically returned by a web service method that returns a number of media
    /// items matching certain criteria.
    /// </summary>
    [DataContract]
    public class MediaItems
    {
        /// <summary>
        /// A list of books (BookInfo objects).
        /// </summary>
        [DataMember]
        public readonly System.Collections.Generic.List<BookInfo> Books;

        /// <summary>
        /// A list of movies (MovieInfo objects).
        /// </summary>
        [DataMember]
        public readonly System.Collections.Generic.List<MovieInfo> Movies;

        /// <summary>
        /// A list of albums (AlbumInfo objects).
        /// </summary>
        [DataMember]
        public readonly System.Collections.Generic.List<AlbumInfo> Albums;

        /// <summary>
        /// A list of songs (SongInfo objects).
        /// </summary>
        [DataMember]
        public readonly System.Collections.Generic.List<SongInfo> Songs;

        /// <summary>
        /// Constructor for MediaItems objects.
        /// </summary>
        /// <param name="books">
        /// The list of books.
        /// </param>
        /// <param name="movies">
        /// The list of movies.
        /// </param>
        /// <param name="albums">
        /// The list of albums.
        /// </param>
        /// <param name="songs">
        /// The list of songs.
        /// </param>
        public MediaItems(System.Collections.Generic.List<BookInfo> books,
                    System.Collections.Generic.List<MovieInfo> movies,
                    System.Collections.Generic.List<AlbumInfo> albums,
                    System.Collections.Generic.List<SongInfo> songs)
        {
            Books = books;
            Movies = movies;
            Albums = albums;
            Songs = songs;
        }
    }

    /// <summary>
    /// Metadata about a media item. This class holds all the properties that
    /// are common to the four different kinds of media offered by the service namely 
    /// book, movies, songs and music albums (represented by instances of BookInfo,
    /// MovieInfo, SongInfo and AlbumInfo respectively)
    /// </summary>
    [KnownType(typeof(BookInfo))]
    [KnownType(typeof(AlbumInfo))]
    [KnownType(typeof(SongInfo))]
    [KnownType(typeof(MovieInfo))]
    [DataContract]
    public abstract class MediaInfo
    {
        /// <summary>
        /// A unique identifier of the media item this object represents.
        /// </summary>
        [DataMember]
        public readonly int Id;

        /// <summary>
        /// The title of the media item this object represents.
        /// </summary>
        [DataMember]
        public readonly string Title;

        /// <summary>
        /// The type of the media item this object represents.
        /// </summary>
        [DataMember]
        public readonly MediaType Type;

        /// <summary>
        /// The genre of the media item this object represents.
        /// </summary>
        [DataMember]
        public readonly string Genre;

        /// <summary>
        /// The price of the media item the customer has to pay before being able to rent it.
        /// </summary>
        [DataMember]
        public readonly int Price;

        /// <summary>
        /// The date at which the media item this object represents was released.
        /// </summary>
        [DataMember]
        public readonly System.DateTime ReleaseDate;

        /// <summary>
        /// The name of the publisher who published the media item this object represents.
        /// </summary>
        [DataMember]
        public readonly string Publisher;

        /// <summary>
        /// The rating of the media item this object represents.
        /// </summary>
        [DataMember]
        public readonly MediaRating Rating;

        protected MediaInfo(int id, string title, MediaType type, string genre, int price, System.DateTime releaseDate, string publisher, MediaRating rating)
        {
            Id = id;
            Title = title;
            Type = type;
            Genre = genre;
            Price = price;
            ReleaseDate = releaseDate;
            Publisher = publisher;
            Rating = rating;
        }
    }

    /// <summary>
    /// Represents the metadata of a media item of type book.
    /// </summary>
    [DataContract]
    public class BookInfo : MediaInfo
    {
        /// <summary>
        /// The author of the book this object represents.
        /// </summary>
        [DataMember]
        public readonly string Author;

        /// <summary>
        ///	The number of pages of the book this object represents.
        /// </summary>
        [DataMember]
        public readonly int Pages;

        /// <summary>
        /// A summary of the movie this object represents.
        /// The summary includes a summary of the book.
        /// </summary>
        [DataMember]
        public readonly string Summary;

        public BookInfo(int id, string title, MediaType type, string genre, int price, System.DateTime releaseDate, string publisher, MediaRating rating, string author, int pages, string summary)
            : base(id, title, type, genre, price, releaseDate, publisher, rating)
        {
            Author = author;
            Pages = pages;
            Summary = summary;
        }

        /// <author>Kenneth Søhrmann</author>
        /// <summary>
        /// 
        /// </summary>
        /// <param name="databaseMedia"></param>
        /// <param name="rating"></param>
        /// <returns></returns>
        internal static BookInfo ValueOf(RentItDatabase.Book databaseMedia, MediaRating rating)
        {
            return new BookInfo(
                databaseMedia.media_id,
                databaseMedia.Media.title,
                Util.MediaTypeOfValue(databaseMedia.Media.Media_type.name),
                databaseMedia.Media.Genre.name,
                databaseMedia.Media.price,
                databaseMedia.Media.release_date,
                databaseMedia.Media.Publisher.title,
                rating,
                databaseMedia.author,
                databaseMedia.pages,
                databaseMedia.summary);
        }
    }

    /// <summary>
    /// Represents the metadata of a media item of type movie.
    /// </summary>
    [DataContract]
    public class MovieInfo : MediaInfo
    {
        /// <summary>
        /// The director of the movie this object represents.
        /// </summary>
        [DataMember]
        public readonly string Director;

        /// <summary>
        /// The duration of the movie this object represents.
        /// </summary>
        [DataMember]
        public readonly System.TimeSpan Duration;

        /// <summary>
        /// A summary of the movie this object represents.
        /// The summary includes a summary of the movie as well as a list of the actors
        /// starring in the movie.
        /// </summary>
        [DataMember]
        public readonly string Summary;

        public MovieInfo(int id, string title, MediaType type, string genre, int price, System.DateTime releaseDate, string publisher, MediaRating rating, string director, System.TimeSpan duration, string summary)
            : base(id, title, type, genre, price, releaseDate, publisher, rating)
        {
            Director = director;
            Duration = duration;
            Summary = summary;
        }

        /// <author>Kenneth Søhrmann</author>
        /// <summary>
        /// 
        /// </summary>
        /// <param name="databaseMedia"></param>
        /// <param name="rating"></param>
        /// <returns></returns>
        internal static MovieInfo ValueOf(RentItDatabase.Movie databaseMedia, MediaRating rating)
        {
            return new MovieInfo(
                databaseMedia.media_id,
                databaseMedia.Media.title,
                Util.MediaTypeOfValue(databaseMedia.Media.Media_type.name),
                databaseMedia.Media.Genre.name,
                databaseMedia.Media.price,
                databaseMedia.Media.release_date,
                databaseMedia.Media.Publisher.title,
                rating,
                databaseMedia.director,
                TimeSpan.FromSeconds(databaseMedia.length),
                databaseMedia.summary);
        }
    }

    /// <summary>
    /// Represents the metadata of a media item of type song.
    /// </summary>
    [DataContract]
    public class SongInfo : MediaInfo
    {
        /// <summary>
        /// The artist of the song this object represents.
        /// </summary>
        [DataMember]
        public readonly string Artist;

        [DataMember]
        public readonly int AlbumId;

        /// <summary>
        /// The duration of the song this object represents.
        /// </summary>
        [DataMember]
        public readonly System.TimeSpan Duration;

        public SongInfo(int id, string title, MediaType type, string genre, int price, System.DateTime releaseDate, string publisher, MediaRating rating, string artist, System.TimeSpan duration, int albumId)
            : base(id, title, type, genre, price, releaseDate, publisher, rating)
        {
            Artist = artist;
            Duration = duration;
            AlbumId = albumId;
        }

        /// <author>Kenneth Søhrmann</author>
        /// <summary>
        /// 
        /// </summary>
        /// <param name="databaseMedia"></param>
        /// <param name="rating"></param>
        /// <returns></returns>
        internal static SongInfo ValueOf(RentItDatabase.Song databaseMedia, MediaRating rating)
        {
            return new SongInfo(
                databaseMedia.media_id,
                databaseMedia.Media.title,
                Util.MediaTypeOfValue(databaseMedia.Media.Media_type.name),
                databaseMedia.Media.Genre.name,
                databaseMedia.Media.price,
                databaseMedia.Media.release_date,
                databaseMedia.Media.Publisher.title,
                rating,
                databaseMedia.artist,
                System.TimeSpan.FromSeconds((int)databaseMedia.length),
                databaseMedia.Album_songs.First().album_id);
        }
    }

    /// <summary>
    /// Represents metadata of a media item of type album.
    /// </summary>
    [DataContract]
    public class AlbumInfo : MediaInfo
    {
        /// <summary>
        ///	The album artist of the album this object represents.
        /// </summary>
        [DataMember]
        public readonly string AlbumArtist;

        /// <summary>
        /// The total duration of all the songs of the album this object represents.
        /// </summary>
        [DataMember]
        public readonly System.TimeSpan TotalDuration;

        /// <summary>
        /// Description of the album this object represents, typically in the form
        /// of the press release published upon the album's release.
        /// </summary>
        [DataMember]
        public readonly string Description;

        /// <summary>
        /// A list of all the song items that is a part of the album this object represents.
        /// </summary>
        [DataMember]
        public readonly System.Collections.Generic.List<SongInfo> Songs;

        public AlbumInfo(int id, string title, MediaType type, string genre, int price, System.DateTime releaseDate, string publisher, MediaRating rating, string albumArtist, System.TimeSpan totalDuration, string description, System.Collections.Generic.List<SongInfo> songs)
            : base(id, title, type, genre, price, releaseDate, publisher, rating)
        {
            AlbumArtist = albumArtist;
            TotalDuration = totalDuration;
            Description = description;
            Songs = songs;
        }

        /// <author>Kenneth Søhrmann</author>
        /// <summary>
        /// 
        /// </summary>
        /// <param name="databaseMedia"></param>
        /// <param name="albumSongs"></param>
        /// <param name="rating"></param>
        /// <returns></returns>
        internal static AlbumInfo ValueOf(RentItDatabase.Album databaseMedia, List<SongInfo> albumSongs, MediaRating rating)
        {
            int albumDuration = 0;

            foreach (RentItDatabase.Album_song song in databaseMedia.Album_songs)
            {
                albumDuration += song.Song.length;
            }

            return new AlbumInfo(
                databaseMedia.media_id,
                databaseMedia.Media.title,
                Util.MediaTypeOfValue(databaseMedia.Media.Media_type.name),
                databaseMedia.Media.Genre.name,
                databaseMedia.Media.price,
                databaseMedia.Media.release_date,
                databaseMedia.Media.Publisher.title,
                rating,
                databaseMedia.album_artist,
                System.TimeSpan.FromSeconds(albumDuration),
                databaseMedia.description,
                albumSongs);
        }
    }

    /// <summary>
    /// Holds data associated with a single media item rental.
    /// </summary>
    [DataContract]
    public struct Rental
    {
        /// <summary>
        /// The media data of the media item that has been rented.
        /// </summary>
        [DataMember]
        public readonly int MediaId;

        /// <summary>
        /// The type of the rented media. Combined with the media id
        /// (see above), one can get complete info on the media
        /// by using the Get*Info methods in the service interface.
        /// </summary>
        [DataMember]
        public readonly MediaType MediaType;

        /// <summary>
        /// The time at when the media item was paid for.
        /// </summary>
        [DataMember]
        public readonly System.DateTime StartTime;

        /// <summary>
        /// The time at when the rental of the media item expires.
        /// </summary>
        [DataMember]
        public readonly System.DateTime EndTime;

        public Rental(int mediaId, MediaType mediaType, System.DateTime startTime, System.DateTime endTime)
        {
            MediaId = mediaId;
            MediaType = mediaType;
            StartTime = startTime;
            EndTime = endTime;
        }
    }

    /// <summary>
    /// Represents data of a service account. This class holds all the properties that
    /// are common to the two different kinds of accounts offered by the service namely 
    /// publisher and user accounts (represented by instances of PublisherAccount and
    /// UserAccount respectively)
    /// </summary>
    [KnownType(typeof(UserAccount))]
    [KnownType(typeof(PublisherAccount))]
    [DataContract]
    public class Account
    {
        /// <summary>
        /// The user name of the account this object represents.
        /// </summary>
        [DataMember]
        public readonly string UserName;

        /// <summary>
        /// The full name of the person who owns the account this object represents.
        /// </summary>
        [DataMember]
        public readonly string FullName;

        /// <summary>
        /// The email address of the person who owns the account this object represents.
        /// </summary>
        [DataMember]
        public readonly string Email;

        /// <summary>
        /// The hashed password of the account this object represents.
        /// </summary>
        [DataMember]
        public readonly string HashedPassword;

        public Account(string userName, string fullName, string email, string hashedPassword)
        {
            UserName = userName;
            FullName = fullName;
            Email = email;
            HashedPassword = hashedPassword;
        }

        public static Account ValueOf(RentItDatabase.Account account)
        {
            return new Account(
                account.user_name,
                account.full_name,
                account.email,
                account.password);
        }
    }

    /// <summary>
    /// Entity representing an account of a costumer of the service.
    /// A customer account is used by the users of the service to register data about
    /// themselves which is reguired in order to pay and rent media of the service.
    /// </summary>
    [DataContract]
    public class UserAccount : Account
    {
        /// <summary>
        /// The balance of credits used by the customer to pay and rent media.
        /// This is the balance currently registered at the service server.
        /// </summary>
        [DataMember]
        public readonly int Credits;

        /// <summary>
        /// The rental history of the user indentified by this account. This includes all rental,
        /// both old and currently active ones. The client can do the appropriate handling to get
        /// the currently active rentals.
        /// </summary>
        [DataMember]
        public readonly System.Collections.Generic.List<Rental> Rentals;

        public UserAccount(string userName, string fullName, string email, string hashedPassword, int credits, System.Collections.Generic.List<Rental> rentals)
            : base(userName, fullName, email, hashedPassword)
        {
            Credits = credits;
            Rentals = rentals;
        }
    }

    /// <summary>
    /// Entity representing an account of a publisher registered at the service.
    /// The publisher account has the ability to add and manage media at the service
    /// available for browsing and rental.
    /// </summary>
    [DataContract]
    public class PublisherAccount : Account
    {
        /// <summary>
        /// The public name of the publisher.
        /// </summary>
        [DataMember]
        public readonly string PublisherName;

        /// <summary>
        /// Collection of media items that has been published by the publisher identified
        /// by this account.
        /// </summary>
        [DataMember]
        public readonly MediaItems PublishedItems;

        public PublisherAccount(string userName, string fullName, string email, string hashedPassword, string publisherName, MediaItems publishedItems)
            : base(userName, fullName, email, hashedPassword)
        {
            PublisherName = publisherName;
            PublishedItems = publishedItems;
        }
    }

    /// <summary>
    /// Construct holding the credentials of either a publisher or customer account.
    /// Holds both user name and the hashed password.
    /// </summary>
    [DataContract]
    public class AccountCredentials
    {
        /// <summary>
        /// The user name of the credential
        /// </summary>
        [DataMember]
        public readonly string UserName;

        /// <summary>
        /// The hashed value of the password associated with the user name.
        /// </summary>
        [DataMember]
        public readonly string HashedPassword;

        public AccountCredentials(string userName, string hashedPassword)
        {
            UserName = userName;
            HashedPassword = hashedPassword;
        }
    }

    /// <summary>
    /// Indicates that the submitted user credentials are not valid for this action.
    /// </summary>
    [Serializable]
    public class InvalidCredentialsException : System.Exception
    {
        public InvalidCredentialsException() { }
        public InvalidCredentialsException(string message) : base(message) { }
        public InvalidCredentialsException(string message, System.Exception inner) : base(message, inner) { }
        public InvalidCredentialsException(System.Runtime.Serialization.SerializationInfo info, System.Runtime.Serialization.StreamingContext context) : base(info, context) { }
    }

    /// <summary>
    /// Indicates that the submitted user information is not valid for creating a new user.
    /// </summary>
    [Serializable]
    public class UserCreationException : System.Exception
    {
        public UserCreationException() { }
        public UserCreationException(string message) : base(message) { }
        public UserCreationException(string message, System.Exception inner) : base(message, inner) { }
        public UserCreationException(System.Runtime.Serialization.SerializationInfo info, System.Runtime.Serialization.StreamingContext context) : base(info, context) { }
    }
}
