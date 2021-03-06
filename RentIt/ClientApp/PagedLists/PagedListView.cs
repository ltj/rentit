﻿
namespace ClientApp
{
    using System;
    using System.Collections.Generic;
    using System.Windows.Forms;

    /// <author>Kenneth Søhrmann</author>
    /// <summary>
    /// An implementation of the System.Windows.Forms.ListView class that
    /// allows for paging.
    /// </summary>
    internal abstract class PagedListView : ListView
    {
        /// <summary>
        /// All the ListViewItems currently contained in the list.
        /// This does not resemble the items currently shown.
        /// </summary>
        private List<ListViewItem> currentItems;

        /// <summary>
        /// Number of items to list at a single list page.
        /// 25 is displayed by default.
        /// </summary>
        private int itemsPerPage = 25;

        /// <summary>
        /// The current page that is being displayed.
        /// The list is initialized to start the view at the first page.
        /// </summary>
        private int currentPageNumber = 1;

        /// <summary>
        /// The total number of pages of the list.
        /// There is always at least 1 page, even when the list is empty.
        /// </summary>
        private int numberOfPages = 1;

        /// <summary>
        /// Initializes a new instance of the PagedListView class.
        /// </summary>
        protected PagedListView()
        {
            this.Resize += this.ListResizedEventHandler;
        }

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
        /// Gets or sets the number of items the ListView must display.
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
        /// <returns>
        /// True if there is a previous page seen from the perspective of the 
        /// currently displayed page, false otherwise.
        /// </returns>
        internal bool HasPreviousPage()
        {
            return currentPageNumber != 1;
        }

        /// <summary>
        /// Determines whether there are more pages of items in this list.
        /// </summary>
        /// <returns>
        /// True if there is a next page seen from the perspective of the
        /// currently displayed page, false otherwise.
        /// </returns>
        internal bool HasNextPage()
        {
            return this.currentPageNumber != this.numberOfPages;
        }

        /// <summary>
        /// Gets the number of pages of the current list instance.
        /// </summary>
        /// <returns>
        /// An integer indicating the number of pages the list
        /// consists of in total.
        /// </returns>
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
        /// <returns>
        /// An integer indicating the number of 
        /// the currently displayed page.
        /// </returns>
        internal int CurrentPageNumber
        {
            get
            {
                return this.currentPageNumber;
            }
        }

        /// <summary>
        /// Advances the ListView one page, if the current view has a next page.
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
        /// Go back the ListView one page, if the current view has a previous page.
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
        /// Go to the last page of the list.
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
        /// Go to the first page of the list.
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
        /// This method adds the ListViewItems to the ListView.
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

        /// <summary>
        /// EventHandler for the ListResizedEvent of the ListView.
        /// This EventHandler resizes the columns to fill out the list when
        /// the component is resized.
        /// </summary>
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
