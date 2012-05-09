// -----------------------------------------------------------------------
// <copyright file="DetailedMediaList.cs" company="">
// TODO: Update copyright text.
// </copyright>
// -----------------------------------------------------------------------

namespace ClientApp
{
    using System;
    using System.Collections;
    using System.Collections.Generic;
    using System.ServiceModel;
    using System.Windows.Forms;

    using RentIt;

    /// <author>Kenneth Søhrmann</author>
    /// <summary>
    /// TODO: Update summary.
    /// </summary>
    public class RentalsList : ListView
    {
        private ColumnHeader titleColumn;
        private ColumnHeader startDateColumn;
        private ColumnHeader endDateColumn;
        // private ColumnHeader priceColumn;

        private ListViewGroup songListViewGroup;
        private ListViewGroup albumListViewGroup;
        private ListViewGroup movieListViewGroup;
        private ListViewGroup bookListViewGroup;

        private Dictionary<ListViewItem, MediaInfo> map;

        private List<ListViewItem> currentItems;

        // The default value for items shown per page.
        private int itemsPerPage = 25;

        private int currentPageNumber = 1;

        private int numberOfPages = 1;

        private RentItClient serviceClient;

        /// <summary>
        /// Initializes a new instance of the 
        /// </summary>
        public RentalsList()
        {
            // Initialze columns
            this.titleColumn = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.startDateColumn = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.endDateColumn = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            // this.priceColumn = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));

            this.AutoArrange = false;
            this.Columns.AddRange(new System.Windows.Forms.ColumnHeader[] {
                this.titleColumn,
                this.startDateColumn,
                this.endDateColumn,
                // this.priceColumn
            });
            // this.Dock = System.Windows.Forms.DockStyle.Fill;
            this.FullRowSelect = true;
            this.GridLines = true;
            this.Location = new System.Drawing.Point(0, 0);
            this.MultiSelect = false;
            this.Name = "RentalsList";
            this.Size = new System.Drawing.Size(500, 470);
            this.TabIndex = 9;
            this.UseCompatibleStateImageBehavior = false;
            this.View = System.Windows.Forms.View.Details;
            // 
            // titleColumn
            // 
            this.titleColumn.Text = "Title";
            this.titleColumn.Width = 220;
            // 
            // startDateColumn
            // 
            this.startDateColumn.Text = "Rental start date";
            this.startDateColumn.Width = 120;
            // 
            // endDateColumn
            // 
            this.endDateColumn.Text = "Rental end date";
            this.endDateColumn.Width = 120;
            // 
            // priceColumn
            // 
            // this.priceColumn.Text = "Price";
            // this.priceColumn.Width = 85;

            // Initialize and add list groups.
            this.songListViewGroup = new ListViewGroup("Songs", HorizontalAlignment.Left);
            this.albumListViewGroup = new ListViewGroup("Albums", HorizontalAlignment.Left);
            this.movieListViewGroup = new ListViewGroup("Movies", HorizontalAlignment.Left);
            this.bookListViewGroup = new ListViewGroup("Books", HorizontalAlignment.Left);
            this.Groups.AddRange(
                new ListViewGroup[] { this.songListViewGroup, this.albumListViewGroup, this.movieListViewGroup, this.bookListViewGroup });

            BasicHttpBinding binding = new BasicHttpBinding();
            EndpointAddress address = new EndpointAddress("http://rentit.itu.dk/rentit01/RentItService.svc");
            serviceClient = new RentItClient(binding, address);
        }

        /// <summary>
        /// Update the ListView with new data.
        /// When this method is called, all data previously added to the list 
        /// will be disregarded, and the contents of the ListView will match
        /// the media items passed in the parameter.
        /// </summary>
        /// <param name="rentals"></param>
        internal void UpdateListContents(List<Rental> rentals)
        {
            this.BeginUpdate();

            this.map = new Dictionary<ListViewItem, MediaInfo>();
            this.currentItems = new List<ListViewItem>();

            foreach (var rental in rentals)
            {
                switch (rental.MediaType)
                {
                    case MediaType.Album:
                        AlbumInfo album = this.serviceClient.GetAlbumInfo(rental.MediaId);

                        var listItemA = new ListViewItem(album.Title);
                        listItemA.SubItems.Add(rental.StartTime.ToShortDateString());
                        listItemA.SubItems.Add(rental.EndTime.ToShortDateString());
                        listItemA.Group = this.albumListViewGroup;

                        this.currentItems.Add(listItemA); // this.Items.Add(listItem);
                        this.map.Add(listItemA, album);
                        break;
                    case MediaType.Movie:
                        MovieInfo movie = this.serviceClient.GetMovieInfo(rental.MediaId);

                        var listItemM = new ListViewItem(movie.Title);
                        listItemM.SubItems.Add(rental.StartTime.ToShortDateString());
                        listItemM.SubItems.Add(rental.EndTime.ToShortDateString());
                        listItemM.Group = this.movieListViewGroup;

                        this.currentItems.Add(listItemM); // this.Items.Add(listItem);
                        this.map.Add(listItemM, movie);
                        break;
                    case MediaType.Book:
                        BookInfo book = this.serviceClient.GetBookInfo(rental.MediaId);

                        var listItemB = new ListViewItem(book.Title);
                        listItemB.SubItems.Add(rental.StartTime.ToShortDateString());
                        listItemB.SubItems.Add(rental.EndTime.ToShortDateString());
                        listItemB.Group = this.bookListViewGroup;

                        this.currentItems.Add(listItemB); // this.Items.Add(listItem);
                        this.map.Add(listItemB, book);
                        break;
                    default:
                        break;
                }
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



        /// <summary>
        /// Helper method for populating the ListView with ListViewItems.
        /// </summary>
        private void RePopulateList()
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
