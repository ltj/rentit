namespace ClientApp {
    using System;
    using System.Windows.Forms;
    using RentIt;

    public partial class TopBarControl : UserControl {
        public TopBarControl(string title = "RentIt") {
            InitializeComponent();
            Title = title;
            UserName = "";
            TypeComboBox.SelectedIndex = 0;
        }

        /// <summary>
        /// The title of the top bar.
        /// </summary>
        public string Title {
            get { return TitleLabel.Text; }
            set { TitleLabel.Text = value; }
        }

        /// <summary>
        /// The user name displayed on the top bar.
        /// Setter sets the displayed name and changes the log in/log out button text.
        /// An empty string ("") will be considered as "user not logged in".
        /// </summary>
        public string UserName {
            get { return UserNameLabel.Text; }
            set {
                UserNameLabel.Text = value;
                LogInLogOutButton.Text = value.Length > 0 ? "Log out" : "Log in";
                LoggedIn = value.Length > 0;
            }
        }

        /// <summary>
        /// Sets whether the login button should display "log in" or "log out"
        /// </summary>
        private bool LoggedIn { get; set; }

        private void SearchButtonClick(object sender, EventArgs e) {
            var comboBoxType = (string)TypeComboBox.SelectedItem;

            MediaType mediaType;
            if(comboBoxType.Equals("Movies"))
                mediaType = MediaType.Movie;
            else if(comboBoxType.Equals("Music"))
                mediaType = MediaType.Album;
            else if(comboBoxType.Equals("Books"))
                mediaType = MediaType.Book;
            else
                mediaType = MediaType.Any;

            var criteria = new MediaCriteria {
                SearchText = SearchTextBox.Text,
                Genre = "",
                Type = mediaType,
                Limit = -1,
                Offset = 0,
                Order = MediaOrder.AlphabeticalAsc
            };

            // switch to search results, using criteria object
        }

        private void LogInLogOutButtonClick(object sender, EventArgs e) {
            if(LoggedIn) {
                
            }
        }

        private void HomeButtonClick(object sender, EventArgs e) {

        }

        private void MovieButtonClick(object sender, EventArgs e) {

        }

        private void MusicButtonClick(object sender, EventArgs e) {

        }

        private void BookButtonClick(object sender, EventArgs e) {

        }

        private void UserNameLabelLinkClicked(object sender, LinkLabelLinkClickedEventArgs e) {
            LoggedIn = false;

            if(!LoggedIn) {
                RentItMessageBox.NotLoggedIn();
            }
            else {

            }
        }
    }
}
