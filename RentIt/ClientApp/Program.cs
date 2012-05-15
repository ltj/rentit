

namespace ClientApp
{
    using System;
    using System.Windows.Forms;

    using RentIt;

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



            /*
            RentItClient client;
            AccountCredentials credentials;

            BasicHttpBinding binding = new BasicHttpBinding();
            EndpointAddress address = new EndpointAddress("http://rentit.itu.dk/rentit01/RentItService.svc");
            client = new RentItClient(binding, address);

            credentials = new AccountCredentials()
            {
                UserName = "publishCorp",
                HashedPassword = "7110EDA4D09E062AA5E4A390B0A572AC0D2C0220"
            };

            BookInfo movie = client.GetBookInfo(75);


            var mediaDisplay = new MediaDisplayForm();

            var moviePlayer = new BookReaderControl();
            moviePlayer.Credentials = credentials;
            moviePlayer.Book = movie;
            moviePlayer.Start();
            mediaDisplay.Content = moviePlayer;

            Application.Run(mediaDisplay);
            */

            Application.Run(new MainForm());
        }
    }
}
