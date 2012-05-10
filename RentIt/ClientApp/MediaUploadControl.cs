namespace ClientApp
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Windows.Forms;

    using BinaryCommunicator;

    using RentIt;

    public partial class MediaUploadControl : RentItUserControl
    {
        /// <summary>
        /// The current book instance to upload.
        /// </summary>
        private BookInfoUpload bookInfo = new BookInfoUpload();

        /// <summary>
        /// The current movie instance to upload.
        /// </summary>
        private MovieInfoUpload movieInfo = new MovieInfoUpload();

        /// <summary>
        /// The current album instance to upload.
        /// </summary>
        private AlbumInfoUpload albumInfo = new AlbumInfoUpload();

        /// <summary>
        /// Used for mapping from SongInfoUpload to ListViesItem.
        /// </summary>
        private Dictionary<SongInfoUpload, ListViewItem> dic = new Dictionary<SongInfoUpload, ListViewItem>();

        /// <summary>
        /// Used for mapping from ListViewItem to SongInfoUpload.
        /// </summary>
        private Dictionary<ListViewItem, SongInfoUpload> dic2 = new Dictionary<ListViewItem, SongInfoUpload>();

        private PublisherCredentials publisherCredentials;

        public MediaUploadControl()
        {
            InitializeComponent();

            // Initialize selection.
            comboBox1.SelectedIndex = 0;
            mediaPropertyGrid.SelectedObject = bookInfo;
            songsGroupBox.Enabled = false;

            // Add the event handlers
            songListView.SelectedIndexChanged += this.ListViewSelectionChanged;
            songPropertyGrid.PropertyValueChanged += this.SongPropertyGrid_PropertyValueChanged;
            comboBox1.SelectedValueChanged += this.ComboBox1_SelectedValueChanged;

            BinaryCommuncator.FileUploadedEvent += this.UploadLabel_FileUploaded;

            this.publisherCredentials = new PublisherCredentials("publishCorp", "7110EDA4D09E062AA5E4A390B0A572AC0D2C0220");
        }

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
                    new PublisherCredentials(this.Credentials.UserName, this.Credentials.HashedPassword);
            }
        }

        #region Controllers

        /// <author>Kenneth Søhrmann</author>
        /// <summary>
        /// This method is invoked whenever the Upload-button on the form 
        /// has been clicked.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
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
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void newSongButton_Click(object sender, EventArgs e)
        {
            SongInfoUpload cu = new SongInfoUpload();
            songPropertyGrid.SelectedObject = cu;

            ListViewItem item = new ListViewItem("<name>");
            item.SubItems.Add("<email>");

            this.dic.Add(cu, item);
            this.dic2.Add(item, cu);

            songListView.Items.Add(item);
        }

        /// <auhtor>Kenneth Søhrmann</auhtor>
        /// <summary>
        /// 
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void deleteAllSongsButton_Click(object sender, EventArgs e)
        {
            songPropertyGrid.SelectedObject = null;
            songListView.Items.Clear();
        }

        /// <author>Kenneth Søhrmann</author>
        /// <summary>
        /// 
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
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
        /// 
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void discardButton_Click(object sender, EventArgs e)
        {
            if (comboBox1.SelectedItem.ToString().Equals("Album"))
            {
                this.albumInfo = new AlbumInfoUpload();

                mediaPropertyGrid.SelectedObject = albumInfo;

                songPropertyGrid.SelectedObject = null;
                songListView.Items.Clear();
            }
            if (comboBox1.SelectedItem.ToString().Equals("Book"))
            {
                this.bookInfo = new BookInfoUpload();
                mediaPropertyGrid.SelectedObject = bookInfo;
            }
            if (comboBox1.SelectedItem.ToString().Equals("Movie"))
            {
                this.movieInfo = new MovieInfoUpload();
                mediaPropertyGrid.SelectedObject = movieInfo;
            }
        }

        #endregion Controllers

        #region EventHandlers

        /// <author>Kenneth Søhrmann</author>
        /// <summary>
        /// Eventhandler of the event that is fired whenever the 
        /// selection in the ListView has changed.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
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
        /// 
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
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
        /// 
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
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
                // this.Size = new System.Drawing.Size(818, 570);
                // songsGroupBox.Visible = true;
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
