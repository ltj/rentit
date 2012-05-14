namespace ClientApp {
    using System.ServiceModel;
    using System.Windows.Forms;

    using RentIt;

    public partial class MainForm : Form {
        private readonly RentItClient rentItProxy;
        private AccountCredentials credentials;

        public MainForm() {
            InitializeComponent();

            var binding = new BasicHttpBinding();
            var address = new EndpointAddress("http://rentit.itu.dk/rentit01/RentItService.svc");
            rentItProxy = new RentItClient(binding, address);

            credentials = new AccountCredentials {
                                                     UserName = "publishCorp",
                                                     HashedPassword = "7110eda4d09e062aa5e4a390b0a572ac0d2c0220"
                                                 };

            TopBar.RentItProxy = rentItProxy;
            TopBar.Credentials = credentials;
            TopBar.ContentChangeEvent += ChangeContent;

            Content = new MainScreen();
        }

        internal TopBarControl TopBar {
            get { return topBarControl; }
        }

        private RentItUserControl Content {
            set {
                contentPane.Controls.Clear();
                value.RentItProxy = rentItProxy;
                value.Credentials = credentials;
                value.ContentChangeEvent += ChangeContent;
                value.Dock = DockStyle.Fill;
                contentPane.Controls.Add(value);
            }
        }

        private void ChangeContent(object sender, ContentChangeArgs args) {
            Content = args.NewControl;
            TopBar.Title = args.NewTitle;
        }
    }
}
