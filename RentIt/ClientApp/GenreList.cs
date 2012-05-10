using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace ClientApp {
    public partial class GenreList : RentItUserControl {

        private RentItClient client;
        private RentIt.MediaType mtype;

        public GenreList() {
            InitializeComponent();
        }

        internal RentIt.MediaType Mtype {
            get { return mtype; }
            set {
                mtype = value;
                getGenres();
            }
        }

        internal RentItClient Proxy {
            set { client = value; }
        }

        // get genres for set type
        private void getGenres() {
            if (mtype == RentIt.MediaType.Any || client == null) return;
            string[] genres = client.GetAllGenres(mtype);

            lstGenres.Clear();

            foreach (string s in genres) {
                lstGenres.Items.Add(s);
            }
        }
    }
}
