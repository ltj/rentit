﻿using System;

namespace ClientApp
{
    /// <summary>
    /// The User Control used on the User account page in the Credits pane.
    /// </summary>
    /// <author>Jacob Rasmussen</author>
    internal partial class CreditsControl : RentItUserControl
    {
        internal int Credits { get; private set; }

        // Option to choose from 3 different credit values to add to the user account.
        internal int Radio1 = 50;
        internal int Radio2 = 100;
        internal int Radio3 = 500;

        // Credentials representing the user currently logged in.
        private RentIt.AccountCredentials accCred;

        /// <summary>
        /// Updates Credits value each time the credentials are set.
        /// </summary>
        internal RentIt.AccountCredentials AccCred
        {
            get { return accCred; }
            set
            {
                accCred = value;
                this.UpdateValue();
            }
        }

        public CreditsControl()
        {
            InitializeComponent();
            label1.Text = @"Credits Remaining:";
            button1.Text = @"Add Credits!";
            groupBox1.Text = @"Manage Credits";
            label2.Text = Credits.ToString();
            radioButton1.Text = Radio1 + @" Credits.";
            radioButton2.Text = Radio2 + @" Credits.";
            radioButton3.Text = Radio3 + @" Credits.";
        }

        /// <summary>
        /// Updates the displayed Credits value to the value stored in the database.
        /// </summary>
        private void UpdateValue()
        {
            if (AccCred != null)
                Credits = RentItProxy.GetAllCustomerData(accCred).Credits;
            label2.Text = Credits.ToString();

        }

        /// <summary>
        /// Event when clicking the "Add Credits!" button.
        /// </summary>
        private void button1_Click(object sender, EventArgs e)
        {
            int selectedAmount = 0;
            //Sets the chosen Credits value.
            if (radioButton1.Checked)
                selectedAmount = Radio1;
            else if (radioButton2.Checked)
                selectedAmount = Radio2;
            else if (radioButton3.Checked)
                selectedAmount = Radio3;

            //Prompts the user to accept the transaction through a MessageBox.
            if (RentItMessageBox.CreditConfirmation(selectedAmount) && accCred != null)
                //Clicking Yes, the Credits are added to the user account.
                RentItProxy.AddCredits(accCred, (uint)selectedAmount);

            //Updates the displayed Credits value.
            this.UpdateValue();
        }
    }
}
