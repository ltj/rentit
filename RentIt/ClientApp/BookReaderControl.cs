namespace ClientApp
{
    using BinaryCommunicator;

    using RentIt;

    public partial class BookReaderControl : RentItUserControl
    {
        private BookInfo book;

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
    }
}
