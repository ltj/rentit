namespace ClientApp {
    using System;
    using System.ServiceModel;
    using System.Windows.Forms;

    using RentIt;

    public partial class RatingList : UserControl {
        private MediaInfo media;
        private readonly RentItClient rentit;

        public RatingList() {
            InitializeComponent();
            RatingSelector.SelectedIndex = 2;
            var binding = new BasicHttpBinding();
            var address = new EndpointAddress("http://rentit.itu.dk/rentit01/RentItService.svc");
            rentit = new RentItClient(binding, address);
        }

        internal MediaInfo MediaInfo {
            get { return media; }
            set {
                media = value;
                UpdateAvgRating();
                PopulateList();
            }
        }

        private void UpdateAvgRating() {
            AvgRating.Text = media.Rating.AverageRating.ToString();
            AvgRatingCount.Text = media.Rating.RatingsCount.ToString();
        }

        private void PopulateList() {
            foreach(var review in media.Rating.Reviews) {
                var item = new ListViewItem(review.UserName);
                item.SubItems.Add(review.Timestamp.Date.ToString());
                item.SubItems.Add(review.ReviewText);
                item.SubItems.Add(review.Rating.ToString());
                ReviewList.Items.Add(item);
            }
        }

        private void SubmitReviewButtonClick(object sender, EventArgs e) {
            //TODO: get correct credentials from somewhere
            var credentials = new AccountCredentials {
                UserName = "per",
                HashedPassword = "7110eda4d09e062aa5e4a390b0a572ac0d2c0220"
            };

            Rating rating;
            switch(RatingSelector.SelectedIndex) {
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

            var review = new MediaReview {
                                             MediaId = media.Id,
                                             Rating = rating,
                                             ReviewText = ReviewText.Text,
                                             Timestamp = DateTime.Now,
                                             UserName = credentials.UserName
                                         };

            rentit.SubmitReview(review, credentials);
        }
    }
}
