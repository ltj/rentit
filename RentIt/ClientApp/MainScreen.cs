﻿namespace ClientApp {
    using System;

    using RentIt;

    public partial class MainScreen : RentItUserControl {
        public MainScreen() {
            InitializeComponent();
            moviesList.Title = "Featured movies";
            booksList.Title = "Featured books";
            musicList.Title = "Featured music";

            moviesList.RentItProxy = RentItProxy;
            booksList.RentItProxy = RentItProxy;
            musicList.RentItProxy = RentItProxy;

            moviesList.AddDoubleClickEventHandler(MovieListDoubleClick);
            booksList.AddDoubleClickEventHandler(BookListDoubleClick);
            musicList.AddDoubleClickEventHandler(MusicListDoubleClick);

            UpdateLists();
        }

        private void UpdateLists() {
            var criteria = new MediaCriteria {
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
        }

        private void MoviesButtonClick(object sender, EventArgs e) {
            var mediaFront = new MediaFrontpage { RentItProxy = RentItProxy, Mtype = MediaType.Movie };
            FireContentChangeEvent(mediaFront, "Movies");
        }

        private void BooksButtonClick(object sender, EventArgs e) {
            var mediaFront = new MediaFrontpage { RentItProxy = RentItProxy, Mtype = MediaType.Book };
            FireContentChangeEvent(mediaFront, "Books");
        }

        private void MusicButtonClick(object sender, EventArgs e) {
            var mediaFront = new MediaFrontpage { RentItProxy = RentItProxy, Mtype = MediaType.Album };
            FireContentChangeEvent(mediaFront, "Music");
        }

        private void MovieListDoubleClick(object sender, EventArgs e) {
            MediaInfo media = moviesList.GetSingleMedia();
            DisplayMediaItem(media);
        }

        private void BookListDoubleClick(object sender, EventArgs e) {
            MediaInfo media = booksList.GetSingleMedia();
            DisplayMediaItem(media);
        }

        private void MusicListDoubleClick(object sender, EventArgs e) {
            MediaInfo media = musicList.GetSingleMedia();
            DisplayMediaItem(media);
        }

        private void DisplayMediaItem(MediaInfo media) {
            RentItUserControl mediaDetails;
            string title;
            switch(media.Type) {
                case MediaType.Album:
                    mediaDetails = new AlbumDetails { RentItProxy = RentItProxy, AlbumInfo = (AlbumInfo) media };
                    title = "Album details";
                    break;
                case MediaType.Book:
                    mediaDetails = new BookMovieDetails { RentItProxy = RentItProxy, BookInfo = (BookInfo) media };
                    title = "Book details";
                    break;
                case MediaType.Movie:
                    mediaDetails = new BookMovieDetails { RentItProxy = RentItProxy, MovieInfo = (MovieInfo) media };
                    title = "Movie details";
                    break;
                default:
                    return;
            }

            FireContentChangeEvent(mediaDetails, title);
        }
    }
}
