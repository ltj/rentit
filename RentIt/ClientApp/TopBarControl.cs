namespace ClientApp
{
    using System;
    using System.Windows.Forms;
    using RentIt;

    internal partial class TopBarControl : RentItUserControl
    {
        public TopBarControl()
        {
            InitializeComponent();
            Title = "RentIt";
            TypeComboBox.SelectedIndex = 0;
        }

        /// <summary>
        /// The title of the top bar.
        /// </summary>
        public string Title
        {
            get { return TitleLabel.Text; }
            set { TitleLabel.Text = value; }
        }

        /// <summary>
        /// The user name displayed on the top bar.
        /// Setter sets the displayed name and changes the log in/log out button text.
        /// An empty string ("") will be considered as "user not logged in".
        /// </summary>
        public string UserName
        {
            get { return UserNameLabel.Text; }
            set
            {
                UserNameLabel.Text = value;

            }
        }

        /// <summary>
        /// The credits displayed on the top bar.
        /// Setter sets the amount of credits displayed.
        /// If credits are less than or equal to0,
        /// "no credits" will be displayed.
        /// </summary>
        public int Credits
        {
            set
            {
                string credits = value > 0 ? value.ToString() : "no";
                creditsLabel.Text = "(" + credits + " credits)";
            }
        }

        /// <summary>
        /// Sets whether the top bar should display user information
        /// or not, as well as "log in" / "log out".
        /// </summary>
        public bool LoggedIn
        {
            get { return loggedIn; }
            set
            {
                loggedIn = value;
                UserNameLabel.Visible = value;
                creditsLabel.Visible = value;
                LogInLogOutButton.Text = value ? "Log out" : "Log in/register";
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

            // switch to search results, using criteria object
            var search = new SearchResultsControl { RentItProxy = RentItProxy, Criteria = criteria };
            //(ParentForm as MainForm).Content = search;
            FireContentChangeEvent(search, "Search results");
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
                FireContentChangeEvent(new UserRegistration(), "Register user account");

                //only update the following when logged in
                UserName = "skinkehyllebanke";
                Credits = 650;
                LoggedIn = true;
            }
            else
            {
                // log the user out
                UserName = "";
                Credits = 0;
                LoggedIn = false;
                //TODO: if the user logs out while on a user page, reset to main screen
            }
        }

        private void HomeButtonClick(object sender, EventArgs e) {
            FireContentChangeEvent(new MainScreen { RentItProxy = RentItProxy }, "RentIt");
        }

        private void MovieButtonClick(object sender, EventArgs e) {
            FireContentChangeEvent(new MediaFrontpage { RentItProxy = RentItProxy, Mtype = MediaType.Movie}, "Movies");
        }

        private void MusicButtonClick(object sender, EventArgs e) {
            FireContentChangeEvent(new MediaFrontpage { RentItProxy = RentItProxy, Mtype = MediaType.Album }, "Music");
        }

        private void BookButtonClick(object sender, EventArgs e) {
            FireContentChangeEvent(new MediaFrontpage { RentItProxy = RentItProxy, Mtype = MediaType.Book }, "Books");
        }

        private void UserNameLabelLinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            if (!LoggedIn)
            {
                RentItMessageBox.NotLoggedIn();
                return;
            }

            // go to account screen (check whether publisher or user)
            FireContentChangeEvent(new PublisherAccountManagement(), "Publisher management");
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
            var pubMan = new PublisherAccountManagement();
            pubMan.SelectTab(1);
            FireContentChangeEvent(pubMan, "Publisher management");
        }
    }
}
