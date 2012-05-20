
namespace ClientApp
{
    using System;
    using System.Collections.Generic;
    using System.Windows.Forms;

    using RentIt;

    /// <author>Kenneth Søhrmann</author>
    /// <summary>
    /// A UserControl containing an instance of the PagedRentalsList.
    /// The control add buttons for navigating the list pages and for setting
    /// the number of list to be displayed at the list at each page.
    /// The list is utilized in the screen displaying user rentals.
    /// </summary>
    internal partial class PagedRentalsListControl : RentItUserControl
    {
        /// <summary>
        /// Initializes a new instance of the PagedRentalsListControl class.
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
        /// Overridden to also set the proxy instance of the rentals list.
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
                this.mediaList.RentItProxy = value;
            }
        }

        /// <summary>
        /// Toggle whether the list is to display active or expired
        /// rentals. A true-value will make the list show active rentals,
        /// a false-value will make the list show expired rentals.
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
        /// <param name="handler">
        /// The EventHandler to be added.
        /// </param>
        internal void AddDoubleClickEventHandler(EventHandler handler)
        {
            this.mediaList.DoubleClick += handler;
        }

        /// <summary>
        /// Gets the single selected MediaInfo-object.
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

        /// <summary>
        /// Controller for the First-button.
        /// </summary>
        private void firstPageButton_Click(object sender, EventArgs e)
        {
            this.mediaList.GoToFirstPage();
            this.DetermineButtons();
            this.currentPageTextbox.Text =
                this.mediaList.CurrentPageNumber + "/" + this.mediaList.NumberOfPages;
        }

        /// <summary>
        /// Controller for he Previous-button.
        /// </summary>
        private void previousPageButton_Click(object sender, EventArgs e)
        {
            this.mediaList.PreviousPage();
            this.DetermineButtons();
            this.currentPageTextbox.Text =
                this.mediaList.CurrentPageNumber + "/" + this.mediaList.NumberOfPages;
        }

        /// <summary>
        /// Controller for the Next-button.
        /// </summary>
        private void nextPageButton_Click(object sender, EventArgs e)
        {
            this.mediaList.NextPage();
            this.DetermineButtons();
            this.currentPageTextbox.Text =
                this.mediaList.CurrentPageNumber + "/" + this.mediaList.NumberOfPages;
        }

        /// <summary>
        /// Controller for the Last-button.
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
        /// EventHandler for the SelectedItemChangedEvent on the combobox.
        /// Updates the list to display the selected number of items.
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
