
namespace ClientApp
{
    using System;
    using System.Collections.Generic;

    using RentIt;

    /// <author>Kenneth Søhrmann</author>
    /// <summary>
    /// This class represents the UserControl that is used whenever a publisher
    /// manages his account. Within this control the user can manage his account
    /// information, the published media items and it also provides the MediaUploadControl
    /// that the publisher uses to publish new media to the service.
    /// </summary>
    internal partial class PublisherAccountManagement : RentItUserControl
    {
        /// <summary>
        /// Initializes a new instance of the PublisherAccountManagement class.
        /// </summary>
        public PublisherAccountManagement()
        {
            InitializeComponent();

            deleteMediaButton.Enabled = false;
            changePriceButton.Enabled = false;

            // Subscribe to the inner UserControls' events in order to propagate to the MainForm's
            // subscribtions.
            List<RentItUserControl> innerControls = new List<RentItUserControl>()
                {
                    this.editAccountControl, this.mediaUploadControl
                };

            foreach (var control in innerControls)
            {
                control.CredentialsChangeEvent += this.CredentialsChangeEventPropagated;
                control.ContentChangeEvent += this.ContentChangeEventPropagated;
                control.CreditsChangeEvent += this.CreditsChangeEventPropagated;
            }

            // Add eventhandlers:
            publishedMediaList.AddSelectedIndexChangedEventHandler(this.SelectedIndexChangedHandler);
            publishedMediaList.AddLostFocusEventHandler(this.LostFocusHandler);
            publishedMediaList.AddDoubleClickEventHandler(this.DoubleClickEventHandler);
            editAccountControl.CredentialsChangeEvent += this.CredentialsChangeEventPropagated;

            priceTextBox.TextChanged += this.PriceTextBoxTextChangedEventHandler;
        }

        /// <summary>
        /// Overridden so that it sets the RentItProxy instance of the
        /// inner UserControls and components as well.
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
                this.editAccountControl.RentItProxy = this.RentItProxy;
                this.mediaUploadControl.RentItProxy = this.RentItProxy;
                this.publishedMediaList.RentItProxy = this.RentItProxy;
            }
        }

        /// <summary>
        /// Overridden so that it sets the Credentials property of the 
        /// inner UserControls and components as well.
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
                this.editAccountControl.Credentials = this.Credentials;
                this.mediaUploadControl.Credentials = this.Credentials;
                this.publishedMediaList.Credentials = this.Credentials;

                try
                {
                    PublisherAccount accountData = this.RentItProxy.GetAllPublisherData(value);
                    this.mediaUploadControl.PublisherAccount = accountData;
                    this.publishedMediaList.MediaItems = accountData.PublishedItems;
                }
                // Communication with the server failed.
                catch (Exception)
                {
                    RentItMessageBox.ServerCommunicationError();
                }
            }
        }

        /// <summary>
        /// Selects the tab with the specified index.
        /// If the index is out of range the first tab of the control is selected.
        /// </summary>
        /// <param name="index">
        /// The index value of the tab that the TabControl must switch to.
        /// Index 0 denotes the first tab.
        /// </param>
        public void SelectTab(int index)
        {
            if (index < 0 || index > this.TabControl.TabCount - 1)
            {
                index = 0;
            }

            this.TabControl.SelectTab(index);
        }


        #region EventHandlers

        /// <summary>
        /// EventHandler for the SelectedIndexChangedEvent on the list displaying
        /// published media located on the second of the three tabs. 
        /// </summary>
        private void SelectedIndexChangedHandler(object obj, EventArgs e)
        {
            // Both buttons are enabled when only one item in the list is selected.
            if (publishedMediaList.SelectedMedia.Count != 1)
            {
                deleteMediaButton.Enabled = false;
                changePriceButton.Enabled = false;
                return;
            }

            // If the single selected item is contained in the Songs group, it is
            // not possible to delete the media item; the delete button is disabled.
            priceTextBox.Enabled = true;
            changePriceButton.Enabled = false;

            var selectedMedia = publishedMediaList.GetSingleMedia();

            MediaType groupName = selectedMedia.Type;
            deleteMediaButton.Enabled = groupName != MediaType.Song;
            priceTextBox.Text = selectedMedia.Price.ToString();
        }

        /// <summary>
        /// EventHandler of the TextChangedEvent of the editable text box displaying
        /// the price of the selected published media.
        /// This method determines when it should be possible for the client to submit a 
        /// price change: This is only possible when the user types in an integer that is
        /// different from the price already registrered for the selected media.
        /// </summary>
        private void PriceTextBoxTextChangedEventHandler(object obj, EventArgs e)
        {
            MediaInfo mediaInfo;
            if ((mediaInfo = this.publishedMediaList.GetSingleMedia()) == null)
            {
                return;
            }

            int newPrice;
            try
            {
                newPrice = int.Parse(priceTextBox.Text);
            }
            catch (Exception)
            {
                changePriceButton.Enabled = false;
                return;
            }

            if (newPrice == mediaInfo.Price || newPrice < 0)
            {
                changePriceButton.Enabled = false;
                return;
            }

            changePriceButton.Enabled = true;
        }

        /// <summary>
        /// EventHandler of the LostFocusEvent of the list displaying published 
        /// media. Whenever the focus is lost, it checks if there is a single media 
        /// on the list og published media selected. If this i not the case, the 
        /// "Change price"- and "Delete media"-buttons are disabled.
        /// </summary>
        private void LostFocusHandler(object obj, EventArgs e)
        {
            // If everything but a single media is selected in the list
            if (this.publishedMediaList.GetSingleMedia() == null)
            {
                // Deactivate the buttons.
                deleteMediaButton.Enabled = false;
                changePriceButton.Enabled = false;

                priceTextBox.Enabled = false;
                priceTextBox.Text = string.Empty;
            }
        }

        /// <summary>
        /// EventHandler of the DoubleClickEvent of the list displaying
        /// published media. The double click will display either a AlbumDetails
        /// or BookMovieDetails (depending of the type of the clicked media) in
        /// the MainForm.
        /// </summary>
        private void DoubleClickEventHandler(object obj, EventArgs e)
        {
            MediaInfo mediaInfo = this.publishedMediaList.GetSingleMedia();

            if (mediaInfo == null)
            {
                return;
            }

            RentItUserControl mediaDetail;
            string title;

            switch (mediaInfo.Type)
            {
                case MediaType.Album:
                    var albumDetails = new AlbumDetails
                        {
                            RentItProxy = this.RentItProxy,
                            Credentials = this.Credentials,
                            AlbumInfo = (AlbumInfo)mediaInfo
                        };
                    mediaDetail = albumDetails;
                    title = MainForm.Titles.MediaDetailsAlbum;
                    break;
                case MediaType.Movie:
                    var movieDetails = new BookMovieDetails
                        {
                            RentItProxy = this.RentItProxy,
                            Credentials = this.Credentials,
                            MovieInfo = (MovieInfo)mediaInfo
                        };
                    mediaDetail = movieDetails;
                    title = MainForm.Titles.MediaDetailsMovie;
                    break;
                case MediaType.Book:
                    var bookDetails = new BookMovieDetails
                        {
                            RentItProxy = this.RentItProxy,
                            Credentials = this.Credentials,
                            BookInfo = (BookInfo)mediaInfo
                        };
                    mediaDetail = bookDetails;
                    title = MainForm.Titles.MediaDetailsBook;
                    break;
                default:
                    return;
            }

            this.FireContentChangeEvent(mediaDetail, title);
        }

        #endregion

        #region Controllers

        /// <summary>
        /// This method is invoked whenever the "Delete media"-button in the 
        /// tab with the list of published media is clicked.
        /// It deletes the selected media from the list of published media
        /// from the service and updates the contents of the list of published
        /// media to reflect the deletion.
        /// </summary>
        private void deleteMediaButton_Click(object sender, EventArgs e)
        {
            // Retrieve the selected media item.
            MediaInfo mediaInfo = this.publishedMediaList.GetSingleMedia();

            if (mediaInfo == null)
            {
                return;
            }

            try
            {
                // Delete the media from the service.
                RentItProxy.DeleteMedia(mediaInfo.Id, this.Credentials);
            }
            // Deletion of the media failed.
            catch (Exception)
            {
                RentItMessageBox.DeleteMediaFailed();
            }

            try
            {
                // Update the list to reflec the deletion changes.
                PublisherAccount pubAcc = this.RentItProxy.GetAllPublisherData(this.Credentials);
                this.publishedMediaList.MediaItems = pubAcc.PublishedItems;
            }
            catch (Exception)
            {
                RentItMessageBox.ServerCommunicationError();
            }
        }

        /// <summary>
        /// This methos is invoked whenever the "Change price"-button in the
        /// tab with the list of published media i clicked.
        /// It changes the price of the selected media in the list of published
        /// media by communication with the service. Then the list of published media
        /// is updated to reflect the price change.
        /// </summary>
        private void changePriceButton_Click(object sender, EventArgs e)
        {
            MediaInfo selectedMedia;
            if ((selectedMedia = this.publishedMediaList.GetSingleMedia()) == null)
            {
                return;
            }

            selectedMedia.Price = int.Parse(priceTextBox.Text);

            try
            {
                this.RentItProxy.UpdateMediaMetadata(selectedMedia, this.Credentials);
            }
            catch (Exception)
            {
                RentItMessageBox.PriceChangeFailed();
            }

            try
            {
                // Update the list to reflec the deletion changes.
                PublisherAccount pubAcc = this.RentItProxy.GetAllPublisherData(this.Credentials);
                this.publishedMediaList.MediaItems = pubAcc.PublishedItems;
            }
            catch (Exception)
            {
                RentItMessageBox.ServerCommunicationError();
            }
        }

        #endregion
    }
}
