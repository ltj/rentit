using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace ClientApp {
    public partial class GenreList : UserControl {

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

            lstGenres.Clear(); // clear current content

            // web service call might throw a fault exception
            try {
                string[] genres = client.GetAllGenres(mtype);
                foreach (string s in genres) {
                    lstGenres.Items.Add(s);
                }
            }
            catch {
                lstGenres.Items.Add("Unable to retrieve list");
            }
        }

    }
}
