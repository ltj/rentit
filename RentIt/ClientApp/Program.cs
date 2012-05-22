namespace ClientApp
{
    using System;
    using System.ServiceModel;
    using System.Windows.Forms;

    internal static class Program
    {
        /// <summary>
        /// The main entry point for the application.
        /// </summary>
        [STAThread]
        internal static void Main()
        {
            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);

            MainForm main;
            try {
                main = new MainForm();
            } catch(EndpointNotFoundException) {
                RentItMessageBox.ServerCommunicationError();
                return;
            }

            Application.Run(main);
        }
    }
}
