// -----------------------------------------------------------------------
// <copyright file="DetailedMediaList.cs" company="">
// TODO: Update copyright text.
// </copyright>
// -----------------------------------------------------------------------

namespace ClientApp
{
    using System;
    using System.Collections.Generic;
    using System.Windows.Forms;

    using RentIt;

    /// <author>Kenneth Søhrmann</author>
    /// <summary>
    /// TODO: Update summary.
    /// </summary>
    internal class PagedDetailedMediaList : PagedListView
    {
        private ColumnHeader titleColumn;
        private ColumnHeader authorColumn;
        private ColumnHeader genreColumn;
        private ColumnHeader releaseDateColumn;
        private ColumnHeader priceColumn;

        private ListViewGroup songListViewGroup;
        private ListViewGroup albumListViewGroup;
        private ListViewGroup movieListViewGroup;
        private ListViewGroup bookListViewGroup;

        /// <summary>
        /// Initializes a new instance of the MediaList class.
        /// </summary>
        public PagedDetailedMediaList()
        {
            // Initialze columns
            this.titleColumn = new ColumnHeader();
            this.authorColumn = new ColumnHeader();
            this.genreColumn = new ColumnHeader();
            this.releaseDateColumn = new ColumnHeader();
            this.priceColumn = new ColumnHeader();

            this.AutoArrange = false;
            this.Columns.AddRange(new ColumnHeader[] {
                this.titleColumn,
                this.authorColumn,
                this.genreColumn,
                this.releaseDateColumn,
                this.priceColumn
            });

            // this.Dock = System.Windows.Forms.DockStyle.Fill;
            this.FullRowSelect = true;
            this.GridLines = true;
            this.Location = new System.Drawing.Point(0, 0);
            this.MultiSelect = false;
            this.Name = "DetailedMediaList";
            this.Size = new System.Drawing.Size(716, 519);
            this.TabIndex = 9;
            this.UseCompatibleStateImageBehavior = false;
            this.View = System.Windows.Forms.View.Details;
            // 
            // titleColumn
            // 
            this.titleColumn.Text = "Title";
            this.titleColumn.Width = 214;
            // 
            // authorColumn
            // 
            this.authorColumn.Text = "Author";
            this.authorColumn.Width = 173;
            // 
            // genreColumn
            // 
            this.genreColumn.Text = "Genre";
            this.genreColumn.Width = 156;
            // 
            // releaseDateColumn
            // 
            this.releaseDateColumn.Text = "Release Date";
            this.releaseDateColumn.Width = 84;
            // 
            // priceColumn
            // 
            this.priceColumn.Text = "Price";
            this.priceColumn.Width = 85;

            // Initialize and add list groups.
            this.songListViewGroup = new ListViewGroup("Songs", HorizontalAlignment.Left);
            this.albumListViewGroup = new ListViewGroup("Albums", HorizontalAlignment.Left);
            this.movieListViewGroup = new ListViewGroup("Movies", HorizontalAlignment.Left);
            this.bookListViewGroup = new ListViewGroup("Books", HorizontalAlignment.Left);
            this.Groups.AddRange(
                new ListViewGroup[] { this.songListViewGroup, this.albumListViewGroup, this.movieListViewGroup, this.bookListViewGroup });
        }

        /// <summary>
        /// Updates the ListView with the submitted data.
        /// When this method is called, all data previously added to the list 
        /// will be disregarded, and the contents of the ListView will match
        /// the media items passed in the parameter.
        /// </summary>
        /// <param name="mediaItems"></param>
        internal void UpdateListContents(MediaItems mediaItems)
        {
            this.BeginUpdate();

            this.CurrentItems = new List<ListViewItem>();

            foreach (var song in mediaItems.Songs)
            {
                var listItem = new ListViewItem(song.Title);
                listItem.SubItems.Add(song.Artist);
                listItem.SubItems.Add(song.Genre);
                listItem.SubItems.Add(song.ReleaseDate.ToShortDateString());
                listItem.SubItems.Add(song.Price.ToString());
                listItem.Group = this.songListViewGroup;
                listItem.Tag = song;

                this.CurrentItems.Add(listItem); // this.Items.Add(listItem);
            }

            foreach (var album in mediaItems.Albums)
            {
                var listItem = new ListViewItem(album.Title);
                listItem.SubItems.Add(album.AlbumArtist);
                listItem.SubItems.Add(album.Genre);
                listItem.SubItems.Add(album.ReleaseDate.ToShortDateString());
                listItem.SubItems.Add(album.Price.ToString());
                listItem.Group = this.albumListViewGroup;
                listItem.Tag = album;

                this.CurrentItems.Add(listItem); // this.Items.Add(listItem);
            }

            foreach (var movie in mediaItems.Movies)
            {
                var listItem = new ListViewItem(movie.Title);
                listItem.SubItems.Add(movie.Director);
                listItem.SubItems.Add(movie.Genre);
                listItem.SubItems.Add(movie.ReleaseDate.ToShortDateString());
                listItem.SubItems.Add(movie.Price.ToString());
                listItem.Group = this.movieListViewGroup;
                listItem.Tag = movie;

                this.CurrentItems.Add(listItem); // this.Items.Add(listItem);
            }

            foreach (var book in mediaItems.Books)
            {
                var listItem = new ListViewItem(book.Title);
                listItem.SubItems.Add(book.Author);
                listItem.SubItems.Add(book.Genre);
                listItem.SubItems.Add(book.ReleaseDate.ToShortDateString());
                listItem.SubItems.Add(book.Price.ToString());
                listItem.Group = this.bookListViewGroup;
                listItem.Tag = book;

                this.CurrentItems.Add(listItem); // this.Items.Add(listItem);
            }

            this.RePopulateList();
            this.EndUpdate();
        }
    }
}
