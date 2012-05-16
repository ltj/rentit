namespace ClientApp
{
    using BinaryCommunicator;

    using RentIt;

    internal partial class MoviePlayerControl : RentItUserControl
    {
        private MovieInfo movie;

        private BookMovieDetails movieDetails;

        public MoviePlayerControl()
        {
            InitializeComponent();
        }

        internal MovieInfo Movie
        {
            set
            {
                titleLabel.Text = value.Title;
                movie = value;
            }
        }

        internal void Start()
        {
            var c = new Credentials(Credentials.UserName, Credentials.HashedPassword);
            mediaPlayer.URL = BinaryCommuncator.DownloadMediaURL(c, movie.Id).ToString();
        }

        #region Controllers

        /// <author>Kenneth Søhrmann</author>
        /// <summary>
        /// 
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void viewMovieDetails_Click(object sender, System.EventArgs e)
        {
            if (this.movieDetails == null)
            {
                this.movieDetails = new BookMovieDetails()
                {
                    RentItProxy = this.RentItProxy,
                    Credentials = this.Credentials,
                    MovieInfo = this.movie
                };
            }

            this.FireContentChangeEvent(movieDetails, TopBarControl.Titles.MediaDetailsMovie);
        }

        #endregion
    }
}
