﻿
namespace ClientApp
{
    using System.Collections.Generic;
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

            // Subscribe the propagated EventHandlers to the control's subcontrols
            List<RentItUserControl> innerControls = new List<RentItUserControl>()
                {
                    this.alsoRentedList, this.mediaSideBar, this.bookRatingList
                };

            foreach (var control in innerControls)
            {
                control.CredentialsChangeEvent += this.CredentialsChangeEventPropagated;
                control.ContentChangeEvent += this.ContentChangeEventPropagated;
                control.CreditsChangeEvent += this.CreditsChangeEventPropagated;
            }
        }

        /// <summary>
        /// Overridden so that it sets the RentItProxy instance of the
        /// MediaSideBar instance as well.
        /// </summary>
        internal override RentItClient RentItProxy
        {
            get
            {
                return base.RentItProxy;
            }
            set
            {
                base.RentItProxy = value;
                this.alsoRentedList.RentItProxy = value;
                this.mediaSideBar.RentItProxy = value;
            }
        }

        /// <summary>
        /// Overridden so that it sets the Credentials property of the 
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
                this.alsoRentedList.Credentials = value;
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

                this.alsoRentedList.PrioritizedMediaType = book.Type;
                this.alsoRentedList.MediaId = book.Id;

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

                this.alsoRentedList.PrioritizedMediaType = movie.Type;
                this.alsoRentedList.MediaId = movie.Id;

                this.mediaSideBar.MediaInfoData = movie;

                this.albumTitleLabel.Text = movie.Title;
                this.authorLabel.Text = movie.Director;
                this.bookDescriptionTextBox.Text = movie.Summary;

                this.bookRatingList.Media = movie;
            }
        }
    }
}
