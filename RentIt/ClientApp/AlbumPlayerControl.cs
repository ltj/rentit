namespace ClientApp {
    using BinaryCommunicator;

    using RentIt;

    public partial class AlbumPlayerControl : RentItUserControl {
        private AlbumInfo album;

        public AlbumPlayerControl() {
            InitializeComponent();
        }

        internal AlbumInfo Album {
            set {
                titleLabel.Text = value.Title;
                album = value;
            }
        }

        internal void Start() {
            var c = new Credentials(Credentials.UserName, Credentials.HashedPassword);
            mediaPlayer.URL = BinaryCommuncator.DownloadMediaURL(c, album.Id).ToString();
            //todo: event handlers for the user choosing songs, and then play them
        }
    }
}
