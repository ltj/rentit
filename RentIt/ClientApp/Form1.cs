using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.ServiceModel;

namespace ClientApp
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            BasicHttpBinding binding = new BasicHttpBinding();
            EndpointAddress address = new EndpointAddress("http://rentit.itu.dk/rentit01/RentItService.svc");
            var rentIt = new RentItClient(binding, address);
            InitializeComponent();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            mediaFrontpage1.Mtype = RentIt.MediaType.Album;
        }

        private void comboBox1_SelectedIndexChanged(object sender, EventArgs e)
        {

        }

    }
}
