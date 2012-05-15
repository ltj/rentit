﻿
namespace ClientApp
{
    using BinaryCommunicator;

    internal partial class MediaFrontpage : RentItUserControl
    {

        private RentIt.MediaType mtype;
        private RentItClient client;

        public MediaFrontpage()
        {
            InitializeComponent();
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
                client = value;
                genreList1.RentItProxy = value;
            }
        }

        // Get newest media and publish to "new and hot" area
        private void GetNewest()
        {
            // we need valid prerequisites to query the webservice
            if (mtype == RentIt.MediaType.Any || client == null) return;

            // build search criteria
            RentIt.MediaCriteria mc = new RentIt.MediaCriteria
            {
                Type = mtype,
                Limit = 1, // only one result
                Order = RentIt.MediaOrder.ReleaseDateDesc,
                Genre = "",
                SearchText = ""
            };

            try
            {
                RentIt.MediaItems result = client.GetMediaItems(mc);
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
            if (mtype == RentIt.MediaType.Any || client == null) return;

            // build search criteria
            RentIt.MediaCriteria mc = new RentIt.MediaCriteria
            {
                Type = mtype,
                Limit = 10, // only ten most pouplar
                Order = RentIt.MediaOrder.PopularityDesc,
                Genre = "",
                SearchText = ""
            };

            mediaGrid1.MediaCriteria = mc;
        }

        // extrac correct type result from MediaItems return value
        private RentIt.MediaInfo[] ExtractTypeList(RentIt.MediaItems items)
        {
            switch (this.mtype)
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
            GetNewest();
            mediaGrid1.Title = "Popular " + mtype.ToString() + "s";
            GetMostPopular();
        }
    }
}
