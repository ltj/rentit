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

    public partial class AlbumDetails : UserControl
    {



        public AlbumDetails()
        {
            InitializeComponent();

            BasicHttpBinding binding = new BasicHttpBinding();
            EndpointAddress address = new EndpointAddress("http://rentit.itu.dk/rentit01/RentItService.svc");
            RentItClient serviceClient = new RentItClient(binding, address);

            AlbumInfo album = serviceClient.GetAlbumInfo(78);

            this.mediaSideBar.MediaInfoData = album;

            this.albumTitleLabel.Text = album.Title;
            this.albumArtistLabel.Text = album.AlbumArtist;

            this.albumDurationValueLabel.Text = album.TotalDuration.ToString();

            this.albumDescriptionTextBox.Text = album.Description;
            this.PopulateSongList(album);
        }

        internal AlbumDetails(AlbumInfo album)
        {
            BasicHttpBinding binding = new BasicHttpBinding();
            EndpointAddress address = new EndpointAddress("http://rentit.itu.dk/rentit01/RentItService.svc");
            RentItClient serviceClient = new RentItClient(binding, address);

            album = serviceClient.GetAlbumInfo(78);

            this.mediaSideBar = new MediaSideBar(album);

            this.albumTitleLabel.Text = album.Title;
            this.albumArtistLabel.Text = album.AlbumArtist;

            this.albumDurationValueLabel.Text = album.TotalDuration.ToString();

            this.albumDescriptionTextBox.Text = album.Description;
            this.PopulateSongList(album);
        }

        private void PopulateSongList(AlbumInfo info)
        {
            foreach (var song in info.Songs)
            {
                var listItem = new ListViewItem(song.Title);
                listItem.SubItems.Add(song.Artist);
                listItem.SubItems.Add(song.Genre);
                listItem.SubItems.Add(song.Price.ToString());
                listItem.SubItems.Add((song.Duration.ToString()));
                songList.Items.Add(listItem);
            }
        }

    }
}
