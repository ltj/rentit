﻿namespace ClientApp
{
    using System;
    using System.ServiceModel;
    using System.Windows.Forms;
    using RentIt;

    /// <summary>
    /// The top bar of the main application window, MainForm.
    /// This contains various buttons and controls for navigation and search.
    /// It also displays a title of the current screen.
    /// </summary>
    internal partial class TopBarControl : RentItUserControl
    {
        public TopBarControl()
        {
            InitializeComponent();
            Title = MainForm.Titles.MainScreen;
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

        internal override AccountCredentials Credentials
        {
            set
            {
                base.Credentials = value;
                if (value != null)
                {
                    UserNameLabel.Text = value.UserName;
                    LoggedIn = true;
                }
                else
                {
                    LoggedIn = false;
                }
            }
        }

        /// <summary>
        /// The amount of credits displayed next to the user name,
        /// if a user is logged in.
        /// </summary>
        internal int Credits
        {
            get { return credits; }
            set
            {
                credits = value;
                string creditsDisplay = value > 0 ? value.ToString() : "no";
                creditsLabel.Text = "(" + creditsDisplay + " credits)";
            }
        }
        private int credits;

        /// <summary>
        /// Sets whether the top bar should display user information
        /// or not, as well as "log in" / "log out" on the log in button.
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
                creditsLabel.Visible = value;

                if (!value) return; //don't try to get user data if this is logging out

                Cursor.Current = Cursors.WaitCursor;
                try
                {
                    UserAccount account = RentItProxy.GetAllCustomerData(Credentials);
                    Credits = account.Credits;
                    isPublisher = false;
                }
                catch (FaultException)
                {
                    try
                    {
                        RentItProxy.GetAllPublisherData(Credentials);
                        creditsLabel.Visible = false; //never show credits for publisher
                        isPublisher = true;
                    }
                    catch (FaultException) { }
                }
                Cursor.Current = Cursors.Default;
            }
        }
        private bool loggedIn;

        /// <summary>
        /// Initiates a search for media, using the information entered in the
        /// text field and the selected media type in the combo box.
        /// </summary>
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
            FireContentChangeEvent(search, MainForm.Titles.SearchResults);
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
                Cursor.Current = Cursors.WaitCursor;
                FireContentChangeEvent(new UserRegistration { RentItProxy = RentItProxy }, MainForm.Titles.UserRegistration);
                Cursor.Current = Cursors.Default;
            }
            else
            {
                // go to main screen
                LoggedIn = false;
                Cursor.Current = Cursors.WaitCursor;
                FireContentChangeEvent(new MainScreen { RentItProxy = RentItProxy }, MainForm.Titles.MainScreen);
                FireCredentialsChangeEvent(null);
                Cursor.Current = Cursors.Default;
            }
        }

        private void HomeButtonClick(object sender, EventArgs e)
        {
            Cursor.Current = Cursors.WaitCursor;
            FireContentChangeEvent(new MainScreen { RentItProxy = RentItProxy }, MainForm.Titles.MainScreen);
            Cursor.Current = Cursors.Default;
        }

        private void MovieButtonClick(object sender, EventArgs e)
        {
            Cursor.Current = Cursors.WaitCursor;
            FireContentChangeEvent(
                new MediaFrontpage
                    {
                        RentItProxy = RentItProxy,
                        Credentials = this.Credentials,
                        Mtype = MediaType.Movie,
                    },
                    MainForm.Titles.MediaFrontpageMovies);
            Cursor.Current = Cursors.Default;
        }

        private void MusicButtonClick(object sender, EventArgs e)
        {
            Cursor.Current = Cursors.WaitCursor;
            FireContentChangeEvent(
                new MediaFrontpage
                {
                    RentItProxy = RentItProxy,
                    Credentials = this.Credentials,
                    Mtype = MediaType.Album,
                },
                MainForm.Titles.MediaFrontpageMusic);
            Cursor.Current = Cursors.Default;
        }

        private void BookButtonClick(object sender, EventArgs e)
        {
            Cursor.Current = Cursors.WaitCursor;
            FireContentChangeEvent(
                new MediaFrontpage
                {
                    RentItProxy = RentItProxy,
                    Credentials = this.Credentials,
                    Mtype = MediaType.Book,
                },
                MainForm.Titles.MediaFrontpageBooks);
            Cursor.Current = Cursors.Default;
        }

        private void UserNameLabelLinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            if (!LoggedIn)
            {
                RentItMessageBox.NotLoggedIn();
                return;
            }

            // go to account screen (check whether publisher or user)
            Cursor.Current = Cursors.WaitCursor;
            if (isPublisher)
            { //if publisher
                var pubMan = new PublisherAccountManagement { RentItProxy = RentItProxy, Credentials = Credentials };
                FireContentChangeEvent(pubMan, MainForm.Titles.PublisherAccountManagement);
            }
            else
            {
                var userMan = new UserAccountManagement { RentItProxy = RentItProxy, Credentials = Credentials };
                FireContentChangeEvent(userMan, MainForm.Titles.UserAccountManagement);
            }
            Cursor.Current = Cursors.Default;
        }

        /// <summary>
        /// Handles the enter key being pressed in the search text field.
        /// </summary>
        /// <param name="sender">Ignored here.</param>
        /// <param name="keyEventArgs">Used to check whether the enter key was pressed.</param>
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
            Cursor.Current = Cursors.WaitCursor;
            if (isPublisher)
            { //if publisher
                var pubMan = new PublisherAccountManagement { RentItProxy = RentItProxy, Credentials = Credentials };
                pubMan.SelectTab(1);
                FireContentChangeEvent(pubMan, MainForm.Titles.PublisherAccountManagement);
            }
            else
            {
                var userMan = new UserAccountManagement { RentItProxy = RentItProxy, Credentials = Credentials };
                userMan.SelectTab(1);
                FireContentChangeEvent(userMan, MainForm.Titles.UserAccountManagement);
            }
            Cursor.Current = Cursors.Default;
        }
    }
}
