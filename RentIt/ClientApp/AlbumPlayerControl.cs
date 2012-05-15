namespace ClientApp
{
    using BinaryCommunicator;

    using RentIt;

    internal partial class AlbumPlayerControl : RentItUserControl
    {
        private AlbumInfo album;

        private AlbumDetails albumDetails;

        public AlbumPlayerControl()
        {
            InitializeComponent();
        }

        internal AlbumInfo Album
        {
            set
            {
                titleLabel.Text = value.Title;
                album = value;
            }
        }

        internal void Start()
        {
            var c = new Credentials(Credentials.UserName, Credentials.HashedPassword);
            mediaPlayer.URL = BinaryCommuncator.DownloadMediaURL(c, album.Id).ToString();
            //todo: event handlers for the user choosing songs, and then play them
        }

        #region Controllers

        private void viewAlbumDetails_Click(object sender, System.EventArgs e)
        {
            if (this.albumDetails == null)
            {
                this.albumDetails = new AlbumDetails()
                    {
                        RentItProxy = this.RentItProxy,
                        Credentials = this.Credentials,
                        AlbumInfo = this.album
                    };
            }

            this.FireContentChangeEvent(albumDetails, this.album.Title);
        }

        #endregion
    }
}
