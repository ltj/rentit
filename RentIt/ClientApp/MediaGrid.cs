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

    using BinaryCommunicator;

    using RentIt;

    public partial class MediaGrid : UserControl
    {
        private MediaCriteria mediaCriteria;

        public MediaGrid()
        {
            InitializeComponent();

            mediaCriteria = new MediaCriteria()
                {
                    Type = MediaType.Movie,
                    Limit = -1
                };

            BasicHttpBinding binding = new BasicHttpBinding();
            EndpointAddress address = new EndpointAddress("http://rentit.itu.dk/rentit01/RentItService.svc");
            RentItClient serviceClient = new RentItClient(binding, address);

            MediaItems mediaItems = serviceClient.GetMediaItems(this.mediaCriteria);
            this.PopulateGrid(mediaItems);
        }

        internal MediaGrid(string title, MediaCriteria mediaCriteria)
            : this()
        {
            if (mediaCriteria.Type == MediaType.Any)
            {
                throw new ArgumentException();
            }

            this.mediaCriteria = mediaCriteria;
            this.titleValueLabel.Text = title;

            BasicHttpBinding binding = new BasicHttpBinding();
            EndpointAddress address = new EndpointAddress("http://rentit.itu.dk/rentit01/RentItService.svc");
            RentItClient serviceClient = new RentItClient(binding, address);

            MediaItems mediaItems = serviceClient.GetMediaItems(mediaCriteria);
            this.PopulateGrid(mediaItems);
        }

        private void PopulateGrid(MediaItems items)
        {
            var listItemCollection = new List<ListViewItem>();

            var imageList = new ImageList();
            var imageSize = new Size(70, 70);
            imageList.ImageSize = imageSize;
            ListViewGrid.LargeImageList = imageList;

            switch (mediaCriteria.Type)
            {
                case MediaType.Album:
                    int iAlbum = 0;
                    foreach (AlbumInfo album in items.Albums)
                    {
                        ListViewGrid.LargeImageList.Images.Add(this.getImage(album.Id));
                        var item = new ListViewItem(album.Title, iAlbum++);
                        item.SubItems.Add(album.AlbumArtist);
                        item.SubItems.Add(album.Price.ToString());
                        listItemCollection.Add(item);
                    }
                    break;
                case MediaType.Book:
                    int iBook = 0;
                    foreach (BookInfo book in items.Books)
                    {
                        ListViewGrid.LargeImageList.Images.Add(this.getImage(book.Id));
                        var item = new ListViewItem(book.Title, iBook++);
                        item.SubItems.Add(book.Author);
                        item.SubItems.Add(book.Price.ToString());
                        listItemCollection.Add(item);
                    }
                    break;
                case MediaType.Song:
                    int iSong = 0;
                    foreach (SongInfo song in items.Songs)
                    {
                        ListViewGrid.LargeImageList.Images.Add(this.getImage(song.Id));
                        var item = new ListViewItem(song.Title, iSong++);
                        item.SubItems.Add(song.Artist);
                        item.SubItems.Add(song.Price.ToString());
                        listItemCollection.Add(item);
                    }
                    break;
                case MediaType.Movie:
                    int iMovie = 0;
                    foreach (MovieInfo movie in items.Movies)
                    {
                        ListViewGrid.LargeImageList.Images.Add(this.getImage(movie.Id));
                        var item = new ListViewItem(movie.Title, iMovie++);
                        item.SubItems.Add(movie.Director);
                        item.SubItems.Add(movie.Price.ToString());
                        listItemCollection.Add(item);
                    }
                    break;
            }
            ListViewGrid.Items.AddRange(listItemCollection.ToArray());
        }

        private Image getImage(int id)
        {
            Image image = Image.FromFile(@"C:\Users\Kenneth88\Desktop\gta\test.jpg");
            try
            {
                image = BinaryCommuncator.GetThumbnail(id);
            }
            catch (Exception)
            {
                image = Image.FromFile(@"C:\Users\Kenneth88\Desktop\gta\GtaThumb.jpg");
            }
            return image;
        }


        private void DoubleClickEventHandler(object obj, EventArgs e)
        {

        }
    }
}
