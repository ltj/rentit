﻿namespace ClientApp
{
    using System.Collections.Generic;

    using RentIt;

    /// <author>Kenneth Søhrmann</author>
    /// <summary>
    /// The UserControl is used for displayed metadata of either a book or a movie.
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

            // Subscribe to the inner UserControls' events in order to propagate to the MainForm's
            // subscribtions.
            foreach (var control in innerControls)
            {
                control.CredentialsChangeEvent += this.CredentialsChangeEventPropagated;
                control.ContentChangeEvent += this.ContentChangeEventPropagated;
                control.CreditsChangeEvent += this.CreditsChangeEventPropagated;
            }
        }

        /// <summary>
        /// Overridden so that it sets the RentItProxy instance of the
        /// inner UserControls and components as well.
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
                this.bookRatingList.RentItProxy = value;
            }
        }

        /// <summary>
        /// Overridden so that it sets the Credentials property of the 
        /// inner UserControls and components as well.
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
                this.bookRatingList.Credentials = value;
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

                // Forward to alsoRentedList.
                this.alsoRentedList.PrioritizedMediaType = book.Type;
                this.alsoRentedList.MediaId = book.Id;

                // Forward to mediaSideBar instance.
                this.mediaSideBar.MediaInfoData = book;

                // Change control labels and text boxes.
                this.albumTitleLabel.Text = book.Title;
                this.authorLabel.Text = book.Author;
                this.bookDescriptionTextBox.Text = book.Summary;

                // Forward to rating list.
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

                // Forward to alsoRentedList.
                this.alsoRentedList.PrioritizedMediaType = movie.Type;
                this.alsoRentedList.MediaId = movie.Id;

                // Forward to mediaSideBar instance.
                this.mediaSideBar.MediaInfoData = movie;

                // Change control labels and text boxes.
                this.albumTitleLabel.Text = movie.Title;
                this.authorLabel.Text = movie.Director;
                this.bookDescriptionTextBox.Text = movie.Summary;

                // Forward to rating list.
                this.bookRatingList.Media = movie;
            }
        }
    }
}
