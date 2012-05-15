using BinaryCommunicator;

namespace ClientApp {
    using System;

    using RentIt;

    internal partial class MediaFrontpage : RentItUserControl
    {
        private RentIt.MediaType mtype;

        public MediaFrontpage()
        {
            InitializeComponent();
            popularMediaGrid.ContentChangeEvent += ContentChangeEventPropagated;
            newMediaGrid.ContentChangeEvent += ContentChangeEventPropagated;
            bestMediaGrid.ContentChangeEvent += ContentChangeEventPropagated;
        }

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

        internal override RentItClient RentItProxy
        {
            set
            {
                base.RentItProxy = value;
                genreList1.RentItProxy = value;
                popularMediaGrid.RentItProxy = value;
                newMediaGrid.RentItProxy = value;
                bestMediaGrid.RentItProxy = value;
            }
        }

        // Get newest media and publish to "new and hot" area
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

        // Get ten most popular medias
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

        // Get ten highest rated medias
        private void GetHighestRated() {
            // we need valid prerequisites to query the webservice
            if(mtype == RentIt.MediaType.Any || RentItProxy == null) return;

            // build search criteria
            var mc = new MediaCriteria {
                Type = mtype,
                Limit = 10, // only ten highest rated
                Order = MediaOrder.RatingDesc,
                Genre = "",
                SearchText = ""
            };

            bestMediaGrid.MediaCriteria = mc;
        }

        private void GetRandomGenreMedias() {
            string[] genres = RentItProxy.GetAllGenres(Mtype);

            var random = new Random();
            string randomGenre = genres[random.Next(genres.Length)];

            var mc = new MediaCriteria {
                                           Genre = randomGenre,
                                           Limit = 10,
                                           SearchText = "",
                                           Type = mtype
                                       };

            RentItProxy.GetMediaItems(mc);
        }

        // extrac correct type result from MediaItems return value
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

        private void RefreshContents()
        {
            newMediaGrid.Title = "Newest " + mtype.ToString() + "s";
            GetNewest();
            popularMediaGrid.Title = "Popular " + mtype.ToString() + "s";
            GetMostPopular();
            bestMediaGrid.Title = "Highest rated " + mtype.ToString() + "s";
            GetHighestRated();
        }
    }
}
