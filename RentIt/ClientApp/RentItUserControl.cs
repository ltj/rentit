namespace ClientApp {
    using System.Windows.Forms;

    using RentIt;

    public class RentItUserControl : UserControl {
        internal virtual RentItClient RentItProxy { get; set; }
        internal virtual AccountCredentials Credentials { get; set; }
    }
}
