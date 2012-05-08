namespace ClientApp {
    using System.Windows.Forms;

    public partial class MainForm : Form {
        private UserControl contentControl;

        public MainForm() {
            InitializeComponent();
            Content = new RatingList();
        }

        public UserControl Content {
            get { return contentControl; }
            set {
                contentControl = value;
                ContentPane.Controls.Clear();
                value.Dock = DockStyle.Fill;
                ContentPane.Controls.Add(value);
            }
        }
    }
}
