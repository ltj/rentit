using System.Windows.Forms;

namespace ClientApp {
    using RentIt;

    public partial class MoviePlayerControl : UserControl {
        private MovieInfo movie;

        public MoviePlayerControl() {
            InitializeComponent();
        }

        internal AccountCredentials Credentials { get; set; }

        internal MovieInfo Movie {
            get { return movie; }
            set {
                movie = value;
                
                //mediaPlayer.newMedia()
            }
        }
    }
}
