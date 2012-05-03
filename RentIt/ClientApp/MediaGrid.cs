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
    public partial class MediaGrid : UserControl
    {
        public MediaGrid()
        {
            InitializeComponent();

            var item = new ListViewItem("Test");
            item.SubItems.Add("Test1");
            item.SubItems.Add("Test2");
            item.SubItems.Add("Test3");
            listView1.Items.Add("Test4");
        }
    }
}
