﻿// -----------------------------------------------------------------------
// <copyright file="PagedListView.cs" company="">
// TODO: Update copyright text.
// </copyright>
// -----------------------------------------------------------------------

namespace ClientApp
{
    using System;
    using System.Collections.Generic;
    using System.Windows.Forms;

    using RentIt;

    /// <author>Kenneth Søhrmann</author>
    /// <summary>
    /// TODO: Update summary.
    /// </summary>
    internal abstract class PagedListView : ListView
    {
        /// <summary>
        /// For mapping from a ListViewItem to is corresponding MediaInfo object.
        /// </summary>
        // private Dictionary<ListViewItem, MediaInfo> map;

        /// <summary>
        /// All the ListViewItems currently contained in the list.
        /// This does not resemble the items currently shown.
        /// </summary>
        private List<ListViewItem> currentItems;

        /// <summary>
        /// Number of items to list at a single list page.
        /// </summary>
        private int itemsPerPage = 25;

        /// <summary>
        /// The current page that is being displayed.
        /// </summary>
        private int currentPageNumber = 1;

        /// <summary>
        /// The total number of pages of the list.
        /// </summary>
        private int numberOfPages = 1;

        public PagedListView()
        {
            this.Resize += this.ListResizedEventHandler;
        }

        /// <summary>
        /// Gets of sets the mapping between ListViewItems and their
        /// corresponding MediaInfo objects.
        /// </summary>

        /*internal Dictionary<ListViewItem, MediaInfo> Map
        {
            get
            {
                return map;
            }
            set
            {
                map = value;
            }
        }
         * */

        /// <summary>
        /// Gets or sets the current contents of ListViewItems contained
        /// in the list.
        /// </summary>
        internal List<ListViewItem> CurrentItems
        {
            get
            {
                return currentItems;
            }
            set
            {
                currentItems = value;
            }
        }

        /// <summary>
        /// Gets or sets the number of items the ListView at most will display.
        /// </summary>
        internal int ItemsPerPage
        {
            get
            {
                return this.itemsPerPage;
            }
            set
            {
                this.itemsPerPage = value;
                this.RePopulateList();
            }
        }

        /// <summary>
        /// Determines whether there are any previous pages beyond the 
        /// current page.
        /// </summary>
        /// <returns></returns>
        internal bool HasPreviousPage()
        {
            return currentPageNumber != 1;
        }

        /// <summary>
        /// Determines whether there are more pages of items in this list.
        /// </summary>
        /// <returns></returns>
        internal bool HasNextPage()
        {
            return this.currentPageNumber != this.numberOfPages;
        }

        /// <summary>
        /// Gets the number of pages of the current ListView.
        /// </summary>
        /// <returns></returns>
        internal int NumberOfPages
        {
            get
            {
                return this.numberOfPages;
            }
        }

        /// <summary>
        /// Gets the current page number of the ListView.
        /// </summary>
        internal int CurrentPageNumber
        {
            get
            {
                return this.currentPageNumber;
            }
        }

        /// <summary>
        /// Advance the ListView one page.
        /// </summary>
        internal void NextPage()
        {
            if (!this.HasNextPage()) return;

            int startIndex = this.currentPageNumber++ * this.itemsPerPage;

            int endIndex = (startIndex + this.itemsPerPage) <= this.currentItems.Count
                ? (startIndex + this.itemsPerPage) : this.currentItems.Count;

            this.Items.Clear();

            for (int i = startIndex; i < endIndex; i++)
            {
                // The item has to be cloned, because when the item is removed from the
                // ListView its Group is removed from the ListViewItem as well.
                this.Items.Add((ListViewItem)this.currentItems[i].Clone());
            }
        }

        /// <summary>
        /// Go back the ListView one page.
        /// </summary>
        internal void PreviousPage()
        {
            if (!this.HasPreviousPage()) return;

            int startIndex = (--this.currentPageNumber * this.itemsPerPage);
            int endIndex = startIndex - this.itemsPerPage;

            this.Items.Clear();

            for (int i = endIndex; i < startIndex; i++)
            {
                this.Items.Add((ListViewItem)this.currentItems[i].Clone());
            }
        }

        /// <summary>
        /// Go to the last page of the ListView
        /// </summary>
        internal void GoToLastPage()
        {
            int startIndex = this.currentItems.Count - (this.currentItems.Count % this.itemsPerPage);
            int endIndex = startIndex + (this.currentItems.Count % this.itemsPerPage);

            this.Items.Clear();

            for (int i = startIndex; i < endIndex; i++)
            {
                this.Items.Add((ListViewItem)this.currentItems[i].Clone());
            }

            this.currentPageNumber = this.numberOfPages;
        }

        /// <summary>
        /// Go to the first page of the ListView.
        /// </summary>
        internal void GoToFirstPage()
        {
            int startIndex = 0;
            int endIndex = startIndex + this.itemsPerPage;

            this.Items.Clear();

            for (int i = startIndex; i < endIndex; i++)
            {
                this.Items.Add((ListViewItem)this.currentItems[i].Clone());
            }

            this.currentPageNumber = 1;
        }

        /// <summary>
        /// Refresh the ListViewItems shown in the list.
        /// </summary>
        protected virtual void RePopulateList()
        {
            if (ReferenceEquals(this.currentItems, null)) return;

            this.Items.Clear();

            int limit = this.itemsPerPage > this.currentItems.Count ? this.currentItems.Count : this.itemsPerPage;

            // Populate the list with the first itemPerPage items.
            for (int i = 0; i < limit; i++)
            {
                this.Items.Add((ListViewItem)this.currentItems[i].Clone());
            }

            this.currentPageNumber = 1;
            this.numberOfPages = this.Items.Count == 0 ?
                1 : (int)Math.Ceiling((double)this.currentItems.Count / this.itemsPerPage);
        }

        #region EventHandler

        private void ListResizedEventHandler(object obj, EventArgs e)
        {
            int totalWidth = 0;

            // Get the total width of all the columns of the table.
            foreach (ColumnHeader column in this.Columns)
            {
                totalWidth += column.Width;
            }

            // Adjust the width so that the ratio of the column width will remain the same.
            foreach (ColumnHeader column in this.Columns)
            {
                column.Width = (int)(((double)column.Width / totalWidth) * this.Width);
            }
        }

        #endregion
    }
}
