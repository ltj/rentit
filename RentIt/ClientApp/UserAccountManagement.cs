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
    /// <summary>
    /// A user control containing user controls relevant to user account management. 
    /// </summary>
    public partial class UserAccountManagement : RentItUserControl
    {
        public UserAccountManagement()
        {
            InitializeComponent();
        }

        /// <summary>
        /// Selects the tab with the specified index.
        /// If the index is out of range that first tab
        /// of the control is selected.
        /// </summary>
        /// <param name="index"></param>
        public void SelectTab(int index)
        {
            if (index < 0 || index > this.tabControl.TabCount - 1)
            {
                index = 0;
            }

            this.tabControl.SelectTab(index);
        }


    }
}
