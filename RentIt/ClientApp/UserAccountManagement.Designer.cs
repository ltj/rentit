namespace ClientApp
{
	partial class UserAccountManagement
	{
		/// <summary> 
		/// Required designer variable.
		/// </summary>
		private System.ComponentModel.IContainer components = null;

		/// <summary> 
		/// Clean up any resources being used.
		/// </summary>
		/// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
		protected override void Dispose(bool disposing)
		{
			if (disposing && (components != null))
			{
				components.Dispose();
			}
			base.Dispose(disposing);
		}

		#region Component Designer generated code

		/// <summary> 
		/// Required method for Designer support - do not modify 
		/// the contents of this method with the code editor.
		/// </summary>
		private void InitializeComponent()
		{
            this.tabControl = new System.Windows.Forms.TabControl();
            this.tabPage1 = new System.Windows.Forms.TabPage();
            this.deleteAccountButton = new System.Windows.Forms.Button();
            this.editAccount1 = new ClientApp.EditAccount();
            this.tabPage2 = new System.Windows.Forms.TabPage();
            this.rentalsListControl = new ClientApp.PagedRentalsListControl();
            this.tabPage3 = new System.Windows.Forms.TabPage();
            this.creditsControl1 = new ClientApp.CreditsControl();
            this.tabControl.SuspendLayout();
            this.tabPage1.SuspendLayout();
            this.tabPage2.SuspendLayout();
            this.tabPage3.SuspendLayout();
            this.SuspendLayout();
            // 
            // tabControl
            // 
            this.tabControl.Anchor = ((System.Windows.Forms.AnchorStyles) ((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom)
                        | System.Windows.Forms.AnchorStyles.Left)
                        | System.Windows.Forms.AnchorStyles.Right)));
            this.tabControl.Controls.Add(this.tabPage1);
            this.tabControl.Controls.Add(this.tabPage2);
            this.tabControl.Controls.Add(this.tabPage3);
            this.tabControl.Location = new System.Drawing.Point(4, 4);
            this.tabControl.Name = "tabControl";
            this.tabControl.SelectedIndex = 0;
            this.tabControl.Size = new System.Drawing.Size(768, 494);
            this.tabControl.TabIndex = 0;
            // 
            // tabPage1
            // 
            this.tabPage1.Controls.Add(this.deleteAccountButton);
            this.tabPage1.Controls.Add(this.editAccount1);
            this.tabPage1.Location = new System.Drawing.Point(4, 22);
            this.tabPage1.Name = "tabPage1";
            this.tabPage1.Padding = new System.Windows.Forms.Padding(3);
            this.tabPage1.Size = new System.Drawing.Size(760, 468);
            this.tabPage1.TabIndex = 0;
            this.tabPage1.Text = "Account Management";
            this.tabPage1.UseVisualStyleBackColor = true;
            // 
            // deleteAccountButton
            // 
            this.deleteAccountButton.Anchor = System.Windows.Forms.AnchorStyles.None;
            this.deleteAccountButton.Location = new System.Drawing.Point(429, 275);
            this.deleteAccountButton.Name = "deleteAccountButton";
            this.deleteAccountButton.Size = new System.Drawing.Size(91, 25);
            this.deleteAccountButton.TabIndex = 1;
            this.deleteAccountButton.Text = "Delete account";
            this.deleteAccountButton.UseVisualStyleBackColor = true;
            this.deleteAccountButton.Click += new System.EventHandler(this.DeleteAccountButtonClick);
            // 
            // editAccount1
            // 
            this.editAccount1.Anchor = System.Windows.Forms.AnchorStyles.None;
            this.editAccount1.BackColor = System.Drawing.SystemColors.Window;
            this.editAccount1.Location = new System.Drawing.Point(194, 142);
            this.editAccount1.Name = "editAccount1";
            this.editAccount1.Size = new System.Drawing.Size(356, 173);
            this.editAccount1.TabIndex = 0;
            // 
            // tabPage2
            // 
            this.tabPage2.Controls.Add(this.rentalsListControl);
            this.tabPage2.Location = new System.Drawing.Point(4, 22);
            this.tabPage2.Name = "tabPage2";
            this.tabPage2.Padding = new System.Windows.Forms.Padding(3);
            this.tabPage2.Size = new System.Drawing.Size(760, 468);
            this.tabPage2.TabIndex = 1;
            this.tabPage2.Text = "Active Rentals";
            this.tabPage2.UseVisualStyleBackColor = true;
            // 
            // rentalsListControl
            // 
            this.rentalsListControl.Anchor = ((System.Windows.Forms.AnchorStyles) ((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom)
                        | System.Windows.Forms.AnchorStyles.Left)
                        | System.Windows.Forms.AnchorStyles.Right)));
            this.rentalsListControl.Location = new System.Drawing.Point(6, 6);
            this.rentalsListControl.Name = "rentalsListControl";
            this.rentalsListControl.Size = new System.Drawing.Size(748, 456);
            this.rentalsListControl.TabIndex = 0;
            // 
            // tabPage3
            // 
            this.tabPage3.Controls.Add(this.creditsControl1);
            this.tabPage3.Location = new System.Drawing.Point(4, 22);
            this.tabPage3.Name = "tabPage3";
            this.tabPage3.Padding = new System.Windows.Forms.Padding(3);
            this.tabPage3.Size = new System.Drawing.Size(760, 468);
            this.tabPage3.TabIndex = 2;
            this.tabPage3.Text = "Credits";
            this.tabPage3.UseVisualStyleBackColor = true;
            // 
            // creditsControl1
            // 
            this.creditsControl1.Anchor = System.Windows.Forms.AnchorStyles.None;
            this.creditsControl1.Location = new System.Drawing.Point(285, 108);
            this.creditsControl1.Name = "creditsControl1";
            this.creditsControl1.Size = new System.Drawing.Size(216, 209);
            this.creditsControl1.TabIndex = 0;
            // 
            // UserAccountManagement
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.tabControl);
            this.Name = "UserAccountManagement";
            this.Size = new System.Drawing.Size(775, 501);
            this.tabControl.ResumeLayout(false);
            this.tabPage1.ResumeLayout(false);
            this.tabPage2.ResumeLayout(false);
            this.tabPage3.ResumeLayout(false);
            this.ResumeLayout(false);

		}

		#endregion

		private System.Windows.Forms.TabControl tabControl;
		private System.Windows.Forms.TabPage tabPage1;
		private System.Windows.Forms.TabPage tabPage2;
		private System.Windows.Forms.TabPage tabPage3;
		private EditAccount editAccount1;
		private PagedRentalsListControl rentalsListControl;
		private CreditsControl creditsControl1;
        private System.Windows.Forms.Button deleteAccountButton;
	}
}
