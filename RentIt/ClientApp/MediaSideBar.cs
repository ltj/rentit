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
    using BinaryCommunicator;

    using RentIt;

    public partial class MediaSideBar : UserControl
    {

        private MediaInfo mediaInfo;

        public MediaSideBar()
        {
            InitializeComponent();
        }

        internal MediaSideBar(MediaInfo mediaInfo)
            : this()
        {
            this.mediaInfo = mediaInfo;

            this.priceLabel.Text = mediaInfo.Price + " credits.";
            //(this.thumbnailBox.Image = BinaryCommuncator.GetThumbnail(mediaInfo.Id);

            this.genreValueLabel.Text = mediaInfo.Genre;
            this.releaseDateValueLabel.Text = mediaInfo.ReleaseDate.ToShortDateString();
            this.publisherValueLabel.Text = mediaInfo.Publisher;

            this.originLabel.Visible = true;
            this.originValueLabel.Visible = true;

            switch (mediaInfo.Type)
            {
                case MediaType.Album:
                    var albumInfo = (AlbumInfo)mediaInfo;
                    this.originLabel.Text = "Album artist:";
                    this.originValueLabel.Text = albumInfo.AlbumArtist;
                    break;
                case MediaType.Book:
                    var bookInfo = (BookInfo)mediaInfo;
                    this.originLabel.Text = "Author:";
                    this.originValueLabel.Text = bookInfo.Author;
                    break;
                case MediaType.Song:
                    var songInfo = (SongInfo)mediaInfo;
                    this.originLabel.Text = "Artist";
                    this.originValueLabel.Text = songInfo.Artist;
                    break;
                case MediaType.Movie:
                    var movieInfo = (MovieInfo)mediaInfo;
                    this.originLabel.Text = "Director";
                    this.originValueLabel.Text = movieInfo.Director;
                    break;
                default:
                    this.originLabel.Visible = false;
                    this.originValueLabel.Visible = false;
                    break;
            }
        }

        private void MediaSideBar_Load(object sender, EventArgs e)
        {

        }
    }
}
