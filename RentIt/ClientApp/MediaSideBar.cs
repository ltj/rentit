
namespace ClientApp
{
    using System;
    using System.Windows.Forms;

    using BinaryCommunicator;

    using RentIt;

    /// <author>Kenneth Søhrmann</author>
    /// <summary>
    /// 
    /// </summary>
    public partial class MediaSideBar : RentItUserControl
    {
        /// <summary>
        /// The MediaInfo instance which metadata is being displayed
        /// on the MediaSideBar.
        /// </summary>
        private MediaInfo mediaInfo;

        /// <summary>
        /// Initializes a new instance of the MediaSideBar class.
        /// </summary>
        public MediaSideBar()
        {
            InitializeComponent();
        }

        /// <summary>
        /// Sets the control to display the metadata of the submitted
        /// MediaInfo object.
        /// </summary>
        internal MediaInfo MediaInfoData
        {
            set
            {
                var mediaInfo = value;

                this.mediaInfo = mediaInfo;

                this.priceLabel.Text = mediaInfo.Price + " credits.";
                this.genreValueLabel.Text = mediaInfo.Genre;
                this.releaseDateValueLabel.Text = mediaInfo.ReleaseDate.ToShortDateString();
                this.publisherValueLabel.Text = mediaInfo.Publisher;

                this.originLabel.Visible = true;
                this.originValueLabel.Visible = true;
                this.lengthLabel.Visible = true;
                this.lengthValueLabel.Visible = true;

                switch (mediaInfo.Type)
                {
                    case MediaType.Album:
                        var albumInfo = (AlbumInfo)mediaInfo;
                        this.originLabel.Text = "Album artist:";
                        this.originValueLabel.Text = albumInfo.AlbumArtist;
                        this.lengthLabel.Text = "Total duration:";
                        this.lengthValueLabel.Text = albumInfo.TotalDuration.ToString();
                        break;
                    case MediaType.Book:
                        var bookInfo = (BookInfo)mediaInfo;
                        this.originLabel.Text = "Author:";
                        this.originValueLabel.Text = bookInfo.Author;
                        this.lengthLabel.Text = "Number of pages:";
                        this.lengthValueLabel.Text = bookInfo.Pages.ToString();
                        break;
                    case MediaType.Song:
                        var songInfo = (SongInfo)mediaInfo;
                        this.originLabel.Text = "Artist";
                        this.originValueLabel.Text = songInfo.Artist;
                        this.lengthLabel.Text = "Song duration:";
                        this.lengthValueLabel.Text = songInfo.Duration.ToString();
                        break;
                    case MediaType.Movie:
                        var movieInfo = (MovieInfo)mediaInfo;
                        this.originLabel.Text = "Director";
                        this.originValueLabel.Text = movieInfo.Director;
                        this.lengthLabel.Text = "Duration:";
                        this.lengthValueLabel.Text = movieInfo.Duration.ToString();
                        break;
                    default:
                        this.originLabel.Visible = false;
                        this.originValueLabel.Visible = false;
                        this.lengthLabel.Visible = false;
                        this.lengthValueLabel.Visible = false;
                        break;
                }

                this.thumbnailBox.Image = BinaryCommuncator.GetThumbnail(mediaInfo.Id);
            }
        }

        #region Controllers

        /// <summary>
        /// Controller for the Rent-button.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void rentButton_Click(object sender, System.EventArgs e)
        {
            if (this.Credentials == null)
            {
                RentItMessageBox.NotLoggedIn();
            }
            else
            {
                try
                {
                    this.RentItProxy.RentMedia(this.mediaInfo.Id, this.Credentials);
                }
                catch (Exception)
                {
                    RentItMessageBox.ServerCommunicationFailure();
                }
            }
        }

        #endregion
    }
}
