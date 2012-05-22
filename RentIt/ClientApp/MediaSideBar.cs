
namespace ClientApp
{
    using System;
    using System.Linq;
    using System.Windows.Forms;

    using BinaryCommunicator;

    using RentIt;

    /// <author>Kenneth Søhrmann</author>
    /// <summary>
    /// The class represents the side bar that is visible on all the UserControls
    /// that displays media metadata (namely AlbumDetails and BookMovieDetails).
    /// The side bar displays the media thumbnail, provides a button that enables the
    /// user to rent the media, and a group of labels that displays metadata such as 
    /// author, release date and publisher of the media.
    /// </summary>
    internal partial class MediaSideBar : RentItUserControl
    {
        /// <summary>
        /// The MediaInfo instance which metadata is being displayed
        /// on the MediaSideBar.
        /// </summary>
        private MediaInfo mediaInfo;

        /// <summary>
        /// Initializes a new instance of the MediaSideBar class.
        /// </summary>
        public MediaSideBar()
        {
            InitializeComponent();
        }

        /// <summary>
        /// Sets the control to display the metadata of the submitted
        /// MediaInfo object.
        /// </summary>
        internal MediaInfo MediaInfoData
        {
            set
            {
                var mediaInfo = value;

                this.mediaInfo = mediaInfo;

                // Update label texts.
                this.priceLabel.Text = mediaInfo.Price + " credits.";
                this.genreValueLabel.Text = mediaInfo.Genre;
                this.releaseDateValueLabel.Text = mediaInfo.ReleaseDate.ToShortDateString();
                this.publisherValueLabel.Text = mediaInfo.Publisher;

                this.originLabel.Visible = true;
                this.originValueLabel.Visible = true;
                this.lengthLabel.Visible = true;
                this.lengthValueLabel.Visible = true;

                // Based upon the type of the submitted MediaInfo, set 
                // the texts of the originLabel, originValueLabel,
                // lengthLabel and lengthValueLabel of the side bar.
                switch (mediaInfo.Type)
                {
                    case MediaType.Album:
                        var albumInfo = (AlbumInfo)mediaInfo;
                        this.originLabel.Text = "Album artist:";
                        this.originValueLabel.Text = albumInfo.AlbumArtist;
                        this.lengthLabel.Text = "Total duration:";
                        this.lengthValueLabel.Text = albumInfo.TotalDuration.ToString();
                        break;
                    case MediaType.Book:
                        var bookInfo = (BookInfo)mediaInfo;
                        this.originLabel.Text = "Author:";
                        this.originValueLabel.Text = bookInfo.Author;
                        this.lengthLabel.Text = "Number of pages:";
                        this.lengthValueLabel.Text = bookInfo.Pages.ToString();
                        break;
                    case MediaType.Song:
                        var songInfo = (SongInfo)mediaInfo;
                        this.originLabel.Text = "Artist";
                        this.originValueLabel.Text = songInfo.Artist;
                        this.lengthLabel.Text = "Song duration:";
                        this.lengthValueLabel.Text = songInfo.Duration.ToString();
                        break;
                    case MediaType.Movie:
                        var movieInfo = (MovieInfo)mediaInfo;
                        this.originLabel.Text = "Director";
                        this.originValueLabel.Text = movieInfo.Director;
                        this.lengthLabel.Text = "Duration:";
                        this.lengthValueLabel.Text = movieInfo.Duration.ToString();
                        break;
                    default:
                        this.originLabel.Visible = false;
                        this.originValueLabel.Visible = false;
                        this.lengthLabel.Visible = false;
                        this.lengthValueLabel.Visible = false;
                        break;
                }

                this.thumbnailBox.Image = BinaryCommuncator.GetThumbnail(mediaInfo.Id);
                this.DetermineButton(value.Id);
            }
        }

        /// <summary>
        /// This method determines the nature of the rent button:
        /// If no credentials are submitted, a text on button will tell the
        /// user to log in first. If credentials are submitted it is determined if
        /// the user alreadey has a active rental or not.
        /// </summary>
        /// <param name="mediaId"></param>
        private void DetermineButton(int mediaId)
        {
            if (this.Credentials == null)
            {
                this.rentButton.Enabled = false;
                this.rentButton.Text = "Login to rent";
                return;
            }

            UserAccount account;

            try
            {
                account = this.RentItProxy.GetAllCustomerData(this.Credentials);
            }
            // An error communicating with the server occured or the signed in user is
            // publisher.
            catch (Exception)
            {
                this.rentButton.Enabled = false;
                this.rentButton.Text = "Rent It";
                return;
            }

            bool alreadyRented = account.Rentals.Where(
                rental => rental.MediaId == mediaId && rental.EndTime > DateTime.Now).Any();

            rentButton.Enabled = !alreadyRented;
            rentButton.Text = alreadyRented ? "Already rented" : "Rent It";
        }

        #region Controllers

        /// <summary>
        /// Controller for the Rent-button.
        /// If credentials have been submitted, the rental is reqistered at 
        /// the service if the user has sufficient credits fundings.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void rentButton_Click(object sender, System.EventArgs e)
        {
            if (this.Credentials == null)
            {
                RentItMessageBox.NotLoggedIn();
                return;
            }

            Cursor.Current = Cursors.WaitCursor;

            try
            {
                RentItProxy.RentMedia(this.mediaInfo.Id, this.Credentials);
            }
            // Rental of media failed, inform the user.
            catch (Exception)
            {
                RentItMessageBox.RentalFailed();
                Cursor.Current = Cursors.Default;
                return;
            }

            try
            {
                int newCredits = RentItProxy.GetAllCustomerData(Credentials).Credits;
                FireCreditsChangeEvent(newCredits);
                RentItMessageBox.SuccesfulRental();
            }
            catch (Exception)
            {
                RentItMessageBox.ServerCommunicationError();
            }

            Cursor.Current = Cursors.Default;
        }

        #endregion
    }
}
