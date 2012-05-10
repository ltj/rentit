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
            results.MediaItems = mediaItems;
            resultsLabel.Text = "Results for \"" + Criteria.SearchText + "\"";
        }

        /*private MediaItems FilterByPrice(MediaItems items) {
            int minPrice = 0;
            int maxPrice = 0;

            int selectedIndex = 

            var albums = new List<AlbumInfo>();
            var movies = new List<MovieInfo>();
            var books = new List<BookInfo>();
            var songs = new List<SongInfo>();

            albums = items.Albums.Where(m => m.Price);

            return new MediaItems {
                Albums = albums.ToArray(),
                Movies = movies.ToArray(),
                Books = books.ToArray(),
                Songs = songs.ToArray()
            };
        }*/

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

            if(TypeFilter.SelectedItem.Equals("Movies"))
                newType = MediaType.Movie;
            else if(TypeFilter.SelectedItem.Equals("Music"))
                newType = MediaType.Album;
            else if(TypeFilter.SelectedItem.Equals("Books"))
                newType = MediaType.Book;
            else
                newType = MediaType.Any;

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
    }
}
