
namespace ClientApp
{
    using System;
    using System.Drawing;
    using System.Windows.Forms;
    using System.Collections.Generic;

    using BinaryCommunicator;

    using RentIt;

    /// <author>Kenneth Søhrmann</author>
    /// <summary>
    /// This class represents the MediaGrid UserControl. MediaInfo-objects,
    /// retrieved from the service on basis on the submitted MediaCriteria, is displayed in a list
    /// with a tile view. Each media item of the list has a thumbnail image as well.
    /// Each item is displaying three pieces of metadata information, namely title,
    /// author and price.
    /// The media items is displayed in the order that is dictated by the ordering
    /// property of the submitted MediaCriteria object.
    /// </summary>
    internal partial class MediaGrid : RentItUserControl
    {
        /// <summary>
        /// The mediaCriteria behind the current contents of the MediaGrid.
        /// </summary>
        private MediaCriteria mediaCriteria;

        /// <summary>
        /// Initializes a new instance o fht MediaGrid class.
        /// </summary>
        public MediaGrid()
        {
            InitializeComponent();

            // Add EventHandlers.
            this.ListViewGrid.DoubleClick += this.DoubleClickEventHandler;
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
                Cursor.Current = Cursors.WaitCursor;
                this.mediaCriteria = value;
                MediaItems mediaItems = this.RentItProxy.GetMediaItems(this.mediaCriteria);
                this.PopulateGrid(mediaItems);
                Cursor.Current = Cursors.Default;
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

            // Initialize the thumbnail list.
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
                        ListViewGrid.LargeImageList.Images.Add(
                            BinaryCommuncator.GetThumbnail(album.Id));
                        var item = new ListViewItem(album.Title, iAlbum++);
                        item.SubItems.Add(album.AlbumArtist);
                        item.SubItems.Add(album.Price.ToString());
                        item.Tag = album;

                        listItemCollection.Add(item);
                    }
                    break;
                case MediaType.Book:
                    int iBook = 0;
                    foreach (BookInfo book in items.Books)
                    {
                        ListViewGrid.LargeImageList.Images.Add(
                            BinaryCommuncator.GetThumbnail(book.Id));
                        var item = new ListViewItem(book.Title, iBook++);
                        item.SubItems.Add(book.Author);
                        item.SubItems.Add(book.Price.ToString());
                        item.Tag = book;

                        listItemCollection.Add(item);
                    }
                    break;
                case MediaType.Song:
                    int iSong = 0;
                    foreach (SongInfo song in items.Songs)
                    {
                        ListViewGrid.LargeImageList.Images.Add(
                            BinaryCommuncator.GetThumbnail(song.Id));
                        var item = new ListViewItem(song.Title, iSong++);
                        item.SubItems.Add(song.Artist);
                        item.SubItems.Add(song.Price.ToString());
                        item.Tag = song;

                        listItemCollection.Add(item);
                    }
                    break;
                case MediaType.Movie:
                    int iMovie = 0;
                    foreach (MovieInfo movie in items.Movies)
                    {
                        ListViewGrid.LargeImageList.Images.Add(
                            BinaryCommuncator.GetThumbnail(movie.Id));
                        var item = new ListViewItem(movie.Title, iMovie++);
                        item.SubItems.Add(movie.Director);
                        item.SubItems.Add(movie.Price.ToString());
                        item.Tag = movie;

                        listItemCollection.Add(item);
                    }
                    break;
            }

            // Add all the created ListViewItems to the MediaGrid at once.
            ListViewGrid.Items.AddRange(listItemCollection.ToArray());
        }

        #region EventHandlers

        /// <summary>
        /// EventHandler for handling double clicks on the MediaGrid.
        /// Depending on the type of the clicked media, the corresponding
        /// UserControl for displayed media metadata (AlbumDetails for media of 
        /// type album, and BookMovieDetails for media of type either book or movie)
        /// will be displayed in the MainForm.
        /// </summary>
        private void DoubleClickEventHandler(object obj, EventArgs e)
        {
            Cursor.Current = Cursors.WaitCursor;

            if (this.ListViewGrid.SelectedItems.Count != 1)
            {
                return;
            }

            ListViewItem selectedItem = this.ListViewGrid.SelectedItems[0];
            var mediaInfo = (MediaInfo)selectedItem.Tag;

            RentItUserControl mediaDetail;
            string title;

            if (this.mediaCriteria.Type == MediaType.Album)
            {
                mediaDetail = new AlbumDetails
                {
                    RentItProxy = this.RentItProxy,
                    Credentials = this.Credentials,
                    AlbumInfo = (AlbumInfo)mediaInfo
                };
                title = MainForm.Titles.MediaDetailsAlbum;
            }
            else if (this.mediaCriteria.Type == MediaType.Movie)
            {
                mediaDetail = new BookMovieDetails
                {
                    RentItProxy = this.RentItProxy,
                    Credentials = this.Credentials,
                    MovieInfo = (MovieInfo)mediaInfo
                };
                title = MainForm.Titles.MediaDetailsMovie;
            }
            else if (this.mediaCriteria.Type == MediaType.Book)
            {
                mediaDetail = new BookMovieDetails
                {
                    RentItProxy = this.RentItProxy,
                    Credentials = this.Credentials,
                    BookInfo = (BookInfo)mediaInfo
                };
                title = MainForm.Titles.MediaDetailsBook;
            }
            else return;

            FireContentChangeEvent(mediaDetail, title);

            Cursor.Current = Cursors.Default;
        }

        #endregion
    }
}
