using System;
using System.Windows.Forms;
using System.ServiceModel;

namespace ClientApp {
    using System.Collections.Generic;

    using System.Linq;

    using RentIt;

    public partial class SearchResultsControl : RentItUserControl {
        private readonly RentItClient rentIt;
        private MediaCriteria criteria;

        public SearchResultsControl() {
            InitializeComponent();
            var binding = new BasicHttpBinding();
            var address = new EndpointAddress("http://rentit.itu.dk/rentit01/RentItService.svc");
            rentIt = new RentItClient(binding, address);

            priceFilter.Items.Add(new PriceRange { Description = "Any", MinimumPrice = int.MinValue, MaximumPrice = int.MaxValue });
            priceFilter.Items.Add(new PriceRange { Description = "Under 50 credits", MinimumPrice = int.MinValue, MaximumPrice = 49 });
            priceFilter.Items.Add(new PriceRange { Description = "50 - 75 credits", MinimumPrice = 50, MaximumPrice = 75 });
            priceFilter.Items.Add(new PriceRange { Description = "76 - 100 credits", MinimumPrice = 76, MaximumPrice = 100 });
            priceFilter.Items.Add(new PriceRange { Description = "101 - 125 credits", MinimumPrice = 101, MaximumPrice = 125 });
            priceFilter.Items.Add(new PriceRange { Description = "126 - 150 credits", MinimumPrice = 126, MaximumPrice = 150 });
            priceFilter.Items.Add(new PriceRange { Description = "over 150 credits", MinimumPrice = 151, MaximumPrice = Int16.MaxValue });
            priceFilter.SelectedIndex = 0;
        }

        internal MediaCriteria Criteria {
            get { return criteria; }
            set {
                criteria = value;
                UpdateTypes();
                UpdateGenres(criteria.Type);
                UpdateResults();
            }
        }

        private void UpdateResults() {
            MediaItems mediaItems = rentIt.GetMediaItems(Criteria);
            results.MediaItems = FilterByPrice(mediaItems);
            resultsLabel.Text = "Results for \"" + Criteria.SearchText + "\"";
        }

        private MediaItems FilterByPrice(MediaItems items) {
            var p = (PriceRange)priceFilter.SelectedItem;

            if(p.MinimumPrice == int.MinValue && p.MaximumPrice == int.MaxValue)
                return items; // no price filtering

            IEnumerable<AlbumInfo> albums = items.Albums.Where(m => m.Price >= p.MinimumPrice && m.Price <= p.MaximumPrice);
            IEnumerable<MovieInfo> movies = items.Movies.Where(m => m.Price >= p.MinimumPrice && m.Price <= p.MaximumPrice);
            IEnumerable<BookInfo> books = items.Books.Where(m => m.Price >= p.MinimumPrice && m.Price <= p.MaximumPrice);
            IEnumerable<SongInfo> songs = items.Songs.Where(m => m.Price >= p.MinimumPrice && m.Price <= p.MaximumPrice);
            
            return new MediaItems {
                                      Albums = albums.ToArray(),
                                      Movies = movies.ToArray(),
                                      Books = books.ToArray(),
                                      Songs = songs.ToArray()
                                  };
        }

        private void UpdateTypes() {
            MediaType type = Criteria.Type;
            int index;

            switch(type) {
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

        private void UpdateGenres(MediaType mediaType) {
            GenreFilter.Items.Clear();
            GenreFilter.Items.Add("All");
            GenreFilter.Items.AddRange(rentIt.GetAllGenres(mediaType));
            GenreFilter.SelectedIndex = 0;
        }

        private void TypeFilterSelectedIndexChanged(object sender, EventArgs e) {
            MediaType newType;

            if(TypeFilter.SelectedItem.Equals("Movies")) newType = MediaType.Movie;
            else if(TypeFilter.SelectedItem.Equals("Music")) newType = MediaType.Album;
            else if(TypeFilter.SelectedItem.Equals("Books")) newType = MediaType.Book;
            else newType = MediaType.Any;

            Criteria.Genre = "";
            Criteria.Type = newType;
            UpdateGenres(newType);
        }

        private void GenreFilterSelectedIndexChanged(object sender, EventArgs e) {
            string genre = GenreFilter.SelectedItem.Equals("All") ? "" : GenreFilter.SelectedItem.ToString();
            Criteria.Genre = genre;
        }

        private void FilterButtonClick(object sender, EventArgs e) {
            UpdateResults();
        }

        private struct PriceRange {
            public int MinimumPrice;
            public int MaximumPrice;
            public string Description;

            public override string ToString() {
                return Description;
            }
        }
    }
}
