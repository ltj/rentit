namespace ClientApp
{
    using System;
    using System.Collections.Generic;
    using System.ServiceModel;
    using System.Windows.Forms;

    using RentIt;

    public partial class PublisherAccountManagement : UserControl
    {
        private PublisherAccount accountData;

        private Dictionary<ListViewItem, MediaInfo> map = new Dictionary<ListViewItem, MediaInfo>();


        private ListViewGroup songListViewGroup;
        private ListViewGroup albumListViewGroup;
        private ListViewGroup movieListViewGroup;
        private ListViewGroup bookListViewGroup;

        public PublisherAccountManagement()
        {
            InitializeComponent();

            deleteMediaButton.Enabled = false;
            changePriceButton.Enabled = false;

            // Initialize the groups and add them to the list view.
            this.songListViewGroup = new ListViewGroup("Songs", HorizontalAlignment.Left);
            this.albumListViewGroup = new ListViewGroup("Albums", HorizontalAlignment.Left);
            this.movieListViewGroup = new ListViewGroup("Movies", HorizontalAlignment.Left);
            this.bookListViewGroup = new ListViewGroup("Books", HorizontalAlignment.Left);
            this.publishedMediaList.Groups.AddRange(new ListViewGroup[] 
            {
                this.songListViewGroup,
                this.albumListViewGroup,
                this.movieListViewGroup,
                this.bookListViewGroup
            });

            publishedMediaList.SelectedIndexChanged += this.SelectedIndexChangedHandler;

            BasicHttpBinding binding = new BasicHttpBinding();
            EndpointAddress address = new EndpointAddress("http://rentit.itu.dk/rentit01/RentItService.svc");
            RentItClient serviceClient = new RentItClient(binding, address);
            accountData = serviceClient.GetAllPublisherData(new AccountCredentials()
                {
                    UserName = "publishCorp",
                    HashedPassword = "7110EDA4D09E062AA5E4A390B0A572AC0D2C0220"
                });
            this.PopulateList();
        }

        internal PublisherAccountManagement(PublisherAccount accountData)
            : this()
        {
            this.accountData = accountData;
            this.PopulateList();
        }

        private void PopulateList()
        {
            foreach (var song in accountData.PublishedItems.Songs)
            {
                var listItem = new ListViewItem(song.Title);
                listItem.SubItems.Add(song.Artist);
                listItem.SubItems.Add(song.Genre);
                listItem.SubItems.Add(song.ReleaseDate.ToShortDateString());
                listItem.SubItems.Add(song.Price.ToString());
                listItem.Group = this.songListViewGroup;
                publishedMediaList.Items.Add(listItem);

                map.Add(listItem, song);
            }

            foreach (var album in accountData.PublishedItems.Albums)
            {
                var listItem = new ListViewItem(album.Title);
                listItem.SubItems.Add(album.AlbumArtist);
                listItem.SubItems.Add(album.Genre);
                listItem.SubItems.Add(album.ReleaseDate.ToShortDateString());
                listItem.SubItems.Add(album.Price.ToString());
                listItem.Group = this.albumListViewGroup;
                publishedMediaList.Items.Add(listItem);

                map.Add(listItem, album);
            }

            foreach (var book in accountData.PublishedItems.Books)
            {
                var listItem = new ListViewItem(book.Title);
                listItem.SubItems.Add(book.Author);
                listItem.SubItems.Add(book.Genre);
                listItem.SubItems.Add(book.ReleaseDate.ToShortDateString());
                listItem.SubItems.Add(book.Price.ToString());
                listItem.Group = this.songListViewGroup;
                publishedMediaList.Items.Add(listItem);

                map.Add(listItem, book);
            }

            foreach (var movie in accountData.PublishedItems.Movies)
            {
                var listItem = new ListViewItem(movie.Title);
                listItem.SubItems.Add(movie.Director);
                listItem.SubItems.Add(movie.Genre);
                listItem.SubItems.Add(movie.ReleaseDate.ToShortDateString());
                listItem.SubItems.Add(movie.Price.ToString());
                listItem.Group = this.movieListViewGroup;
                publishedMediaList.Items.Add(listItem);

                map.Add(listItem, movie);
            }
        }

        #region EventHandlers

        private void SelectedIndexChangedHandler(object obj, EventArgs e)
        {
            // Both buttons are enabled when only one item in the list is selected.
            if (publishedMediaList.SelectedItems.Count != 1)
            {
                deleteMediaButton.Enabled = false;
                changePriceButton.Enabled = false;
            }
            // If the single selected item is contained in the Songs group, it is
            // not possible to delete the media item; the delete button is disabled.
            else
            {
                changePriceButton.Enabled = true;
                if (publishedMediaList.SelectedItems.Count == 0) return;
                string groupName = publishedMediaList.SelectedItems[0].Group.Header;
                deleteMediaButton.Enabled = !groupName.Equals("Songs");
            }
        }


        #endregion
    }
}
