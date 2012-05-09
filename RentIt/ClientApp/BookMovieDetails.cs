using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace ClientApp
{
    using System.ServiceModel;

    using RentIt;

    public partial class BookMovieDetails : UserControl
    {

        private RentItClient serviceClient;

        public BookMovieDetails()
        {
            InitializeComponent();

            BasicHttpBinding binding = new BasicHttpBinding();
            EndpointAddress address = new EndpointAddress("http://rentit.itu.dk/rentit01/RentItService.svc");
            serviceClient = new RentItClient(binding, address);


            this.BookInfo = this.serviceClient.GetBookInfo(75);
        }

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

        internal MovieInfo MovieInfo
        {
            set
            {
                MovieInfo movie = value;

                this.mediaSideBar.MediaInfoData = movie;
                this.albumTitleLabel.Text = movie.Title;
                this.authorLabel.Text = movie.Director;

                // this.albumDurationValueLabel.Text = album.TotalDuration.ToString();

                this.bookDescriptionTextBox.Text = movie.Summary;
                this.bookRatingList.Media = movie;
            }
        }
    }
}
