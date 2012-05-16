namespace ClientApp
{
    using System;
    using System.ServiceModel;
    using System.Windows.Forms;
    using RentIt;

    internal partial class TopBarControl : RentItUserControl
    {
        public TopBarControl()
        {
            InitializeComponent();
            Title = Titles.MainScreen;
            TypeComboBox.SelectedIndex = 0;
            yourMediaButton.Enabled = false;
        }

        private bool isPublisher;

        /// <summary>
        /// The title of the top bar.
        /// </summary>
        public string Title
        {
            get { return TitleLabel.Text; }
            set { TitleLabel.Text = value; }
        }

        internal override AccountCredentials Credentials {
            set {
                base.Credentials = value;
                if(value != null) {
                    UserNameLabel.Text = value.UserName;
                    LoggedIn = true;
                }
            }
        }

        /// <summary>
        /// Sets whether the top bar should display user information
        /// or not, as well as "log in" / "log out".
        /// </summary>
        private bool LoggedIn
        {
            get { return loggedIn; }
            set
            {
                loggedIn = value;
                UserNameLabel.Visible = value;
                LogInLogOutButton.Text = value ? "Log out" : "Log in/register";
                yourMediaButton.Enabled = value;
                
                try {
                    UserAccount account = RentItProxy.GetAllCustomerData(Credentials);
                    string credits = account.Credits > 0 ? account.Credits.ToString() : "no";
                    creditsLabel.Text = "(" + credits + " credits)";
                    creditsLabel.Visible = value;
                    isPublisher = false;
                } catch(FaultException) {
                    try {
                        RentItProxy.GetAllPublisherData(Credentials);
                        isPublisher = true;
                    } catch(FaultException) {}
                }
            }
        }
        private bool loggedIn;

        private void Search()
        {
            var comboBoxType = (string)TypeComboBox.SelectedItem;

            MediaType mediaType;
            if (comboBoxType.Equals("Movies"))
                mediaType = MediaType.Movie;
            else if (comboBoxType.Equals("Music"))
                mediaType = MediaType.Album;
            else if (comboBoxType.Equals("Books"))
                mediaType = MediaType.Book;
            else
                mediaType = MediaType.Any;

            var criteria = new MediaCriteria
            {
                SearchText = SearchTextBox.Text,
                Genre = "",
                Type = mediaType,
                Limit = -1,
                Offset = 0,
                Order = MediaOrder.AlphabeticalAsc
            };

            Cursor.Current = Cursors.WaitCursor;
            // switch to search results, using criteria object
            var search = new SearchResultsControl { RentItProxy = RentItProxy, Criteria = criteria };
            //(ParentForm as MainForm).Content = search;
            FireContentChangeEvent(search, Titles.SearchResults);
            Cursor.Current = Cursors.Default;
        }

        private void SearchButtonClick(object sender, EventArgs e)
        {
            Search();
        }

        private void LogInLogOutButtonClick(object sender, EventArgs e)
        {
            if (!LoggedIn)
            {
                // go to log in screen
                FireContentChangeEvent(new UserRegistration { RentItProxy = RentItProxy }, Titles.UserRegistration);
            }
            else
            {
                // go to main screen
                LoggedIn = false;
                FireContentChangeEvent(new MainScreen { RentItProxy = RentItProxy }, Titles.MainScreen);
            }
        }

        private void HomeButtonClick(object sender, EventArgs e) {
            FireContentChangeEvent(new MainScreen { RentItProxy = RentItProxy }, Titles.MainScreen);
        }

        private void MovieButtonClick(object sender, EventArgs e) {
            FireContentChangeEvent(new MediaFrontpage { RentItProxy = RentItProxy, Mtype = MediaType.Movie}, Titles.MediaFrontpageMovies);
        }

        private void MusicButtonClick(object sender, EventArgs e) {
            FireContentChangeEvent(new MediaFrontpage { RentItProxy = RentItProxy, Mtype = MediaType.Album }, Titles.MediaFrontpageMusic);
        }

        private void BookButtonClick(object sender, EventArgs e) {
            FireContentChangeEvent(new MediaFrontpage { RentItProxy = RentItProxy, Mtype = MediaType.Book }, Titles.MediaFrontpageBooks);
        }

        private void UserNameLabelLinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            if (!LoggedIn)
            {
                RentItMessageBox.NotLoggedIn();
                return;
            }

            // go to account screen (check whether publisher or user)
            if(isPublisher) { //if publisher
                var pubMan = new PublisherAccountManagement { RentItProxy = RentItProxy, Credentials = Credentials };
                FireContentChangeEvent(pubMan, Titles.PublisherAccountManagement);
            }
            else {
                var userMan = new UserAccountManagement { RentItProxy = RentItProxy, Credentials = Credentials };
                FireContentChangeEvent(userMan, Titles.UserAccountManagement);
            }
        }

        private void SearchTextBoxKeyPressed(object sender, KeyEventArgs keyEventArgs)
        {
            if (keyEventArgs.KeyCode == Keys.Enter)
            {
                keyEventArgs.Handled = true;
                Search();
            }
        }

        private void YourMediaButtonClick(object sender, EventArgs e)
        {
            // go to active rentals list if user
            // go to published media list if publisher

            if(isPublisher) { //if publisher
                var pubMan = new PublisherAccountManagement { RentItProxy = RentItProxy, Credentials = Credentials };
                pubMan.SelectTab(1);
                FireContentChangeEvent(pubMan, Titles.PublisherAccountManagement);
            }
            else {
                var userMan = new UserAccountManagement { RentItProxy = RentItProxy, Credentials = Credentials };
                userMan.SelectTab(1);
                FireContentChangeEvent(userMan, Titles.UserAccountManagement);
            }
        }

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
