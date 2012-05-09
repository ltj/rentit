using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace ClientApp
{
    using System.ServiceModel;

    using RentIt;

    public partial class PagedRatingsListControl : UserControl
    {
        public PagedRatingsListControl()
        {
            InitializeComponent();

            BasicHttpBinding binding = new BasicHttpBinding();
            EndpointAddress address = new EndpointAddress("http://rentit.itu.dk/rentit01/RentItService.svc");
            RentItClient serviceClient = new RentItClient(binding, address);

            this.itemsPerPageComboBox.SelectedIndex = 0;
            this.ratingsList.ItemsPerPage = int.Parse(this.itemsPerPageComboBox.SelectedItem.ToString());

            //this.mediaList.UpdateListContents(serviceClient.GetAllPublisherData(new AccountCredentials()
            //    {
            //        UserName = "publishCorp",
            //        HashedPassword = "7110EDA4D09E062AA5E4A390B0A572AC0D2C0220"
            //    }).PublishedItems);

            // Add event handlers
            this.itemsPerPageComboBox.SelectedIndexChanged += this.ComboBoxSelectedItemChangedEventHandler;
        }

        internal MediaInfo MediaItems
        {
            set
            {
                this.ratingsList.UpdateListContents(value.Rating.Reviews);
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
            this.currentPageTextbox.Text =
                this.ratingsList.CurrentPageNumber + "/" + this.ratingsList.NumberOfPages;
            this.DetermineButtons();
        }

        #endregion

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
