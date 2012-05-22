namespace ClientApp
{
    using System.Windows.Forms;

    using RentIt;

    /// <summary>
    /// RentItUserControl to display a list of genres for
    /// given media type.
    /// </summary>
    internal partial class GenreList : RentItUserControl
    {
        private RentIt.MediaType mtype;

        public GenreList()
        {
            InitializeComponent();
        }

        /// <summary>
        /// get og set media type property
        /// </summary>
        internal RentIt.MediaType Mtype
        {
            get { return mtype; }
            set
            {
                mtype = value;
                GetGenres();
            }
        }

        /// <summary>
        /// Get genres for set type and adds items to ListView
        /// </summary>
        private void GetGenres()
        {
            Cursor.Current = Cursors.WaitCursor;
            if (mtype == RentIt.MediaType.Any || RentItProxy == null) return;

            lstGenres.Clear(); // clear current content

            // web service call might throw a fault exception
            try
            {
                string[] genres = RentItProxy.GetAllGenres(mtype);
                foreach (string s in genres)
                {
                    lstGenres.Items.Add(s);
                }
            }
            catch
            {
                lstGenres.Items.Add("Unable to retrieve list");
            }

            Cursor.Current = Cursors.Default;
        }
    }
}
