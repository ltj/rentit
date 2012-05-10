using System.Windows.Forms;

namespace ClientApp {
    using System;

    using BinaryCommunicator;

    using RentIt;

    public partial class MoviePlayerControl : UserControl {

        public MoviePlayerControl() {
            InitializeComponent();
        }

        internal AccountCredentials Credentials { get; set; }

        internal MovieInfo Movie {
            set {
                titleLabel.Text = value.Title;
                var c = new Credentials(Credentials.UserName, Credentials.HashedPassword);
                mediaPlayer.newMedia(BinaryCommuncator.DownloadMediaURL(c, value.Id).ToString());
            }
        }
    }
}
