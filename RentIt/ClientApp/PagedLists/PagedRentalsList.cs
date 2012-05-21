
namespace ClientApp
{
    using System;
    using System.Collections.Generic;
    using System.Windows.Forms;

    using RentIt;

    /// <author>Kenneth Søhrmann</author>
    /// <summary>
    /// This class represent the paged list used for listing user rentals.
    /// The list has a title, start date and end date column.
    /// The list divides the various media types into correspondigs groups.
    /// 
    /// The list shows active rentals by default.
    /// The ShowActive method can be used to toggle view of
    /// active or expired rentals.
    /// </summary>
    internal class PagedRentalsList : PagedListView
    {
        private ColumnHeader titleColumn;
        private ColumnHeader startDateColumn;
        private ColumnHeader endDateColumn;

        private ListViewGroup songListViewGroup;
        private ListViewGroup albumListViewGroup;
        private ListViewGroup movieListViewGroup;
        private ListViewGroup bookListViewGroup;

        /// <summary>
        /// Field holding all the rentals that are currently active.
        /// </summary>
        private List<Rental> activeRentals;

        /// <summary>
        /// Field hold all the rentals that are currently expired.
        /// </summary>
        private List<Rental> expiredRentals;

        /// <summary>
        /// Determines whether to display active or expired rentals.
        /// The value true makes the list displaying all the active rentals,
        /// and the false value makes the list display all expired rentals.
        /// </summary>
        private bool showActive = true;

        /// <summary>
        /// Initializes a new instance of the PagedRentalsList class.
        /// </summary>
        public PagedRentalsList()
        {
            // Initialze columns
            this.titleColumn = new ColumnHeader();
            this.startDateColumn = new ColumnHeader();
            this.endDateColumn = new ColumnHeader();

            this.AutoArrange = false;
            this.Columns.AddRange(new System.Windows.Forms.ColumnHeader[] {
                this.titleColumn,
                this.startDateColumn,
                this.endDateColumn,
            });

            this.FullRowSelect = true;
            this.GridLines = true;
            this.Location = new System.Drawing.Point(0, 0);
            this.MultiSelect = false;
            this.Name = "RentalsList";
            this.Size = new System.Drawing.Size(500, 470);
            this.TabIndex = 9;
            this.UseCompatibleStateImageBehavior = false;
            this.View = View.Details;
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

            // Initialize and add list groups.
            this.songListViewGroup = new ListViewGroup("Songs", HorizontalAlignment.Left);
            this.albumListViewGroup = new ListViewGroup("Albums", HorizontalAlignment.Left);
            this.movieListViewGroup = new ListViewGroup("Movies", HorizontalAlignment.Left);
            this.bookListViewGroup = new ListViewGroup("Books", HorizontalAlignment.Left);
            this.Groups.AddRange(
                new ListViewGroup[] { this.songListViewGroup, this.albumListViewGroup, this.movieListViewGroup, this.bookListViewGroup });
        }

        /// <summary>
        /// Gets and sets the instances RentItClient proxy instance.
        /// </summary>
        internal RentItClient RentItProxy { get; set; }

        /// <summary>
        /// Get the MediaInfo-object corresponding to the given ListViewItem-object.
        /// </summary>
        /// <param name="item"></param>
        /// <returns></returns>
        internal MediaInfo GetMediaInfoValueOf(ListViewItem item)
        {
            return (MediaInfo)item.Tag;
        }

        /// <summary>
        /// For setting whether the list is to show active rentals.
        /// True means that the list will display active rentals, false
        /// means that the list will display expired rentals.
        /// </summary>
        internal void ShowActive(bool active)
        {
            this.showActive = active;
            this.PopulateList(this.showActive ? this.activeRentals : this.expiredRentals);
        }

        /// <summary>
        /// Update the ListView with new data.
        /// When this method is called, all data previously added to the list 
        /// will be disregarded, and the contents of the ListView will match
        /// the media items passed in the parameter.
        /// </summary>
        /// <param name="rentals">
        /// The rentals that the list must display.
        /// </param>
        internal void UpdateListContents(List<Rental> rentals)
        {
            this.BeginUpdate();

            this.CurrentItems = new List<ListViewItem>();

            // Only active
            this.activeRentals = rentals.FindAll(rental => rental.EndTime > DateTime.Now);
            this.expiredRentals = rentals.FindAll(rental => rental.EndTime < DateTime.Now);

            this.PopulateList(this.showActive ? this.activeRentals : this.expiredRentals);
        }

        /// <summary>
        /// Helper method for populating the list with rentals.
        /// This method compiles a list of ListViewItems. The items
        /// are not added to the list; this is done in the Repopulate-method.
        /// </summary>
        /// <param name="rentals">
        /// The list of rentals that the list must display.
        /// </param>
        private void PopulateList(List<Rental> rentals)
        {
            try
            {
                foreach (var rental in rentals)
                {
                    switch (rental.MediaType)
                    {
                        case MediaType.Album:
                            AlbumInfo album = this.RentItProxy.GetAlbumInfo(rental.MediaId);

                            var listItemA = new ListViewItem(album.Title);
                            listItemA.SubItems.Add(rental.StartTime.ToShortDateString());
                            listItemA.SubItems.Add(rental.EndTime.ToShortDateString());
                            listItemA.Group = this.albumListViewGroup;
                            listItemA.Tag = album;

                            this.CurrentItems.Add(listItemA);
                            break;
                        case MediaType.Movie:
                            MovieInfo movie = this.RentItProxy.GetMovieInfo(rental.MediaId);

                            var listItemM = new ListViewItem(movie.Title);
                            listItemM.SubItems.Add(rental.StartTime.ToShortDateString());
                            listItemM.SubItems.Add(rental.EndTime.ToShortDateString());
                            listItemM.Group = this.movieListViewGroup;
                            listItemM.Tag = movie;

                            this.CurrentItems.Add(listItemM);
                            break;
                        case MediaType.Book:
                            BookInfo book = this.RentItProxy.GetBookInfo(rental.MediaId);

                            var listItemB = new ListViewItem(book.Title);
                            listItemB.SubItems.Add(rental.StartTime.ToShortDateString());
                            listItemB.SubItems.Add(rental.EndTime.ToShortDateString());
                            listItemB.Group = this.bookListViewGroup;
                            listItemB.Tag = book;

                            this.CurrentItems.Add(listItemB);
                            break;
                        default:
                            break;
                    }

                    this.RePopulateList();
                    this.EndUpdate();
                }
            }
            // The calls the service might throw exceptions.
            catch (Exception e)
            {
                // Inform the user.
                RentItMessageBox.ServerCommunicationError();
            }
        }
    }
}
