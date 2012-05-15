namespace ClientApp
{
    internal partial class GenreList : RentItUserControl
    {
        private RentIt.MediaType mtype;

        public GenreList()
        {
            InitializeComponent();
        }

        internal RentIt.MediaType Mtype
        {
            get { return mtype; }
            set
            {
                mtype = value;
                GetGenres();
            }
        }

        // get genres for set type
        private void GetGenres()
        {
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
        }

    }
}
