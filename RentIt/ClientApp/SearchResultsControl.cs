using System;
using System.Windows.Forms;
using System.ServiceModel;

namespace ClientApp {
    using RentIt;

    public partial class SearchResultsControl : UserControl {
        private readonly RentItClient rentIt;
        private MediaCriteria criteria;

        public SearchResultsControl() {
            InitializeComponent();
            var binding = new BasicHttpBinding();
            var address = new EndpointAddress("http://rentit.itu.dk/rentit01/RentItService.svc");
            rentIt = new RentItClient(binding, address);
        }

        internal MediaCriteria Criteria {
            get { return criteria; }
            set {
                criteria = value;
                TypeFilter.SelectedIndex = 0;
            }
        }

        private void UpdateResults() {
            MediaItems mediaItems = rentIt.GetMediaItems(Criteria);
            results.MediaItems = mediaItems;
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
            UpdateResults();
        }
    }
}
