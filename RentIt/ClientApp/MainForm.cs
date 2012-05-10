namespace ClientApp {
    using System.Windows.Forms;

    public partial class MainForm : Form {
        public MainForm() {
            InitializeComponent();
            Content = new MainScreen();
        }

        internal TopBarControl TopBar {
            get { return topBarControl; }
        }

        internal RentItUserControl Content {
            set {
                contentPane.Controls.Clear();
                value.Dock = DockStyle.Fill;
                contentPane.Controls.Add(value);
            }
        }
    }
}
