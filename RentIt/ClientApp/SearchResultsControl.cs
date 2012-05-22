namespace ClientApp
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.ServiceModel;

    using RentIt;

    /// <summary>
    /// This class represents a screen which displays search results.
    /// It is activated from the top bar.
    /// </summary>
    internal partial class SearchResultsControl : RentItUserControl
    {
        private MediaCriteria criteria;

        public SearchResultsControl()
        {
            InitializeComponent();

            results.AddDoubleClickEventHandler(MediaItemDoubleClick);

            // price ranges for the price filter
            priceFilter.Items.Add(new PriceRange { Description = "Any", MinimumPrice = int.MinValue, MaximumPrice = int.MaxValue });
            priceFilter.Items.Add(new PriceRange { Description = "Under 50 credits", MinimumPrice = int.MinValue, MaximumPrice = 49 });
            priceFilter.Items.Add(new PriceRange { Description = "50 - 75 credits", MinimumPrice = 50, MaximumPrice = 75 });
            priceFilter.Items.Add(new PriceRange { Description = "76 - 100 credits", MinimumPrice = 76, MaximumPrice = 100 });
            priceFilter.Items.Add(new PriceRange { Description = "101 - 125 credits", MinimumPrice = 101, MaximumPrice = 125 });
            priceFilter.Items.Add(new PriceRange { Description = "126 - 150 credits", MinimumPrice = 126, MaximumPrice = 150 });
            priceFilter.Items.Add(new PriceRange { Description = "over 150 credits", MinimumPrice = 151, MaximumPrice = Int16.MaxValue });
            priceFilter.SelectedIndex = 0;
        }

        /// <summary>
        /// The criteria to query the service with.
        /// When set, updates the entire control.
        /// </summary>
        internal MediaCriteria Criteria
        {
            get { return criteria; }
            set
            {
                criteria = value;
                UpdateTypes();
                UpdateGenres(criteria.Type);
                UpdateResults();
            }
        }

        /// <summary>
        /// Updates the search results list by querying the service
        /// with a set of criteria.
        /// </summary>
        private void UpdateResults() {
            MediaItems mediaItems;
            try {
                mediaItems = RentItProxy.GetMediaItems(Criteria);
            } catch(FaultException) {
                RentItMessageBox.ServerCommunicationError();
                return;
            }
            results.MediaItems = FilterByPrice(mediaItems);
            resultsLabel.Text = "Results for \"" + Criteria.SearchText + "\"";
        }

        /// <summary>
        /// Filters the search results based on price range selected.
        /// This is a local filtering.
        /// </summary>
        /// <param name="items">The media items to filter.</param>
        /// <returns>A filtered collection of media.</returns>
        private MediaItems FilterByPrice(MediaItems items)
        {
            var p = (PriceRange)priceFilter.SelectedItem;

            if (p.MinimumPrice == int.MinValue && p.MaximumPrice == int.MaxValue)
                return items; // no price filtering

            IEnumerable<AlbumInfo> albums = items.Albums.Where(m => m.Price >= p.MinimumPrice && m.Price <= p.MaximumPrice);
            IEnumerable<MovieInfo> movies = items.Movies.Where(m => m.Price >= p.MinimumPrice && m.Price <= p.MaximumPrice);
            IEnumerable<BookInfo> books = items.Books.Where(m => m.Price >= p.MinimumPrice && m.Price <= p.MaximumPrice);
            IEnumerable<SongInfo> songs = items.Songs.Where(m => m.Price >= p.MinimumPrice && m.Price <= p.MaximumPrice);

            return new MediaItems
            {
                Albums = albums.ToArray(),
                Movies = movies.ToArray(),
                Books = books.ToArray(),
                Songs = songs.ToArray()
            };
        }

        /// <summary>
        /// Selects the correct media type in the media type list
        /// depending on the type in the criteria object.
        /// </summary>
        private void UpdateTypes()
        {
            MediaType type = Criteria.Type;
            int index;

            switch (type)
            {
                case MediaType.Movie:
                    index = 1;
                    break;
                case MediaType.Album:
                    index = 2;
                    break;
                case MediaType.Book:
                    index = 3;
                    break;
                default:
                    index = 0;
                    break;
            }

            TypeFilter.SelectedIndex = index;
        }

        /// <summary>
        /// Updates the list of genres depending on the media type.
        /// </summary>
        /// <param name="mediaType">The media type for which all genres will be shown.</param>
        private void UpdateGenres(MediaType mediaType) {
            string[] genres;
            try {
                genres = RentItProxy.GetAllGenres(mediaType);
            } catch(FaultException) {
                RentItMessageBox.ServerCommunicationError();
                return;
            }
            GenreFilter.Items.Clear();
            GenreFilter.Items.Add("All");
            GenreFilter.Items.AddRange(genres);
            GenreFilter.SelectedIndex = 0;
        }

        /// <summary>
        /// An event handler for when the selected item in the type list changes.
        /// </summary>
        private void TypeFilterSelectedIndexChanged(object sender, EventArgs e)
        {
            MediaType newType;

            if (TypeFilter.SelectedItem.Equals("Movies")) newType = MediaType.Movie;
            else if (TypeFilter.SelectedItem.Equals("Music")) newType = MediaType.Album;
            else if (TypeFilter.SelectedItem.Equals("Books")) newType = MediaType.Book;
            else newType = MediaType.Any;

            Criteria.Genre = "";
            Criteria.Type = newType;
            UpdateGenres(newType);
        }

        /// <summary>
        /// An event handler for when the selected item in the genre list changes.
        /// </summary>
        private void GenreFilterSelectedIndexChanged(object sender, EventArgs e)
        {
            string genre = GenreFilter.SelectedItem.Equals("All") ? "" : GenreFilter.SelectedItem.ToString();
            Criteria.Genre = genre;
        }

        /// <summary>
        /// An event handler for when the filter button is clicked.
        /// </summary>
        private void FilterButtonClick(object sender, EventArgs e)
        {
            UpdateResults();
        }

        /// <summary>
        /// An event handler for when an item in the search results is double-clicked.
        /// </summary>
        private void MediaItemDoubleClick(object sender, EventArgs e)
        {
            MediaInfo media = results.GetSingleMedia();
            DisplayMediaItem(media);
        }

        /// <summary>
        /// Changes the window content to display details about a media item.
        /// </summary>
        /// <param name="media">The item to display details of.</param>
        private void DisplayMediaItem(MediaInfo media)
        {
            RentItUserControl mediaDetails;
            string title;
            switch (media.Type)
            {
                case MediaType.Album:
                    mediaDetails = new AlbumDetails
                        {
                            RentItProxy = RentItProxy,
                            Credentials = Credentials,
                            AlbumInfo = (AlbumInfo)media
                        };
                    title = "Album details";
                    break;
                case MediaType.Book:
                    mediaDetails = new BookMovieDetails
                        {
                            RentItProxy = RentItProxy,
                            Credentials = Credentials,
                            BookInfo = (BookInfo)media
                        };
                    title = "Book details";
                    break;
                case MediaType.Movie:
                    mediaDetails = new BookMovieDetails
                        {
                            RentItProxy = RentItProxy,
                            Credentials = Credentials,
                            MovieInfo = (MovieInfo)media
                        };
                    title = "Movie details";
                    break;
                case MediaType.Song:
                    mediaDetails = new AlbumDetails
                        {
                            RentItProxy = RentItProxy,
                            Credentials = Credentials,
                            AlbumInfo = RentItProxy.GetAlbumInfo(((SongInfo)media).AlbumId)
                        };
                    title = "Album details";
                    break;
                default:
                    return;
            }

            FireContentChangeEvent(mediaDetails, title);
        }

        /// <summary>
        /// A struct representing a price range for the price filtering.
        /// </summary>
        private struct PriceRange
        {
            public int MinimumPrice;
            public int MaximumPrice;
            public string Description;

            public override string ToString()
            {
                return Description;
            }
        }
    }
}
