namespace ClientApp
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Windows.Forms;

    using RentIt;

    /// <summary>
    /// The user control used to display medias rented by users who also rented a given media item.
    /// </summary>
    /// <author>Jacob Rasmussen</author>
    internal partial class AlsoRentedList : RentItUserControl
    {
        private int mediaId;

        private List<MediaInfo> mediaList;

        public AlsoRentedList()
        {
            InitializeComponent();

            this.mediaListView.DoubleClick += this.DoubleClickEventHandler;
        }

        /// <summary>
        /// The user control will list media of books, movies and albums. 
        /// This property affects the order in which the medias are listed.
        /// </summary>
        internal MediaType PrioritizedMediaType { get; set; }

        //The id of a given media item.
        internal int MediaId
        {
            get
            {
                return this.mediaId;
            }
            set
            {
                this.mediaId = value;
                this.UpdateList();
            }
        }

        /// <summary>
        /// Updates the list with the given media id and prioritized media type property. 
        /// </summary>
        private void UpdateList()
        {
            Cursor.Current = Cursors.WaitCursor;
            //Gets all relevant medias from the database.
            var medias = this.RentItProxy.GetAlsoRentedItems(MediaId);

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
                    mediaInf = this.RentItProxy.GetAlbumInfo(MediaId);
                    break;
                case MediaType.Book:
                    primaryList = medias.Books;
                    secondaryList = medias.Movies;
                    tertiaryList = medias.Albums;
                    mediaInf = this.RentItProxy.GetBookInfo(MediaId);
                    break;
                case MediaType.Movie:
                    primaryList = medias.Movies;
                    secondaryList = medias.Books;
                    tertiaryList = medias.Albums;
                    mediaInf = this.RentItProxy.GetMovieInfo(MediaId);
                    break;
                default:
                    primaryList = new MediaInfo[0];
                    secondaryList = new MediaInfo[0];
                    tertiaryList = new MediaInfo[0];
                    mediaInf = new MediaInfo { Title = "NOT AVAILABLE" };
                    break;
            }
            label1.Text = @"Customers who rented" + Environment.NewLine + mediaInf.Title + @" also rented:";

            this.mediaList = new List<MediaInfo>();
            mediaList.AddRange(primaryList);
            mediaList.AddRange(secondaryList);
            mediaList.AddRange(tertiaryList);

            //Same as above but including numbering of media items.
            if (mediaList.Count() > 0)
            {
                for (int i = 0; i < mediaList.Count(); i++)
                {
                    mediaListView.Items.Add(mediaList[i].Type.ToString() + ", " + mediaList[i].Title);
                }
            }

            /*
            //Adds each media item to the list in correct order.
            foreach (var mediaInfo in primaryList)
            {
                var item = new ListViewItem(mediaInfo.Title);
                item.Tag = mediaInfo;
                mediaList.Items.Add();
            }

            foreach (var mediaInfo in secondaryList)
            {
                var item = new ListViewItem(mediaInfo.Title);
                item.Tag = mediaInfo;
                mediaList.Items.Add(item);
            }

            foreach (var mediaInfo in tertiaryList)
            {
                var item = new ListViewItem(mediaInfo.Title);
                item.Tag = mediaInfo;
                mediaList.Items.Add(item);
            }
             * */

            Cursor.Current = Cursors.Default;
        }

        private void DoubleClickEventHandler(object obj, EventArgs e)
        {
            Cursor.Current = Cursors.WaitCursor;
            if (this.mediaListView.SelectedItems.Count != 1)
            {
                return;
            }

            var mediaInfo = this.mediaList[mediaListView.SelectedIndex];

            switch (mediaInfo.Type)
            {
                case MediaType.Album:
                    var albumDetails = new AlbumDetails {
                                                            RentItProxy = this.RentItProxy,
                                                            Credentials = this.Credentials,
                                                            AlbumInfo = (AlbumInfo)mediaInfo
                                                        };
                    this.FireContentChangeEvent(albumDetails, MainForm.Titles.MediaDetailsAlbum);
                    break;
                case MediaType.Book:
                    var bookDetails = new BookMovieDetails {
                                                               RentItProxy = this.RentItProxy,
                                                               Credentials = this.Credentials,
                                                               BookInfo = (BookInfo)mediaInfo
                                                           };
                    this.FireContentChangeEvent(bookDetails, MainForm.Titles.MediaDetailsBook);
                    break;
                case MediaType.Movie:
                    var movieDetails = new BookMovieDetails {
                                                                RentItProxy = this.RentItProxy,
                                                                Credentials = this.Credentials,
                                                                MovieInfo = (MovieInfo)mediaInfo
                                                            };
                    this.FireContentChangeEvent(movieDetails, MainForm.Titles.MediaDetailsMovie);
                    break;
            }

            Cursor.Current = Cursors.Default;
        }
    }
}
