// -----------------------------------------------------------------------
// <copyright file="PagedRatingsList.cs" company="">
// TODO: Update copyright text.
// </copyright>
// -----------------------------------------------------------------------

namespace ClientApp
{
    using System.Collections.Generic;
    using System.Windows.Forms;

    using RentIt;

    /// <author>Kenneth Søhrmann</author>
    /// <summary>
    /// TODO: Update summary.
    /// </summary>
    public class PagedRatingsList : PagedListView
    {
        private ColumnHeader userColumn;
        private ColumnHeader dateColumn;
        private ColumnHeader commentColumn;
        private ColumnHeader ratingColumn;

        private MediaInfo media;

        /// <summary>
        /// Initializes a new instance of the PagedRatingsList class.
        /// </summary>
        public PagedRatingsList()
        {
            // Initialze columns
            this.userColumn = new ColumnHeader();
            this.dateColumn = new ColumnHeader();
            this.commentColumn = new ColumnHeader();
            this.ratingColumn = new ColumnHeader();

            this.AutoArrange = false;
            this.Columns.AddRange(new ColumnHeader[] {
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
            this.View = View.Details;
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
