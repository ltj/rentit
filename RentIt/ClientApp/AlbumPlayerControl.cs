namespace ClientApp
{
    using System;

    using BinaryCommunicator;

    using RentIt;

    internal partial class AlbumPlayerControl : RentItUserControl
    {
        private AlbumInfo album;

        private AlbumDetails albumDetails;

        public AlbumPlayerControl()
        {
            InitializeComponent();
            this.songList.AddDoubleClickEventHandler(this.DoubleClickEventHandler);
        }

        internal AlbumInfo Album
        {
            set
            {
                titleLabel.Text = value.Title;
                this.album = value;
                this.songList.MediaItems = new MediaItems()
                    {
                        Songs = this.album.Songs,
                        Albums = new AlbumInfo[] { },
                        Books = new BookInfo[] { },
                        Movies = new MovieInfo[] { }
                    };
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

            this.FireContentChangeEvent(albumDetails, TopBarControl.Titles.MediaDetailsAlbum);
        }

        #endregion

        #region EventHandler

        /// <summary>
        /// When the user double clicks a song in the list, this handler
        /// will start playing the song in the media player.
        /// </summary>
        /// <param name="obj"></param>
        /// <param name="e"></param>
        private void DoubleClickEventHandler(object obj, EventArgs e)
        {
            MediaInfo mediaInfo = this.songList.GetSingleMedia();

            if (mediaInfo == null)
            {
                return;
            }

            var credentials = new Credentials(Credentials.UserName, Credentials.HashedPassword);
            this.mediaPlayer.URL =
                BinaryCommuncator.DownloadMediaURL(credentials, mediaInfo.Id).ToString();
        }

        #endregion
    }
}
