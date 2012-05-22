namespace ClientApp
{
    using System.ServiceModel;
    using System.Windows.Forms;

    using RentIt;

    /// <summary>
    /// The main application window with a top bar and a content control.
    /// Everything but media playback is displayed inside this window.
    /// </summary>
    internal partial class MainForm : Form
    {
        private readonly RentItClient rentItProxy;
        private AccountCredentials credentials;

        public MainForm()
        {
            InitializeComponent();

            var binding = new BasicHttpBinding();
            var address = new EndpointAddress("http://rentit.itu.dk/rentit01/RentItService.svc");
            rentItProxy = new RentItClient(binding, address);
            
            TopBar.RentItProxy = rentItProxy;
            TopBar.Credentials = credentials;
            TopBar.ContentChangeEvent += ChangeContent;
            TopBar.CredentialsChangeEvent += ChangeCredentials;
            TopBar.CreditsChangeEvent += ChangeCredits;

            Content = new MainScreen { RentItProxy = rentItProxy };
        }

        /// <summary>
        /// The top-most bar with shortcuts and a search box.
        /// </summary>
        internal TopBarControl TopBar
        {
            get { return topBarControl; }
        }

        /// <summary>
        /// The content of the window, below the top bar.
        /// </summary>
        private RentItUserControl Content
        {
            set
            {
                contentPane.Controls.Clear();
                if (value.RentItProxy == null)
                    value.RentItProxy = rentItProxy;
                if (value.Credentials == null)
                    value.Credentials = credentials;

                // Add EventHandlers.
                value.ContentChangeEvent += ChangeContent;
                value.CredentialsChangeEvent += ChangeCredentials;
                value.CreditsChangeEvent += ChangeCredits;

                value.Dock = DockStyle.Fill;
                contentPane.Controls.Add(value);
            }
        }

        #region EventHandlers

        /// <summary>
        /// Handles change requests to the content of the window to a new control.
        /// </summary>
        /// <param name="sender">The sender of the request.</param>
        /// <param name="args">The arguments specifying the new control.</param>
        private void ChangeContent(object sender, ContentChangeArgs args)
        {
            Content = args.NewControl;
            TopBar.Title = args.NewTitle;
        }

        /// <summary>
        /// Handles change requests to the current global credentials of the application.
        /// </summary>
        /// <param name="sender">The sender of the request.</param>
        /// <param name="args">The arguments specifying the new credentials.</param>
        private void ChangeCredentials(object sender, CredentialsChangeArgs args)
        {
            credentials = args.Credentials;

            Cursor.Current = Cursors.WaitCursor;
            RentItUserControl nextScreen = new MainScreen {
                                                              RentItProxy = this.rentItProxy,
                                                              Credentials = this.credentials
                                                          };
            ChangeContent(sender, new ContentChangeArgs(nextScreen, "RentIt"));
            Cursor.Current = Cursors.Default;

            TopBar.Credentials = this.credentials;
        }

        /// <summary>
        /// Handles change requests to the credits of the current user.
        /// </summary>
        /// <param name="sender">The sender of the request.</param>
        /// <param name="args">The arguments specifying the new amount of credits.</param>
        private void ChangeCredits(object sender, CreditsChangeArgs args)
        {
            TopBar.Credits = args.Credits;
        }

        #endregion

        /// <summary>
        /// Contains references to standard titles for the various RentItUserControls
        /// that are to be displayed in MainForm's content.
        /// </summary>
        internal static class Titles {
            public static string MainScreen = "RentIt";
            public static string UserRegistration = "Log in / Register new account";
            public static string MediaFrontpageBooks = "Books";
            public static string MediaFrontpageMovies = "Movies";
            public static string MediaFrontpageMusic = "Music";
            public static string SearchResults = "Search results";
            public static string MediaDetailsBook = "Book details";
            public static string MediaDetailsMovie = "Movie details";
            public static string MediaDetailsAlbum = "Album details";
            public static string PublisherAccountManagement = "Publisher management";
            public static string UserAccountManagement = "User management";
        }
    }
}
