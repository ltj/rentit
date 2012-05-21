namespace ClientApp
{
    using System.Windows.Forms;

    using BinaryCommunicator;

    using RentIt;

    /// <auhtor>Per Mortensen</auhtor>
    /// <summary>
    /// This UserControl is used for displaying a rented book.
    /// </summary>
    internal partial class BookReaderControl : RentItUserControl
    {
        /// <summary>
        /// The BookInfo-instance representing the metadata of the book
        /// currently view in this BookReaderControl-instance.
        /// </summary>
        private BookInfo book;

        /// <summary>
        /// The BookMovieDetails instance that displays the metadata of the played
        /// movie. Is displayed in the MainForm whenever the user clicks the Book 
        /// details-button.
        /// </summary>
        private BookMovieDetails bookDetails;

        /// <summary>
        /// Initializes a new instance of the BookReaderControl class.
        /// </summary>
        public BookReaderControl()
        {
            InitializeComponent();
        }

        /// <summary>
        /// Sets the BookInfo-object that the book to be displayed in the
        /// UserControl is based upon.
        /// </summary>
        internal BookInfo Book
        {
            set
            {
                titleLabel.Text = value.Title;
                book = value;
            }
        }

        /// <summary>
        /// Streams and displays the rented book in the reader control.
        /// </summary>
        internal void Start()
        {
            var c = new Credentials(Credentials.UserName, Credentials.HashedPassword);

            string filePath = BinaryCommuncator.DownloadMediaURL(c, this.book.Id).ToString();

            this.pdfReader.src = filePath;
            this.pdfReader.setShowToolbar(false);
            this.pdfReader.setView("FitH");
            // this.pdfReader.setLayoutMode("SinglePage");
            this.pdfReader.Show();
        }

        #region Controllers

        /// <author>Kenneth Søhrmann</author>
        /// <summary>
        /// EventHandler for the Book details-button. Loads an BookMovieDetails-instance
        /// into the MainForm displaying the metadata of the book.
        /// </summary>
        private void viewBookDetails_Click(object sender, System.EventArgs e)
        {
            Cursor.Current = Cursors.WaitCursor;
            if (this.bookDetails == null)
            {
                this.bookDetails = new BookMovieDetails()
                {
                    RentItProxy = this.RentItProxy,
                    Credentials = this.Credentials,
                    BookInfo = this.book
                };
            }

            this.FireContentChangeEvent(bookDetails, MainForm.Titles.MediaDetailsBook);
            Cursor.Current = Cursors.Default;
        }

        #endregion
    }
}
