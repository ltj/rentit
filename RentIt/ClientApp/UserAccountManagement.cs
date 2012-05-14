using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace ClientApp
{
    using RentIt;

    /// <summary>
    /// A user control containing user controls relevant to user account management. 
    /// </summary>
    public partial class UserAccountManagement : RentItUserControl
    {
        public UserAccountManagement()
        {
            InitializeComponent();

            this.rentalsListControl.AddDoubleClickEventHandler(this.DoubleClickEventHandler);
        }

        /// <summary>
        /// Selects the tab with the specified index.
        /// If the index is out of range that first tab
        /// of the control is selected.
        /// </summary>
        /// <param name="index"></param>
        public void SelectTab(int index)
        {
            if (index < 0 || index > this.tabControl.TabCount - 1)
            {
                index = 0;
            }

            this.tabControl.SelectTab(index);
        }

        #region EventHandlers

        /// <summary>
        /// EventHandler of the DoubleClick event when it is fired from the 
        /// list containing the rentals.
        /// </summary>
        /// <param name="obj"></param>
        /// <param name="e"></param>
        private void DoubleClickEventHandler(object obj, EventArgs e)
        {
            MediaInfo mediaInfo = this.rentalsListControl.SelectedItem;
            var mediaDisplay = new MediaDisplayForm();

            switch (mediaInfo.Type)
            {
                case MediaType.Book:
                    var bookReader = new BookReaderControl();
                    bookReader.Credentials = this.Credentials;
                    bookReader.Book = (BookInfo)mediaInfo;
                    bookReader.Start();

                    // Propagate the ContentChangeEventHandler of the MainForm to the BookReader.
                    bookReader.ContentChangeEvent += this.ContentChangeEventPropagated;

                    mediaDisplay.Content = bookReader;
                    break;
                case MediaType.Movie:
                    var moviePlayer = new MoviePlayerControl();
                    moviePlayer.Credentials = this.Credentials;
                    moviePlayer.Movie = (MovieInfo)mediaInfo;
                    moviePlayer.Start();

                    // Propagate the ContentChangeEventHandler of the MainForm to the MoviePlayer.
                    moviePlayer.ContentChangeEvent += this.ContentChangeEventPropagated;

                    mediaDisplay.Content = moviePlayer;
                    break;
                case MediaType.Album:
                    var albumPlayer = new AlbumPlayerControl();
                    albumPlayer.Credentials = this.Credentials;
                    albumPlayer.Album = (AlbumInfo)mediaInfo;
                    albumPlayer.Start();

                    // Propagate the ContentChangeEventHandler of the MainForm to the AlbumPlayer.
                    albumPlayer.ContentChangeEvent += this.ContentChangeEventPropagated;

                    mediaDisplay.Content = albumPlayer;
                    break;
                default:
                    mediaDisplay.Dispose();
                    return;
            }

            mediaDisplay.Show();
        }

        #endregion

        /// <summary>
        /// For propagating the MainForms subscription to the ContentChangeEvent
        /// to the media players.
        /// </summary>
        /// <param name="obj"></param>
        /// <param name="e"></param>
        private void ContentChangeEventPropagated(object obj, ContentChangeArgs e)
        {
            this.FireContentChangeEvent(e.NewControl, e.NewTitle);
        }
    }
}
