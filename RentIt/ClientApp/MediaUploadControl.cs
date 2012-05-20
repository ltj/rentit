namespace ClientApp
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Windows.Forms;

    using BinaryCommunicator;

    using RentIt;

    /// <author>Kenneth Søhrmann</author>
    /// <summary>
    /// This class represents the UserControl that allows publishers to submit
    /// and upload new media to the service.
    /// </summary>
    internal partial class MediaUploadControl : RentItUserControl
    {
        private PublisherAccount publisherAccount;

        /// <summary>
        /// The current book instance to upload.
        /// </summary>
        private BookInfoUpload bookInfo;

        /// <summary>
        /// The current movie instance to upload.
        /// </summary>
        private MovieInfoUpload movieInfo;

        /// <summary>
        /// The current album instance to upload.
        /// </summary>
        private AlbumInfoUpload albumInfo;

        /// <summary>
        /// Used for mapping from SongInfoUpload to ListViesItem.
        /// </summary>
        private Dictionary<SongInfoUpload, ListViewItem> dic = new Dictionary<SongInfoUpload, ListViewItem>();

        /// <summary>
        /// Used for mapping from ListViewItem to SongInfoUpload.
        /// </summary>
        private Dictionary<ListViewItem, SongInfoUpload> dic2 = new Dictionary<ListViewItem, SongInfoUpload>();

        private Credentials publisherCredentials;

        /// <summary>
        /// Initializes a new instance of the MediaUploadControl class.
        /// </summary>
        public MediaUploadControl()
        {
            InitializeComponent();

            // Initialize upload media
            this.bookInfo = new BookInfoUpload();
            this.movieInfo = new MovieInfoUpload();
            this.albumInfo = new AlbumInfoUpload();

            // Initialize selection.
            comboBox1.SelectedIndex = 0;
            mediaPropertyGrid.SelectedObject = bookInfo;
            songsGroupBox.Enabled = false;

            // Add the event handlers
            songListView.SelectedIndexChanged += this.ListViewSelectionChanged;
            songPropertyGrid.PropertyValueChanged += this.SongPropertyGrid_PropertyValueChanged;
            comboBox1.SelectedValueChanged += this.ComboBox1_SelectedValueChanged;

            BinaryCommuncator.FileUploadedEvent += this.UploadLabel_FileUploaded;

            this.publisherCredentials = new Credentials("publishCorp", "7110EDA4D09E062AA5E4A390B0A572AC0D2C0220");
        }

        /// <summary>
        /// Override the Credentials property to set the publiserCredentials used
        /// for BinaryCommunicator method invocation.
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
                this.publisherCredentials =
                    new Credentials(this.Credentials.UserName, this.Credentials.HashedPassword);
            }
        }


        /// <summary>
        /// Auto property for getting and setting the PublisherAccount.
        /// The setter also sets the publisher of the currently displayed
        /// book, movie and album items currently displayed in the property grid.
        /// of the upload control.
        /// </summary>
        internal PublisherAccount PublisherAccount
        {
            get
            {
                return this.publisherAccount;
            }
            set
            {
                this.publisherAccount = value;
                this.bookInfo.Publisher = this.PublisherAccount.PublisherName;
                this.movieInfo.Publisher = this.PublisherAccount.PublisherName;
                this.albumInfo.Publisher = this.PublisherAccount.PublisherName;
            }
        }

        #region Controllers

        /// <author>Kenneth Søhrmann</author>
        /// <summary>
        /// This method is invoked whenever the Upload-button on the form 
        /// has been clicked. This uploads the metadata as well as the specifed media file
        /// to the service.
        /// </summary>
        private void uploadButton_Click(object sender, EventArgs e)
        {
            uploadLabel.Text = "Begun uploading...";
            uploadLabel.Refresh();

            if (comboBox1.SelectedItem.ToString().Equals("Movie"))
            {
                BinaryCommuncator.UploadMovie(this.publisherCredentials, this.movieInfo);
                uploadLabel.Text = "Upload of movie done!";
            }
            else if (comboBox1.SelectedItem.ToString().Equals("Album"))
            {
                BinaryCommuncator.UploadAlbum(this.publisherCredentials, this.dic.Keys.ToList(), this.albumInfo);
                uploadLabel.Text = "Upload of album done!";
            }
            else if (comboBox1.SelectedItem.ToString().Equals("Book"))
            {
                BinaryCommuncator.UploadBook(this.publisherCredentials, this.bookInfo);
                uploadLabel.Text = "Upload of book done!";
            }
        }

        /// <author>Kenneth Søhrmann</author>
        /// <summary>
        /// This mehod is invoked whenenver the "New Song"-button on the form
        /// is clicked. It creates a new SongInfoUpload instance.
        /// </summary>
        private void newSongButton_Click(object sender, EventArgs e)
        {
            var cu = new SongInfoUpload();
            cu.Publisher = this.PublisherAccount.PublisherName;

            songPropertyGrid.SelectedObject = cu;

            var item = new ListViewItem("<name>");
            item.SubItems.Add("<email>");

            this.dic.Add(cu, item);
            this.dic2.Add(item, cu);

            songListView.Items.Add(item);
        }

        /// <auhtor>Kenneth Søhrmann</auhtor>
        /// <summary>
        /// This method is invoked whenever the "Delete Songs"-button on the form
        /// is clicked. It deletes all the songs in the song list.
        /// </summary>
        private void deleteAllSongsButton_Click(object sender, EventArgs e)
        {
            songPropertyGrid.SelectedObject = null;
            songListView.Items.Clear();
        }

        /// <author>Kenneth Søhrmann</author>
        /// <summary>
        /// This method is invoked whenever the "Deleted selected songs"-button on the
        /// form is clicked. It deletes all the selected songs in the song list.
        /// </summary>
        private void deleteSelectedSongsButton_Click(object sender, EventArgs e)
        {
            foreach (ListViewItem songItem in songListView.SelectedItems)
            {
                songListView.Items.Remove(songItem);
            }

            songPropertyGrid.SelectedObject = null;
        }

        /// <author>Kenneth Søhrmann</author>
        /// <summary>
        /// This method is invoked whenever the "Discard"-button on the form i clicked.
        /// It resets the currently displayed media object.
        /// </summary>
        private void discardButton_Click(object sender, EventArgs e)
        {
            if (comboBox1.SelectedItem.ToString().Equals("Album"))
            {
                this.albumInfo = new AlbumInfoUpload();
                this.albumInfo.Publisher = this.PublisherAccount.PublisherName;

                this.mediaPropertyGrid.SelectedObject = this.albumInfo;

                songPropertyGrid.SelectedObject = null;
                songListView.Items.Clear();
            }
            if (comboBox1.SelectedItem.ToString().Equals("Book"))
            {
                this.bookInfo = new BookInfoUpload();
                this.bookInfo.Publisher = this.PublisherAccount.PublisherName;
                this.mediaPropertyGrid.SelectedObject = this.bookInfo;
            }
            if (comboBox1.SelectedItem.ToString().Equals("Movie"))
            {
                this.movieInfo = new MovieInfoUpload();
                this.movieInfo.Publisher = this.PublisherAccount.PublisherName;
                this.mediaPropertyGrid.SelectedObject = this.movieInfo;
            }
        }

        #endregion Controllers

        #region EventHandlers

        /// <author>Kenneth Søhrmann</author>
        /// <summary>
        /// EventHandler of the event that is fired whenever the 
        /// selection in the song list has changed. Gets the currently selected
        /// item an displays its properties in the property grid.
        /// </summary>
        private void ListViewSelectionChanged(object sender, System.EventArgs e)
        {
            ListView.SelectedListViewItemCollection selectedItems =
                this.songListView.SelectedItems;

            if (selectedItems.Count == 0 || selectedItems.Count > 1)
            {
                songPropertyGrid.SelectedObject = null;
                return;
            }

            songPropertyGrid.SelectedObject = dic2[selectedItems[0]];
        }

        /// <author>Kenneth Søhrmann</author>
        /// <summary>
        /// EventHandler of the PropertyValueChangedEvent on the PropertyGrid display the
        /// properties of a song. It ensures that the title shown in the song list
        /// corresponds to the title of the song currently displayed in the songPropertyGrid.
        /// </summary>
        private void SongPropertyGrid_PropertyValueChanged(Object sender, PropertyValueChangedEventArgs e)
        {
            SongInfoUpload cu = (SongInfoUpload)songPropertyGrid.SelectedObject;
            ListViewItem item = dic[cu];

            if (!string.IsNullOrEmpty(cu.Title))
            {
                item.SubItems[0].Text = cu.Title;
            }
        }

        /// <author>Kenneth Søhrmann</author>
        /// <summary>
        /// EventHandler of the SelectedValueChangedEvent of the combobox containing the 
        /// different media type that the publisher can upload.
        /// </summary>
        private void ComboBox1_SelectedValueChanged(object sender, EventArgs e)
        {
            if (comboBox1.SelectedIndex == -1)
            {
                return;
            }
            if (comboBox1.SelectedItem.ToString().Equals("Book"))
            {
                mediaPropertyGrid.SelectedObject = bookInfo;
            }
            if (comboBox1.SelectedItem.ToString().Equals("Album"))
            {
                mediaPropertyGrid.SelectedObject = albumInfo;
                songsGroupBox.Enabled = true;
            }
            else
            {
                songsGroupBox.Enabled = false;
            }
            if (comboBox1.SelectedItem.ToString().Equals("Movie"))
            {
                mediaPropertyGrid.SelectedObject = movieInfo;
            }
        }

        private void UploadLabel_FileUploaded()
        {
            uploadLabel.Text = "File uploaded";
        }

        #endregion EventHandlers
    }
}
