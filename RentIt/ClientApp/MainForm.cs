namespace ClientApp
{
    using System.ServiceModel;
    using System.Windows.Forms;

    using RentIt;

    internal partial class MainForm : Form
    {
        private readonly RentItClient rentItProxy;
        private AccountCredentials credentials;

        public MainForm()
        {
            InitializeComponent();

            var binding = new BasicHttpBinding();
            var address = new EndpointAddress("http://rentit.itu.dk/rentit01/RentItService.svc");
            rentItProxy = new RentItClient(binding, address);

            /*credentials = new AccountCredentials
            {
                UserName = "publishCorp",
                HashedPassword = "7110eda4d09e062aa5e4a390b0a572ac0d2c0220"
            };*/

            TopBar.RentItProxy = rentItProxy;
            TopBar.Credentials = credentials;
            TopBar.ContentChangeEvent += ChangeContent;

            Content = new MainScreen { RentItProxy = rentItProxy };
        }

        internal TopBarControl TopBar
        {
            get { return topBarControl; }
        }

        private RentItUserControl Content
        {
            set
            {
                contentPane.Controls.Clear();
                if (value.RentItProxy == null)
                    value.RentItProxy = rentItProxy;
                if (value.Credentials == null)
                    value.Credentials = credentials;

                // Add EventHandlers.
                value.ContentChangeEvent += ChangeContent;
                value.CredentialsChangeEvent += ChangeCredentials;

                value.Dock = DockStyle.Fill;
                contentPane.Controls.Add(value);
            }
        }

        #region EventHandlers

        private void ChangeContent(object sender, ContentChangeArgs args)
        {
            Content = args.NewControl;
            TopBar.Title = args.NewTitle;
        }

        private void ChangeCredentials(object sender, CredentialsChangeArgs args)
        {
            credentials = args.credentials;

            RentItUserControl nextScreen = new MainScreen {
                                                              RentItProxy = this.rentItProxy,
                                                              Credentials = this.credentials
                                                          };
            ChangeContent(sender, new ContentChangeArgs(nextScreen, "RentIt"));

            TopBar.Credentials = this.credentials;
        }

        #endregion
    }
}
