using System;

namespace ClientApp
{
    using System.Windows.Forms;

    /// <summary>
    /// The User Control used on the User account page in the Credits pane.
    /// </summary>
    /// <author>Jacob Rasmussen</author>
    internal partial class CreditsControl : RentItUserControl {
        private int credits;

        // Option to choose from 3 different credit values to add to the user account.
        internal int Radio1 = 50;
        internal int Radio2 = 100;
        internal int Radio3 = 500;

        /// <summary>
        /// Updates Credits value each time the credentials are set.
        /// </summary>
        internal override RentIt.AccountCredentials Credentials
        {
            set
            {
                base.Credentials = value;
                UpdateValue();
            }
        }

        public CreditsControl()
        {
            InitializeComponent();
            label1.Text = @"Credits Remaining:";
            button1.Text = @"Add Credits!";
            groupBox1.Text = @"Manage Credits";
            label2.Text = credits.ToString();
            radioButton1.Text = Radio1 + @" Credits.";
            radioButton2.Text = Radio2 + @" Credits.";
            radioButton3.Text = Radio3 + @" Credits.";
        }

        /// <summary>
        /// Updates the displayed Credits value to the value stored in the database.
        /// </summary>
        private void UpdateValue()
        {
            Cursor.Current = Cursors.WaitCursor;

            if(Credentials != null) {
                try{ credits = RentItProxy.GetAllCustomerData(Credentials).Credits;}
                catch { return; }
                FireCreditsChangeEvent(credits);
            }
            label2.Text = credits.ToString();

            Cursor.Current = Cursors.Default;
        }

        /// <summary>
        /// Event when clicking the "Add Credits!" button.
        /// </summary>
        private void Button1Click(object sender, EventArgs e)
        {
            Cursor.Current = Cursors.WaitCursor;

            int selectedAmount = 0;
            //Sets the chosen Credits value.
            if (radioButton1.Checked)
                selectedAmount = Radio1;
            else if (radioButton2.Checked)
                selectedAmount = Radio2;
            else if (radioButton3.Checked)
                selectedAmount = Radio3;

            //Prompts the user to accept the transaction through a MessageBox.
            if (RentItMessageBox.CreditConfirmation(selectedAmount) && Credentials != null) {
                //Clicking Yes, the Credits are added to the user account.
                try { RentItProxy.AddCredits(Credentials, (uint)selectedAmount); }
                catch {Cursor.Current = Cursors.Default; return; }
            }
            //Updates the displayed Credits value.
            UpdateValue();

            Cursor.Current = Cursors.Default;
        }
    }
}
