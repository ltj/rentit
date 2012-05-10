// -----------------------------------------------------------------------
// <copyright file="DetailedMediaList.cs" company="">
// TODO: Update copyright text.
// </copyright>
// -----------------------------------------------------------------------

namespace ClientApp
{
    using System.Collections.Generic;
    using System.ServiceModel;
    using System.Windows.Forms;

    using RentIt;

    /// <author>Kenneth Søhrmann</author>
    /// <summary>
    /// TODO: Update summary.
    /// </summary>
    public class RentalsList : PagedListView
    {
        private ColumnHeader titleColumn;
        private ColumnHeader startDateColumn;
        private ColumnHeader endDateColumn;

        private ListViewGroup songListViewGroup;
        private ListViewGroup albumListViewGroup;
        private ListViewGroup movieListViewGroup;
        private ListViewGroup bookListViewGroup;

        private RentItClient serviceClient;

        /// <summary>
        /// Initializes a new instance of the 
        /// </summary>
        public RentalsList()
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

            // Initialize the service proxy client.
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

            this.Map = new Dictionary<ListViewItem, MediaInfo>();
            this.CurrentItems = new List<ListViewItem>();

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

                        this.CurrentItems.Add(listItemA); // this.Items.Add(listItem);
                        this.Map.Add(listItemA, album);
                        break;
                    case MediaType.Movie:
                        MovieInfo movie = this.serviceClient.GetMovieInfo(rental.MediaId);

                        var listItemM = new ListViewItem(movie.Title);
                        listItemM.SubItems.Add(rental.StartTime.ToShortDateString());
                        listItemM.SubItems.Add(rental.EndTime.ToShortDateString());
                        listItemM.Group = this.movieListViewGroup;

                        this.CurrentItems.Add(listItemM); // this.Items.Add(listItem);
                        this.Map.Add(listItemM, movie);
                        break;
                    case MediaType.Book:
                        BookInfo book = this.serviceClient.GetBookInfo(rental.MediaId);

                        var listItemB = new ListViewItem(book.Title);
                        listItemB.SubItems.Add(rental.StartTime.ToShortDateString());
                        listItemB.SubItems.Add(rental.EndTime.ToShortDateString());
                        listItemB.Group = this.bookListViewGroup;

                        this.CurrentItems.Add(listItemB); // this.Items.Add(listItem);
                        this.Map.Add(listItemB, book);
                        break;
                    default:
                        break;
                }

                this.RePopulateList();
                this.EndUpdate();
            }
        }
    }
}
