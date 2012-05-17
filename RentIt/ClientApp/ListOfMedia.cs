namespace ClientApp
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Windows.Forms;

    using RentIt;

    /// <summary>
    /// User control showing a list media based on a given criteria. Media items can be optionally numbered.
    /// </summary>
    /// <author>Jacob Rasmussen</author>
    internal partial class ListOfMedia : RentItUserControl {
        private List<MediaInfo> mediaList;

        public ListOfMedia()
        {
            InitializeComponent();
            Title = "";
            Numbering = false;
        }

        /// <summary>
        /// The title of the list eg. "Movie Chart".
        /// </summary>
        internal string Title
        {
            get { return label1.Text; }
            set { label1.Text = value; }
        }

        /// <summary>
        /// A true value will enable numbering on displayed media items.
        /// </summary>
        internal bool Numbering { get; set; }

        /// <summary>
        /// Updates the list with media items fullfilling the given criteria.
        /// </summary>
        /// <param name="criteria">The criteria used to filter the media.</param>
        internal void UpdateList(MediaCriteria criteria)
        {
            Cursor.Current = Cursors.WaitCursor;
            //Gets the relevant media from the database.
            var medias = RentItProxy.GetMediaItems(criteria);

            //Concatenates the lists contained in the object returned from the database in a new list.
            mediaList = new List<MediaInfo>();
            mediaList.AddRange(medias.Albums);
            mediaList.AddRange(medias.Books);
            mediaList.AddRange(medias.Movies);

            //Adds the items to the list in the user control.
            if (mediaList.Count() > 0 && !Numbering)
                for (int i = 0; i < mediaList.Count() - 1; i++)
                    listBox1.Items.Add(mediaList[i].Title);

            //Same as above but including numbering of media items.
            if (mediaList.Count() > 0 && Numbering)
                for (int i = 0; i < mediaList.Count() - 1; i++)
                    listBox1.Items.Add((i + 1) + ". " + mediaList[i].Title);

            Cursor.Current = Cursors.Default;
        }

        private void ListBox1SelectedIndexChanged(object sender, EventArgs e)
        {

        }

        /// <summary>
        /// Adds an EventHandler to the Double Click event on the list.
        /// </summary>
        /// <param name="handler"></param>
        internal void AddDoubleClickEventHandler(EventHandler handler) {
            listBox1.DoubleClick += handler;
        }

        /// <summary>
        /// Returns the single selected item of the list. If the number of selected
        /// items is different from one, the method returns null.
        /// </summary>
        /// <returns></returns>
        internal MediaInfo GetSingleMedia() {
            if(listBox1.SelectedItems.Count != 1) {
                return null;
            }

            return mediaList[listBox1.SelectedIndex];
        }
    }
}
