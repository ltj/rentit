namespace ClientApp
{
    using System;
    using System.Windows.Forms;

    using BinaryCommunicator;

    using RentIt;

    /// <auhtor>Per Mortensen</auhtor>
    /// <summary>
    /// This UserControl is used for play back of songs of a rented
    /// album.
    /// </summary>
    internal partial class AlbumPlayerControl : RentItUserControl
    {
        /// <summary>
        /// The AlbumInfo-instance representing the metadata of the album
        /// currently played in this AlbumPlayerControl-instance.
        /// </summary>
        private AlbumInfo album;

        /// <summary>
        /// The AlbumDetails instance that displays the metadata of the played
        /// album. Is displayed in the MainForm whenever the user clicks the Album 
        /// details-button.
        /// </summary>
        private AlbumDetails albumDetails;

        /// <summary>
        /// Initializes a new instance of the AlbumPlayerControl class.
        /// </summary>
        public AlbumPlayerControl()
        {
            InitializeComponent();
            this.songList.AddDoubleClickEventHandler(this.DoubleClickEventHandler);
        }

        /// <summary>
        /// Sets the AlbumInfo-object that the album to be displayed in the
        /// UserControl is based upon.
        /// </summary>
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

        #region EventHandlers

        /// <author>Kenneth Søhrmann</author>
        /// <summary>
        /// EventHandler for the Album details-button. Loads an AlbumDetails-instance
        /// into the MainForm displaying the metadata of the album.
        /// </summary>
        private void viewAlbumDetails_Click(object sender, System.EventArgs e)
        {
            Cursor.Current = Cursors.WaitCursor;
            if (this.albumDetails == null)
            {
                this.albumDetails = new AlbumDetails()
                    {
                        RentItProxy = this.RentItProxy,
                        Credentials = this.Credentials,
                        AlbumInfo = this.album
                    };
            }

            this.FireContentChangeEvent(albumDetails, MainForm.Titles.MediaDetailsAlbum);
            Cursor.Current = Cursors.Default;
        }

        /// <author>Kenneth Søhrmann</author>
        /// <summary>
        /// When the user double clicks a song in the list, this handler
        /// will start playing the song in the media player.
        /// </summary>
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
