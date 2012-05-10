﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Windows.Forms;

namespace ClientApp
{
    using System.ServiceModel;

    using RentIt;

    static class Program
    {
        /// <summary>
        /// The main entry point for the application.
        /// </summary>
        [STAThread]
        static void Main()
        {
            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);




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

            MovieInfo movie = client.GetMovieInfo(72);


            Application.Run(new Form1());

            var mediaDisplay = new MediaDisplayForm();

            var moviePlayer = new MoviePlayerControl();
            moviePlayer.Credentials = credentials;
            moviePlayer.Movie = movie;
            moviePlayer.Start();
            mediaDisplay.Content = moviePlayer;

            mediaDisplay.Show();

        }
    }
}
