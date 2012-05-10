namespace ClientApp {
    using BinaryCommunicator;

    using RentIt;

    public partial class MoviePlayerControl : RentItUserControl {
        private MovieInfo movie;

        public MoviePlayerControl() {
            InitializeComponent();
        }

        internal MovieInfo Movie {
            set {
                titleLabel.Text = value.Title;
                movie = value;
            }
        }

        internal void Start() {
            var c = new Credentials(Credentials.UserName, Credentials.HashedPassword);
            mediaPlayer.URL = BinaryCommuncator.DownloadMediaURL(c, movie.Id).ToString();
        }
    }
}
