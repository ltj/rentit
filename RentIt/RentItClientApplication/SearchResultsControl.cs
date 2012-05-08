namespace RentItClientApplication {
    using System;
    using System.Windows.Forms;

    using RentIt;

    public partial class SearchResultsControl : UserControl {
        private readonly RentItClient rentIt;
        private readonly MediaCriteria criteria;

        public SearchResultsControl() {
            InitializeComponent();
            rentIt = new RentItClient();

            criteria = new MediaCriteria {
                SearchText = "",
                Genre = "",
                Type = MediaType.Any,
                Limit = -1,
                Offset = 0,
                Order = MediaOrder.AlphabeticalAsc
            };

            TypeFilter.SelectedIndex = 0;
        }

        private void UpdateResults() {
            Results.BeginUpdate();
            Results.Items.Clear();

            MediaItems mediaItems = rentIt.GetMediaItems(criteria);

            foreach(var movieInfo in mediaItems.Movies) {
                var item = new ListViewItem(movieInfo.Type.ToString());
                item.SubItems.Add(movieInfo.Title);
                item.SubItems.Add(movieInfo.Price.ToString());
                item.SubItems.Add(movieInfo.Genre);
                Results.Items.Add(item);
            }

            foreach(var bookInfo in mediaItems.Books) {
                var item = new ListViewItem(bookInfo.Type.ToString());
                item.SubItems.Add(bookInfo.Title);
                item.SubItems.Add(bookInfo.Price.ToString());
                item.SubItems.Add(bookInfo.Genre);
                Results.Items.Add(item);
            }

            foreach(var albumInfo in mediaItems.Albums) {
                var item = new ListViewItem(albumInfo.Type.ToString());
                item.SubItems.Add(albumInfo.Title);
                item.SubItems.Add(albumInfo.Price.ToString());
                item.SubItems.Add(albumInfo.Genre);
                Results.Items.Add(item);
            }

            Results.EndUpdate();
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

            criteria.Genre = "";
            criteria.Type = newType;
            UpdateGenres(newType);
        }

        private void GenreFilterSelectedIndexChanged(object sender, EventArgs e) {
            string genre = GenreFilter.SelectedItem.Equals("All") ? "" : GenreFilter.SelectedItem.ToString();
            criteria.Genre = genre;
            UpdateResults();
        }
    }
}
