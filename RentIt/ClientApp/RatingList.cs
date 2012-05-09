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

        internal MediaInfo Media {
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
            reviewList.MediaItems = Media;
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

            // reload the media to list newly submitted review and update average rating
            switch(media.Type) {
                case MediaType.Book:
                    Media = rentit.GetBookInfo(media.Id);
                    break;
                case MediaType.Movie:
                    Media = rentit.GetMovieInfo(media.Id);
                    break;
                case MediaType.Album:
                    Media = rentit.GetMovieInfo(media.Id);
                    break;
                default:
                    throw new ArgumentOutOfRangeException();
            }
        }
    }
}
