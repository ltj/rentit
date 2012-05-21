
namespace ClientApp
{
    using System;
    using System.Collections.Generic;
    using System.Windows.Forms;

    using RentIt;

    /// <author>Kenneth Søhrmann</author>
    /// <summary>
    /// A UserControl containing an instance of the PagedDetailedMediaLIst.
    /// The control add buttons for navigating the list pages and for setting
    /// the number of list to be displayed at the list at each page.
    /// The list is utilized in the screen displaying search results and in the 
    /// list where the publisher can see medias he has published.
    /// </summary>
    internal partial class PagedDetailedMediaListControl : RentItUserControl
    {
        /// <summary>
        /// Initializes a new instance of the DetailedMediaListControl class.
        /// </summary>
        public PagedDetailedMediaListControl()
        {
            InitializeComponent();

            this.itemsPerPageComboBox.SelectedIndex = 0;
            this.mediaList.ItemsPerPage = int.Parse(this.itemsPerPageComboBox.SelectedItem.ToString());

            // Add event handlers
            this.itemsPerPageComboBox.SelectedIndexChanged += this.ComboBoxSelectedItemChangedEventHandler;
        }


        /// <summary>
        /// Sets the contents of the list and updates the list
        /// to display the media items. Items displayed before invoking the
        /// setter will be disregarded.
        /// </summary>
        internal MediaItems MediaItems
        {
            set
            {
                this.mediaList.UpdateListContents(value);

                // Update the text box showing the currently displayed list page.
                this.currentPageTextbox.Text =
                    this.mediaList.CurrentPageNumber + "/" + this.mediaList.NumberOfPages;

                this.DetermineButtons();
            }
        }

        /// <summary>
        /// Adds an EventHandler to the SelectedIndexChanged event on the paged list.
        /// </summary>
        /// <param name="handler">
        /// The EventHandler to be added.
        /// </param>
        internal void AddSelectedIndexChangedEventHandler(EventHandler handler)
        {
            this.mediaList.SelectedIndexChanged += handler;
        }

        /// <summary>
        /// Adds an Eventhandler to the LostFocus event on the paged list.
        /// </summary>
        /// <param name="handler">
        /// The EventHandler to be added.
        /// </param>
        internal void AddLostFocusEventHandler(EventHandler handler)
        {
            this.mediaList.LostFocus += handler;
        }

        /// <summary>
        /// Adds an EventHandler to the Double Click event on the paged list.
        /// </summary>
        /// <param name="handler">
        /// The EventHandler to be added.
        /// </param>
        internal void AddDoubleClickEventHandler(EventHandler handler)
        {
            this.mediaList.DoubleClick += handler;
        }

        /// <summary>
        /// Gets the currently selected MediaInfo-objects.
        /// </summary>
        /// <returns>
        /// A list of MediaInfo-objects currently selected in the list.
        /// </returns>
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
        /// <returns>
        /// Return an instance of the single selected media. If less or more than 
        /// one media is selected, returns null.
        /// </returns>
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

        /// <summary>
        /// Controller of the "First"-button.
        /// Navigates to the first page of the list.
        /// </summary>
        private void firstPageButton_Click(object sender, EventArgs e)
        {
            this.mediaList.GoToFirstPage();
            this.DetermineButtons();
            this.currentPageTextbox.Text =
                this.mediaList.CurrentPageNumber + "/" + this.mediaList.NumberOfPages;
        }

        /// <summary>
        /// Controller of the "Previous"-button.
        /// Navigates to the previous page of the list.
        /// </summary>
        private void previousPageButton_Click(object sender, EventArgs e)
        {
            this.mediaList.PreviousPage();
            this.DetermineButtons();
            this.currentPageTextbox.Text =
                this.mediaList.CurrentPageNumber + "/" + this.mediaList.NumberOfPages;
        }

        /// <summary>
        /// Controller of the "Next"-button.
        /// Navigates to the next page of the list.
        /// </summary>
        private void nextPageButton_Click(object sender, EventArgs e)
        {
            this.mediaList.NextPage();
            this.DetermineButtons();
            this.currentPageTextbox.Text =
                this.mediaList.CurrentPageNumber + "/" + this.mediaList.NumberOfPages;
        }

        /// <summary>
        /// Controller of the "Last"-button.
        /// Navigates to the last page of the list.
        /// </summary>
        private void lastPageButton_Click(object sender, EventArgs e)
        {
            this.mediaList.GoToLastPage();
            this.DetermineButtons();
            this.currentPageTextbox.Text =
                this.mediaList.CurrentPageNumber + "/" + this.mediaList.NumberOfPages;
        }

        #endregion

        #region EventHandlers

        /// <summary>
        /// EventHandler for the SelectedItemChangedEvent of the Combobox.
        /// </summary>
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
