﻿
namespace ClientApp
{
    using System;
    using System.Windows.Forms;

    using RentIt;

    /// <author>Kenneth Søhrmann</author>
    /// <summary>
    /// 
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
            this.ratingsList.ItemsPerPage = int.Parse(this.itemsPerPageComboBox.SelectedItem.ToString());

            this.currentPageTextbox.Text =
                this.ratingsList.CurrentPageNumber + "/" + this.ratingsList.NumberOfPages;

            // Add event handlers
            this.itemsPerPageComboBox.SelectedIndexChanged += this.ComboBoxSelectedItemChangedEventHandler;
            this.ratingsList.SelectedIndexChanged += this.SelectedRatingsListIndexChangedEventHandler;
        }

        /// <summary>
        /// Sets the contents of the rating list based on the MediaInfo
        /// object submitted.
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

        private void firstPageButton_Click(object sender, EventArgs e)
        {
            this.ratingsList.GoToFirstPage();
            this.DetermineButtons();
            this.currentPageTextbox.Text =
                this.ratingsList.CurrentPageNumber + "/" + this.ratingsList.NumberOfPages;
        }

        private void previousPageButton_Click(object sender, EventArgs e)
        {
            this.ratingsList.PreviousPage();
            this.DetermineButtons();
            this.currentPageTextbox.Text =
                this.ratingsList.CurrentPageNumber + "/" + this.ratingsList.NumberOfPages;
        }

        private void nextPageButton_Click(object sender, EventArgs e)
        {
            this.ratingsList.NextPage();
            this.DetermineButtons();
            this.currentPageTextbox.Text =
                this.ratingsList.CurrentPageNumber + "/" + this.ratingsList.NumberOfPages;
        }

        private void lastPageButton_Click(object sender, EventArgs e)
        {
            this.ratingsList.GoToLastPage();
            this.DetermineButtons();
            this.currentPageTextbox.Text =
                this.ratingsList.CurrentPageNumber + "/" + this.ratingsList.NumberOfPages;
        }

        #endregion

        #region EventHandlers

        private void ComboBoxSelectedItemChangedEventHandler(object obj, EventArgs e)
        {
            this.ratingsList.ItemsPerPage = int.Parse(
                this.itemsPerPageComboBox.SelectedItem.ToString());

            // Update the text box showing the currently displayed list page.
            this.currentPageTextbox.Text =
                this.ratingsList.CurrentPageNumber + "/" + this.ratingsList.NumberOfPages;

            this.DetermineButtons();
        }

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
