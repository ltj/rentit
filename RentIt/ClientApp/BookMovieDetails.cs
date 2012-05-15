
namespace ClientApp
{
    using System.ServiceModel;
    using System.Windows.Forms;

    using RentIt;

    /// <author>Kenneth Søhrmann</author>
    /// <summary>
    /// 
    /// </summary>
    internal partial class BookMovieDetails : RentItUserControl
    {
        /// <summary>
        /// Initializes a new instance of the BookMovieDetails class.
        /// </summary>
        public BookMovieDetails()
        {
            InitializeComponent();

            //BasicHttpBinding binding = new BasicHttpBinding();
            //EndpointAddress address = new EndpointAddress("http://rentit.itu.dk/rentit01/RentItService.svc");
            //this.RentItProxy = new RentItClient(binding, address);

            //this.BookInfo = this.RentItProxy.GetBookInfo(75);
        }

        /// <summary>
        /// Overridden so that is sets the Credentials property of the 
        /// MediaSideBar instance as well.
        /// </summary>
        internal override AccountCredentials Credentials
        {
            get
            {
                return base.Credentials;
            }
            set
            {
                base.Credentials = value;
                this.mediaSideBar.Credentials = value;
            }
        }

        /// <summary>
        /// Sets the BookInfo instance whoose metadata and
        /// ratings are to be displayed on the UserControl.
        /// </summary>
        internal BookInfo BookInfo
        {
            set
            {
                BookInfo book = value;
                this.mediaSideBar.MediaInfoData = book;

                this.albumTitleLabel.Text = book.Title;
                this.authorLabel.Text = book.Author;
                this.bookDescriptionTextBox.Text = book.Summary;

                this.bookRatingList.Media = book;
            }
        }

        /// <summary>
        /// Sets the MovieInfo instance whoose metadata and
        /// ratings are to be displayed on the UserControl.
        /// </summary>
        internal MovieInfo MovieInfo
        {
            set
            {
                MovieInfo movie = value;
                this.mediaSideBar.MediaInfoData = movie;

                this.albumTitleLabel.Text = movie.Title;
                this.authorLabel.Text = movie.Director;
                this.bookDescriptionTextBox.Text = movie.Summary;

                this.bookRatingList.Media = movie;
            }
        }
    }
}
