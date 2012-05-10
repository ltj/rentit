namespace ClientApp {
    partial class MainForm {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing) {
            if(disposing && (components != null)) {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent() {
            this.contentPane = new System.Windows.Forms.Panel();
            this.topBarControl = new ClientApp.TopBarControl();
            this.SuspendLayout();
            // 
            // contentPane
            // 
            this.contentPane.Anchor = ((System.Windows.Forms.AnchorStyles) ((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom)
                        | System.Windows.Forms.AnchorStyles.Left)
                        | System.Windows.Forms.AnchorStyles.Right)));
            this.contentPane.Location = new System.Drawing.Point(1, 76);
            this.contentPane.Name = "contentPane";
            this.contentPane.Size = new System.Drawing.Size(769, 443);
            this.contentPane.TabIndex = 0;
            // 
            // topBarControl
            // 
            this.topBarControl.Anchor = ((System.Windows.Forms.AnchorStyles) (((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left)
                        | System.Windows.Forms.AnchorStyles.Right)));
            this.topBarControl.Location = new System.Drawing.Point(1, 1);
            this.topBarControl.LoggedIn = false;
            this.topBarControl.MaximumSize = new System.Drawing.Size(99999, 76);
            this.topBarControl.MinimumSize = new System.Drawing.Size(604, 76);
            this.topBarControl.Name = "topBarControl";
            this.topBarControl.Size = new System.Drawing.Size(769, 76);
            this.topBarControl.TabIndex = 1;
            this.topBarControl.Title = "RentIt";
            this.topBarControl.UserName = "";
            // 
            // MainForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(770, 520);
            this.Controls.Add(this.topBarControl);
            this.Controls.Add(this.contentPane);
            this.MinimumSize = new System.Drawing.Size(700, 500);
            this.Name = "MainForm";
            this.Text = "MainForm";
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Panel contentPane;
        private TopBarControl topBarControl;
    }
}