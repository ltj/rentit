namespace ClientApp
{
    using System;
    using System.ServiceModel;

    using RentIt;

    internal partial class PublisherAccountManagement : RentItUserControl
    {

        public PublisherAccountManagement()
        {
            InitializeComponent();

            deleteMediaButton.Enabled = false;
            changePriceButton.Enabled = false;

            // Add eventhandlers:
            publishedMediaList.AddSelectedIndexChangedEventHandler(this.SelectedIndexChangedHandler);
            publishedMediaList.AddLostFocusEventHandler(this.LostFocusHandler);
            publishedMediaList.AddDoubleClickEventHandler(this.DoubleClickEventHandler);

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
        }

        /// <summary>
        /// Sets the account credentials to be used by the UserControl.
        /// When the credentials is set, the UserControl is populated with data.
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
                PublisherAccount accountData = this.RentItProxy.GetAllPublisherData(value);
                this.mediaUploadControl.Credentials = this.Credentials;
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
            changePriceButton.Enabled = true;
            MediaType groupName = publishedMediaList.GetSingleMedia().Type;
            deleteMediaButton.Enabled = groupName != MediaType.Song;
        }

        private void LostFocusHandler(object obj, EventArgs e)
        {
            deleteMediaButton.Enabled = false;
            changePriceButton.Enabled = false;
        }

        private void DoubleClickEventHandler(object obj, EventArgs e)
        {
            MediaInfo mediaInfo = this.publishedMediaList.GetSingleMedia();

            if (mediaInfo == null)
            {
                return;
            }

            RentItUserControl mediaDetail = null;

            if (mediaInfo.Type == MediaType.Album)
            {
                var albumDetails = new AlbumDetails();
                albumDetails.AlbumInfo = (AlbumInfo)mediaInfo;
            }
            else if (mediaInfo.Type == MediaType.Movie)
            {
                var movieDetails = new BookMovieDetails();
                movieDetails.MovieInfo = (MovieInfo)mediaInfo;
            }
            else if (mediaInfo.Type == MediaType.Book)
            {
                var bookDetails = new BookMovieDetails();
                bookDetails.BookInfo = (BookInfo)mediaInfo;
            }

            mediaDetail.RentItProxy = this.RentItProxy;
            mediaDetail.Credentials = this.Credentials;

            this.FireContentChangeEvent(mediaDetail, mediaInfo.Title);
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
            //this.serviceClient.DeleteMedia(selectedMediaInfo.Id, this.accountCredentials);

            // Update the list to reflec the deletion changes.
            PublisherAccount pubAcc = this.RentItProxy.GetAllPublisherData(this.Credentials);
            this.publishedMediaList.MediaItems = pubAcc.PublishedItems;
        }

        #endregion
    }
}
