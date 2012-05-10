
namespace ClientApp
{
    using System;
    using System.ServiceModel;
    using System.Drawing;
    using System.Windows.Forms;
    using System.Collections.Generic;

    using BinaryCommunicator;

    using RentIt;

    /// <author>Kenneth Søhrmann</author>
    /// <summary>
    /// 
    /// </summary>
    public partial class MediaGrid : RentItUserControl
    {
        /// <summary>
        /// The mediaCriteria behind the current contents of the MediaGrid.
        /// </summary>
        private MediaCriteria mediaCriteria;

        private RentItClient serviceClient;

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
            serviceClient = new RentItClient(binding, address);

            MediaItems mediaItems = serviceClient.GetMediaItems(this.mediaCriteria);
            this.PopulateGrid(mediaItems);
        }

        /// <summary>
        /// Sets the title of the media grid.
        /// </summary>
        internal string Title
        {
            set
            {
                this.titleValueLabel.Text = value;
            }
        }

        /// <summary>
        /// Sets the MediaCriteria and updates the contents
        /// of the MediaGrid with the media items identifyed and returned
        /// by the service using the GetMediaItems web service.
        /// </summary>
        internal MediaCriteria MediaCriteria
        {
            set
            {
                this.mediaCriteria = value;
                MediaItems mediaItems = this.serviceClient.GetMediaItems(this.mediaCriteria);
                this.PopulateGrid(mediaItems);
            }
        }

        /// <summary>
        /// Helper method for populating the contents of the MediaGrid
        /// with the items given in the parameter.
        /// </summary>
        /// <param name="items"></param>
        private void PopulateGrid(MediaItems items)
        {
            ListViewGrid.Items.Clear();
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
            Image image;
            try
            {
                image = BinaryCommuncator.GetThumbnail(id);
            }
            catch (Exception)
            {
                image = new Bitmap(200, 200);
            }
            return image;
        }


        private void DoubleClickEventHandler(object obj, EventArgs e)
        {

        }
    }
}
