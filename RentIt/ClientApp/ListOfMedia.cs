using System;
using System.Collections.Generic;
using System.Linq;
using System.Windows.Forms;

namespace ClientApp
{
    using System.ServiceModel;

    using RentIt;

    public partial class ListOfMedia : UserControl {
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

        internal string Title {
            get { return label1.Text; }
            set { label1.Text = value; }
        }

        internal bool Numbering { get; set; }

        internal void UpdateList(MediaCriteria criteria)
        {
            var medias = rentIt.GetMediaItems(criteria);

            var list = new List<MediaInfo>();
            list.AddRange(medias.Albums);
            list.AddRange(medias.Books);
            list.AddRange(medias.Movies);
            
            if (list.Count() > 0 && !Numbering)
                for (int i = 0; i < list.Count() - 1; i++)
                    listBox1.Items.Add(list[i].Title);

            if (list.Count() > 0 && Numbering)
                for (int i = 0; i < list.Count() - 1; i++)
                    listBox1.Items.Add((i + 1) + ". " + list[i].Title);
             
        }

        private void ListBox1SelectedIndexChanged(object sender, EventArgs e)
        {

        }
    }
}
