﻿namespace ClientApp
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

            this.mediaListView.ItemHeight = 20;

            this.mediaListView.DoubleClick += this.DoubleClickEventHandler;

            label1.Text = "Customers who rented this \nalso rented:";
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
                    break;
                case MediaType.Book:
                    primaryList = medias.Books;
                    secondaryList = medias.Movies;
                    tertiaryList = medias.Albums;
                    break;
                case MediaType.Movie:
                    primaryList = medias.Movies;
                    secondaryList = medias.Books;
                    tertiaryList = medias.Albums;
                    break;
                default:
                    primaryList = new MediaInfo[0];
                    secondaryList = new MediaInfo[0];
                    tertiaryList = new MediaInfo[0];
                    break;
            }

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
                    var albumDetails = new AlbumDetails
                    {
                        RentItProxy = this.RentItProxy,
                        Credentials = this.Credentials,
                        AlbumInfo = (AlbumInfo)mediaInfo
                    };
                    this.FireContentChangeEvent(albumDetails, MainForm.Titles.MediaDetailsAlbum);
                    break;
                case MediaType.Book:
                    var bookDetails = new BookMovieDetails
                    {
                        RentItProxy = this.RentItProxy,
                        Credentials = this.Credentials,
                        BookInfo = (BookInfo)mediaInfo
                    };
                    this.FireContentChangeEvent(bookDetails, MainForm.Titles.MediaDetailsBook);
                    break;
                case MediaType.Movie:
                    var movieDetails = new BookMovieDetails
                    {
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
