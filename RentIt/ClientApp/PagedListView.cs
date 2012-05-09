// -----------------------------------------------------------------------
// <copyright file="PagedListView.cs" company="">
// TODO: Update copyright text.
// </copyright>
// -----------------------------------------------------------------------

namespace ClientApp
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Text;
    using System.Windows.Forms;

    using RentIt;

    /// <author>Kenneth Søhrmann</author>
    /// <summary>
    /// TODO: Update summary.
    /// </summary>
    public abstract class PagedListView : ListView
    {
        private Dictionary<ListViewItem, MediaInfo> map;

        private List<ListViewItem> currentItems;

        private int itemsPerPage = 25;

        private int currentPageNumber = 1;

        private int numberOfPages = 1;


        internal Dictionary<ListViewItem, MediaInfo> Map
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
        /// Get the MediaInfo-object corresponding to the given ListViewItem-object.
        /// </summary>
        /// <param name="item"></param>
        /// <returns></returns>
        internal MediaInfo GetMediaInfoValueOf(ListViewItem item)
        {
            return this.map[item];
        }

        protected void RePopulateList()
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
            this.numberOfPages = (int)Math.Ceiling((double)this.currentItems.Count / this.itemsPerPage);
        }
    }
}
