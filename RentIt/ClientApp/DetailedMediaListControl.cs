﻿using System;
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

    public partial class DetailedMediaListControl : UserControl
    {
        public DetailedMediaListControl()
        {
            InitializeComponent();

            BasicHttpBinding binding = new BasicHttpBinding();
            EndpointAddress address = new EndpointAddress("http://rentit.itu.dk/rentit01/RentItService.svc");
            RentItClient serviceClient = new RentItClient(binding, address);

            this.itemsPerPageComboBox.SelectedIndex = 0;
            this.mediaList.ItemsPerPage = int.Parse(this.itemsPerPageComboBox.SelectedItem.ToString());

            this.mediaList.UpdateListContents(serviceClient.GetAllPublisherData(new AccountCredentials()
                {
                    UserName = "publishCorp",
                    HashedPassword = "7110EDA4D09E062AA5E4A390B0A572AC0D2C0220"
                }).PublishedItems);

            // Add event handlers
            this.itemsPerPageComboBox.SelectedIndexChanged += this.ComboBoxSelectedItemChangedEventHandler;
        }

        internal MediaItems MediaItems
        {
            set
            {
                this.mediaList.UpdateListContents(value);
                this.DetermineButtons();
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