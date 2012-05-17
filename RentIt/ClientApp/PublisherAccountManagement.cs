﻿namespace ClientApp
{
    using System;
    using System.Collections.Generic;
    using System.ServiceModel;

    using RentIt;

    internal partial class PublisherAccountManagement : RentItUserControl
    {

        public PublisherAccountManagement()
        {
            InitializeComponent();

            deleteMediaButton.Enabled = false;
            changePriceButton.Enabled = false;

            // Subscribe the propagated EventHandlers to the control's subcontrols
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
            /*
            BasicHttpBinding binding = new BasicHttpBinding();
            EndpointAddress address = new EndpointAddress("http://rentit.itu.dk/rentit01/RentItService.svc");
            this.RentItProxy = new RentItClient(binding, address);

            this.Credentials = new AccountCredentials()
            {
                UserName = "publishCorp",
                HashedPassword = "7110EDA4D09E062AA5E4A390B0A572AC0D2C0220"
            };

            this.editAccountControl.RentItProxy = this.RentItProxy;
            this.editAccountControl.Credentials = this.Credentials;

            this.mediaUploadControl.RentItProxy = this.RentItProxy;

            PublisherAccount accountData = this.RentItProxy.GetAllPublisherData(this.Credentials);
            this.publishedMediaList.MediaItems = accountData.PublishedItems;
             * */
        }

        /// <summary>
        /// 
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
        /// Sets the account credentials to be used by the UserControl.
        /// When the credentials is set, the UserControl is populated with data.
        /// The RentItProxy property must be set before this property is set.
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

                PublisherAccount accountData = this.RentItProxy.GetAllPublisherData(value);
                this.mediaUploadControl.PublisherAccount = accountData;
                this.publishedMediaList.MediaItems = accountData.PublishedItems;
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
            if (index < 0 || index > this.TabControl.TabCount - 1)
            {
                index = 0;
            }

            this.TabControl.SelectTab(index);
        }


        #region EventHandlers

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

            if (newPrice == mediaInfo.Price)
            {
                changePriceButton.Enabled = false;
                return;
            }

            changePriceButton.Enabled = true;
        }

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

        private void deleteMediaButton_Click(object sender, EventArgs e)
        {
            // Retrieve the selected media item.
            MediaInfo mediaInfo = this.publishedMediaList.GetSingleMedia();

            if (mediaInfo == null)
            {
                return;
            }

            // Delete the media from the service.
            RentItProxy.DeleteMedia(mediaInfo.Id, this.Credentials);

            // Update the list to reflec the deletion changes.
            PublisherAccount pubAcc = this.RentItProxy.GetAllPublisherData(this.Credentials);
            this.publishedMediaList.MediaItems = pubAcc.PublishedItems;
        }

        private void changePriceButton_Click(object sender, EventArgs e)
        {
            MediaInfo selectedMedia;
            if ((selectedMedia = this.publishedMediaList.GetSingleMedia()) == null)
            {
                return;
            }

            selectedMedia.Price = int.Parse(priceTextBox.Text);

            this.RentItProxy.UpdateMediaMetadata(selectedMedia, this.Credentials);

            // Update the list to reflect the changes.
            PublisherAccount accountData = this.RentItProxy.GetAllPublisherData(this.Credentials);
            this.publishedMediaList.MediaItems = accountData.PublishedItems;
        }

        #endregion
    }
}
