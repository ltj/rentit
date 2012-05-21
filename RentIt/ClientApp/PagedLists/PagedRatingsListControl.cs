
namespace ClientApp
{
    using System;

    using RentIt;

    /// <author>Kenneth Søhrmann</author>
    /// <summary>
    /// A UserControl containing an instance of the PagedRatingsList.
    /// The control add buttons for navigating the list pages and for setting
    /// the number of list to be displayed at the list at each page.
    /// The list is utilized in the screens displaying media metadata.
    /// The Control also contains a text box that displays the complete review
    /// text of the single selected media review in the PagedRatingsList.
    /// </summary>
    internal partial class PagedRatingsListControl : RentItUserControl
    {

        /// <summary>
        /// Initializes a new instance of the PagedRatingsListControl class.
        /// </summary>
        public PagedRatingsListControl()
        {
            InitializeComponent();

            this.itemsPerPageComboBox.SelectedIndex = 0;

            // Set the number of items to be displayed at once in the list.
            this.ratingsList.ItemsPerPage = int.Parse(this.itemsPerPageComboBox.SelectedItem.ToString());

            // Initialize the current page-text box
            this.currentPageTextbox.Text =
                this.ratingsList.CurrentPageNumber + "/" + this.ratingsList.NumberOfPages;

            // Add event handlers
            this.itemsPerPageComboBox.SelectedIndexChanged += this.ComboBoxSelectedItemChangedEventHandler;
            this.ratingsList.SelectedIndexChanged += this.SelectedRatingsListIndexChangedEventHandler;
        }

        /// <summary>
        /// Sets the contents of the rating list based on the MediaInfo
        /// object submitted. Any reviews contained in the list prior calling this
        /// property will be disregarded.
        /// </summary>
        internal MediaInfo MediaItems
        {
            set
            {
                this.ratingsList.UpdateListContents(value.Rating.Reviews);

                // Update the text box showing the currently displayed list page.
                this.currentPageTextbox.Text =
                    this.ratingsList.CurrentPageNumber + "/" + this.ratingsList.NumberOfPages;

                this.DetermineButtons();
            }
        }

        #region Controllers

        /// <summary>
        /// Controller of the "First"-button.
        /// Navigates to the first page of the list.
        /// </summary>
        private void firstPageButton_Click(object sender, EventArgs e)
        {
            this.ratingsList.GoToFirstPage();
            this.DetermineButtons();
            this.currentPageTextbox.Text =
                this.ratingsList.CurrentPageNumber + "/" + this.ratingsList.NumberOfPages;
        }

        /// <summary>
        /// Controller of the "Previous"-button.
        /// Navigates to the previous page of the list.
        /// </summary>
        private void previousPageButton_Click(object sender, EventArgs e)
        {
            this.ratingsList.PreviousPage();
            this.DetermineButtons();
            this.currentPageTextbox.Text =
                this.ratingsList.CurrentPageNumber + "/" + this.ratingsList.NumberOfPages;
        }

        /// <summary>
        /// Controller of the "Next"-button.
        /// Navigates to the next page of the list.
        /// </summary>
        private void nextPageButton_Click(object sender, EventArgs e)
        {
            this.ratingsList.NextPage();
            this.DetermineButtons();
            this.currentPageTextbox.Text =
                this.ratingsList.CurrentPageNumber + "/" + this.ratingsList.NumberOfPages;
        }

        /// <summary>
        /// Controller of the "Last"-button.
        /// Navigates to the last page of the list.
        /// </summary>
        private void lastPageButton_Click(object sender, EventArgs e)
        {
            this.ratingsList.GoToLastPage();
            this.DetermineButtons();
            this.currentPageTextbox.Text =
                this.ratingsList.CurrentPageNumber + "/" + this.ratingsList.NumberOfPages;
        }

        #endregion

        #region EventHandlers

        /// <summary>
        /// EventHandler for the SelectedItemsChangedEvent on the combobox.
        /// Updates the list to show the chosen amount of items at a single page.
        /// </summary>
        private void ComboBoxSelectedItemChangedEventHandler(object obj, EventArgs e)
        {
            this.ratingsList.ItemsPerPage = int.Parse(
                this.itemsPerPageComboBox.SelectedItem.ToString());

            // Update the text box showing the currently displayed list page.
            this.currentPageTextbox.Text =
                this.ratingsList.CurrentPageNumber + "/" + this.ratingsList.NumberOfPages;

            this.DetermineButtons();
        }

        /// <summary>
        /// EventHandler for the IndexChangedEvent on the RatingsList.
        /// Shows the review text of the selected review in the textbox.
        /// </summary>
        private void SelectedRatingsListIndexChangedEventHandler(object obj, EventArgs e)
        {
            MediaReview review = this.ratingsList.GetSingleReview();
            this.fullTextReviewTextBox.Text = review.ReviewText;
        }

        #endregion

        /// <summary>
        /// Determines whether a navigation button should be activated.
        /// This is done for all four navigation buttons.
        /// </summary>
        private void DetermineButtons()
        {
            bool hasPreviousPage = this.ratingsList.HasPreviousPage();
            this.firstPageButton.Enabled = hasPreviousPage;
            this.previousPageButton.Enabled = hasPreviousPage;

            bool hasNextPage = this.ratingsList.HasNextPage();
            this.nextPageButton.Enabled = hasNextPage;
            this.lastPageButton.Enabled = hasNextPage;
        }
    }
}
