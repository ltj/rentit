using System;
using System.Collections.Generic;
using System.ComponentModel;


namespace ClientApp
{
    using RentIt;

    /// <summary>
    /// A user control containing user controls relevant to user account management. 
    /// </summary>
    internal partial class UserAccountManagement : RentItUserControl
    {
        public UserAccountManagement()
        {
            InitializeComponent();

            // Subscribe the propagated EventHandlers to the control's subcontrols
            List<RentItUserControl> innerControls = new List<RentItUserControl>()
                {
                    this.editAccount1, this.rentalsListControl, this.creditsControl1
                };

            foreach (var control in innerControls)
            {
                control.CredentialsChangeEvent += this.CredentialsChangeEventPropagated;
                control.ContentChangeEvent += this.ContentChangeEventPropagated;
                control.CreditsChangeEvent += this.CreditsChangeEventPropagated;
            }


            this.rentalsListControl.AddDoubleClickEventHandler(this.DoubleClickEventHandler);
            this.editAccount1.CredentialsChangeEvent += this.CredentialsChangeEventPropagated;
        }

        internal override RentItClient RentItProxy
        {
            set
            {
                base.RentItProxy = value;
                editAccount1.RentItProxy = value;
                rentalsListControl.RentItProxy = value;
                creditsControl1.RentItProxy = value;
            }
        }

        internal override AccountCredentials Credentials
        {
            set
            {
                base.Credentials = value;
                editAccount1.Credentials = value;
                rentalsListControl.Credentials = value;
                creditsControl1.Credentials = value;

                UserAccount accountData = RentItProxy.GetAllCustomerData(value);
                rentalsListControl.Rentals = new List<Rental>(accountData.Rentals);
            }
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
                    bookReader.RentItProxy = this.RentItProxy;
                    bookReader.Credentials = this.Credentials;
                    bookReader.Book = (BookInfo)mediaInfo;
                    bookReader.Start();

                    // Propagate the ContentChangeEventHandler of the MainForm to the BookReader.
                    bookReader.ContentChangeEvent += this.ContentChangeEventPropagated;

                    mediaDisplay.Content = bookReader;
                    break;
                case MediaType.Movie:
                    var moviePlayer = new MoviePlayerControl();
                    moviePlayer.RentItProxy = this.RentItProxy;
                    moviePlayer.Credentials = this.Credentials;
                    moviePlayer.Movie = (MovieInfo)mediaInfo;
                    moviePlayer.Start();

                    // Propagate the ContentChangeEventHandler of the MainForm to the MoviePlayer.
                    moviePlayer.ContentChangeEvent += this.ContentChangeEventPropagated;

                    mediaDisplay.Content = moviePlayer;
                    break;
                case MediaType.Album:
                    var albumPlayer = new AlbumPlayerControl();
                    albumPlayer.RentItProxy = this.RentItProxy;
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

    }
}
