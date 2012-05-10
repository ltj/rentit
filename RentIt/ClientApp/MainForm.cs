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

            TopBar.RentItProxy = rentItProxy;
            TopBar.Credentials = credentials;

            Content = new MainScreen();
        }

        internal TopBarControl TopBar {
            get { return topBarControl; }
        }

        internal RentItUserControl Content {
            set {
                contentPane.Controls.Clear();
                value.RentItProxy = rentItProxy;
                value.Credentials = credentials;
                value.Dock = DockStyle.Fill;
                contentPane.Controls.Add(value);
            }
        }
    }
}
