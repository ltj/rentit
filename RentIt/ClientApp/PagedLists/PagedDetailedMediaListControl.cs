
namespace ClientApp
{
    using System;
    using System.Collections.Generic;
    using System.ServiceModel;
    using System.Windows.Forms;

    using RentIt;

    /// <author>Kenneth Søhrmann</author>
    /// <summary>
    /// 
    /// </summary>
    public partial class PagedDetailedMediaListControl : RentItUserControl
    {
        /// <summary>
        /// Initializes a new instance of the DetailedMediaListControl class.
        /// </summary>
        public PagedDetailedMediaListControl()
        {
            InitializeComponent();

            BasicHttpBinding binding = new BasicHttpBinding();
            EndpointAddress address = new EndpointAddress("http://rentit.itu.dk/rentit01/RentItService.svc");
            RentItProxy = new RentItClient(binding, address);

            this.itemsPerPageComboBox.SelectedIndex = 0;
            this.mediaList.ItemsPerPage = int.Parse(this.itemsPerPageComboBox.SelectedItem.ToString());

            this.mediaList.UpdateListContents(RentItProxy.GetAllPublisherData(new AccountCredentials()
                {
                    UserName = "publishCorp",
                    HashedPassword = "7110EDA4D09E062AA5E4A390B0A572AC0D2C0220"
                }).PublishedItems);

            this.currentPageTextbox.Text =
                this.mediaList.CurrentPageNumber + "/" + this.mediaList.NumberOfPages;

            // Add event handlers
            this.itemsPerPageComboBox.SelectedIndexChanged += this.ComboBoxSelectedItemChangedEventHandler;
        }


        /// <summary>
        /// Sets the contents of the list and updates the list
        /// to display the media items.
        /// </summary>
        internal MediaItems MediaItems
        {
            set
            {
                this.mediaList.UpdateListContents(value);
                this.DetermineButtons();
            }
        }

        /// <summary>
        /// Add an EventHandler to the SelectedIndexChanged event on the paged list.
        /// </summary>
        /// <param name="handler"></param>
        internal void AddSelectedIndexChangedEventHandler(EventHandler handler)
        {
            this.mediaList.SelectedIndexChanged += handler;
        }

        /// <summary>
        /// Adds an Eventhandler to the LostFocus event on the paged list.
        /// </summary>
        /// <param name="handler"></param>
        internal void AddLostFocusEventHandler(EventHandler handler)
        {
            this.mediaList.LostFocus += handler;
        }

        /// <summary>
        /// Adds an EventHandler to the Double Click event on the paged list.
        /// </summary>
        /// <param name="handler"></param>
        internal void AddDoubleClickEventHandler(EventHandler handler)
        {
            this.mediaList.DoubleClick += handler;
        }

        /// <summary>
        /// Gets the SelectedListViewItemCollection object containing all the
        /// ListViewItems that is currently selected in the paged list.
        /// </summary>
        internal List<MediaInfo> SelectedMedia
        {
            get
            {
                var tmpMediaList = new List<MediaInfo>();
                foreach (ListViewItem item in this.mediaList.SelectedItems)
                {
                    tmpMediaList.Add(this.mediaList.GetMediaInfoValueOf(item));
                }

                return tmpMediaList;
            }
        }

        /// <summary>
        /// Returns the single selected item of the list. If the number of selected
        /// items is different from one, the method returns null.
        /// </summary>
        /// <returns></returns>
        internal MediaInfo GetSingleMedia()
        {
            if (this.mediaList.SelectedItems.Count != 1)
            {
                return null;
            }

            ListViewItem selectedItem = this.mediaList.SelectedItems[0];
            return this.mediaList.GetMediaInfoValueOf(selectedItem);
        }

        #region Controllers

        private void firstPageButton_Click(object sender, EventArgs e)
        {
            this.mediaList.GoToFirstPage();
            this.DetermineButtons();
            this.currentPageTextbox.Text =
                this.mediaList.CurrentPageNumber + "/" + this.mediaList.NumberOfPages;
        }

        private void previousPageButton_Click(object sender, EventArgs e)
        {
            this.mediaList.PreviousPage();
            this.DetermineButtons();
            this.currentPageTextbox.Text =
                this.mediaList.CurrentPageNumber + "/" + this.mediaList.NumberOfPages;
        }

        private void nextPageButton_Click(object sender, EventArgs e)
        {
            this.mediaList.NextPage();
            this.DetermineButtons();
            this.currentPageTextbox.Text =
                this.mediaList.CurrentPageNumber + "/" + this.mediaList.NumberOfPages;
        }

        private void lastPageButton_Click(object sender, EventArgs e)
        {
            this.mediaList.GoToLastPage();
            this.DetermineButtons();
            this.currentPageTextbox.Text =
                this.mediaList.CurrentPageNumber + "/" + this.mediaList.NumberOfPages;
        }

        #endregion

        #region EventHandlers

        private void ComboBoxSelectedItemChangedEventHandler(object obj, EventArgs e)
        {
            this.mediaList.ItemsPerPage = int.Parse(
                this.itemsPerPageComboBox.SelectedItem.ToString());
            this.currentPageTextbox.Text =
                this.mediaList.CurrentPageNumber + "/" + this.mediaList.NumberOfPages;
            this.DetermineButtons();
        }

        #endregion

        /// <summary>
        /// Determines whether a navigation button should be activated.
        /// This is done for all four navigation buttons.
        /// </summary>
        private void DetermineButtons()
        {
            bool hasPreviousPage = this.mediaList.HasPreviousPage();
            this.firstPageButton.Enabled = hasPreviousPage;
            this.previousPageButton.Enabled = hasPreviousPage;

            bool hasNextPage = this.mediaList.HasNextPage();
            this.nextPageButton.Enabled = hasNextPage;
            this.lastPageButton.Enabled = hasNextPage;
        }
    }
}
