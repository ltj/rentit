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
	public partial class CreditsControl : UserControl {
		internal int Credits { get; private set; }

		internal int Radio1 = 50;
		internal int Radio2 = 100;
		internal int Radio3 = 500;

		public CreditsControl()
		{
			InitializeComponent();
			label1.Text = @"Credits Remaining:";
			button1.Text = @"Add Credits!";
			groupBox1.Text = @"Manage Credits";
			radioButton1.Text = Radio1 + @" Credits.";
			radioButton2.Text = Radio2 + @" Credits.";
			radioButton3.Text = Radio3 + @" Credits.";
			this.UpdateValue();
		}

		private void UpdateValue() {
			label2.Text = Credits.ToString();
		}

		private void button1_Click(object sender, EventArgs e) {
			int selectedAmount = 0;
			if (radioButton1.Checked) 
				selectedAmount = Radio1;
			else if (radioButton2.Checked) 
				selectedAmount = Radio2;
			else if (radioButton3.Checked) 
				selectedAmount = Radio3;

			if (RentItMessageBox.CreditConfirmation(selectedAmount))
				Credits += selectedAmount;

			this.UpdateValue();
		}

		private void maskedTextBox1_MaskInputRejected(object sender, MaskInputRejectedEventArgs e)
		{

		}


	}
}
