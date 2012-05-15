using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace ClientApp
{
    using System.ServiceModel;

    using RentIt;

    /// <summary>
    /// The user control used to display medias rented by users who also rented a given media item.
    /// </summary>
    /// <author>Jacob Rasmussen</author>
    internal partial class AlsoRentedList : RentItUserControl
    {
        private readonly RentItClient rentIt;

        //The id of a given media item.
        internal int MediaId { get; set; }

        //The user control will list media of books, movies and albums. This property affects the order in which the medias are listed.
        internal MediaType PrioritizedMediaType { get; set; }

        public AlsoRentedList()
        {
            InitializeComponent();
            var binding = new BasicHttpBinding();
            var address = new EndpointAddress("http://rentit.itu.dk/rentit01/RentItService.svc");
            rentIt = new RentItClient(binding, address);
        }

        /// <summary>
        /// Updates the list with the given media id and prioritized media type property. 
        /// </summary>
        public void UpdateList()
        {
            //Gets all relevant medias from the database.
            var medias = rentIt.GetAlsoRentedItems(MediaId);

            //Used to retrieve the title from the given media.
            MediaInfo mediaInf;

            //The lists containing media of varying relevance. The elements in the primaryList will be displayed at the top.
            MediaInfo[] primaryList;
            MediaInfo[] secondaryList;
            MediaInfo[] tertiaryList;

            //Each media type is assigned a list depending on the relevance of the media type compared to the prioritized media type.
            switch (PrioritizedMediaType)
            {
                case MediaType.Album:
                    primaryList = medias.Albums;
                    secondaryList = medias.Movies;
                    tertiaryList = medias.Books;
                    mediaInf = rentIt.GetAlbumInfo(MediaId);
                    break;
                case MediaType.Book:
                    primaryList = medias.Books;
                    secondaryList = medias.Movies;
                    tertiaryList = medias.Albums;
                    mediaInf = rentIt.GetBookInfo(MediaId);
                    break;
                case MediaType.Movie:
                    primaryList = medias.Movies;
                    secondaryList = medias.Books;
                    tertiaryList = medias.Albums;
                    mediaInf = rentIt.GetMovieInfo(MediaId);
                    break;
                default:
                    primaryList = new MediaInfo[0];
                    secondaryList = new MediaInfo[0];
                    tertiaryList = new MediaInfo[0];
                    mediaInf = new MediaInfo { Title = "NOT AVAILABLE" };
                    break;
            }
            label1.Text = @"Customers who rented" + Environment.NewLine + mediaInf.Title + @" also rented:";

            //Adds each media item to the list in correct order.
            foreach (var mediaInfo in primaryList)
                listBox1.Items.Add(mediaInfo.Title);
            foreach (var mediaInfo in secondaryList)
                listBox1.Items.Add(mediaInfo.Title);
            foreach (var mediaInfo in tertiaryList)
                listBox1.Items.Add(mediaInfo.Title);
        }

        private void label1_Click(object sender, EventArgs e)
        {

        }

        private void listBox1_SelectedIndexChanged(object sender, EventArgs e)
        {

        }
    }
}
