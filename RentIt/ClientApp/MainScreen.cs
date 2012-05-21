namespace ClientApp
{
    using System;
    using System.Windows.Forms;

    using RentIt;

    /// <summary>
    /// MainScreen is the frontpage of the RentIt client with 3 columns,
    /// one for each media type.
    /// </summary>
    internal partial class MainScreen : RentItUserControl
    {
        public MainScreen()
        {
            InitializeComponent();
            moviesList.Title = "Featured movies";
            booksList.Title = "Featured books";
            musicList.Title = "Featured music";

            moviesList.AddDoubleClickEventHandler(MovieListDoubleClick);
            booksList.AddDoubleClickEventHandler(BookListDoubleClick);
            musicList.AddDoubleClickEventHandler(MusicListDoubleClick);
        }

        internal override RentItClient RentItProxy
        {
            set
            {
                base.RentItProxy = value;
                moviesList.RentItProxy = value;
                booksList.RentItProxy = value;
                musicList.RentItProxy = value;
                UpdateLists();
            }
        }

        internal override AccountCredentials Credentials
        {
            get
            {
                return base.Credentials;
            }
            set
            {
                base.Credentials = value;
                moviesList.Credentials = value;
                booksList.Credentials = value;
                musicList.Credentials = value;
            }
        }

        /// <summary>
        /// Updates the 3 lists by querying the server.
        /// </summary>
        private void UpdateLists()
        {
            Cursor.Current = Cursors.WaitCursor;
            var criteria = new MediaCriteria
            {
                Genre = "",
                Limit = 10,
                Offset = 0,
                Order = MediaOrder.PopularityDesc,
                SearchText = "",
                Type = MediaType.Movie
            };

            moviesList.UpdateList(criteria);

            criteria.Type = MediaType.Book;
            booksList.UpdateList(criteria);

            criteria.Type = MediaType.Album;
            musicList.UpdateList(criteria);

            Cursor.Current = Cursors.Default;
        }

        private void MoviesButtonClick(object sender, EventArgs e)
        {
            Cursor.Current = Cursors.WaitCursor;
            var mediaFront = new MediaFrontpage { RentItProxy = RentItProxy, Mtype = MediaType.Movie };
            FireContentChangeEvent(mediaFront, MainForm.Titles.MediaFrontpageMovies);
            Cursor.Current = Cursors.Default;
        }

        private void BooksButtonClick(object sender, EventArgs e)
        {
            Cursor.Current = Cursors.WaitCursor;
            var mediaFront = new MediaFrontpage { RentItProxy = RentItProxy, Mtype = MediaType.Book };
            FireContentChangeEvent(mediaFront, MainForm.Titles.MediaFrontpageBooks);
            Cursor.Current = Cursors.Default;
        }

        private void MusicButtonClick(object sender, EventArgs e)
        {
            Cursor.Current = Cursors.WaitCursor;
            var mediaFront = new MediaFrontpage { RentItProxy = RentItProxy, Mtype = MediaType.Album };
            FireContentChangeEvent(mediaFront, MainForm.Titles.MediaFrontpageMusic);
            Cursor.Current = Cursors.Default;
        }

        private void MovieListDoubleClick(object sender, EventArgs e)
        {
            MediaInfo media = moviesList.GetSingleMedia();
            DisplayMediaItem(media);
        }

        private void BookListDoubleClick(object sender, EventArgs e)
        {
            MediaInfo media = booksList.GetSingleMedia();
            DisplayMediaItem(media);
        }

        private void MusicListDoubleClick(object sender, EventArgs e)
        {
            MediaInfo media = musicList.GetSingleMedia();
            DisplayMediaItem(media);
        }

        /// <summary>
        /// Switches to a media details screen, depending on the type
        /// of media selected.
        /// </summary>
        /// <param name="media">The media to open a details page for.</param>
        private void DisplayMediaItem(MediaInfo media)
        {
            Cursor.Current = Cursors.WaitCursor;

            RentItUserControl mediaDetails;
            string title;
            switch (media.Type)
            {
                case MediaType.Album:
                    mediaDetails = new AlbumDetails
                        {
                            RentItProxy = RentItProxy,
                            Credentials = Credentials,
                            AlbumInfo = (AlbumInfo)media
                        };
                    title = MainForm.Titles.MediaDetailsAlbum;
                    break;
                case MediaType.Book:
                    mediaDetails = new BookMovieDetails
                        {
                            RentItProxy = RentItProxy,
                            Credentials = Credentials,
                            BookInfo = (BookInfo)media
                        };
                    title = MainForm.Titles.MediaDetailsBook;
                    break;
                case MediaType.Movie:
                    mediaDetails = new BookMovieDetails
                        {
                            RentItProxy = RentItProxy,
                            Credentials = Credentials,
                            MovieInfo = (MovieInfo)media
                        };
                    title = MainForm.Titles.MediaDetailsMovie;
                    break;
                default:
                    return;
            }

            FireContentChangeEvent(mediaDetails, title);

            Cursor.Current = Cursors.Default;
        }
    }
}
