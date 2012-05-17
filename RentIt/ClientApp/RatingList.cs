namespace ClientApp
{
    using System;
    using System.ServiceModel;
    using System.Windows.Forms;

    using RentIt;

    internal partial class RatingList : RentItUserControl
    {
        private MediaInfo media;

        public RatingList()
        {
            InitializeComponent();
            RatingSelector.SelectedIndex = 2;
        }

        internal MediaInfo Media
        {
            get { return media; }
            set
            {
                Cursor.Current = Cursors.WaitCursor;
                media = value;
                UpdateAvgRating();
                PopulateList();
                Cursor.Current = Cursors.Default;
            }
        }

        private void UpdateAvgRating()
        {
            AvgRating.Text = media.Rating.AverageRating.ToString();
            AvgRatingCount.Text = media.Rating.RatingsCount.ToString();
        }

        private void PopulateList()
        {
            reviewList.MediaItems = Media;
        }

        private void SubmitReviewButtonClick(object sender, EventArgs e)
        {
            if(Credentials == null)
                return;

            Rating rating;
            switch (RatingSelector.SelectedIndex)
            {
                case 0:
                    rating = RentIt.Rating.One;
                    break;
                case 1:
                    rating = RentIt.Rating.Two;
                    break;
                case 2:
                    rating = RentIt.Rating.Three;
                    break;
                case 3:
                    rating = RentIt.Rating.Four;
                    break;
                case 4:
                    rating = RentIt.Rating.Five;
                    break;
                default:
                    rating = RentIt.Rating.Three;
                    break;
            }

            var review = new MediaReview
            {
                MediaId = Media.Id,
                Rating = rating,
                ReviewText = ReviewText.Text,
                Timestamp = DateTime.Now,
                UserName = Credentials.UserName
            };

            // submit review
            Cursor.Current = Cursors.WaitCursor;
            try
            {
                RentItProxy.SubmitReview(review, Credentials);
            }
            catch (FaultException)
            {
                // if review was already submitted by this user
                RentItMessageBox.AlreadyReviewedItem();
                return;
            }

            // reload list
            ReloadList();
            Cursor.Current = Cursors.Default;
        }

        /// <summary>
        /// Reload the media to list newly submitted review
        /// and update average rating.
        /// </summary>
        private void ReloadList()
        {
            switch (Media.Type)
            {
                case MediaType.Book:
                    Media = RentItProxy.GetBookInfo(media.Id);
                    break;
                case MediaType.Movie:
                    Media = RentItProxy.GetMovieInfo(media.Id);
                    break;
                case MediaType.Album:
                    Media = RentItProxy.GetMovieInfo(media.Id);
                    break;
                default:
                    throw new ArgumentOutOfRangeException();
            }
        }
    }
}
