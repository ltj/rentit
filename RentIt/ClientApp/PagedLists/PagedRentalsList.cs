﻿// -----------------------------------------------------------------------
// <copyright file="DetailedMediaList.cs" company="">
// TODO: Update copyright text.
// </copyright>
// -----------------------------------------------------------------------

namespace ClientApp
{
    using System;
    using System.Collections.Generic;
    using System.ServiceModel;
    using System.Windows.Forms;

    using RentIt;

    /// <author>Kenneth Søhrmann</author>
    /// <summary>
    /// The list shows active rentals by default.
    /// The ShowActive method can be used to toggle view of
    /// active or expired rentals.
    /// </summary>
    internal class PagedRentalsList : PagedListView
    {
        private ColumnHeader titleColumn;
        private ColumnHeader startDateColumn;
        private ColumnHeader endDateColumn;

        private ListViewGroup songListViewGroup;
        private ListViewGroup albumListViewGroup;
        private ListViewGroup movieListViewGroup;
        private ListViewGroup bookListViewGroup;

        private RentItClient serviceClient;

        private List<Rental> activeRentals;

        private List<Rental> expiredRentals;

        private bool showActive = false;

        /// <summary>
        /// Initializes a new instance of the 
        /// </summary>
        public PagedRentalsList()
        {
            // Initialze columns
            this.titleColumn = new ColumnHeader();
            this.startDateColumn = new ColumnHeader();
            this.endDateColumn = new ColumnHeader();

            this.AutoArrange = false;
            this.Columns.AddRange(new System.Windows.Forms.ColumnHeader[] {
                this.titleColumn,
                this.startDateColumn,
                this.endDateColumn,
            });

            this.FullRowSelect = true;
            this.GridLines = true;
            this.Location = new System.Drawing.Point(0, 0);
            this.MultiSelect = false;
            this.Name = "RentalsList";
            this.Size = new System.Drawing.Size(500, 470);
            this.TabIndex = 9;
            this.UseCompatibleStateImageBehavior = false;
            this.View = View.Details;
            // 
            // titleColumn
            // 
            this.titleColumn.Text = "Title";
            this.titleColumn.Width = 220;
            // 
            // startDateColumn
            // 
            this.startDateColumn.Text = "Rental start date";
            this.startDateColumn.Width = 120;
            // 
            // endDateColumn
            // 
            this.endDateColumn.Text = "Rental end date";
            this.endDateColumn.Width = 120;
            // 
            // priceColumn
            // 
            // this.priceColumn.Text = "Price";
            // this.priceColumn.Width = 85;

            // Initialize and add list groups.
            this.songListViewGroup = new ListViewGroup("Songs", HorizontalAlignment.Left);
            this.albumListViewGroup = new ListViewGroup("Albums", HorizontalAlignment.Left);
            this.movieListViewGroup = new ListViewGroup("Movies", HorizontalAlignment.Left);
            this.bookListViewGroup = new ListViewGroup("Books", HorizontalAlignment.Left);
            this.Groups.AddRange(
                new ListViewGroup[] { this.songListViewGroup, this.albumListViewGroup, this.movieListViewGroup, this.bookListViewGroup });

            // Initialize the service proxy client.
            BasicHttpBinding binding = new BasicHttpBinding();
            EndpointAddress address = new EndpointAddress("http://rentit.itu.dk/rentit01/RentItService.svc");
            serviceClient = new RentItClient(binding, address);
        }

        /// <summary>
        /// For setting
        /// </summary>
        internal void ShowActive(bool active)
        {
            this.showActive = active;
            this.PopulateList(this.showActive ? this.activeRentals : this.expiredRentals);
        }

        /// <summary>
        /// Update the ListView with new data.
        /// When this method is called, all data previously added to the list 
        /// will be disregarded, and the contents of the ListView will match
        /// the media items passed in the parameter.
        /// </summary>
        /// <param name="rentals"></param>
        internal void UpdateListContents(List<Rental> rentals)
        {
            this.BeginUpdate();

            this.CurrentItems = new List<ListViewItem>();

            // Only active
            this.activeRentals = rentals.FindAll(rental => rental.EndTime > DateTime.Now);
            this.expiredRentals = rentals.FindAll(rental => rental.EndTime < DateTime.Now);

            this.PopulateList(this.showActive ? this.activeRentals : this.expiredRentals);
        }

        /// <summary>
        /// Helper method for populating the list with rentals.
        /// This method compiles a list of ListViewItems. The items
        /// are not added to the list; this is done in the Repopulate-method.
        /// </summary>
        /// <param name="rentals"></param>
        private void PopulateList(List<Rental> rentals)
        {
            foreach (var rental in rentals)
            {
                switch (rental.MediaType)
                {
                    case MediaType.Album:
                        AlbumInfo album = this.serviceClient.GetAlbumInfo(rental.MediaId);

                        var listItemA = new ListViewItem(album.Title);
                        listItemA.SubItems.Add(rental.StartTime.ToShortDateString());
                        listItemA.SubItems.Add(rental.EndTime.ToShortDateString());
                        listItemA.Group = this.albumListViewGroup;
                        listItemA.Tag = album;

                        this.CurrentItems.Add(listItemA); // this.Items.Add(listItem);
                        break;
                    case MediaType.Movie:
                        MovieInfo movie = this.serviceClient.GetMovieInfo(rental.MediaId);

                        var listItemM = new ListViewItem(movie.Title);
                        listItemM.SubItems.Add(rental.StartTime.ToShortDateString());
                        listItemM.SubItems.Add(rental.EndTime.ToShortDateString());
                        listItemM.Group = this.movieListViewGroup;
                        listItemM.Tag = movie;

                        this.CurrentItems.Add(listItemM); // this.Items.Add(listItem);
                        break;
                    case MediaType.Book:
                        BookInfo book = this.serviceClient.GetBookInfo(rental.MediaId);

                        var listItemB = new ListViewItem(book.Title);
                        listItemB.SubItems.Add(rental.StartTime.ToShortDateString());
                        listItemB.SubItems.Add(rental.EndTime.ToShortDateString());
                        listItemB.Group = this.bookListViewGroup;
                        listItemB.Tag = book;

                        this.CurrentItems.Add(listItemB); // this.Items.Add(listItem);
                        break;
                    default:
                        break;
                }

                this.RePopulateList();
                this.EndUpdate();
            }
        }
    }
}
