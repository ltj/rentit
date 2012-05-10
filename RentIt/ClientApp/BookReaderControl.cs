namespace ClientApp {
    using BinaryCommunicator;

    using RentIt;

    public partial class BookReaderControl : RentItUserControl {
        private BookInfo book;

        public BookReaderControl() {
            InitializeComponent();
        }

        internal BookInfo Book {
            set {
                titleLabel.Text = value.Title;
                book = value;
            }
        }

        internal void Start() {
            var c = new Credentials(Credentials.UserName, Credentials.HashedPassword);
            pdfReader.LoadFile(BinaryCommuncator.DownloadMediaURL(c, book.Id).ToString());
        }
    }
}
