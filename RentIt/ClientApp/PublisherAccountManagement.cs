namespace ClientApp
{
    using System;
    using System.Collections.Generic;
    using System.ServiceModel;
    using System.Windows.Forms;

    using RentIt;

    public partial class PublisherAccountManagement : RentItUserControl
    {

        private Dictionary<ListViewItem, MediaInfo> map;

        public PublisherAccountManagement()
        {
            InitializeComponent();

            deleteMediaButton.Enabled = false;
            changePriceButton.Enabled = false;

            // Add eventhandlers:
            publishedMediaList.AddSelectedIndexChangedEventHandler(this.SelectedIndexChangedHandler);
            publishedMediaList.AddLostFocusEventHandler(this.LostFocusHandler);

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

            PublisherAccount accountData = this.RentItProxy.GetAllPublisherData(this.Credentials);
            this.publishedMediaList.MediaItems = accountData.PublishedItems;
        }

        /*
        internal PublisherAccountManagement(AccountCredentials accountCredentials)
            : this()
        {
            BasicHttpBinding binding = new BasicHttpBinding();
            EndpointAddress address = new EndpointAddress("http://rentit.itu.dk/rentit01/RentItService.svc");
            this.serviceClient = new RentItClient(binding, address);

            this.accountCredentials = new AccountCredentials()
                {
                    UserName = "publishCorp",
                    HashedPassword = "7110EDA4D09E062AA5E4A390B0A572AC0D2C0220"
                };
            // accountCredentials;

            PublisherAccount accountData = serviceClient.GetAllPublisherData(this.accountCredentials);
            this.publishedMediaList.MediaItems = accountData.PublishedItems;
        }
         * */


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
            if (publishedMediaList.SelectedItems.Count != 1)
            {
                deleteMediaButton.Enabled = false;
                changePriceButton.Enabled = false;
            }
            // If the single selected item is contained in the Songs group, it is
            // not possible to delete the media item; the delete button is disabled.
            else
            {
                changePriceButton.Enabled = true;
                if (publishedMediaList.SelectedItems.Count == 0)
                {
                    deleteMediaButton.Enabled = false;
                    return;
                }
                string groupName = publishedMediaList.SelectedItems[0].Group.Header;
                deleteMediaButton.Enabled = !groupName.Equals("Songs");
            }
        }

        private void LostFocusHandler(object obj, EventArgs e)
        {
            deleteMediaButton.Enabled = false;
            changePriceButton.Enabled = false;
        }

        #endregion

        #region Controllers

        private void deleteMediaButton_Click(object sender, EventArgs e)
        {
            // Retrieve the selected media item.
            ListView.SelectedListViewItemCollection selectedItems = this.publishedMediaList.SelectedItems;
            if (selectedItems.Count == 0) return;

            ListViewItem selectedItem = this.publishedMediaList.SelectedItems[0];

            // Get the corresponding MediaInfo object.
            MediaInfo selectedMediaInfo = this.map[selectedItem];

            // Delete the media from the service.
            //this.serviceClient.DeleteMedia(selectedMediaInfo.Id, this.accountCredentials);

            // Update the list to reflec the deletion changes.
            PublisherAccount pubAcc = this.RentItProxy.GetAllPublisherData(this.Credentials);
            this.publishedMediaList.MediaItems = pubAcc.PublishedItems;
        }

        #endregion
    }
}
