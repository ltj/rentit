namespace ClientApp {
    using System.Windows.Forms;

    public partial class MainForm : Form {
        private UserControl contentControl;

        public MainForm() {
            InitializeComponent();
            Content = new MainScreen();
        }

        public TopBarControl TopBar {
            get { return topBarControl; }
        }

        public UserControl Content {
            get { return contentControl; }
            set {
                contentControl = value;
                contentPane.Controls.Clear();
                value.Dock = DockStyle.Fill;
                contentPane.Controls.Add(value);
            }
        }
    }
}
