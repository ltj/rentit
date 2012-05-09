using System.Windows.Forms;

namespace ClientApp {
    using RentIt;

    public partial class MainScreen : UserControl {
        public MainScreen() {
            InitializeComponent();
            moviesList.Title = "Featured movies";
            booksList.Title = "Featured books";
            musicList.Title = "Featured music";

            updateLists();
        }

        private void updateLists() {
            var criteria = new MediaCriteria {
                                                 Genre = "",
                                                 Limit = 10,
                                                 Offset = 0,
                                                 Order = MediaOrder.Default,
                                                 SearchText = "",
                                                 Type = MediaType.Movie
                                             };

            moviesList.UpdateList(MediaType.Movie, criteria);

            criteria.Type = MediaType.Book;
            booksList.UpdateList(MediaType.Book, criteria);

            criteria.Type = MediaType.Album;
            booksList.UpdateList(MediaType.Album, criteria);
        }
    }
}
