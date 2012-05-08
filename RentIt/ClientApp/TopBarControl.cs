namespace ClientApp {
    using System;
    using System.Windows.Forms;
    using RentIt;

    public partial class TopBarControl : UserControl {
        public TopBarControl() {
            InitializeComponent();
            TypeComboBox.SelectedIndex = 0;
        }

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
        }

        private void AccountButtonClick(object sender, EventArgs e) {

        }

        private void HomeButtonClick(object sender, EventArgs e) {

        }

        private void MovieButtonClick(object sender, EventArgs e) {

        }

        private void MusicButtonClick(object sender, EventArgs e) {

        }

        private void BookButtonClick(object sender, EventArgs e) {

        }
    }
}
