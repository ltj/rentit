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
            this.ContentPane = new System.Windows.Forms.Panel();
            this.topBarControl1 = new ClientApp.TopBarControl();
            this.SuspendLayout();
            // 
            // ContentPane
            // 
            this.ContentPane.Anchor = ((System.Windows.Forms.AnchorStyles) ((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom)
                        | System.Windows.Forms.AnchorStyles.Left)
                        | System.Windows.Forms.AnchorStyles.Right)));
            this.ContentPane.Location = new System.Drawing.Point(1, 76);
            this.ContentPane.Name = "ContentPane";
            this.ContentPane.Size = new System.Drawing.Size(783, 328);
            this.ContentPane.TabIndex = 0;
            // 
            // topBarControl1
            // 
            this.topBarControl1.Anchor = ((System.Windows.Forms.AnchorStyles) (((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left)
                        | System.Windows.Forms.AnchorStyles.Right)));
            this.topBarControl1.Location = new System.Drawing.Point(1, 1);
            this.topBarControl1.MaximumSize = new System.Drawing.Size(99999, 76);
            this.topBarControl1.MinimumSize = new System.Drawing.Size(730, 76);
            this.topBarControl1.Name = "topBarControl1";
            this.topBarControl1.Size = new System.Drawing.Size(783, 76);
            this.topBarControl1.TabIndex = 1;
            this.topBarControl1.Title = "RentIt";
            this.topBarControl1.UserName = "";
            // 
            // MainForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(784, 405);
            this.Controls.Add(this.topBarControl1);
            this.Controls.Add(this.ContentPane);
            this.MinimumSize = new System.Drawing.Size(747, 378);
            this.Name = "MainForm";
            this.Text = "MainForm";
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Panel ContentPane;
        private TopBarControl topBarControl1;
    }
}