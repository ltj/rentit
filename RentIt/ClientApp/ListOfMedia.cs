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

    public partial class ListOfMedia : UserControl {
        private readonly RentItClient rentIt;

        public ListOfMedia()
        {
            InitializeComponent();
            var binding = new BasicHttpBinding();
            var address = new EndpointAddress("http://rentit.itu.dk/rentit01/RentItService.svc");
            rentIt = new RentItClient(binding, address);
            Title = "";
            Numbering = false;
            Type = MediaType.Any;
        }

        internal string Title {
            get { return label1.Text; }
            set { label1.Text = value; }
        }

        internal bool Numbering { get; set; }

        internal void UpdateList(MediaCriteria criteria)
        {
            var medias = rentIt.GetMediaItems(criteria);

            List<MediaInfo> list;
            switch (criteria.Type)
            {
                case MediaType.Album:
                    list = medias.Albums;
                    break;
                case MediaType.Book:
                    list = medias.Books;
                    break;
                case MediaType.Song:
                    list = medias.Songs;
                    break;
                case MediaType.Movie:
                    list = medias.Movies;
                    break;
                default:
                    list = new MediaInfo[0];
                    break;
            }
            if (list.Count() > 0 && !Numbering)
                for (int i = 0; i < list.Count() - 1; i++)
                    listBox1.Items.Add(list[i].Title);

            if (list.Count() > 0 && Numbering)
                for (int i = 0; i < list.Count() - 1; i++)
                    listBox1.Items.Add((i + 1) + ". " + list[i].Title);
        }

        private void listBox1_SelectedIndexChanged(object sender, EventArgs e)
        {

        }
    }
}
