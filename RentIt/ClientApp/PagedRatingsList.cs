// -----------------------------------------------------------------------
// <copyright file="PagedRatingsList.cs" company="">
// TODO: Update copyright text.
// </copyright>
// -----------------------------------------------------------------------

namespace ClientApp
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.ServiceModel;
    using System.Text;
    using System.Windows.Forms;

    using RentIt;

    /// <summary>
    /// TODO: Update summary.
    /// </summary>
    public class PagedRatingsList : PagedListView
    {
        private ColumnHeader userColumn;
        private ColumnHeader dateColumn;
        private ColumnHeader commentColumn;
        private ColumnHeader ratingColumn;

        //private ListViewGroup songListViewGroup;
        //private ListViewGroup albumListViewGroup;
        //private ListViewGroup movieListViewGroup;
        //private ListViewGroup bookListViewGroup;

        private MediaInfo media;

        private RentItClient serviceClient;

        /// <summary>
        /// Initializes a new instance of the 
        /// </summary>
        public PagedRatingsList()
        {
            // Initialze columns
            this.userColumn = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.dateColumn = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.commentColumn = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.ratingColumn = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));

            this.AutoArrange = false;
            this.Columns.AddRange(new System.Windows.Forms.ColumnHeader[] {
                this.userColumn,
                this.dateColumn,
                this.commentColumn,
                this.ratingColumn
            });
            // this.Dock = System.Windows.Forms.DockStyle.Fill;
            this.FullRowSelect = true;
            this.GridLines = true;
            this.Location = new System.Drawing.Point(0, 0);
            this.MultiSelect = false;
            this.Name = "RatingsList";
            this.Size = new System.Drawing.Size(500, 470);
            this.TabIndex = 9;
            this.UseCompatibleStateImageBehavior = false;
            this.View = System.Windows.Forms.View.Details;
            // 
            // titleColumn
            // 
            this.userColumn.Text = "User";
            this.userColumn.Width = 220;
            // 
            // startDateColumn
            // 
            this.dateColumn.Text = "Date posted";
            this.dateColumn.Width = 120;
            // 
            // endDateColumn
            // 
            this.commentColumn.Text = "Comment/review";
            this.commentColumn.Width = 120;
            // 
            // priceColumn
            // 
            this.ratingColumn.Text = "Rating";
            this.ratingColumn.Width = 85;

            // Initialize and add list groups.
            // this.songListViewGroup = new ListViewGroup("Songs", HorizontalAlignment.Left);
            // this.albumListViewGroup = new ListViewGroup("Albums", HorizontalAlignment.Left);
            // this.movieListViewGroup = new ListViewGroup("Movies", HorizontalAlignment.Left);
            // this.bookListViewGroup = new ListViewGroup("Books", HorizontalAlignment.Left);
            // this.Groups.AddRange(
            //     new ListViewGroup[] { this.songListViewGroup, this.albumListViewGroup, this.movieListViewGroup, this.bookListViewGroup });

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
        internal void UpdateListContents(MediaReview[] rentals)
        {
            this.BeginUpdate();
            this.CurrentItems = new List<ListViewItem>();

            foreach (var review in rentals)
            {
                var item = new ListViewItem(review.UserName);
                item.SubItems.Add(review.Timestamp.Date.ToString("dd/MM/yyyy HH:mm"));
                item.SubItems.Add(review.ReviewText);
                item.SubItems.Add(review.Rating.ToString());

                this.CurrentItems.Add(item); // this.Items.Add(listItem);
            }

            this.RePopulateList();
            this.EndUpdate();
        }

    }
}
