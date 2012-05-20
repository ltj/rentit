
namespace ClientApp
{
    using System.Collections.Generic;
    using System.Windows.Forms;

    using RentIt;

    /// <author>Kenneth Søhrmann</author>
    /// <summary>
    /// This class represent the paged list used for listing user ratings of a particular
    /// media. The list has a user, date, comment and rating column.
    /// </summary>
    internal class PagedRatingsList : PagedListView
    {
        private ColumnHeader userColumn;
        private ColumnHeader dateColumn;
        private ColumnHeader commentColumn;
        private ColumnHeader ratingColumn;

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
            this.userColumn.Width = 154;
            // 
            // startDateColumn
            // 
            this.dateColumn.Text = "Date posted";
            this.dateColumn.Width = 118;
            // 
            // endDateColumn
            // 
            this.commentColumn.Text = "Comment/review";
            this.commentColumn.Width = 364;
            // 
            // priceColumn
            // 
            this.ratingColumn.Text = "Rating";
            this.ratingColumn.Width = 104;
        }

        /// <summary>
        /// Returns the single selected MediaReview.
        /// </summary>
        /// <returns>
        /// If the there is anymore og anyless than one review selected,
        /// a default value of the MediaReview class will be returned. If a 
        /// single MediaReview of the list is selected, this instance is returned.
        /// </returns>
        internal MediaReview GetSingleReview()
        {
            if (this.SelectedItems.Count != 1)
            {
                return default(MediaReview);
            }

            ListViewItem selectedItem = this.SelectedItems[0];
            return (MediaReview)selectedItem.Tag;
        }

        /// <summary>
        /// Update the ListView with new data.
        /// When this method is called, all data previously added to the list 
        /// will be disregarded, and the contents of the ListView will match
        /// the media items passed in the parameter.
        /// </summary>
        /// <param name="reviews">
        /// The list of reviews to be added to the list.
        /// </param>
        internal void UpdateListContents(MediaReview[] reviews)
        {
            this.BeginUpdate();
            this.CurrentItems = new List<ListViewItem>();

            foreach (var review in reviews)
            {
                string reviewText = review.ReviewText.Length > 70
                                        ? review.ReviewText.Substring(0, 70) + " ..."
                                        : review.ReviewText;

                var item = new ListViewItem(review.UserName);
                item.SubItems.Add(review.Timestamp.Date.ToString("dd/MM/yyyy HH:mm"));
                item.SubItems.Add(reviewText);
                item.SubItems.Add(review.Rating.ToString());
                item.Tag = review;

                this.CurrentItems.Add(item);
            }

            this.RePopulateList();
            this.EndUpdate();
        }
    }
}
