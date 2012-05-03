using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace ClientApp
{
    using BinaryCommunicator;

    using RentIt;

    public partial class MediaSideBar : UserControl
    {

        private MediaInfo mediaInfo;

        public MediaSideBar()
        {
            InitializeComponent();
        }

        internal MediaSideBar(MediaInfo mediaInfo)
            : this()
        {
            this.mediaInfo = mediaInfo;

            this.priceLabel.Text = mediaInfo.Price + " credits.";
            //(this.thumbnailBox.Image = BinaryCommuncator.GetThumbnail(mediaInfo.Id);


        }

        private void MediaSideBar_Load(object sender, EventArgs e)
        {

        }
    }
}
