
namespace ClientApp
{
    using System;
    using System.Collections.Generic;
    using System.Windows.Forms;

    using RentIt;

    /// <author>Jacob Rasmussen</author>
    /// <summary>
    /// A user control containing user controls relevant to user account management. 
    /// </summary>
    internal partial class UserAccountManagement : RentItUserControl
    {
        /// <summary>
        /// Initializes a new instance of the UserAccountManagement class.
        /// </summary>
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

        /// <summary>
        /// Overridden so that it sets the RentItProxy instance of the
        /// inner UserControls and components as well.
        /// </summary>
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

        /// <summary>
        /// Overridden so that it sets the Credentials property of the 
        /// inner UserControls and components as well.
        /// </summary>
        internal override AccountCredentials Credentials
        {
            set
            {
                base.Credentials = value;
                editAccount1.Credentials = value;
                rentalsListControl.Credentials = value;
                creditsControl1.Credentials = value;

                UserAccount accountData = this.RentItProxy.GetAllCustomerData(value);
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
        /// EventHandler of the DoubleClickEvent of the RentalsList
        /// containing the rentals.
        /// </summary>
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

        /// <summary>
        /// This method is invoked whenever the "Delete account"-button is
        /// clicked. It deletes the account from the service, and changes
        /// the displayed screen.
        /// </summary>
        private void DeleteAccountButtonClick(object sender, EventArgs e)
        {
            if (!RentItMessageBox.AccountDeletionConfirmation())
                return;

            Cursor.Current = Cursors.WaitCursor;
            try
            {
                RentItProxy.DeleteAccount(Credentials);
                RentItMessageBox.AccountDeletionSucceeded();
                FireCredentialsChangeEvent(null); //TODO: er dette rigtigt?
            }
            catch
            {
                RentItMessageBox.AccountDeletionFailed();
            }
            Cursor.Current = Cursors.Default;
        }

        #endregion
    }
}
