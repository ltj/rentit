
namespace ClientApp
{
    using System;
    using System.Collections.Generic;
    using System.Windows.Forms;

    using RentIt;

    /// <author>Kenneth Søhrmann</author>
    /// <summary>
    /// 
    /// </summary>
    internal partial class PagedRentalsListControl : RentItUserControl
    {
        /// <summary>
        /// Initializes a new instance of the RentalsListControl class.
        /// </summary>
        public PagedRentalsListControl()
        {
            InitializeComponent();

            this.itemsPerPageComboBox.SelectedIndex = 0;
            this.mediaList.ItemsPerPage = int.Parse(this.itemsPerPageComboBox.SelectedItem.ToString());

            // Add event handlers
            this.itemsPerPageComboBox.SelectedIndexChanged += this.ComboBoxSelectedItemChangedEventHandler;
        }

        /// <summary>
        /// Sets the rating list to display the ratings contained 
        /// in the submitted list.
        /// </summary>
        internal List<Rental> Rentals
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
        /// Toggle whether the list is to display active or expired
        /// rentals.
        /// </summary>
        internal bool ShowActiveRentals
        {
            set
            {
                this.mediaList.ShowActive(value);
            }
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
        internal MediaInfo SelectedItem
        {
            get
            {
                ListViewItem selectedItem = this.mediaList.SelectedItems[0];
                return this.mediaList.GetMediaInfoValueOf(selectedItem);
            }
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
