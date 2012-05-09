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

        /*
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
            this.lengthLabel.Visible = true;
            this.lengthValueLabel.Visible = true;

            switch (mediaInfo.Type)
            {
                case MediaType.Album:
                    var albumInfo = (AlbumInfo)mediaInfo;
                    this.originLabel.Text = "Album artist:";
                    this.originValueLabel.Text = albumInfo.AlbumArtist;
                    this.lengthLabel.Text = "Total duration:";
                    this.lengthValueLabel.Text = albumInfo.TotalDuration.ToString();
                    break;
                case MediaType.Book:
                    var bookInfo = (BookInfo)mediaInfo;
                    this.originLabel.Text = "Author:";
                    this.originValueLabel.Text = bookInfo.Author;
                    this.lengthLabel.Text = "Number of pages:";
                    this.lengthValueLabel.Text = bookInfo.Pages.ToString();
                    break;
                case MediaType.Song:
                    var songInfo = (SongInfo)mediaInfo;
                    this.originLabel.Text = "Artist";
                    this.originValueLabel.Text = songInfo.Artist;
                    this.lengthLabel.Text = "Song duration:";
                    this.lengthValueLabel.Text = songInfo.Duration.ToString();
                    break;
                case MediaType.Movie:
                    var movieInfo = (MovieInfo)mediaInfo;
                    this.originLabel.Text = "Director";
                    this.originValueLabel.Text = movieInfo.Director;
                    this.lengthLabel.Text = "Duration:";
                    this.lengthValueLabel.Text = movieInfo.Duration.ToString();
                    break;
                default:
                    this.originLabel.Visible = false;
                    this.originValueLabel.Visible = false;
                    this.lengthLabel.Visible = false;
                    this.lengthValueLabel.Visible = false;
                    break;
            }
        }
         */

        internal MediaInfo MediaInfoData
        {
            set
            {
                var mediaInfo = value;

                this.mediaInfo = mediaInfo;

                this.priceLabel.Text = mediaInfo.Price + " credits.";
                //(this.thumbnailBox.Image = BinaryCommuncator.GetThumbnail(mediaInfo.Id);

                this.genreValueLabel.Text = mediaInfo.Genre;
                this.releaseDateValueLabel.Text = mediaInfo.ReleaseDate.ToShortDateString();
                this.publisherValueLabel.Text = mediaInfo.Publisher;

                this.originLabel.Visible = true;
                this.originValueLabel.Visible = true;
                this.lengthLabel.Visible = true;
                this.lengthValueLabel.Visible = true;

                switch (mediaInfo.Type)
                {
                    case MediaType.Album:
                        var albumInfo = (AlbumInfo)mediaInfo;
                        this.originLabel.Text = "Album artist:";
                        this.originValueLabel.Text = albumInfo.AlbumArtist;
                        this.lengthLabel.Text = "Total duration:";
                        this.lengthValueLabel.Text = albumInfo.TotalDuration.ToString();
                        break;
                    case MediaType.Book:
                        var bookInfo = (BookInfo)mediaInfo;
                        this.originLabel.Text = "Author:";
                        this.originValueLabel.Text = bookInfo.Author;
                        this.lengthLabel.Text = "Number of pages:";
                        this.lengthValueLabel.Text = bookInfo.Pages.ToString();
                        break;
                    case MediaType.Song:
                        var songInfo = (SongInfo)mediaInfo;
                        this.originLabel.Text = "Artist";
                        this.originValueLabel.Text = songInfo.Artist;
                        this.lengthLabel.Text = "Song duration:";
                        this.lengthValueLabel.Text = songInfo.Duration.ToString();
                        break;
                    case MediaType.Movie:
                        var movieInfo = (MovieInfo)mediaInfo;
                        this.originLabel.Text = "Director";
                        this.originValueLabel.Text = movieInfo.Director;
                        this.lengthLabel.Text = "Duration:";
                        this.lengthValueLabel.Text = movieInfo.Duration.ToString();
                        break;
                    default:
                        this.originLabel.Visible = false;
                        this.originValueLabel.Visible = false;
                        this.lengthLabel.Visible = false;
                        this.lengthValueLabel.Visible = false;
                        break;
                }

                this.thumbnailBox.Image = BinaryCommuncator.GetThumbnail(mediaInfo.Id);
            }
        }

        private void MediaSideBar_Load(object sender, EventArgs e)
        {

        }
    }
}
