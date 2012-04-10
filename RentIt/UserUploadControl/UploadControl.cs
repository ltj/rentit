﻿
namespace PropertyGridTest
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel;
    using System.Data;
    using System.Drawing;
    using System.Linq;
    using System.Text;
    using System.Windows.Forms;

    using BinaryCommunicator;

    using RentIt;

    public partial class UploadControl : Form
    {
        // The current book instance to upload.
        private BookInfoUpload bookInfo = new BookInfoUpload();
        private AlbumInfoUpload albumInfo = new AlbumInfoUpload();
        private MovieInfoUpload movieInfo = new MovieInfoUpload();

        // Used for mapping from SongInfoUpload to ListViesItem
        private Dictionary<SongInfoUpload, ListViewItem> dic = new Dictionary<SongInfoUpload, ListViewItem>();

        // Used for mapping from ListViewItem to SongInfoUpload
        private Dictionary<ListViewItem, SongInfoUpload> dic2 = new Dictionary<ListViewItem, SongInfoUpload>();

        public UploadControl()
        {
            InitializeComponent();

            // Initialize selection.
            comboBox1.SelectedIndex = 0;
            mediaPropertyGrid.SelectedObject = bookInfo;

            this.Size = new System.Drawing.Size(400, 543);

            // Add the event handlers
            songListView.SelectedIndexChanged += this.ListViewSelectionChanged;
            songPropertyGrid.PropertyValueChanged += this.SongPropertyGrid_PropertyValueChanged;
            comboBox1.SelectedValueChanged += this.ComboBox1_SelectedValueChanged;

            BinaryCommuncator.FileUploadedEvent += this.UploadLabel_FileUploaded;
        }

        #region Controllers

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

        private void deleteAllSongsButton_Click(object sender, EventArgs e)
        {
            songPropertyGrid.SelectedObject = null;
            songListView.Items.Clear();
        }

        private void deleteSelectedSongsButton_Click(object sender, EventArgs e)
        {
            foreach (ListViewItem songItem in songListView.SelectedItems)
            {
                songListView.Items.Remove(songItem);
            }

            songPropertyGrid.SelectedObject = null;
        }

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

        // Uses the SelectedItems property to retrieve and tally the price 
        // of the selected menu items.
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

        private void SongPropertyGrid_PropertyValueChanged(Object sender, PropertyValueChangedEventArgs e)
        {
            SongInfoUpload cu = (SongInfoUpload)songPropertyGrid.SelectedObject;
            ListViewItem item = dic[cu];

            item.SubItems[0].Text = cu.Title;
        }

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
                this.Size = new System.Drawing.Size(818, 543);
                songsGroupBox.Visible = true;
            }
            else
            {
                this.Size = new System.Drawing.Size(400, 543);
                songsGroupBox.Visible = false;
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

        private void uploadButton_Click(object sender, EventArgs e)
        {
            if (comboBox1.SelectedItem.ToString().Equals("Movie"))
            {
                BinaryCommuncator.UploadMovie(new AccountCredentials(), this.movieInfo.File, movieInfo);
            }
        }
    }
}
