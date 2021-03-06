﻿using BinaryCommunicator;

namespace ClientApp
{
    using System;
    using System.Windows.Forms;

    using RentIt;

    /// <summary>
    /// RentItUSerControl for media frontpage context
    /// </summary>
    internal partial class MediaFrontpage : RentItUserControl
    {
        private RentIt.MediaType mtype;

        private MediaInfo newAndHotMedia;

        public MediaFrontpage()
        {
            InitializeComponent();
            popularMediaGrid.ContentChangeEvent += ContentChangeEventPropagated;
            newMediaGrid.ContentChangeEvent += ContentChangeEventPropagated;
            bestMediaGrid.ContentChangeEvent += ContentChangeEventPropagated;
            genreList1.ContentChangeEvent += ContentChangeEventPropagated;
        }

        /// <summary>
        /// set or get medua type property.
        /// Updates view on set.
        /// </summary>
        internal RentIt.MediaType Mtype
        {
            get { return mtype; }
            set
            {
                mtype = value;
                genreList1.Mtype = value;
                RefreshContents();
            }
        }

        /// <summary>
        /// get or set proxy object. set also updates
        /// contained components.
        /// </summary>
        internal override RentItClient RentItProxy
        {
            get
            {
                return base.RentItProxy;
            }
            set
            {
                base.RentItProxy = value;
                genreList1.RentItProxy = value;
                popularMediaGrid.RentItProxy = value;
                newMediaGrid.RentItProxy = value;
                bestMediaGrid.RentItProxy = value;
            }
        }

        /// <summary>
        /// get or set AccountCredentials object. set also
        /// updates contained components.
        /// </summary>
        internal override AccountCredentials Credentials
        {
            get
            {
                return base.Credentials;
            }
            set
            {
                base.Credentials = value;
                genreList1.Credentials = value;
                popularMediaGrid.Credentials = value;
                newMediaGrid.Credentials = value;
                bestMediaGrid.Credentials = value;
            }
        }

        /// <summary>
        /// Get newest media and publish to "new and hot" area
        /// </summary>
        private void GetNewest()
        {
            // we need valid prerequisites to query the webservice
            if (mtype == RentIt.MediaType.Any || RentItProxy == null) return;

            // build search criteria
            var mc = new RentIt.MediaCriteria
            {
                Type = mtype,
                Limit = 10, // ten results (previously one result)
                Order = RentIt.MediaOrder.ReleaseDateDesc,
                Genre = "",
                SearchText = ""
            };

            newMediaGrid.MediaCriteria = mc;

            try
            {
                RentIt.MediaItems result = RentItProxy.GetMediaItems(mc);
                RentIt.MediaInfo[] subresult = ExtractTypeList(result);

                newAndHotMedia = subresult[0];
                lblNewTitle.Text = subresult[0].Title;
                lblNewGenre.Text = subresult[0].Genre;
                lblNewRelease.Text = subresult[0].ReleaseDate.ToShortDateString();
                lblNewPublisher.Text = subresult[0].Publisher;
                lblNewPrice.Text = subresult[0].Price.ToString();

                picNewThumb.Image = BinaryCommuncator.GetThumbnail(subresult[0].Id);
            }
            catch
            {
                lblNewTitle.Text = "Oh snap! Something went wrong :(";
            }

        }

        /// <summary>
        /// Get ten most popular medias
        /// </summary>
        private void GetMostPopular()
        {
            // we need valid prerequisites to query the webservice
            if (mtype == RentIt.MediaType.Any || RentItProxy == null) return;

            // build search criteria
            var mc = new RentIt.MediaCriteria
            {
                Type = mtype,
                Limit = 10, // only ten most pouplar
                Order = RentIt.MediaOrder.PopularityDesc,
                Genre = "",
                SearchText = ""
            };

            popularMediaGrid.MediaCriteria = mc;
        }

        /// <summary>
        /// Get ten highest rated medias
        /// </summary>
        private void GetHighestRated()
        {
            // we need valid prerequisites to query the webservice
            if (mtype == RentIt.MediaType.Any || RentItProxy == null) return;

            // build search criteria
            var mc = new MediaCriteria
            {
                Type = mtype,
                Limit = 10, // only ten highest rated
                Order = MediaOrder.RatingDesc,
                Genre = "",
                SearchText = ""
            };

            bestMediaGrid.MediaCriteria = mc;
        }

        private void GetRandomGenreMedias()
        {
            string[] genres = RentItProxy.GetAllGenres(Mtype);

            var random = new Random();
            string randomGenre = genres[random.Next(genres.Length)];

            var mc = new MediaCriteria
            {
                Genre = randomGenre,
                Limit = 10,
                SearchText = "",
                Type = mtype
            };

            RentItProxy.GetMediaItems(mc);
        }

        /// <summary>
        /// extratc correct type result from MediaItems return value
        /// </summary>
        /// <param name="items"></param>
        /// <returns></returns>
        private RentIt.MediaInfo[] ExtractTypeList(RentIt.MediaItems items)
        {
            switch (mtype)
            {
                case RentIt.MediaType.Album:
                    return items.Albums;
                case RentIt.MediaType.Book:
                    return items.Books;
                case RentIt.MediaType.Movie:
                    return items.Movies;
                default:
                    return null;
            }
        }

        /// <summary>
        /// Updates data in usercontrol.
        /// </summary>
        private void RefreshContents()
        {
            Cursor.Current = Cursors.WaitCursor;
            newMediaGrid.Title = "Newest " + mtype.ToString() + "s";
            GetNewest();
            popularMediaGrid.Title = "Popular " + mtype.ToString() + "s";
            GetMostPopular();
            bestMediaGrid.Title = "Highest rated " + mtype.ToString() + "s";
            GetHighestRated();
            Cursor.Current = Cursors.Default;
        }

        /// <summary>
        /// Go to media details context if "new and hot" item is clicked
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void NewAndHotClick(object sender, MouseEventArgs e)
        {
            Cursor.Current = Cursors.WaitCursor;
            MediaInfo mediaInfo = this.newAndHotMedia;

            RentItUserControl mediaDetail;
            string title;

            if (this.Mtype == MediaType.Album)
            {
                mediaDetail = new AlbumDetails
                {
                    RentItProxy = this.RentItProxy,
                    Credentials = this.Credentials,
                    AlbumInfo = (AlbumInfo)mediaInfo
                };
                title = MainForm.Titles.MediaDetailsAlbum;
            }
            else if (this.Mtype == MediaType.Movie)
            {
                mediaDetail = new BookMovieDetails
                {
                    RentItProxy = this.RentItProxy,
                    Credentials = this.Credentials,
                    MovieInfo = (MovieInfo)mediaInfo
                };
                title = MainForm.Titles.MediaDetailsMovie;
            }
            else if (this.Mtype == MediaType.Book)
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
    }
}
