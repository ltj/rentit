namespace ClientApp
{
    using RentIt;

    internal partial class MainScreen : RentItUserControl
    {
        public MainScreen()
        {
            InitializeComponent();
            moviesList.Title = "Featured movies";
            booksList.Title = "Featured books";
            musicList.Title = "Featured music";

            UpdateLists();
        }

        private void UpdateLists()
        {
            var criteria = new MediaCriteria
            {
                Genre = "",
                Limit = 10,
                Offset = 0,
                Order = MediaOrder.PopularityDesc,
                SearchText = "",
                Type = MediaType.Movie
            };

            moviesList.UpdateList(criteria);

            criteria.Type = MediaType.Book;
            booksList.UpdateList(criteria);

            criteria.Type = MediaType.Album;
            musicList.UpdateList(criteria);
        }

        private void MoviesButtonClick(object sender, System.EventArgs e)
        {
            FireContentChangeEvent(new MediaFrontpage(), "Movies");
        }

        private void BooksButtonClick(object sender, System.EventArgs e)
        {
            FireContentChangeEvent(new MediaFrontpage(), "Books");
        }

        private void MusicButtonClick(object sender, System.EventArgs e)
        {
            FireContentChangeEvent(new MediaFrontpage(), "Music");
        }
    }
}
