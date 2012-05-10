namespace ClientApp
{
    using System;
    using System.Collections.Generic;
    using System.ServiceModel;
    using System.Windows.Forms;

    using RentIt;

    public partial class PublisherAccountManagement : UserControl
    {
        private AccountCredentials accountCredentials;

        private RentItClient serviceClient;

        private Dictionary<ListViewItem, MediaInfo> map;

        public PublisherAccountManagement()
        {
            InitializeComponent();

            deleteMediaButton.Enabled = false;
            changePriceButton.Enabled = false;

            publishedMediaList.AddSelectedIndexChangedEventHandler(this.SelectedIndexChangedHandler);

            BasicHttpBinding binding = new BasicHttpBinding();
            EndpointAddress address = new EndpointAddress("http://rentit.itu.dk/rentit01/RentItService.svc");
            this.serviceClient = new RentItClient(binding, address);

            this.accountCredentials = this.accountCredentials = new AccountCredentials()
            {
                UserName = "publishCorp",
                HashedPassword = "7110EDA4D09E062AA5E4A390B0A572AC0D2C0220"
            };

            PublisherAccount accountData = serviceClient.GetAllPublisherData(accountCredentials);
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

        internal RentItClient ServiceClient
        {
            set
            {
                this.serviceClient = value;
            }
        }

        internal AccountCredentials PublisherAccount
        {
            set
            {
                PublisherAccount accountData = serviceClient.GetAllPublisherData(value);
                this.publishedMediaList.MediaItems = accountData.PublishedItems;
            }
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
            PublisherAccount pubAcc = this.serviceClient.GetAllPublisherData(this.accountCredentials);
            this.publishedMediaList.MediaItems = pubAcc.PublishedItems;
        }

        #endregion
    }
}
