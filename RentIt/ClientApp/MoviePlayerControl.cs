
namespace ClientApp
{
    using BinaryCommunicator;

    using RentIt;

    /// <author>Per Mortensen</author>
    /// <summary>
    /// UserControl used for playing a rented movie.
    /// </summary>
    internal partial class MoviePlayerControl : RentItUserControl
    {
        /// <summary>
        /// The MovieInfo-instance representing the metadata of the movie
        /// currently played in this MoviePlayerControl-instance.
        /// </summary>
        private MovieInfo movie;

        /// <summary>
        /// The BookMovieDetails instance that displays the metadata of the played
        /// movie. Is displayed in the MainForm whenever the user clicks the Movie
        /// details-button.
        /// </summary>
        private BookMovieDetails movieDetails;

        /// <summary>
        /// Initializes a new instance of the MoviePlayerControl
        /// </summary>
        public MoviePlayerControl()
        {
            InitializeComponent();
        }

        /// <summary>
        /// Sets the MovieInfo-object that the movie to be played in the
        /// UserControl is based upon.
        /// </summary>
        internal MovieInfo Movie
        {
            set
            {
                titleLabel.Text = value.Title;
                movie = value;
            }
        }

        /// <summary>
        /// Streams and plays the rented book in the reader control.
        /// </summary>
        internal void Start()
        {
            var c = new Credentials(Credentials.UserName, Credentials.HashedPassword);
            mediaPlayer.URL = BinaryCommuncator.DownloadMediaURL(c, movie.Id).ToString();
        }

        #region Controllers

        /// <author>Kenneth Søhrmann</author>
        /// <summary>
        /// EventHandler for the Movie details-button. Loads an BookMovieDetails-instance
        /// into the MainForm displaying the metadata of the movie.
        /// </summary>
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

            this.FireContentChangeEvent(movieDetails, MainForm.Titles.MediaDetailsMovie);
        }

        #endregion
    }
}
