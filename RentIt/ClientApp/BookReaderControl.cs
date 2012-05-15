namespace ClientApp
{
    using BinaryCommunicator;

    using RentIt;

    internal partial class BookReaderControl : RentItUserControl
    {
        private BookInfo book;

        private BookMovieDetails bookDetails;

        public BookReaderControl()
        {
            InitializeComponent();
        }

        internal BookInfo Book
        {
            set
            {
                titleLabel.Text = value.Title;
                book = value;
            }
        }

        internal void Start()
        {
            var c = new Credentials(Credentials.UserName, Credentials.HashedPassword);

            string filePath = BinaryCommuncator.DownloadMediaURL(c, book.Id).ToString();

            this.pdfReader.src = filePath;
            this.pdfReader.setShowToolbar(false);
            this.pdfReader.setView("FitH");
            // this.pdfReader.setLayoutMode("SinglePage");
            this.pdfReader.Show();
        }

        #region Controllers

        /// <author>Kenneth Søhrmann</author>
        /// <summary>
        /// 
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void viewBookDetails_Click(object sender, System.EventArgs e)
        {
            if (this.bookDetails == null)
            {
                this.bookDetails = new BookMovieDetails()
                {
                    RentItProxy = this.RentItProxy,
                    Credentials = this.Credentials,
                    BookInfo = this.book
                };
            }

            this.FireContentChangeEvent(bookDetails, this.book.Title);
        }

        #endregion
    }
}
