
namespace ClientApp
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.ServiceModel;

    using RentIt;

    /// <summary>
    /// User control showing a list media based on a given criteria. Media items can be optionally numbered.
    /// </summary>
    /// <author>Jacob Rasmussen</author>
    internal partial class ListOfMedia : RentItUserControl
    {
        private readonly RentItClient rentIt;

        public ListOfMedia()
        {
            InitializeComponent();
            var binding = new BasicHttpBinding();
            var address = new EndpointAddress("http://rentit.itu.dk/rentit01/RentItService.svc");
            rentIt = new RentItClient(binding, address);
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
            //Gets the relevant media from the database.
            var medias = rentIt.GetMediaItems(criteria);

            //Concatenates the lists contained in the object returned from the database in a new list.
            var list = new List<MediaInfo>();
            list.AddRange(medias.Albums);
            list.AddRange(medias.Books);
            list.AddRange(medias.Movies);

            //Adds the items to the list in the user control.
            if (list.Count() > 0 && !Numbering)
                for (int i = 0; i < list.Count() - 1; i++)
                    listBox1.Items.Add(list[i].Title);

            //Same as above but including numbering of media items.
            if (list.Count() > 0 && Numbering)
                for (int i = 0; i < list.Count() - 1; i++)
                    listBox1.Items.Add((i + 1) + ". " + list[i].Title);

        }

        private void ListBox1SelectedIndexChanged(object sender, EventArgs e)
        {

        }
    }
}
