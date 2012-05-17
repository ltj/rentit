
namespace ClientApp
{
    using System;
    using System.Collections.Generic;
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

            // Subscribe the propagated EventHandlers to the control's subcontrols
            List<RentItUserControl> innerControls = new List<RentItUserControl>()
                {
                    this.alsoRentedList, this.mediaSideBar, this.albumRatingList
                };

            foreach (var control in innerControls)
            {
                control.CredentialsChangeEvent += this.CredentialsChangeEventPropagated;
                control.ContentChangeEvent += this.ContentChangeEventPropagated;
                control.CreditsChangeEvent += this.CreditsChangeEventPropagated;
            }

            this.songList.Resize += this.ListResizedEventHandler;
        }

        /// <summary>
        /// Overridden so that it sets the RentItProxy instance of the
        /// MediaSideBar instance as well.
        /// </summary>
        internal override RentItClient RentItProxy
        {
            get
            {
                return base.RentItProxy;
            }
            set
            {
                base.RentItProxy = value;
                this.alsoRentedList.RentItProxy = this.RentItProxy;
                this.mediaSideBar.RentItProxy = this.RentItProxy;
                this.albumRatingList.RentItProxy = this.RentItProxy;
            }
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
                this.alsoRentedList.Credentials = value;
                this.mediaSideBar.Credentials = value;
                this.albumRatingList.Credentials = value;
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

                //album = RentItProxy.GetAlbumInfo(78); // Just for debugging, has to be deleted.

                this.alsoRentedList.PrioritizedMediaType = album.Type;
                this.alsoRentedList.MediaId = album.Id;

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

        #region EventHandler

        private void ListResizedEventHandler(object obj, EventArgs e)
        {
            int totalWidth = 0;

            // Get the total width of all the columns of the table.
            foreach (ColumnHeader column in this.songList.Columns)
            {
                totalWidth += column.Width;
            }

            // Adjust the width so that the ratio of the column width will remain the same.
            foreach (ColumnHeader column in this.songList.Columns)
            {
                column.Width = (int)(((double)column.Width / totalWidth) * this.songList.Width);
            }
        }

        #endregion
    }
}
