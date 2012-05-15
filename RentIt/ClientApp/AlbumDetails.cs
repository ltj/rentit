
namespace ClientApp
{
    using System.ServiceModel;
    using System.Windows.Forms;

    using RentIt;

    /// <author>Kenneth Søhrmann</author>
    /// <summary>
    /// 
    /// </summary>
    internal partial class AlbumDetails : RentItUserControl
    {
        /// <summary>
        /// Initializes a new instance of the AlbumDetails class.
        /// To load the control with data, use the AlbumInfo property.
        /// </summary>
        public AlbumDetails()
        {
            InitializeComponent();

            BasicHttpBinding binding = new BasicHttpBinding();
            EndpointAddress address = new EndpointAddress("http://rentit.itu.dk/rentit01/RentItService.svc");
            RentItProxy = new RentItClient(binding, address);

            // rest of contructor has to be deleted.
            AlbumInfo album = RentItProxy.GetAlbumInfo(78);

            this.mediaSideBar.MediaInfoData = album;

            this.albumTitleLabel.Text = album.Title;
            this.albumArtistLabel.Text = album.AlbumArtist;

            this.albumDurationValueLabel.Text = album.TotalDuration.ToString();

            this.albumDescriptionTextBox.Text = album.Description;
            this.PopulateSongList(album);

            this.albumRatingList.Media = album;
        }

        /// <summary>
        /// Overridden so that is sets the Credentials property of the 
        /// MediaSideBar instance as well.
        /// </summary>
        internal override AccountCredentials Credentials
        {
            get
            {
                return base.Credentials;
            }
            set
            {
                base.Credentials = value;
                this.mediaSideBar.Credentials = value;
            }
        }

        /// <summary>
        /// Sets the AlbumInfo instance whose metadata is to be
        /// displayed in the AlbumDetails control.
        /// </summary>
        internal AlbumInfo AlbumInfo
        {
            set
            {
                AlbumInfo album = value;

                album = RentItProxy.GetAlbumInfo(78); // Just for debugging, has to be deleted.

                this.mediaSideBar.MediaInfoData = album;

                this.albumTitleLabel.Text = album.Title;
                this.albumArtistLabel.Text = album.AlbumArtist;
                this.albumDurationValueLabel.Text = album.TotalDuration.ToString();
                this.albumDescriptionTextBox.Text = album.Description;

                this.PopulateSongList(album);
                this.albumRatingList.Media = album;
            }
        }

        /// <summary>
        /// Helper method for populating the song list with songs.
        /// </summary>
        /// <param name="info"></param>
        private void PopulateSongList(AlbumInfo info)
        {
            foreach (var song in info.Songs)
            {
                var listItem = new ListViewItem(song.Title);
                listItem.SubItems.Add(song.Artist);
                listItem.SubItems.Add(song.Genre);
                listItem.SubItems.Add(song.Price.ToString());
                listItem.SubItems.Add((song.Duration.ToString()));
                songList.Items.Add(listItem);
            }
        }

    }
}
