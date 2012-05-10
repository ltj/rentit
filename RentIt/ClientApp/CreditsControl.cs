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
	using System.ServiceModel;

	using RentIt;

	/// <summary>
	/// The User Control used on the User account page in the Credits pane.
	/// </summary>
	/// <author>Jacob Rasmussen</author>
	public partial class CreditsControl : UserControl {
		internal int Credits { get; private set; }

		// Option to choose from 3 different credit values to add to the user account.
		internal int Radio1 = 50;
		internal int Radio2 = 100;
		internal int Radio3 = 500;

		private readonly RentItClient rentIt;

		// Credentials representing the user currently logged in.
		private RentIt.AccountCredentials accCred;

		/// <summary>
		/// Updates Credits value each time the credentials are set.
		/// </summary>
		internal RentIt.AccountCredentials AccCred {
			get { return accCred; }
			set {
				accCred = value;
				this.UpdateValue();
			}
		}

		public CreditsControl() {
			InitializeComponent();
			label1.Text = @"Credits Remaining:";
			button1.Text = @"Add Credits!";
			groupBox1.Text = @"Manage Credits";
			label2.Text = Credits.ToString();
			radioButton1.Text = Radio1 + @" Credits.";
			radioButton2.Text = Radio2 + @" Credits.";
			radioButton3.Text = Radio3 + @" Credits.";
			var binding = new BasicHttpBinding();
			var address = new EndpointAddress("http://rentit.itu.dk/rentit01/RentItService.svc");
			rentIt = new RentItClient(binding, address);
		}

		/// <summary>
		/// Updates the displayed Credits value to the value stored in the database.
		/// </summary>
		private void UpdateValue() {
			if (AccCred != null) 
				Credits = rentIt.GetAllCustomerData(accCred).Credits;
			label2.Text = Credits.ToString();
			
		}

		/// <summary>
		/// Event when clicking the "Add Credits!" button.
		/// </summary>
		private void button1_Click(object sender, EventArgs e) {
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
				rentIt.AddCredits(accCred, (uint)selectedAmount);

			//Updates the displayed Credits value.
			this.UpdateValue();
		}
	}
}
